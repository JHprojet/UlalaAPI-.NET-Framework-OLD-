using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UlalaAPI.Models
{
    //STATUT : OK
    public class BossModel
    {
        public int Id { get; set; }
        public string NomFR { get; set; }
        public string NomEN { get; set; }
        public int Actif { get; set; }
    }
}