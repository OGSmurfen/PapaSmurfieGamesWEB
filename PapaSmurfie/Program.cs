using Microsoft.EntityFrameworkCore;
using PapaSmurfie.Database;
using Microsoft.AspNetCore.Identity;
using PapaSmurfie.Repository.IRepository;
using PapaSmurfie.Repository.RepositoryImpl;
using PapaSmurfie.Repository;
using PapaSmurfie.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Authorization;
using PapaSmurfie.DataAccess.Repository.IRepository;
using PapaSmurfie.DataAccess.Repository.RepositoryImpl;
using PapaSmurfie.Web.Hubs;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("db")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // RequireUniqueEmail should remain true for the login to work with both email and username properly
                options.User.RequireUniqueEmail = true;
                
                
            }
        ).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = "/Identity/Account/Login";
        options.LogoutPath = "/Identity/Account/Logout";
        options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    }
);
// need the following configuraion so app can serve .data file of the game builds
builder.Services.Configure<StaticFileOptions>(options =>
{
    options.ContentTypeProvider = new FileExtensionContentTypeProvider
    {
        Mappings = { [".data"] = "application/octet-stream" }
    };
});

builder.Services.AddScoped<IOwnedGamesRepository, OwnedGamesRepository>();
builder.Services.AddScoped<IGamesRepository, GamesRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IFriendsRepository, FriendsRepository>();

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/chatHub");
app.MapHub<SocialHub>("/socialHub");

app.UseStaticFiles();
app.Run();
