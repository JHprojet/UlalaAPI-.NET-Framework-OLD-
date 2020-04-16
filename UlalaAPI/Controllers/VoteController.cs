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
    public class VoteController : ApiController
    {
        VoteRepository repo = new VoteRepository();

        #region POST Ajout d'un Vote
        /// <summary>
        /// Post API/Vote
        /// </summary>
        /// <param name="E">Vote à insérer</param>
        public IHttpActionResult Post(VoteModel Vote)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (Vote == null || Vote.Enregistrement.Id == 0 || Vote.Utilisateur.Id == 0) return BadRequest();
                else
                {
                    repo.Create(Vote.ToEntity());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les Votes
        /// <summary>
        /// Get API/Vote
        /// </summary>
        /// <returns>Liste de tous les Votes</returns>
        public IHttpActionResult Get()
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<VoteModel> Liste = repo.GetAll().Select(Vote => Vote?.ToModel());
                if (Liste.Count() == 0) return NotFound();
                else return Json(Liste);
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les Votes by Utilisateur
        /// <summary>
        /// Get API/Vote
        /// </summary>
        /// <returns>Liste de tous les Votes</returns>
        [HttpGet]
        public IHttpActionResult GetbyUtilisateur([FromUri] int UtilisateurId)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<VoteModel> Liste = repo.GetAllbyUtilisateurId(UtilisateurId).Select(Vote => Vote?.ToModel());
                if (Liste.Count() == 0) return NotFound();
                else return Json(Liste);
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'un Vote by Id
        /// <summary>
        /// Get API/Vote/{id}
        /// </summary>
        /// <param name="id">id du Vote à récupérer</param>
        /// <returns>Vote avec l'id correspondant</returns>
        public IHttpActionResult Get(int id)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                VoteModel Objet = repo.GetOne(id)?.ToModel();
                if (Objet == null) return NotFound();
                else return Json(Objet);
            }
            else return Unauthorized();
        }
        #endregion

        #region DELETE Suppression d'un Vote by Id
        /// <summary>
        /// Delete API/Vote/{id}
        /// </summary>
        /// <param name="id">id du Vote à supprimer</param>
        public IHttpActionResult Delete(int id)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
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

        #region PUT Update d'un Vote by Id
        /// <summary>
        /// Put API/Vote/{id}
        /// </summary>
        /// <param name="Vote">Vote à insérer</param>
        /// <param name="Id">Id du Vote à modifier</param>
        public IHttpActionResult Put(int Id, VoteModel Vote)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(Id) == null) return NotFound();
                else if (Vote == null || Vote.Enregistrement.Id == 0 || Vote.Utilisateur.Id == 0) return BadRequest();
                else
                {
                    repo.Update(Id, Vote.ToEntity());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion
    }
}
