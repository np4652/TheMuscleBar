using TheMuscleBar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using TheMuscleBar.AppCode.Enums;

namespace TheMuscleBar.AppCode.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JWTConfig _jwtConfig;

        public JwtMiddleware(RequestDelegate next, IOptions<JWTConfig> jwtConfig)
        {
            _next = next;
            _jwtConfig = jwtConfig.Value;
        }
        public async Task Invoke(HttpContext context)
        {
            string authorization = context.Request.Headers[HeaderNames.Authorization];
            if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
            {
                var token = authorization.Split(" ").Last();
                if (token != null)
                    attachUserToContext(context, token);
            }
            await _next(context);
        }

        private void attachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtConfig.Secretkey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //ClockSkew = TimeSpan.Zero // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var claims = jwtToken.Claims.ToList();
                var loginResponse = new LoginResponse
                {
                    StatusCode = ResponseStatus.Success,
                    ResponseText = nameof(ResponseStatus.Success),
                    IsAuthenticate = true,
                    Token = token,
                    Result = new ApplicationUser
                    {
                        Id = int.Parse(claims.First(x => x.Type == "id").Value),
                        Role = Convert.ToString(claims.First(x => x.Type == "role").Value),
                        UserName = Convert.ToString(claims.First(x => x.Type == "userName").Value)
                    }
                };
                context.Items["User"] = loginResponse;
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
