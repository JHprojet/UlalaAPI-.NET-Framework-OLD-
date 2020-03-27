using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Entities
{
    public class BossZoneEntity
    {
        //STATUT : OK
        public int Id { get; set; }
        public int ZoneId { get; set; }
        public int BossId { get; set; }
        public int Actif { get; set; }
    }
}