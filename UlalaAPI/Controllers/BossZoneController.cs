﻿using DAL.Entities;
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
    public class BossZoneController : ApiController
    {
        BossZoneRepository repo = new BossZoneRepository();

        #region POST Ajout d'un BossZone
        /// <summary>
        /// Post API/BossZone
        /// </summary>
        /// <param name="BossZone">BossZone à insérer</param>
        public IHttpActionResult Post(BossZoneModel BossZone)
        {
            if (BossZone == null) return BadRequest();
            else
            {
                repo.Create(BossZone.ToEntity());
                return Ok();
            }
        }
        #endregion

        #region GET Récupération de toutes les BossZone
        /// <summary>
        /// Get API/BossZone
        /// </summary>
        /// <returns>Liste de toutes les BossZone</returns>
        public IHttpActionResult Get()
        {
            IEnumerable<BossZoneModel> Liste = repo.GetAll().Select(BossZone => BossZone?.ToModel());
            if (Liste.Count() == 0) return NotFound();
            else return Json(Liste);
        }
        #endregion

        #region GET Récupération d'une BossZone by Id
        /// <summary>
        /// Get API/BossZone/{id}
        /// </summary>
        /// <param name="id">id du BossZone à récupérer</param>
        /// <returns>BossZone avec l'id correspondant</returns>
        public IHttpActionResult Get(int id)
        {
            BossZoneModel Objet = repo.GetOne(id)?.ToModel();
            if (Objet == null) return NotFound();
            else return Json(Objet);
        }
        #endregion

        #region DELETE Suppression d'un BossZone by Id
        /// <summary>
        /// Delete API/Boss/{id}
        /// </summary>
        /// <param name="id">id du Boss à supprimer</param>
        public void Delete(int id)
        {
            repo.Delete(id);
        }
        #endregion

        #region PUT Update d'un BossZone by Id
        /// <summary>
        /// Put API/BossZone/{id}
        /// </summary>
        /// <param name="BossZone">BossZone à insérer</param>
        /// <param name="id">Id du BossZone à modifier</param>
        public void Put(int id, BossZoneModel BossZone)
        {
            repo.Update(id, BossZone.ToEntity());
        }
        #endregion
    }
}