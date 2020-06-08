using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using JwtToolBox;

namespace UlalaAPI.Helper
{
    public static class ValidateTokenAndRole
    {
        //Chekc Role in the JWT Token
        public static string ValidateAndGetRole(HttpRequestMessage R)
        {
            JWTService JWT = new JWTService("FZeDfgPkyXaDFyMwQfSbIoJhF", "localhost:4200", "localhost:4200");
            string Role = "";
            if (R.Headers.Contains("Authorization"))
            {
                Role = JWT.ValidateToken(R.Headers.GetValues("Authorization").First());
            }
            return "Admin";//To modif
        }
    }
}