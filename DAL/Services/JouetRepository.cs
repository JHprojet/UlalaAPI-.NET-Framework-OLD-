using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL.Services
{
    //STATUT : OK
    public class JouetRepository
    {
        #region Ajout Jouet
        public void Create(JouetEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.NomEN != null && T.NomFR != null && T.ImagePath != null)
                    {
                        cmd.CommandText = "SP_AjoutJouet";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter NomEN = new SqlParameter("NomEN", T.NomEN);
                        SqlParameter NomFR = new SqlParameter("NomFR", T.NomFR);
                        SqlParameter ImagePath = new SqlParameter("ImagePath", T.ImagePath);
                        cmd.Parameters.Add(NomEN);
                        cmd.Parameters.Add(NomFR);
                        cmd.Parameters.Add(ImagePath);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Suppression Jouet by Id
        public void Delete(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Jouets WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    c.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region Récupération Jouets
        public List<JouetEntity> GetAll()
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Jouets WHERE Actif = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<JouetEntity> L = new List<JouetEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new JouetEntity()
                            {
                                Id = (int)Tab["Id"],
                                NomEN = Tab["NomEN"].ToString(),
                                NomFR = Tab["NomFR"].ToString(),
                                ImagePath = Tab["ImagePath"].ToString(),
                                Actif = (int)Tab["Actif"]
                            });
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération Jouet by Id
        public JouetEntity GetOne(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Jouets WHERE Id = @Id AND Actif = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            JouetEntity S = new JouetEntity()
                            {
                                Id = (int)Tab["Id"],
                                NomEN = Tab["NomEN"].ToString(),
                                NomFR = Tab["NomFR"].ToString(),
                                ImagePath = Tab["ImagePath"].ToString(),
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

        #region Update Jouet by Id
        public void Update(int Id, JouetEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.NomEN != null && T.NomFR != null && T.ImagePath != null && Id != 0)
                    {
                        cmd.CommandText = "SP_ModifJouet";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter NomEN = new SqlParameter("NomEN", T.NomEN);
                        SqlParameter NomFR = new SqlParameter("NomFR", T.NomFR);
                        SqlParameter ImagePath = new SqlParameter("ImagePath", T.ImagePath);
                        SqlParameter PId = new SqlParameter("Id", Id);
                        cmd.Parameters.Add(NomFR);
                        cmd.Parameters.Add(NomEN);
                        cmd.Parameters.Add(ImagePath);
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