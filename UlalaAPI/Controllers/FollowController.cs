using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL.Entities;
using DAL.Services;
using UlalaAPI.Helper;
using UlalaAPI.Mapper;
using UlalaAPI.Models;

namespace UlalaAPI.Controllers
{
    public class FollowController : ApiController
    {
        FollowRepository repo = new FollowRepository();

        #region POST Add d'un follow
        /// <summary>
        /// Post API/Follow
        /// </summary>
        /// <param name="Follow">Follow à insérer</param>
        public IHttpActionResult Post(FollowModel Follow)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (Follow == null || Follow.Followed.Id == 0 || Follow.Follower.Id == 0) return BadRequest();
                else
                {
                    repo.Create(Follow.ToEntity());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'un follow by Follower and Followed
        /// <summary>
        /// Get API/Follow/?FollowerId={FollowerId}&FollowedId={FollowedId}
        /// </summary>
        /// <param name="id">id du Follow à récupérer</param>
        /// <returns>Follow avec l'id correspondant</returns>
        public IHttpActionResult Get([FromUri]int FollowerId, [FromUri]int FollowedId)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                int Id = repo.GetOneByFollowerAndFollowed(FollowerId, FollowedId);
                if (Id == 0) return NotFound();
                else return Json(Id);
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les follow
        /// <summary>
        /// Get API/Follow
        /// </summary>
        /// <returns>List de tous les Follow</returns>
        public IHttpActionResult Get()
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<FollowModel> List = repo.GetAll().Select(Follow => Follow?.ToModel());
                if (List.Count() == 0) return NotFound();
                else return Json(List);
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'un follow by Id
        /// <summary>
        /// Get API/Follow/{id}
        /// </summary>
        /// <param name="id">id du Follow à récupérer</param>
        /// <returns>Follow avec l'id correspondant</returns>
        public IHttpActionResult Get(int id)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                FollowModel Objet = repo.GetOne(id)?.ToModel();
                if (Objet == null) return NotFound();
                else return Json(Objet);
            }
            else return Unauthorized();
        }
        #endregion

        #region DELETE Suppression d'un follow by Id
        /// <summary>
        /// Delete API/Follow/{id}
        /// </summary>
        /// <param name="id">id du Follow à supprimer</param>
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
    }
}
