using FundaAssignment;
using FundaAssignment.Clients;
using FundaAssignment.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.local.json", optional: true);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.AddScoped<IBrokerStatisticService, BrokerStatisticService>();
builder.Services.AddHttpClient<IFundaApiClient, FundaApiClient>();
builder.Services.Configure<FundaOptions>(builder.Configuration.GetSection("Funda"));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddOpenApiDocument();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Brokers}/{action=Index}/{id?}")
    .WithStaticAssets();


await app.RunAsync();