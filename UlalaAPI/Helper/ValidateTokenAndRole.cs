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
        //Vérification du Rôle dans le token.
        public static string ValidateAndGetRole(HttpRequestMessage R)
        {
            JWTService JWT = new JWTService("FZeDfgPkyXaDFyMwQfSbIoJhF", "localhost:4200", "localhost:4200");
            string Role = null;
            if (R.Headers.Contains("Authorization"))
            {
                if (JWT.ValidateToken(R.Headers.GetValues("Authorization").First()) == "Admin") Role = "Admin";
                if (JWT.ValidateToken(R.Headers.GetValues("Authorization").First()) == "User") Role = "User";
                if (JWT.ValidateToken(R.Headers.GetValues("Authorization").First()) == "Anonyme") Role = "Anonyme";
            }
            return Role;
        }
    }
}