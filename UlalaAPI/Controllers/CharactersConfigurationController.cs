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
    public class CharactersConfigurationController : ApiController
    {
        CharactersConfigurationRepository repo = new CharactersConfigurationRepository();

        #region POST Add d'une CharactersConfiguration
        /// <summary>
        /// Post API/CharactersConfiguration
        /// </summary>
        /// <param name="CharactersConfiguration">Strategy à insérer</param>
        public IHttpActionResult Post(CharactersConfigurationModel CharactersConfiguration)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (CharactersConfiguration == null || CharactersConfiguration.Classe1.Id == 0 || CharactersConfiguration.Classe2.Id == 0 || CharactersConfiguration.Classe3.Id == 0 || CharactersConfiguration.Classe4.Id == 0) return BadRequest();
                else
                {
                    repo.Create(CharactersConfiguration.ToEntity());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les CharactersConfigurations
        /// <summary>
        /// Get API/CharactersConfiguration
        /// </summary>
        /// <returns>List de toutes les CharactersConfigurations</returns>
        public IHttpActionResult Get()
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<CharactersConfigurationModel> List = repo.GetAll().Select(CharactersConfiguration => CharactersConfiguration?.ToModel());
                if (List.Count() == 0) return NotFound();
                else return Json(List);
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'une CharactersConfiguration by Id
        /// <summary>
        /// Get API/CharactersConfiguration/{id}
        /// </summary>
        /// <param name="id">id de la CharactersConfiguration à récupérer</param>
        /// <returns>CharactersConfiguration avec l'id correspondant</returns>
        public IHttpActionResult Get(int id)
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                CharactersConfigurationModel Objet = repo.GetOne(id)?.ToModel();
                if (Objet == null) return NotFound();
                else return Json(Objet);
            }
            else return Unauthorized();
        }
        #endregion

        #region DELETE Suppression d'une CharactersConfiguration by Id
        /// <summary>
        /// Delete API/CharactersConfiguration/{id}
        /// </summary>
        /// <param name="id">id de la CharactersConfiguration à supprimer</param>
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

        #region PUT Update d'une CharactersConfiguration by Id
        /// <summary>
        /// Put API/CharactersConfiguration/{id}
        /// </summary>
        /// <param name="CharactersConfiguration">CharactersConfiguration à insérer</param>
        /// <param name="id">Id de la CharactersConfiguration à Updateier</param>
        public IHttpActionResult Put(int id, CharactersConfigurationModel CharactersConfiguration)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(id) == null) return NotFound();
                else if (CharactersConfiguration == null || CharactersConfiguration.Classe1.Id == 0 || CharactersConfiguration.Classe2.Id == 0 || CharactersConfiguration.Classe3.Id == 0 || CharactersConfiguration.Classe4.Id == 0) return BadRequest();
                else
                {
                    repo.Update(id, CharactersConfiguration.ToEntity());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les Strategys by Classes
        /// <summary>
        /// Get API/CharactersConfiguration/?C1={C1}2&C2={C2}&C3={C3}&C4={C4}
        /// </summary>
        /// <returns>List de toutes les CharactersConfigurations correspondant à la recherche</returns>
        public IHttpActionResult Get([FromUri] int C1, [FromUri] int C2, [FromUri] int C3, [FromUri] int C4)
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                CharactersConfigurationModel Objet = repo.GetCharactersConfigurationByClasses(C1, C2, C3, C4).ToModel();
                if (Objet == null) return NotFound();
                else return Json(Objet);
            }
            else return Unauthorized();
        }
        #endregion
    }
}
