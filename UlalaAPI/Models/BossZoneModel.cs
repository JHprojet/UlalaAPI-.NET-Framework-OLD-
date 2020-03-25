using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UlalaAPI.Models
{
    //STATUT : OK
    public class BossZoneModel
    {
        public int Id { get; set; }
        public ZoneModel Zone { get; set; }
        public BossModel Boss { get; set; }
        public int Actif { get; set; }
    }
}