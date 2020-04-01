using DAL.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using UlalaAPI.Mapper;
using UlalaAPI.Models;

namespace UlalaAPI.Controllers
{
    public class TeamController : ApiController
    {
        TeamRepository repo = new TeamRepository();

        #region POST Ajout d'un Enregistrement
        /// <summary>
        /// Post API/Enregistrement
        /// </summary>
        /// <param name="Enregistrement">Enregistrement à insérer</param>
        public IHttpActionResult Post(TeamModel Team)
        {
            if (Team == null || Team.Classe1.Id == null || Team.Classe2.Id == null || Team.Classe3.Id == null || Team.Classe4.Id == null) return BadRequest();
            else
            {
                repo.Create(Team.ToEntity());
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
            IEnumerable<TeamModel> Liste = repo.GetAll().Select(Team => Team?.ToModel());
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
            TeamModel Objet = repo.GetOne(id)?.ToModel();
            if (Objet == null) return NotFound();
            else return Json(Objet);
        }
        #endregion

        #region DELETE Suppression d'un Enregistrement by Id
        /// <summary>
        /// Delete API/Enregistrement/{id}
        /// </summary>
        /// <param name="id">id de l'Enregistrement à supprimer</param>
        public IHttpActionResult Delete(int id)
        {
            if (repo.GetOne(id) == null) return NotFound();
            else
            {
                repo.Delete(id);
                return Ok();
            }
        }
        #endregion

        #region PUT Update d'un Enregistrement by Id
        /// <summary>
        /// Put API/Enregistrement/{id}
        /// </summary>
        /// <param name="Team">Enregistrement à insérer</param>
        /// <param name="id">Id de l'Enregistrement à modifier</param>
        public IHttpActionResult Put(int id, TeamModel Team)
        {
            if (repo.GetOne(id) == null) return NotFound();
            else if (Team == null || Team.Classe1.Id == null || Team.Classe2.Id == null || Team.Classe3.Id == null || Team.Classe4.Id == null) return BadRequest();
            else
            {
                repo.Update(id, Team.ToEntity());
                return Ok();
            }
        }
        #endregion

        #region GET Récupération de tous les Enregistrements by BossZone, Utilisateur et Classes
        /// <summary>
        /// Get API/Enregistrement/?U={U}&BZ={BZ}&C1={C1}2&C2={C2}&C3={C3}&C4={C4}
        /// </summary>
        /// <returns>Liste de toutes les Enregistrements correspondant à la recherche</returns>
        public IHttpActionResult Get([FromUri] int C1, [FromUri] int C2, [FromUri] int C3, [FromUri] int C4)
        {
            TeamModel Objet = repo.GetTeamByClasses(C1, C2, C3, C4).ToModel();
            if (Objet == null) return NotFound();
            else return Json(Objet);
        }
        #endregion
    }
}
