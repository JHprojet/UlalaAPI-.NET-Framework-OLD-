using DAL.Entities;
using DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UlalaAPI.Mapper;
using UlalaAPI.Models;

namespace UlalaAPI.Controllers
{
    public class FavorisController : ApiController
    {
        //STATUT : OK
        FavoriRepository repo = new FavoriRepository();

        #region POST Ajout d'un favori
        /// <summary>
        /// Post API/Favoris
        /// </summary>
        /// <param name="Favori">Favori à insérer</param>
        public IHttpActionResult Post(FavoriModel Favori)
        {
            if (Favori == null) return BadRequest();
            else
            {
                repo.Create(Favori.ToEntity());
                return Ok();
            }
        }
        #endregion

        #region GET Récupération de toutes les Favoris
        /// <summary>
        /// Get API/Favori
        /// </summary>
        /// <returns>Liste de tous les Favoris</returns>
        public IHttpActionResult Get()
        {
            IEnumerable<FavoriModel> Liste =  repo.GetAll().Select(Favori => Favori?.ToModel());
            if (Liste.Count() == 0) return NotFound();
            else return Json(Liste);
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
            FavoriModel Objet = repo.GetOne(id)?.ToModel();
            if (Objet == null) return NotFound();
            else return Json(Objet);
        }
        #endregion

        #region GET Récupération de tous les Favoris by UtilisateurId
        /// <summary>
        /// Get API/Favoris/?idUtilisateur={idUtilisateur}
        /// </summary>
        /// <param name="idUtilisateur">id de l'utilisateur pour lequel on veut la liste des favoris</param>
        /// <returns>Liste de tous les Favoris</returns>
        public IHttpActionResult GetByUtilisateurId([FromUri]int idUtilisateur)
        {
            IEnumerable<FavoriModel> Liste = repo.GetAllByUtilisateurId(idUtilisateur).Select(Favori => Favori?.ToModel());
            if (Liste.Count() == 0) return NotFound();
            else return Json(Liste);
        }
        #endregion

        #region DELETE Suppression d'un Favori by Id
        /// <summary>
        /// Delete API/Favori/{id}
        /// </summary>
        /// <param name="id">id du Favori à supprimer</param>
        public void Delete(int id)
        {
            repo.Delete(id);
        }
        #endregion

        #region PUT Update d'un Favori by Id
        /// <summary>
        /// Put API/Favori/{id}
        /// </summary>
        /// <param name="Favori">Favori à insérer</param>
        /// <param name="Id">Id du Favori à modifier</param>
        public void Put(int Id, FavoriModel Favori)
        {
            repo.Update(Id, Favori.ToEntity());
        }
        #endregion
    }
}
