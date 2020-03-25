using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Entities
{
    //STATUT : OK
    public class TeamEntity
    {
        public int Id { get; set; }
        public int ClasseId1 { get; set; }
        public int ClasseId2 { get; set; }
        public int ClasseId3 { get; set; }
        public int ClasseId4 { get; set; }
        public int Actif { get; set; }
    }
}