using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Entities
{
    //STATUT : OK
    public class UtilisateurEntity
    {
        public int Id { get; set; }
        public string Pseudo { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int Actif { get; set; }
    }
}