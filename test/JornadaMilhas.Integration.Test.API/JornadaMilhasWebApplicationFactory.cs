using JornadaMilhas.Dados;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JornadaMilhas.Integration.Test.API;

public class JornadaMilhasWebApplicationFactory : WebApplicationFactory<Program>
{
    // Cria uma aplicação em memória para os testes
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services => 
        {
            // Reconfigura a conexão com o banco para o teste
            services.RemoveAll(typeof(DbContextOptions<JornadaMilhasContext>));

            services.AddDbContext<JornadaMilhasContext>(options =>
            options
            .UseLazyLoadingProxies()
            .UseSqlServer("Server=localhost,11433;Database=JornadaMilhasV3;User Id=sa;Password=Alura#2024;Encrypt=false;TrustServerCertificate=true;MultipleActiveResultSets=true;"));
        });

        base.ConfigureWebHost(builder);
    }
}
