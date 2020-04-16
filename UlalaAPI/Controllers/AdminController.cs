using DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UlalaAPI.Helper;
using UlalaAPI.Mapper;
using UlalaAPI.Models;

namespace UlalaAPI.Controllers
{
    public class AdminController : ApiController
    {
        UtilisateurRepository repo = new UtilisateurRepository();

        #region GET Récupération de tous les boss
        /// <summary>
        /// Get API/Admin
        /// </summary>
        /// <returns>Liste de tous les utiilisateur (même inactif)</returns>
        public IHttpActionResult Get()
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<UtilisateurModel> Liste = repo.GetAllAdmin().Select(User => User?.MapTo<UtilisateurModel>());
                if (Liste.Count() == 0) return NotFound();
                else return Json(Liste);
            }
            else return Unauthorized();
        }
        #endregion
    }
}
