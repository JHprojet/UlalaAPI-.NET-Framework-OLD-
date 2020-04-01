using DAL.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using UlalaAPI.Mapper;
using UlalaAPI.Models;

namespace UlalaAPI.Controllers
{
    public class BossZoneController : ApiController
    {
        BossZoneRepository repo = new BossZoneRepository();

        #region POST Ajout d'un BossZone
        /// <summary>
        /// Post API/BossZone
        /// </summary>
        /// <param name="BossZone">BossZone à insérer</param>
        public IHttpActionResult Post(BossZoneModel BossZone)
        {
            if (BossZone == null || BossZone.Boss.Id == 0 || BossZone.Zone.Id == 0) return BadRequest();
            else
            {
                repo.Create(BossZone.ToEntity());
                return Ok();
            }
        }
        #endregion

        #region GET Récupération de toutes les BossZone
        /// <summary>
        /// Get API/BossZone
        /// </summary>
        /// <returns>Liste de toutes les BossZone</returns>
        public IHttpActionResult Get()
        {
            IEnumerable<BossZoneModel> Liste = repo.GetAll().Select(BossZone => BossZone?.ToModel());
            if (Liste.Count() == 0) return NotFound();
            else return Json(Liste);
        }
        #endregion

        #region GET Récupération d'une BossZone by Id
        /// <summary>
        /// Get API/BossZone/{id}
        /// </summary>
        /// <param name="id">id du BossZone à récupérer</param>
        /// <returns>BossZone avec l'id correspondant</returns>
        public IHttpActionResult Get(int id)
        {
            BossZoneModel Objet = repo.GetOne(id)?.ToModel();
            if (Objet == null) return NotFound();
            else return Json(Objet);
        }
        #endregion

        #region DELETE Suppression d'un BossZone by Id
        /// <summary>
        /// Delete API/Boss/{id}
        /// </summary>
        /// <param name="id">id du Boss à supprimer</param>
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

        #region PUT Update d'un BossZone by Id
        /// <summary>
        /// Put API/BossZone/{id}
        /// </summary>
        /// <param name="BossZone">BossZone à insérer</param>
        /// <param name="id">Id du BossZone à modifier</param>
        public IHttpActionResult Put(int id, BossZoneModel BossZone)
        {
            if (repo.GetOne(id) == null) return NotFound();
            else if (BossZone == null || BossZone.Boss.Id == 0 || BossZone.Zone.Id == 0) return BadRequest();
            else
            {
                repo.Update(id, BossZone.ToEntity());
                return Ok();
            }
        }
        #endregion
    }
}
