using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class FollowRepository
    {
        #region Ajout Follow
        public void Create(FollowEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.FollowedId != 0 && T.FollowerId != 0)
                    {
                        cmd.CommandText = "SP_AjoutFollow";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter FollowerId = new SqlParameter("FollowerId", T.FollowerId);
                        SqlParameter FollowedId = new SqlParameter("FollowedId", T.FollowedId);
                        cmd.Parameters.Add(FollowerId);
                        cmd.Parameters.Add(FollowedId);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Suppression Follow by Id
        public void Delete(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Follow WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    c.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region Récupération Follow
        public List<FollowEntity> GetAll()
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Follow";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<FollowEntity> L = new List<FollowEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new FollowEntity()
                            {
                                Id = (int)Tab["Id"],
                                FollowedId = (int)Tab["FollowedId"],
                                FollowerId = (int)Tab["FollowerId"]
                            });
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération Follow by Follower et Followed Id
        public int GetOneByFollowerAndFollowed(int FollowerId, int FollowedId)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Follow WHERE FollowerId = @FollowerId AND FollowedId = @FollowedId";
                    cmd.Parameters.AddWithValue("FollowerId", FollowerId);
                    cmd.Parameters.AddWithValue("FollowedId", FollowedId);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            FollowEntity S = new FollowEntity()
                            {
                                Id = (int)Tab["Id"],
                                FollowerId = (int)Tab["FollowerId"],
                                FollowedId = (int)Tab["FollowedId"],
                            };
                            return S.Id;
                        }
                        else return 0;
                    }
                }
            }
        }
    #endregion

        #region Récupération Follow by Id
        public FollowEntity GetOne(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Follow WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            FollowEntity S = new FollowEntity()
                            {
                                Id = (int)Tab["Id"],
                                FollowerId = (int)Tab["FollowerId"],
                                FollowedId = (int)Tab["FollowedId"],
                            };
                            return S;
                        }
                        else return null;
                    }
                }
            }
        }
        #endregion
    }
}
