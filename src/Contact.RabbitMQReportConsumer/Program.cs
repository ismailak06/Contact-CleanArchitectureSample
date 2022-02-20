
using Contact.Application.Common.Interfaces;
using Contact.Application.Document.Command;
using Contact.Application.DocumentLog.Commands;
using Contact.Infrastructure.Utilities.IoC;
using Contact.Persistence.Context;
using Contact.RabbitMQReportConsumer.Reports;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddDbContext<ContactDbContext>(options => options.UseNpgsql("Server=127.0.0.1;Port=5432;Database=ContactDb;User Id=postgres;Password=2201;"));
        services.AddScoped<IContactDbContext>(provider => provider.GetService<ContactDbContext>());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services
        .AddTransient<IRequestHandler<UpdateDocumentLogStatusCommand, UpdateDocumentLogStatusResponse>, UpdateDocumentLogStatusCommandHandler>()
        .AddTransient<IRequestHandler<CreateContactInformationStatReportCommand, CreateContactInformationStatReportResponse>, CreateContactInformationStatReportHandler>();

        ServiceLocator.Create(services);


    })
    .Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

ContactInformationStatReportConsumer contactInformationStatReportConsumer = new ContactInformationStatReportConsumer();
contactInformationStatReportConsumer.Consumer();

