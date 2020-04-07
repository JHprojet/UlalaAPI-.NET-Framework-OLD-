using DAL.Services;
using System.Web.Http;
using UlalaAPI.Models;

namespace UlalaAPI.Controllers
{
    public class LoginController : ApiController
    {
        UtilisateurRepository repo = new UtilisateurRepository();

        #region POST Vérification couple pseudo-password
        /// <summary>
        /// Post API/Login
        /// </summary>
        /// <param name="User">User à tester</param>
        public IHttpActionResult Post(UtilisateurModel User)
        {
            if (User == null || User.Password == null || User.Pseudo == null) return BadRequest();
            else
            {
                if (repo.Check(User.Pseudo, User.Password)) return Ok();
                else return BadRequest();
            }
        }
        #endregion

        #region POST Verification Token
        /// <summary>
        /// Post API/Login/{Id}/?Token={Token}
        /// </summary>
        /// <param name="User">User à tester</param>
        public IHttpActionResult Post(int Id, [FromUri]string Token)
        {
            if (Id == null || Token == null) return BadRequest();
            else
            {
                if (repo.UpdateToken(Id, Token)) return Ok();
                else return BadRequest();
            }
        }
        #endregion

        #region POST Renvoi Token
        /// <summary>
        /// Post API/Login/?IdU={Id}
        /// </summary>
        /// <param name="User">User à tester</param>
        public IHttpActionResult Post([FromUri]int IdU)
        {
            if (repo.GetOne(IdU) == null) return BadRequest();
            else
            {
                repo.RenvoiToken(IdU);
                return Ok();
            }
        }
        #endregion
    }
}
