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
    public class SkillController : ApiController
    {
        SkillRepository repo = new SkillRepository();

        #region POST Ajout d'un Skill
        /// <summary>
        /// Post API/Skill
        /// </summary>
        /// <param name="E">Skill à insérer</param>
        public IHttpActionResult Post(SkillModel Skill)
        {
            if (Skill == null) return BadRequest();
            else
            {
                repo.Create(Skill.ToEntity());
                return Ok();
            }
        }
        #endregion

        #region GET Récupération de tous les Skills
        /// <summary>
        /// Get API/Skill
        /// </summary>
        /// <returns>Liste de tous les Skill</returns>
        public IHttpActionResult GetAll()
        {
            IEnumerable<SkillModel> Liste = repo.GetAll().Select(Skill => Skill?.ToModel());
            if (Liste.Count() == 0) return NotFound();
            else return Json(Liste);
        }
        #endregion

        #region GET Récupération de tous les Skills by classeId
        /// <summary>
        /// Get API/Skill/?ClasseId={ClasseId}
        /// </summary>
        /// <param name="ClasseId"></param>
        /// <returns>Liste de tous les Skill</returns>
        public IHttpActionResult GetAll([FromUri]int ClasseId)
        {
            IEnumerable<SkillModel> Liste = repo.GetAll(ClasseId).Select(Skill => Skill?.ToModel());
            if (Liste.Count() == 0) return NotFound();
            else return Json(Liste);
        }
        #endregion

        #region GET Récupération d'un Skill by Id
        /// <summary>
        /// Get API/Skill/{id}
        /// </summary>
        /// <param name="id">id du Skill à récupérer</param>
        /// <returns>Skill avec l'id correspondant</returns>
        public IHttpActionResult Get(int id)
        {
            SkillModel Objet = repo.GetOne(id)?.ToModel();
            if (Objet == null) return NotFound();
            else return Json(Objet);
        }
        #endregion

        #region DELETE Suppression d'un Skill by Id
        /// <summary>
        /// Delete API/Skill/{id}
        /// </summary>
        /// <param name="id">id du Skill à supprimer</param>
        public void Delete(int id)
        {
            repo.Delete(id);
        }
        #endregion

        #region PUT Update d'un Skill by Id
        /// <summary>
        /// Put API/Skill/{id}
        /// </summary>
        /// <param name="Skill">Skill à insérer</param>
        /// <param name="Id">Id du Skill à modifier</param>
        public IHttpActionResult Put(int Id, SkillModel Skill)
        {
            if (Skill == null || repo.GetOne(Id)?.ToModel() == null) return BadRequest();
            else
            {
                repo.Update(Id, Skill.ToEntity());
                return Ok();
            }
        }
        #endregion
    }
}