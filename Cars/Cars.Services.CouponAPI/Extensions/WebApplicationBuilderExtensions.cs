using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

namespace Cars.Services.CouponAPI.Extensions
{
    // MUA : Custom extension class
    public static class WebApplicationBuilderExtensions
    {
        //method
        public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
        {
            var ApiSettings = builder.Configuration.GetSection("ApiSettings");

            var Secret = ApiSettings.GetValue<string>("Secret");
            var Issuer = ApiSettings.GetValue<string>("Issuer");
            var Audience = ApiSettings.GetValue<string>("Audience");

            var key = Encoding.ASCII.GetBytes(Secret);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = Issuer,
                    ValidAudience = Audience,
                    ValidateAudience = true
                };
            });

            return builder;
        }
    }
}
