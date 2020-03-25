using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UlalaAPI.Models
{
    //STATUT : OK
    public class EnregistrementModel
    {
        public int Id { get; set; }
        public UtilisateurModel Utilisateur { get; set; }
        public BossZoneModel BossZone { get; set; }
        public TeamModel Team { get; set; }
        public string ImagePath1 { get; set; }
        public string ImagePath2 { get; set; }
        public string ImagePath3 { get; set; }
        public string ImagePath4 { get; set; }
        public int Note { get; set; }
        public int Actif { get; set; }
    }
}