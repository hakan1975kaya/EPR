using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete.Entities;
using Core.Extensions;
using Core.Utilities.Security.Abstract;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Concrete.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        IConfiguration _configuration;
        TokenOptions _tokenOptions;
        DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var singleCreadential = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwtSecurityToken = CreateJwtSecurityToken(_tokenOptions, user, singleCreadential, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            return new AccessToken
            {
                Token = token,
                TokenExpiration = _accessTokenExpiration
            };

        }
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwtSecurityToken = new JwtSecurityToken(
                 issuer: tokenOptions.Issuer,
                 audience: tokenOptions.Audience,
                 expires: _accessTokenExpiration,
                 notBefore: DateTime.Now,
                 claims: SetClaims(user, operationClaims),
                 signingCredentials: signingCredentials
                );

            return jwtSecurityToken;
        }
        public IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddRegistrationNumber(user.RegistrationNumber);
            claims.AddRoles(operationClaims.Select(x => x.Name).ToArray());

            return claims;
        }













    }
}
