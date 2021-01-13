using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SAEBRecommender.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SAEBRecommender.Resources.AdobeAnalytics 
{
    public class AARequests : IAARequests
    {
        private readonly IAASettings settings;
        private readonly HttpClient client;
        private string JwtToken;

        public AARequests(HttpClient client, IOptions<AASettings> aaSettings)
        {
            settings = aaSettings.Value;

            client.BaseAddress = new Uri(settings.AABaseAuthUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(settings.AuthenticationRequestHeaderType));
            this.client = client;
        }

        public async Task MakeACall_TempAsync()
        {
            GenerateJwtToken();
            var request = new AAAuthenticationRequest
            {
                Client_id = settings.ClientId,
                Client_secret = settings.ClientSecret,
                Jwt_token = JwtToken
            };

            HttpResponseMessage response = await client.PostAsJsonAsync(
                settings.AAAuthPath, request);
            response.EnsureSuccessStatusCode();
        }

        private void GenerateJwtToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.ClientSecret));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(settings.ExpiryDays),
                Issuer = settings.OrganizationId,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, settings.TechnicalAccountId),
                }),
                Audience = settings.ApiKey,
                AdditionalHeaderClaims = new Dictionary<string, object> {{ settings.Metascope, true }},
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            JwtToken = tokenHandler.WriteToken(token);
        }

        private bool ValidateCurrentToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.ClientSecret));
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(JwtToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = settings.OrganizationId,
                    ValidAudience = settings.Audience,
                    IssuerSigningKey = securityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
