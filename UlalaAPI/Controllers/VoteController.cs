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

        #region POST Add d'un Vote
        /// <summary>
        /// Post API/Vote
        /// </summary>
        /// <param name="E">Vote à insérer</param>
        public IHttpActionResult Post(VoteModel Vote)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (Vote == null || Vote.Strategy.Id == 0 || Vote.User.Id == 0) return BadRequest();
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
        /// <returns>List de tous les Votes</returns>
        public IHttpActionResult Get()
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<VoteModel> List = repo.GetAll().Select(Vote => Vote?.ToModel());
                if (List.Count() == 0) return NotFound();
                else return Json(List);
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les Votes by User
        /// <summary>
        /// Get API/Vote
        /// </summary>
        /// <returns>List de tous les Votes</returns>
        [HttpGet]
        public IHttpActionResult GetbyUser([FromUri] int UserId)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<VoteModel> List = repo.GetAllbyUserId(UserId).Select(Vote => Vote?.ToModel());
                if (List.Count() == 0) return NotFound();
                else return Json(List);
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
        /// <param name="Id">Id du Vote à Updateier</param>
        public IHttpActionResult Put(int Id, VoteModel Vote)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(Id) == null) return NotFound();
                else if (Vote == null || Vote.Strategy.Id == 0 || Vote.User.Id == 0) return BadRequest();
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
