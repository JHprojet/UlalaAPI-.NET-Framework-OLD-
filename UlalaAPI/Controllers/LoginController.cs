using DAL.Services;
using JwtToolBox;
using System.Web.Http;
using UlalaAPI.Models;
using UlalaAPI.Helper;
using DAL.Entities;
using System;
using System.Linq;
using System.Web;

namespace UlalaAPI.Controllers
{
    public class LoginController : ApiController
    {
        UtilisateurRepository repo = new UtilisateurRepository();

        #region GET Anonyme Token
        /// <summary>
        /// Post API/Login
        /// </summary>
        /// <param name="User">User à tester</param>
        public IHttpActionResult Get()
        {
            if (HttpContext.Current.Request.UrlReferrer.AbsoluteUri.Contains("http://localhost:4200/"))
            {
                UtilisateurEntity U = new UtilisateurEntity();
                U.Role = "Anonyme";
                JWTService jwt = new JWTService("FZeDfgPkyXaDFyMwQfSbIoJhF", "localhost:4200", "localhost:4200");
                string token = jwt.Encode(U);
                return Ok(token);
            }
            else return Unauthorized();
        }
        #endregion

        #region POST Vérification couple pseudo-password
        /// <summary>
        /// Post API/Login
        /// </summary>
        /// <param name="User">User à tester</param>
        public IHttpActionResult Post(UtilisateurModel User)
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (User == null || User.Password == null || User.Pseudo == null) return BadRequest();
                else
                {
                    if (repo.Check(User.Pseudo, User.Password))
                    {
                        UtilisateurEntity U = repo.GetOneByPseudo(User.Pseudo);
                        JWTService jwt = new JWTService("FZeDfgPkyXaDFyMwQfSbIoJhF", "localhost:4200", "localhost:4200");
                        string token = jwt.Encode(U);
                        return Ok(token);
                    }
                    else return BadRequest();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region POST Verification Token d'activation
        /// <summary>
        /// Post API/Login/{Id}/?Token={Token}
        /// </summary>
        /// <param name="User">User à tester</param>
        public IHttpActionResult Post(int Id, [FromUri]string Token)
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (Token == null) return BadRequest();
                else
                {
                    if (repo.UpdateToken(Id, Token)) return Ok();
                    else return BadRequest();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region POST Renvoi Token Activation
        /// <summary>
        /// Post API/Login/?IdU={Id}
        /// </summary>
        /// <param name="User">User à tester</param>
        public IHttpActionResult Post([FromUri]int IdU)
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(IdU) == null) return BadRequest();
                else
                {
                    repo.RenvoiToken(IdU);
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region POST Changement Mot de passe
        /// <summary>
        /// Post API/Login/?IdUtilisateur={Id}
        /// </summary>
        /// <param name="User">User à tester</param>
        [HttpPost]
        public IHttpActionResult PostPass([FromUri]int IdUtilisateur, [FromBody]string NewPassword)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(IdUtilisateur) == null || NewPassword == "") return BadRequest();
                else
                {
                    repo.UpdatePassword(IdUtilisateur, NewPassword);
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region POST Nouveau Mot de passe
        /// <summary>
        /// Post API/Login/?Mail={Mail}
        /// </summary>
        /// <param name="User">User à tester</param>
        [HttpPost]
        public IHttpActionResult PostPass([FromUri]string Mail)
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOneByMail(Mail) == null || Mail == "") return BadRequest();
                else
                {
                    repo.NouveauPassword(Mail);
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region POST Récupération Pseudo
        /// <summary>
        /// Post API/Login/?MailforPseudo={MailforPseudo}
        /// </summary>
        /// <param name="User">User à tester</param>
        [HttpPost]
        public IHttpActionResult PostPseudo([FromUri]string MailforPseudo)
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOneByMail(MailforPseudo) == null || MailforPseudo == "") return BadRequest();
                else
                {
                    repo.RetrievePseudo(MailforPseudo);
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

    }
}
