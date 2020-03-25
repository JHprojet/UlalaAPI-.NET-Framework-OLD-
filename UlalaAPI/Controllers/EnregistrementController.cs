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
    //STATUT : OK
    public class EnregistrementController : ApiController
    {
        EnregistrementRepository repo = new EnregistrementRepository();

        #region POST Ajout d'un Enregistrement
        /// <summary>
        /// Post API/Enregistrement
        /// </summary>
        /// <param name="Enregistrement">Enregistrement à insérer</param>
        public IHttpActionResult Post(EnregistrementModel Enregistrement)
        {
            if (Enregistrement == null) return BadRequest();
            else
            {
                repo.Create(Enregistrement.ToEntity());
                return Ok();
            }
        }
        #endregion

        #region GET Récupération de tous les Enregistrements
        /// <summary>
        /// Get API/Enregistrement
        /// </summary>
        /// <returns>Liste de toutes les Enregistrements</returns>
        public IHttpActionResult Get()
        {
            IEnumerable<EnregistrementModel> Liste = repo.GetAll().Select(Enregistrement => Enregistrement?.ToModel());
            if (Liste.Count() == 0) return NotFound();
            else return Json(Liste);
        }
        #endregion

        #region GET Récupération d'un Enregistrement by Id
        /// <summary>
        /// Get API/Enregistrement/{id}
        /// </summary>
        /// <param name="id">id de l'Enregistrement à récupérer</param>
        /// <returns>Enregistrement avec l'id correspondant</returns>
        public IHttpActionResult Get(int id)
        {
            EnregistrementModel Objet = repo.GetOne(id)?.ToModel();
            if (Objet == null) return NotFound();
            else return Json(Objet);
        }
        #endregion

        #region DELETE Suppression d'un Enregistrement by Id
        /// <summary>
        /// Delete API/Enregistrement/{id}
        /// </summary>
        /// <param name="id">id de l'Enregistrement à supprimer</param>
        public void Delete(int id)
        {
            repo.Delete(id);
        }
        #endregion

        #region PUT Update d'un Enregistrement by Id
        /// <summary>
        /// Put API/Enregistrement/{id}
        /// </summary>
        /// <param name="Enregistrement">Enregistrement à insérer</param>
        /// <param name="id">Id de l'Enregistrement à modifier</param>
        public void Put(int id, EnregistrementModel Enregistrement)
        {
            repo.Update(id, Enregistrement.ToEntity());
        }
        #endregion

        #region GET Récupération de tous les Enregistrements by BossZone, Utilisateur et Classes
        /// <summary>
        /// Get API/Enregistrement/?U={U}&BZ={BZ}&C1={C1}2&C2={C2}&C3={C3}&C4={C4}
        /// </summary>
        /// <returns>Liste de toutes les Enregistrements correspondant à la recherche</returns>
        public IEnumerable<EnregistrementModel> Get([FromUri]int? U, [FromUri] int? BZ, [FromUri] int? C1, [FromUri] int? C2, [FromUri] int? C3, [FromUri] int? C4)
        {
            return repo.GetAllByInfos(U, BZ, C1, C2, C3, C4).Select(Enregistrement => Enregistrement.ToModel());
        }
        #endregion
    }
}
