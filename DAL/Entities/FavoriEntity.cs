﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Entities
{
    //STATUT : OK
    public class FavoriEntity
    {
        public int Id { get; set; }
        public int UtilisateurId { get; set; }
        public int EnregistrementId { get; set; }
        public int Actif { get; set; }
    }
}