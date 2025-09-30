using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MachineReporting.Api.Dtos;
using MachineReporting.Api.Models.Entities;
using Microsoft.IdentityModel.Tokens;

namespace MachineReporting.Api.Jwt
{
    public class CreateJwtTokenAsynco(BearerTokens tokens)
    {
        public ApiResult<ReturnToken> ExecuteAsync(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokens.Key));
            var signInCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new Claim("UserName",user.ClientId),
                new Claim("UserId",user.Id.ToString())
            };
            var jwtBearerToken = new JwtSecurityToken(tokens.Issuer, tokens.Audience, claims, DateTime.Now, DateTime.Now.AddDays(1), signInCredential);
            var jwt = new JwtSecurityTokenHandler().WriteToken(jwtBearerToken);
            return new ApiResult<ReturnToken>
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Data = new ReturnToken
                {
                    ExpireTime = DateTime.Now.AddDays(1),
                    token = jwt
                }
            };
        }
    }
}