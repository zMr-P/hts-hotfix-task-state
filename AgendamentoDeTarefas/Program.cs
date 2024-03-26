using Microsoft.EntityFrameworkCore;
using AgendamentoDeTarefas.Context;
using AgendamentoDeTarefas.Entites;
using Microsoft.AspNetCore.Identity;
using AgendamentoDeTarefas.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<OrganizadorContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));
builder.Services.AddIdentityCore<MeuUsuario>(options => { });
builder.Services.AddScoped<IUserStore<MeuUsuario>, MeuUsuarioStore>();
builder.Services.AddAuthentication("cookies").AddCookie("cookies", options => options.LoginPath = "/Login");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Shared/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Logar}/{id?}");

app.Run();
