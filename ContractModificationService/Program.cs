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

    x.UsingRabbitMq((context, cfg) =>
    {

        //cfg.AutoStart = true;

        if (builder.Configuration.GetSection("MassTransit")["host"] == "localhost")
            cfg.Host("localhost", "/", h => {
                h.Username("guest");
                h.Password("guest");
            });
        else
            cfg.Host("rabbitmq", "/", h => {
                h.Username("guest");
                h.Password("guest");
            });

        cfg.ReceiveEndpoint("priority-orders-receive", x =>
        {
            x.ConfigureSubscriberContract<PublishContractConsumer>(context, "priority-orders", "submitorder", "PRIORITY", ExchangeType.Direct);
        });

        cfg.ReceiveEndpoint("regular-orders-receive1", x =>
        {
            x.ConfigureSubscriberContract<PublishContractConsumerTwo>(context, "priority-orders", "submitorder", "REGULAR", ExchangeType.Direct);
        });

        cfg.ReceiveEndpoint("regular-orders-receive2", y =>
        {
            y.ConfigureSubscriberContract<CreateContractConsumer>(context, "regular-orders", "createorder", "REGULAR", ExchangeType.Direct);
        });

        cfg.ConfigurePublisherContracts();
        cfg.ConfgurePublisherContract<ContractSubmitMessageEnvelop>("submitorder", ExchangeType.Direct);
        cfg.ConfgurePublisherContract<ContractCreateMessageEnvelop>("createorder", ExchangeType.Direct);
        cfg.ConfigureEndpoints(context);
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
