using Contracts;
using MassTransit;
using Publisher.Events.Consumers;
using Publisher.Helpers;
using RabbitMQ.Client;
using ServiceRegistration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PublishContractConsumer>(typeof(PublishContractConsumerDefinition));
    x.AddConsumer<PublishContractConsumerTwo>(typeof(PublishContractConsumerTwoDefinition));
    x.AddConsumer<CreateContractConsumer>(typeof(CreateContractConsumerDefinition));
    //x.AddConsumersFromNamespaceContaining<PublishContractConsumer>();

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

    //    cfg.ReceiveEndpoint("priority-orders-receive", x =>
    //    {
    //        x.ConfigureSubscriberContract<PublishContractConsumer>(context, "priority-orders", "submitorder", "PRIORITY", ExchangeType.Direct);
    //    });

    //    cfg.ReceiveEndpoint("regular-orders-receive1", x =>
    //    {
    //        x.ConfigureSubscriberContract<PublishContractConsumerTwo>(context, "priority-orders", "submitorder", "REGULAR", ExchangeType.Direct);
    //    });

    //    cfg.ReceiveEndpoint("regular-orders-receive2", y =>
    //    {
    //        y.ConfigureSubscriberContract<CreateContractConsumer>(context, "regular-orders", "createorder", "REGULAR", ExchangeType.Direct);
    //    });

    //    cfg.ConfigurePublisherContracts();
    //    cfg.ConfgurePublisherContract<ContractSubmitMessageEnvelop>("submitorder", ExchangeType.Direct);
    //    cfg.ConfgurePublisherContract<ContractCreateMessageEnvelop>("createorder", ExchangeType.Direct);
    //    cfg.ConfigureEndpoints(context);
    //});
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
