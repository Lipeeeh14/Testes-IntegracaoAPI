using JornadaMilhas.Dominio.Entidades;
using JornadaMilhas.Dominio.ValueObjects;
using System.Net;
using System.Net.Http.Json;

namespace JornadaMilhas.Integration.Test.API;

public class OfertaViagem_PUT : IClassFixture<JornadaMilhasWebApplicationFactory>
{
    private readonly JornadaMilhasWebApplicationFactory app;

    public OfertaViagem_PUT(JornadaMilhasWebApplicationFactory app)
    {
        this.app = app;
    }

    [Fact]
    public async Task Atualizar_OfertaViagem_PorId()
    {
        var ofertaExistente = app.Context.OfertasViagem.FirstOrDefault();

        if (ofertaExistente is null)
        {
            ofertaExistente = new OfertaViagem()
            {
                Preco = 100,
                Rota = new Rota("Origem", "Destino"),
                Periodo = new Periodo(DateTime.Parse("2024-03-03"),
                    DateTime.Parse("2024-03-06"))
            };

            app.Context.Add(ofertaExistente);
            app.Context.SaveChanges();
        }

        using var client = await app.GetClientWithAccessTokenAsync();

        ofertaExistente.Rota.Origem = "Origem Nova";
        ofertaExistente.Rota.Destino = "Destino Novo";

        var response = await client.PutAsJsonAsync($"/ofertas-viagem/", 
            ofertaExistente);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
