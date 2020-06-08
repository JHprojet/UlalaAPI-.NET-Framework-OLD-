using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UlalaAPI.Models
{
    public class FollowModel
    {
        public int Id { get; set; }
        public UserModel Followed { get; set; }
        public UserModel Follower { get; set; }
    }
}