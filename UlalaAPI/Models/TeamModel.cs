using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UlalaAPI.Models
{
    //STATUT : OK
    public class TeamModel
    {
        public int Id { get; set; }
        public ClasseModel Classe1 { get; set; }
        public ClasseModel Classe2 { get; set; }
        public ClasseModel Classe3 { get; set; }
        public ClasseModel Classe4 { get; set; }
        public int Actif { get; set; }
    }
}