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
    public class ClasseController : ApiController
    {
        ClasseRepository repo = new ClasseRepository();

        #region POST Ajout d'une Classe
        /// <summary>
        /// Post API/Classe
        /// </summary>
        /// <param name="Classe">Classe à insérer</param>
        public IHttpActionResult Post(ClasseModel Classe)
        {
            if (Classe == null || Classe.NomEN == null || Classe.NomFR == null) return BadRequest();
            else
            {
                repo.Create(Classe.MapTo<ClasseEntity>());
                return Ok();
            }
        }
        #endregion

        #region GET Récupération de toutes les Classes
        /// <summary>
        /// Get API/Classe
        /// </summary>
        /// <returns>Liste de toutes les Classes</returns>
        public IHttpActionResult Get()
        {
            IEnumerable<ClasseModel> Liste = repo.GetAll().Select(Classe => Classe?.MapTo<ClasseModel>());
            if (Liste.Count() == 0) return NotFound();
            else return Json(Liste);
        }
        #endregion

        #region GET Récupération d'une Classe by Id
        /// <summary>
        /// Get API/Classe/{id}
        /// </summary>
        /// <param name="id">id de la Classe à récupérer</param>
        /// <returns>Classe avec l'id correspondant</returns>
        public IHttpActionResult Get(int id)
        {
            ClasseModel Objet = repo.GetOne(id)?.MapTo<ClasseModel>();
            if (Objet == null) return NotFound();
            else return Json(Objet);
        }
        #endregion

        #region DELETE Suppression d'une Classe by Id
        /// <summary>
        /// Delete API/Classe/{id}
        /// </summary>
        /// <param name="id">id de la Classe à supprimer</param>
        public void Delete(int id)
        {
            repo.Delete(id);
        }
        #endregion

        #region PUT Update d'une Classe by Id
        /// <summary>
        /// Put API/Classe/{id}
        /// </summary>
        /// <param name="Classe">Classe à insérer</param>
        /// <param name="id">Id de la Classe à modifier</param>
        public IHttpActionResult Put(int id, ClasseModel Classe)
        {
            if (Classe == null || Classe.NomEN == null || Classe.NomFR == null || id == 0 || repo.GetOne(id)?.MapTo<ClasseModel>() == null) return BadRequest();
            else
            {
                repo.Update(id, Classe.MapTo<ClasseEntity>());
                return Ok();
            }
        }
        #endregion
    }
}
