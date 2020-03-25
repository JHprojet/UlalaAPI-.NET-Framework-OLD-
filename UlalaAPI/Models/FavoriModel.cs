using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UlalaAPI.Models
{
    //STATUT : OK
    public class FavoriModel
    {
        public int Id { get; set; }
        public UtilisateurModel Utilisateur { get; set; }
        public EnregistrementModel Enregistrement { get; set; }
        public int Actif { get; set; }
    }
}