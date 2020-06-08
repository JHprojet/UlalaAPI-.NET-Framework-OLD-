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
    public class SkillController : ApiController
    {
        SkillRepository repo = new SkillRepository();

        #region POST Add d'un Skill
        /// <summary>
        /// Post API/Skill
        /// </summary>
        /// <param name="E">Skill à insérer</param>
        public IHttpActionResult Post(SkillModel Skill)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (Skill == null || Skill.NameEN == null || Skill.NameFR == null || Skill.Classe.Id == 0) return BadRequest();
                else
                {
                    repo.Create(Skill.ToEntity());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les Skills
        /// <summary>
        /// Get API/Skill
        /// </summary>
        /// <returns>List de tous les Skill</returns>
        public IHttpActionResult GetAll()
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<SkillModel> List = repo.GetAll().Select(Skill => Skill?.ToModel());
                if (List.Count() == 0) return NotFound();
                else return Json(List);
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les Skills by classeId
        /// <summary>
        /// Get API/Skill/?ClasseId={ClasseId}
        /// </summary>
        /// <param name="ClasseId"></param>
        /// <returns>List de tous les Skill</returns>
        public IHttpActionResult GetAll([FromUri]int ClasseId)
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<SkillModel> List = repo.GetAll(ClasseId).Select(Skill => Skill?.ToModel());
                if (List.Count() == 0) return NotFound();
                else return Json(List);
            }
            else return Unauthorized();
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
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                SkillModel Objet = repo.GetOne(id)?.ToModel();
                if (Objet == null) return NotFound();
                else return Json(Objet);
            }
            else return Unauthorized();
        }
        #endregion

        #region DELETE Suppression d'un Skill by Id
        /// <summary>
        /// Delete API/Skill/{id}
        /// </summary>
        /// <param name="id">id du Skill à supprimer</param>
        public IHttpActionResult Delete(int id)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
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

        #region PUT Update d'un Skill by Id
        /// <summary>
        /// Put API/Skill/{id}
        /// </summary>
        /// <param name="Skill">Skill à insérer</param>
        /// <param name="Id">Id du Skill à Updateier</param>
        public IHttpActionResult Put(int Id, SkillModel Skill)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(Id) == null) return NotFound();
                else if (Skill == null || Skill.NameEN == null || Skill.NameFR == null || Skill.Classe.Id == 0) return BadRequest();
                else
                {
                    repo.Update(Id, Skill.ToEntity());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion
    }
}
