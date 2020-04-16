using DAL.Entities;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtToolBox
{
    public class JWTService
    {
        private readonly string signature;
        private readonly string issuer;
        private readonly string audience;

        private readonly JwtSecurityTokenHandler handler;
        public JWTService(string signature, string issuer, string audience)
        {
            this.signature = signature;
            this.issuer = issuer;
            this.audience = audience;
            handler = new JwtSecurityTokenHandler();
 
        }
        public string ValidateToken(string token)
        {
            TokenValidationParameters parameters = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                RequireSignedTokens = true,
                IssuerSigningKey = Generatesignature(),
                ValidAudience = audience,
                ValidIssuer = issuer
            };
            try
            {
                var obj = handler.ValidateToken(token, parameters, out SecurityToken Stoken);
                return obj.FindFirst("Role").Value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public SecurityKey Generatesignature()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.signature));
        }
        public string Encode<T>(T Tpayload)
        {
            //Génère un header sur base de la signature
            JwtHeader header = new JwtHeader(new SigningCredentials(Generatesignature(), SecurityAlgorithms.HmacSha256));
            //Génère un payload
            JwtPayload payload = new JwtPayload(
                this.issuer, 
                this.audience, 
                typeof(T).GetProperties().Where(p => p.GetValue(Tpayload) != null).Select(
                                                p => { return new Claim(p.Name, p.GetValue(Tpayload).ToString()); })
                , DateTime.Now
                , DateTime.Now.AddDays(1));
            //Génère un token sur base du header et du payload
            JwtSecurityToken token = new JwtSecurityToken(header, payload);
            return handler.WriteToken(token);
        }
    }
}