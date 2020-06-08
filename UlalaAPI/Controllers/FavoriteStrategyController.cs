using DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using UlalaAPI.Helper;
using UlalaAPI.Mapper;
using UlalaAPI.Models;

namespace UlalaAPI.Controllers
{
    public class FavoriteStrategyController : ApiController
    {
        FavoriteStrategyRepository repo = new FavoriteStrategyRepository();

        #region POST Add d'un favori
        /// <summary>
        /// Post API/Favoris
        /// </summary>
        /// <param name="Favori">Favori à insérer</param>
        public IHttpActionResult Post(FavoriteStrategyModel Favori)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (Favori == null || Favori.User.Id == 0 || Favori.Strategy.Id == 0) return BadRequest();
                else
                {
                    repo.Create(Favori.ToEntity());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de toutes les Favoris
        /// <summary>
        /// Get API/Favori
        /// </summary>
        /// <returns>List de tous les Favoris</returns>
        public IHttpActionResult Get()
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<FavoriteStrategyModel> List = repo.GetAll().Select(Favori => Favori?.ToModel());
                if (List.Count() == 0) return NotFound();
                else return Json(List);
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'une Favoris by Id
        /// <summary>
        /// Get API/Favori/{id}
        /// </summary>
        /// <param name="id">id du Favori à récupérer</param>
        /// <returns>Favori avec l'id correspondant</returns>
        public IHttpActionResult Get(int id)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                FavoriteStrategyModel Objet = repo.GetOne(id)?.ToModel();
                if (Objet == null) return NotFound();
                else return Json(Objet);
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les Favoris by UserId
        /// <summary>
        /// Get API/Favoris/?idUser={idUser}
        /// </summary>
        /// <param name="idUser">id de l'User pour lequel on veut la List des favoris</param>
        /// <returns>List de tous les Favoris</returns>
        public IHttpActionResult GetByUserId([FromUri]int UserId)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<FavoriteStrategyModel> List = repo.GetAllByUserId(UserId).Select(Favori => Favori?.ToModel());
                if (List.Count() == 0) return NotFound();
                else return Json(List);
            }
            else return Unauthorized();
        }
        #endregion

        #region DELETE Suppression d'un Favori by Id
        /// <summary>
        /// Delete API/Favori/{id}
        /// </summary>
        /// <param name="id">id du Favori à supprimer</param>
        public IHttpActionResult Delete(int id)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(id) == null) return NotFound();
                else
                {
                    repo.Delete(id);
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region PUT Update d'un Favori by Id
        /// <summary>
        /// Put API/Favori/{id}
        /// </summary>
        /// <param name="Favori">Favori à insérer</param>
        /// <param name="Id">Id du Favori à Updateier</param>
        public IHttpActionResult Put(int Id, FavoriteStrategyModel Favori)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(Id) == null) return NotFound();
                else if (Favori == null || Favori.User.Id == 0 || Favori.Strategy.Id == 0) return BadRequest();
                else
                {
                    repo.Update(Id, Favori.ToEntity());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion
    }
}
