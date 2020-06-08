using DAL.Entities;
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
    public class ZoneController : ApiController
    {
        ZoneRepository repo = new ZoneRepository();

        #region POST Add d'une Zone
        /// <summary>
        /// Post API/Zone
        /// </summary>
        /// <param name="Zone">Zone à insérer</param>
        public IHttpActionResult Post(ZoneModel Zone)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (Zone == null || Zone.ContinentEN == null || Zone.ContinentFR == null || Zone.ZoneEN == null || Zone.ZoneFR == null || Zone.ZoneQty == 0) return BadRequest();
                else
                {
                    repo.Create(Zone.MapTo<ZoneEntity>());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de toutes les Zones
        /// <summary>
        /// Get API/Zone
        /// </summary>
        /// <returns>List de tous les zones</returns>
        public IHttpActionResult Get()
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<ZoneModel> List = repo.GetAll().Select(Zone => Zone?.MapTo<ZoneModel>());
                if (List.Count() == 0) return NotFound();
                else return Json(List);
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'une Zone by Id
        /// <summary>
        /// Get API/Zone/{id}
        /// </summary>
        /// <param name="id">id de la Zone à récupérer</param>
        /// <returns>Zone avec l'id correspondant</returns>
        public IHttpActionResult Get(int id)
        {
            if ((new[] { "Admin", "User" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                ZoneModel Objet = repo.GetOne(id)?.MapTo<ZoneModel>();
                if (Objet == null) return NotFound();
                else return Json(Objet);
            }
            else return Unauthorized();
        }
        #endregion

        #region DELETE Suppression d'une Zone by Id
        /// <summary>
        /// Delete API/Zone/{id}
        /// </summary>
        /// <param name="id">id du Zone à supprimer</param>
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

        #region PUT Update d'une Zone by Id
        /// <summary>
        /// Put API/Zone/{id}
        /// </summary>
        /// <param name="Zone">Zone à insérer</param>
        /// <param name="id">Id de la Zone à Updateier</param>
        public IHttpActionResult Put(int id, ZoneModel Zone)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(id) == null) return NotFound();
                else if (Zone.ContinentFR == null || Zone.ContinentEN == null || Zone.ZoneFR == null || Zone.ZoneEN == null || Zone.ZoneQty == 0) return BadRequest();
                else
                {
                    repo.Update(id, Zone.MapTo<ZoneEntity>());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion
    }
}
