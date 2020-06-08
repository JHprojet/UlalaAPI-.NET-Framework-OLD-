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
    public class ToyController : ApiController
    {
        ToyRepository repo = new ToyRepository();

        #region POST Add d'un Toy
        /// <summary>
        /// Post API/Toy
        /// </summary>
        /// <param name="E">Toy à insérer</param>
        public IHttpActionResult Post(ToyModel Toy)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (Toy == null || Toy.ImagePath == null || Toy.NameFR == null || Toy.NameEN == null) return BadRequest();
                else
                {
                    repo.Create(Toy.MapTo<ToyEntity>());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les Toys
        /// <summary>
        /// Get API/Toy
        /// </summary>
        /// <returns>List de tous les Toys</returns>
        public IHttpActionResult Get()
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<ToyModel> List = repo.GetAll().Select(Toy => Toy?.MapTo<ToyModel>());
                if (List.Count() == 0) return NotFound();
                else return Json(List);
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'un Toy by Id
        /// <summary>
        /// Get API/Toy/{id}
        /// </summary>
        /// <param name="id">id du Toy à récupérer</param>
        /// <returns>Toy avec l'id correspondant</returns>
        public IHttpActionResult Get(int id)
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                ToyModel Objet = repo.GetOne(id)?.MapTo<ToyModel>();
                if (Objet == null) return NotFound();
                else return Json(Objet);
            }
            else return Unauthorized();
        }
        #endregion

        #region DELETE Suppression d'un Toy by Id
        /// <summary>
        /// Delete API/Toy/{id}
        /// </summary>
        /// <param name="id">id du Toy à supprimer</param>
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

        #region PUT Update d'un Toy by Id
        /// <summary>
        /// Put API/Toy/{id}
        /// </summary>
        /// <param name="Toy">Toy à insérer</param>
        /// <param name="Id">Id du Toy à Updateier</param>
        public IHttpActionResult Put(int Id, ToyModel Toy)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(Id) != null) return NotFound();
                else if (Toy == null || Toy.ImagePath == null || Toy.NameFR == null || Toy.NameEN == null) return BadRequest();
                else
                {
                    repo.Update(Id, Toy.MapTo<ToyEntity>());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion
    }
}
