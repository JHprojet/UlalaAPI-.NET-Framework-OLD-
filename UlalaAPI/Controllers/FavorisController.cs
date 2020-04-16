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
    public class FavorisController : ApiController
    {
        FavoriRepository repo = new FavoriRepository();

        #region POST Ajout d'un favori
        /// <summary>
        /// Post API/Favoris
        /// </summary>
        /// <param name="Favori">Favori à insérer</param>
        public IHttpActionResult Post(FavoriModel Favori)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (Favori == null || Favori.Utilisateur.Id == 0 || Favori.Enregistrement.Id == 0) return BadRequest();
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
        /// <returns>Liste de tous les Favoris</returns>
        public IHttpActionResult Get()
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<FavoriModel> Liste = repo.GetAll().Select(Favori => Favori?.ToModel());
                if (Liste.Count() == 0) return NotFound();
                else return Json(Liste);
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
                FavoriModel Objet = repo.GetOne(id)?.ToModel();
                if (Objet == null) return NotFound();
                else return Json(Objet);
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les Favoris by UtilisateurId
        /// <summary>
        /// Get API/Favoris/?idUtilisateur={idUtilisateur}
        /// </summary>
        /// <param name="idUtilisateur">id de l'utilisateur pour lequel on veut la liste des favoris</param>
        /// <returns>Liste de tous les Favoris</returns>
        public IHttpActionResult GetByUtilisateurId([FromUri]int UtilisateurId)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<FavoriModel> Liste = repo.GetAllByUtilisateurId(UtilisateurId).Select(Favori => Favori?.ToModel());
                if (Liste.Count() == 0) return NotFound();
                else return Json(Liste);
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
        /// <param name="Id">Id du Favori à modifier</param>
        public IHttpActionResult Put(int Id, FavoriModel Favori)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(Id) == null) return NotFound();
                else if (Favori == null || Favori.Utilisateur.Id == 0 || Favori.Enregistrement.Id == 0) return BadRequest();
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
