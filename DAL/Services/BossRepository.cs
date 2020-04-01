using DAL.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Services
{
    public class BossRepository
    {
        #region Ajout Boss
        public void Create(BossEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.NomFR != null && T.NomEN != null)
                    {
                        cmd.CommandText = "SP_AjoutBoss";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter NomEN = new SqlParameter("NomEN", T.NomEN);
                        SqlParameter NomFR = new SqlParameter("NomFR", T.NomFR);
                        cmd.Parameters.Add(NomFR);
                        cmd.Parameters.Add(NomEN);
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
                    cmd.CommandText = "DELETE FROM Boss WHERE Id = @Id";
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
                    cmd.CommandText = "SELECT * FROM Boss WHERE Actif = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<BossEntity> L = new List<BossEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new BossEntity()
                            {
                                Id = (int)Tab["Id"],
                                NomEN = Tab["NomEN"].ToString(),
                                NomFR = Tab["NomFR"].ToString(),
                                Actif = (int)Tab["Actif"]
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
                    cmd.CommandText = "SELECT * FROM Boss WHERE Id = @Id AND Actif = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            BossEntity S = new BossEntity()
                            {
                                Id = (int)Tab["Id"],
                                NomEN = Tab["NomEN"].ToString(),
                                NomFR = Tab["NomFR"].ToString(),
                                Actif = (int)Tab["Actif"]
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
                    if (T != null && T.NomFR != null && T.NomEN != null && Id != 0)
                    {
                        cmd.CommandText = "SP_ModifBoss";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter NomEN = new SqlParameter("NomEN", T.NomEN);
                        SqlParameter NomFR = new SqlParameter("NomFR", T.NomFR);
                        SqlParameter PId = new SqlParameter("Id", Id);
                        cmd.Parameters.Add(NomFR);
                        cmd.Parameters.Add(NomEN);
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