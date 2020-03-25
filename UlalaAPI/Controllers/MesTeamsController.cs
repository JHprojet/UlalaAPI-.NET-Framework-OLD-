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
    public class MesTeamsController : ApiController
    {
        //STATUT : OK
        MesTeamsRepository repo = new MesTeamsRepository();

        #region POST Ajout d'une team
        /// <summary>
        /// Post API/MesTeams
        /// </summary>
        /// <param name="E">Team à insérer</param>
        public IHttpActionResult Post(MesTeamsModel MaTeam)
        {
            if (MaTeam == null) return BadRequest();
            else
            {
                repo.Create(MaTeam.ToEntity());
                return Ok();
            }
        }
        #endregion

        #region GET Récupération de tous les teams des utilisateurs
        /// <summary>
        /// Get API/MesTeams
        /// </summary>
        /// <returns>Liste de toutes les teams des utilisateurs</returns>
        public IHttpActionResult GetAll()
        {
            IEnumerable<MesTeamsModel> Liste = repo.GetAll().Select(MaTeam => MaTeam?.ToModel());
            if (Liste.Count() == 0) return NotFound();
            else return Json(Liste);
        }
        #endregion

        #region GET Récupération des teams d'un utilisateur by UtilisateurId
        /// <summary>
        /// Get API/MesTeams/?UtilisateurId={UtilisateurId}
        /// </summary>
        /// <param name="UtilisateurId">id de l'utilisateur pour lequel on veut récupérer ses teams</param>
        /// <returns>Liste de toutes les team de l'utilisateur ciblé</returns>
        public IHttpActionResult GetAll([FromUri]int UtilisateurId)
        {
            IEnumerable<MesTeamsModel> Liste = repo.GetAll(UtilisateurId).Select(Teams => Teams?.ToModel());
            if (Liste.Count() == 0) return NotFound();
            else return Json(Liste);
        }
        #endregion

        #region GET Récupération d'une Team by Id
        /// <summary>
        /// Get API/MesTeams/{id}
        /// </summary>
        /// <param name="id">id de la team à récupérer</param>
        /// <returns>Team avec l'id correspondant</returns>
        public IHttpActionResult Get(int id)
        {
            MesTeamsModel Objet = repo.GetOne(id)?.ToModel();
            if (Objet == null) return NotFound();
            else return Json(Objet);
        }
        #endregion

        #region DELETE Suppression d'une Team by Id
        /// <summary>
        /// Delete API/Skill/{id}
        /// </summary>
        /// <param name="id">id du Skill à supprimer</param>
        public void Delete(int id)
        {
            repo.Delete(id);
        }
        #endregion

        #region PUT Update d'une team by Id
        /// <summary>
        /// Put API/MesTeams/{id}
        /// </summary>
        /// <param name="MaTeam">Team à insérer</param>
        /// <param name="id">Id de la team à modifier</param>
        public IHttpActionResult Put(int id, MesTeamsModel MaTeam)
        {
            if (MaTeam == null || repo.GetOne(id)?.ToModel() == null) return BadRequest();
            else
            {
                repo.Update(id, MaTeam.ToEntity());
                return Ok();
            }
        }
        #endregion
    }
}
