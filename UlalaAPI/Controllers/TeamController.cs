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
    public class TeamController : ApiController
    {
        TeamRepository repo = new TeamRepository();

        #region POST Ajout d'une team
        /// <summary>
        /// Post API/Team
        /// </summary>
        /// <param name="Team">Enregistrement à insérer</param>
        public IHttpActionResult Post(TeamModel Team)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (Team == null || Team.Classe1.Id == 0 || Team.Classe2.Id == 0 || Team.Classe3.Id == 0 || Team.Classe4.Id == 0) return BadRequest();
                else
                {
                    repo.Create(Team.ToEntity());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les Teams
        /// <summary>
        /// Get API/Team
        /// </summary>
        /// <returns>Liste de toutes les Teams</returns>
        public IHttpActionResult Get()
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<TeamModel> Liste = repo.GetAll().Select(Team => Team?.ToModel());
                if (Liste.Count() == 0) return NotFound();
                else return Json(Liste);
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'une Team by Id
        /// <summary>
        /// Get API/Team/{id}
        /// </summary>
        /// <param name="id">id de la team à récupérer</param>
        /// <returns>Team avec l'id correspondant</returns>
        public IHttpActionResult Get(int id)
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                TeamModel Objet = repo.GetOne(id)?.ToModel();
                if (Objet == null) return NotFound();
                else return Json(Objet);
            }
            else return Unauthorized();
        }
        #endregion

        #region DELETE Suppression d'une Team by Id
        /// <summary>
        /// Delete API/Team/{id}
        /// </summary>
        /// <param name="id">id de la team à supprimer</param>
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

        #region PUT Update d'une team by Id
        /// <summary>
        /// Put API/Team/{id}
        /// </summary>
        /// <param name="Team">Team à insérer</param>
        /// <param name="id">Id de la team à modifier</param>
        public IHttpActionResult Put(int id, TeamModel Team)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(id) == null) return NotFound();
                else if (Team == null || Team.Classe1.Id == 0 || Team.Classe2.Id == 0 || Team.Classe3.Id == 0 || Team.Classe4.Id == 0) return BadRequest();
                else
                {
                    repo.Update(id, Team.ToEntity());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les Enregistrements by Classes
        /// <summary>
        /// Get API/Team/?C1={C1}2&C2={C2}&C3={C3}&C4={C4}
        /// </summary>
        /// <returns>Liste de toutes les Teams correspondant à la recherche</returns>
        public IHttpActionResult Get([FromUri] int C1, [FromUri] int C2, [FromUri] int C3, [FromUri] int C4)
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                TeamModel Objet = repo.GetTeamByClasses(C1, C2, C3, C4).ToModel();
                if (Objet == null) return NotFound();
                else return Json(Objet);
            }
            else return Unauthorized();
        }
        #endregion
    }
}
