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
        UserRepository repo = new UserRepository();

        #region GET Anonyme Token
        /// <summary>
        /// Post API/Login
        /// </summary>
        /// <param name="User">User à tester</param>
        public IHttpActionResult Get()
        {
            if (HttpContext.Current.Request.UrlReferrer.AbsoluteUri.Contains("http://localhost:4200/"))
            {
                UserEntity U = new UserEntity();
                U.Role = "Anonymous";
                JWTService jwt = new JWTService("FZeDfgPkyXaDFyMwQfSbIoJhF", "localhost:4200", "localhost:4200");
                string token = jwt.Encode(U);
                return Ok(token);
            }
            else return Unauthorized();
        }
        #endregion

        #region POST Vérification couple Username-password
        /// <summary>
        /// Post API/Login
        /// </summary>
        /// <param name="User">User à tester</param>
        public IHttpActionResult Post(UserModel User)
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (User == null || User.Password == null || User.Username == null) return BadRequest();
                else
                {
                    if (repo.Check(User.Username, User.Password))
                    {
                        UserEntity U = repo.GetOneByUsername(User.Username);
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
        public IHttpActionResult Post(int Id, [FromBody]string Token)
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
        /// Post API/Login/?IdUser={Id}
        /// </summary>
        /// <param name="User">User à tester</param>
        [HttpPost]
        public IHttpActionResult PostPass([FromUri]int IdUser, [FromBody]string NewPassword)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(IdUser) == null || NewPassword == "") return BadRequest();
                else
                {
                    repo.UpdatePassword(IdUser, NewPassword);
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

        #region POST Récupération Username
        /// <summary>
        /// Post API/Login/?MailforUsername={MailforUsername}
        /// </summary>
        /// <param name="User">User à tester</param>
        [HttpPost]
        public IHttpActionResult PostUsername([FromUri]string MailforUsername)
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOneByMail(MailforUsername) == null || MailforUsername == "") return BadRequest();
                else
                {
                    repo.RetrieveUsername(MailforUsername);
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

    }
}
