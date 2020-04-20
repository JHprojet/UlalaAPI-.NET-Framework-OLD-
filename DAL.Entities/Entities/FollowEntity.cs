using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class FollowEntity
    {
        public int Id { get; set; }
        public int FollowedId { get; set; }
        public int FollowerId { get; set; }
    }
}
