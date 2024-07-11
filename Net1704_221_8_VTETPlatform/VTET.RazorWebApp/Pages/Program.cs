using VTET.Business;
using VTET.Data.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMvc().AddRazorPagesOptions(options =>
{
    options.Conventions.AddPageRoute("/Login", "");
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IOrderDetailBusiness, OrderDetailBusiness>();
builder.Services.AddDbContext<Net1704_221_8_VTETPlatformContext>();
builder.Services.AddScoped< IWatchBusiness,watchBusiness > ();
builder.Services.AddScoped<IEvaluationBusiness, evaluationBusiness>();
builder.Services.AddScoped< ICustomerBusiness, customerBusiness > ();

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
