using M_Nek_Mohammade_HW_W18_01.DataAccessLayer;
using M_Nek_Mohammade_HW_W18_01.ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<StoreService>();
// Add services to the container.
IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();   
builder.Services.AddControllersWithViews();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Store}/{action=Index}/{id?}");

app.Run();
