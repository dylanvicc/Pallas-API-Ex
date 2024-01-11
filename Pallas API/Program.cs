using Pallas_API;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var application = builder.Build();
startup.Configure(application, builder.Environment);

if (application.Environment.IsDevelopment())
{
    application.UseSwagger();
    application.UseSwaggerUI();
}

application.Run();