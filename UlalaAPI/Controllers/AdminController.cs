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
        UserRepository repo = new UserRepository();

        #region GET Récupération de tous les User
        /// <summary>
        /// Get API/Admin
        /// </summary>
        /// <returns>List de tous les User (même inActive)</returns>
        public IHttpActionResult Get()
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<UserModel> List = repo.GetAllAdmin().Select(User => User?.MapTo<UserModel>());
                if (List.Count() == 0) return NotFound();
                else return Json(List);
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'un User by Id
        /// <summary>
        /// Get API/Admin/{id}
        /// </summary>
        /// <param name="id">id du User à récupérer</param>
        /// <returns>User avec l'id correspondant</returns>
        public IHttpActionResult Get(int id)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                UserModel Objet = repo.GetOneAdmin(id)?.MapTo<UserModel>();
                if (Objet == null) return NotFound();
                else return Json(Objet);
            }
            else return Unauthorized();
        }
        #endregion
    }
}
