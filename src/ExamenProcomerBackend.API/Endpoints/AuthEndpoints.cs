using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.OpenApi;
using ExamenProcomerBackend.Application.Common;

namespace ExamenProcomerBackend.API.Endpoints;

public static class AuthEndpoints
{
    //AUTENTICACION
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/auth").WithTags("Autenticación");

        group.MapPost("/token", GenerateJwtToken)
            .WithName("GenerateToken")
            .WithOpenApi()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .AllowAnonymous();
    }

    private static IResult GenerateJwtToken(
        [FromBody] TokenRequest request,
        IConfiguration configuration)
    {
        if (string.IsNullOrWhiteSpace(request.ApiToken))
        {
            var errorResult = OperationResult<TokenResponse>.Fail("El token de API es requerido.");
            return Results.BadRequest(new { error = errorResult.Error });
        }

        var staticToken = configuration["ApiAuth:StaticToken"] 
            ?? throw new InvalidOperationException("ApiAuth:StaticToken no configurado");

        if (request.ApiToken != staticToken)
        {
            var errorResult = OperationResult<TokenResponse>.Fail("Token de API inválido.");
            return Results.Json(
                new { error = errorResult.Error },
                statusCode: StatusCodes.Status401Unauthorized
            );
        }

        var jwtToken = GenerarJwtToken(configuration);
        
        var tokenResponse = new TokenResponse
        {
            Token = jwtToken,
            TokenType = "Bearer",
            ExpiresAt = DateTime.UtcNow.AddMinutes(
                int.Parse(configuration["Jwt:ExpirationMinutes"] ?? "60")
            )
        };

        var result = OperationResult<TokenResponse>.Ok(tokenResponse);
        return Results.Ok(result.Data);
    }

    private static string GenerarJwtToken(IConfiguration configuration)
    {
        var secret = configuration["Jwt:Secret"] 
            ?? throw new InvalidOperationException("Jwt:Secret no configurado");
        var issuer = configuration["Jwt:Issuer"] 
            ?? throw new InvalidOperationException("Jwt:Issuer no configurado");
        var audience = configuration["Jwt:Audience"] 
            ?? throw new InvalidOperationException("Jwt:Audience no configurado");
        var expirationMinutes = int.Parse(configuration["Jwt:ExpirationMinutes"] ?? "60");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, "api-client"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, "API Client"),
            new Claim(ClaimTypes.Role, "ApiUser")
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public record TokenRequest
{
    public string ApiToken { get; init; } = string.Empty;
}

public record TokenResponse
{
    public string Token { get; init; } = string.Empty;
    public string TokenType { get; init; } = string.Empty;
    public DateTime ExpiresAt { get; init; }
}


