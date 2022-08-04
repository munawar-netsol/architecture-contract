using ContractDataAccessLibrary;
using Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Publisher.Helpers;
using RabbitMQ.Client;
using ServiceRegistration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddHttpClient();
builder.Services.AddDbContext<ContractActivationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x =>
{
    //x.UsingRabbitMq((context, cfg) =>
    //{

    //    //cfg.AutoStart = true;

    //    if (builder.Configuration.GetSection("MassTransit")["host"] == "localhost")
    //        cfg.Host("localhost", "/", h => {
    //            h.Username("guest");
    //            h.Password("guest");
    //        });
    //    else
    //        cfg.Host("rabbitmq", "/", h => {
    //            h.Username("guest");
    //            h.Password("guest");
    //        });

    //    cfg.ConfigurePublisherContracts();
    //    cfg.ConfgurePublisherContract<ContractSubmitMessageEnvelop>("submitorder", ExchangeType.Direct);
    //    cfg.ConfgurePublisherContract<ContractCreateMessageEnvelop>("createorder", ExchangeType.Direct);
    //    cfg.ConfigureEndpoints(context);

    //});

    //services.AddHostedService<Worker>();
    builder.Services.AddMassTransit(x =>
    {
        if (builder.Configuration.GetSection("MassTransit")["Type"] == "rabbitmq")
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.AutoStart = true;
                    cfg.Host(builder.Configuration.GetSection("MassTransit")["host"], "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                cfg.ConfigureEndpoints(context);
            });
        }
        else if (builder.Configuration.GetSection("MassTransit")["Type"] == "AWS")
        {
            x.UsingAmazonSqs((context, cfg) =>
            {
                var accessId = Environment.GetEnvironmentVariable("AWS-ACCESS-ID");
                var secretId = Environment.GetEnvironmentVariable("AWS-SECRET-ID");
                cfg.Host("us-east-1", h => {
                    h.AccessKey(accessId);
                    h.SecretKey(secretId);
                });

                cfg.ConfigureEndpoints(context);
            });
        }
    });

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.RegisterConsul(app.Lifetime, builder.Configuration);

app.Run();
