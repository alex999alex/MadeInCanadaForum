using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MadeInCanadaForum.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MadeInCanadaForumContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MadeInCanadaForumContext") ?? throw new InvalidOperationException("Connection string 'MadeInCanadaForumContext' not found."),
    sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }));


builder.Services.AddControllersWithViews();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
