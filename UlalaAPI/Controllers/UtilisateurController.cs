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
    public class UtilisateurController : ApiController
    {
        //STATUT : OK
        UtilisateurRepository repo = new UtilisateurRepository();

        #region POST Ajout d'un Utilisateur
        /// <summary>
        /// Post API/Utilisateur
        /// </summary>
        /// <param name="E">Utilisateur à insérer</param>
        public IHttpActionResult Post(UtilisateurModel Utilisateur)
        {
            if (Utilisateur == null) return BadRequest();
            else
            {
                repo.Create(Utilisateur.MapTo<UtilisateurEntity>());
                return Ok();
            }
        }
        #endregion

        #region GET Récupération de tous les Utilisateurs
        /// <summary>
        /// Get API/Utilisateur
        /// </summary>
        /// <returns>Liste de tous les Utilisateur</returns>
        public IHttpActionResult Get()
        {
            IEnumerable<UtilisateurModel> Liste = repo.GetAll().Select(Utilisateur => Utilisateur?.MapTo<UtilisateurModel>());
            if (Liste.Count() == 0) return NotFound();
            else return Json(Liste);
        }
        #endregion

        #region GET Récupération d'un Utilisateur by Id
        /// <summary>
        /// Get API/Utilisateur/{id}
        /// </summary>
        /// <param name="id">id du Utilisateur à récupérer</param>
        /// <returns>Utilisateur avec l'id correspondant</returns>
        public IHttpActionResult Get(int id)
        {
            UtilisateurModel Objet = repo.GetOne(id)?.MapTo<UtilisateurModel>();
            if (Objet == null) return NotFound();
            else return Json(Objet);
        }
        #endregion

        #region GET Récupération d'un Utilisateur by pseudo
        /// <summary>
        /// Get API/Utilisateur/?pseudo={pseudo}
        /// </summary>
        /// <param name="pseudo">pseudo de l'Utilisateur à récupérer</param>
        /// <returns>Utilisateur avec le pseudo correspondant</returns>
        public IHttpActionResult GetByPseudo([FromUri] string pseudo)
        {
            UtilisateurModel Objet = repo.GetOneByPseudo(pseudo)?.MapTo<UtilisateurModel>();
            if (Objet == null) return NotFound();
            else return Json(Objet);
        }
        #endregion

        #region GET Récupération d'un Utilisateur by mail
        /// <summary>
        /// Get API/Utilisateur/?mail={mail}
        /// </summary>
        /// <param name="mail">mail de l'Utilisateur à récupérer</param>
        /// <returns>Utilisateur avec le mail correspondant</returns>
        public IHttpActionResult GetByMail([FromUri] string mail)
        {
            UtilisateurModel Objet = repo.GetOneByMail(mail)?.MapTo<UtilisateurModel>();
            if (Objet == null) return NotFound();
            else return Json(Objet);
        }
        #endregion

        #region DELETE Suppression d'un Utilisateur by Id
        /// <summary>
        /// Delete API/Utilisateur/{id}
        /// </summary>
        /// <param name="id">id de l'Utilisateur à supprimer</param>
        public void Delete(int id)
        {
            repo.Delete(id);
        }
        #endregion

        #region PUT Update d'un Utilisateur by Id
        /// <summary>
        /// Put API/Utilisateur/{id}
        /// </summary>
        /// <param name="Utilisateur">Utilisateur à insérer</param>
        /// <param name="Id">Id du Utilisateur à modifier</param>
        public IHttpActionResult Put(int Id, UtilisateurModel Utilisateur)
        {
            if (Utilisateur == null || repo.GetOne(Id)?.MapTo<UtilisateurModel>() == null) return BadRequest();
            else
            {
                repo.Update(Id, Utilisateur.MapTo<UtilisateurEntity>());
                return Ok();
            }
        }
        #endregion
    }
}
