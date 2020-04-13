using MarketsTracker.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketsTracker
{
    internal static class AuthenticationHelper
    {
        public static byte[] GetSecret(IConfiguration configuration) => Encoding.ASCII.GetBytes(configuration.GetValue<string>("Secret"));

        public static void AddJwtAuthentication(this IServiceCollection services, byte[] secretKey)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(x =>
             {
                 x.Events = new JwtBearerEvents
                 {
                     OnTokenValidated = async context =>
                     {
                         var userService = context.HttpContext.RequestServices.GetRequiredService<IUsersService>();
                         var userId = int.Parse(context.Principal.Identity.Name);
                         if (!await userService.IsUserExists(userId))
                         {
                             // return unauthorized if user no longer exists
                             context.Fail("Unauthorized");
                         }
                         else context.Success();
                     },
                 };
            //     x.Authority = "http://localhost:5000/";
                 x.RequireHttpsMetadata = false;
                 x.SaveToken = true;
                 //instead of dealing with a JWT issuer, which must contain the port number used and might change
                 //in different environments ant test scenarios, i'm using a symetric key validation. Far from ideal, but good enough for now
                 x.TokenValidationParameters = new TokenValidationParameters
                 {

                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidAudience ="Audience",
                     ValidIssuer = "Issuer"

                 };

             });

        }
        /// <summary>
        /// check if password meet strength reuierments
        /// </summary>
        /// <returns>validation errors of passwords, returns null if password is valid</returns>
        public static string CheckPasswordRequierments(string password)
        {
            var containsUpperCase = password.Any(char.IsUpper);
            var containsLowerCase = password.Any(char.IsLower);
            var containsNumber = password.Any(char.IsNumber);
            var isLongEnough = password.Length >= 6;
            if(containsUpperCase && containsLowerCase && containsNumber && isLongEnough)
                return null;
            return "password must be longer than 6 charecters and contain an upper case letter, a lower case letter and a number";
        }
    }
}