using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UlalaAPI.Models
{
    public class FollowModel
    {
        public int Id { get; set; }
        public UtilisateurModel Followed { get; set; }
        public UtilisateurModel Follower { get; set; }
    }
}