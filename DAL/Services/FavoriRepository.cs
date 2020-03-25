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
    public class FavoriRepository
    {
        #region Ajout Favoris
        public void Create(FavoriEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.EnregistrementId != 0 && T.UtilisateurId != 0)
                    {
                        cmd.CommandText = "SP_AjoutFavori";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter EnregistrementId = new SqlParameter("EnregistrementId", T.EnregistrementId);
                        SqlParameter UtilisateurId = new SqlParameter("UtilisateurId", T.UtilisateurId);
                        cmd.Parameters.Add(EnregistrementId);
                        cmd.Parameters.Add(UtilisateurId);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Suppression Favoris by Id
        public void Delete(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Favoris WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    c.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region Récupération Favoris
        public List<FavoriEntity> GetAll()
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Favoris WHERE Actif = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<FavoriEntity> L = new List<FavoriEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new FavoriEntity()
                            {
                                Id = (int)Tab["Id"],
                                UtilisateurId = (int)Tab["UtilisateurId"],
                                EnregistrementId = (int)Tab["EnregistrementId"],
                                Actif = (int)Tab["Actif"]
                            });
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération Favoris by Id
        public FavoriEntity GetOne(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Favoris WHERE Id = @Id AND Actif = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            FavoriEntity S = new FavoriEntity()
                            {
                                Id = (int)Tab["Id"],
                                UtilisateurId = (int)Tab["UtilisateurId"],
                                EnregistrementId = (int)Tab["EnregistrementId"],
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

        #region Récupération Favoris by UtilisateurId
        public List<FavoriEntity> GetAllByUtilisateurId(int UtilisateurId)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Favoris WHERE UtilisateurId = @UtilisateurId AND Actif = 1 ";
                    cmd.Parameters.AddWithValue("UtilisateurId", UtilisateurId);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<FavoriEntity> L = new List<FavoriEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new FavoriEntity()
                            {
                                Id = (int)Tab["Id"],
                                UtilisateurId = (int)Tab["UtilisateurId"],
                                EnregistrementId = (int)Tab["EnregistrementId"],
                                Actif = (int)Tab["Actif"],
                            });
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Update Favoris by Id
        public void Update(int Id, FavoriEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.EnregistrementId != 0 && T.UtilisateurId != 0 && Id != 0)
                    {
                        cmd.CommandText = "SP_ModifFavori";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter UtilisateurId = new SqlParameter("UtilisateurId", T.UtilisateurId);
                        SqlParameter EnregistrementId = new SqlParameter("EnregistrementId", T.EnregistrementId);
                        SqlParameter PId = new SqlParameter("Id", Id);
                        cmd.Parameters.Add(UtilisateurId);
                        cmd.Parameters.Add(EnregistrementId);
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