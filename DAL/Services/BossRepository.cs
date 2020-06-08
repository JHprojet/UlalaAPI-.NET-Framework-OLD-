using DAL.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Services
{
    public class BossRepository
    {
        #region Add Boss
        public void Create(BossEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.NameFR != null && T.NameEN != null)
                    {
                        cmd.CommandText = "SP_AddBoss";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter NameEN = new SqlParameter("NameEN", T.NameEN);
                        SqlParameter NameFR = new SqlParameter("NameFR", T.NameFR);
                        cmd.Parameters.Add(NameFR);
                        cmd.Parameters.Add(NameEN);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Suppression Boss by Id
        public void Delete(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Bosses WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    c.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region Récupération Bosses
        public List<BossEntity> GetAll()
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Bosses WHERE Active = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<BossEntity> L = new List<BossEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new BossEntity()
                            {
                                Id = (int)Tab["Id"],
                                NameEN = Tab["NameEN"].ToString(),
                                NameFR = Tab["NameFR"].ToString(),
                                Active = (int)Tab["Active"]
                            });
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération Boss by Id
        public BossEntity GetOne(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Bosses WHERE Id = @Id AND Active = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            BossEntity S = new BossEntity()
                            {
                                Id = (int)Tab["Id"],
                                NameEN = Tab["NameEN"].ToString(),
                                NameFR = Tab["NameFR"].ToString(),
                                Active = (int)Tab["Active"]
                            };
                            return S;
                        }
                        else return null;
                    }
                }
            }
        }
        #endregion

        #region Update Boss by Id
        public void Update(int Id, BossEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.NameFR != null && T.NameEN != null && Id != 0)
                    {
                        cmd.CommandText = "SP_UpdateBoss";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter NameEN = new SqlParameter("NameEN", T.NameEN);
                        SqlParameter NameFR = new SqlParameter("NameFR", T.NameFR);
                        SqlParameter PId = new SqlParameter("Id", Id);
                        cmd.Parameters.Add(NameFR);
                        cmd.Parameters.Add(NameEN);
                        cmd.Parameters.Add(PId);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion
    }
}