using DAL.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
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
            if (Vote == null || Vote.Enregistrement.Id == 0 || Vote.Utilisateur.Id == 0) return BadRequest();
            else
            {
                repo.Create(Vote.ToEntity());
                return Ok();
            }
        }
        #endregion

        #region GET Récupération de tous les Votes
        /// <summary>
        /// Get API/Vote
        /// </summary>
        /// <returns>Liste de tous les Votes</returns>
        public IHttpActionResult Get()
        {
            IEnumerable<VoteModel> Liste = repo.GetAll().Select(Vote => Vote?.ToModel());
            if (Liste.Count() == 0) return NotFound();
            else return Json(Liste);
        }
        #endregion

        #region GET Récupération de tous les Votes
        /// <summary>
        /// Get API/Vote
        /// </summary>
        /// <returns>Liste de tous les Votes</returns>
        public IHttpActionResult GetbyUtilisateur([FromUri] int UtilisateurId)
        {
            IEnumerable<VoteModel> Liste = repo.GetAllbyUtilisateurId(UtilisateurId).Select(Vote => Vote?.ToModel());
            if (Liste.Count() == 0) return NotFound();
            else return Json(Liste);
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
            VoteModel Objet = repo.GetOne(id)?.ToModel();
            if (Objet == null) return NotFound();
            else return Json(Objet);
        }
        #endregion

        #region DELETE Suppression d'un Vote by Id
        /// <summary>
        /// Delete API/Vote/{id}
        /// </summary>
        /// <param name="id">id du Vote à supprimer</param>
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

        #region PUT Update d'un Vote by Id
        /// <summary>
        /// Put API/Vote/{id}
        /// </summary>
        /// <param name="Vote">Vote à insérer</param>
        /// <param name="Id">Id du Vote à modifier</param>
        public IHttpActionResult Put(int Id, VoteModel Vote)
        {
            if (repo.GetOne(Id) == null) return NotFound();
            else if (Vote == null || Vote.Enregistrement.Id == 0 || Vote.Utilisateur.Id == 0 ) return BadRequest();
            else
            {
                repo.Update(Id, Vote.ToEntity());
                return Ok();
            }
        }
        #endregion
    }
}
