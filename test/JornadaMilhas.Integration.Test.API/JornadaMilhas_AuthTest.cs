using JornadaMilhas.API.DTO.Auth;
using System.Net;
using System.Net.Http.Json;

namespace JornadaMilhas.Integration.Test.API;

public class JornadaMilhas_AuthTest
{
    [Fact]
    public async Task POST_Efetua_Login_Com_Sucesso()
    {
        var app = new JornadaMilhasWebApplicationFactory();
        var user = new UserDTO 
        {
            Email = "tester@email.com",
            Password = "Senha123@"
        };

        using var client = app.CreateClient();

        var resultado = await client.PostAsJsonAsync("/auth-login", user);

        Assert.Equal(HttpStatusCode.OK, resultado.StatusCode);
    }

    [Fact]
    public async Task POST_Efetua_Login_Com_Falha()
    {
        var app = new JornadaMilhasWebApplicationFactory();
        var user = new UserDTO
        {
            Email = "test@email.com",
            Password = "senha12@"
        };

        using var client = app.CreateClient();

        var resultado = await client.PostAsJsonAsync("/auth-login", user);

        Assert.Equal(HttpStatusCode.BadRequest, resultado.StatusCode);
    }
}