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
    }
}
