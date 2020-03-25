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
    public class VoteRepository
    {
        #region Ajout Vote
        public void Create(VoteEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.UtilisateurId != 0 && T.EnregistrementId != 0) ;
                    else
                    {
                        cmd.CommandText = "SP_AjoutVote";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter UtilisateurId = new SqlParameter("UtilisateurId", T.UtilisateurId);
                        SqlParameter EnregistrementId = new SqlParameter("EnregistrementId", T.EnregistrementId);
                        SqlParameter Vote = new SqlParameter("Vote", T.Vote);
                        cmd.Parameters.Add(UtilisateurId);
                        cmd.Parameters.Add(EnregistrementId);
                        cmd.Parameters.Add(Vote);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Suppression Vote by Id
        public void Delete(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Votes WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    c.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region Récupération Votes
        public List<VoteEntity> GetAll()
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Votes WHERE Actif = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<VoteEntity> L = new List<VoteEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new VoteEntity()
                            {
                                Id = (int)Tab["Id"],
                                EnregistrementId = (int)Tab["EnregistrementId"],
                                Vote = (int)Tab["Vote"],
                                UtilisateurId = (int)Tab["UtilisateurId"],
                                Actif = (int)Tab["Actif"]
                            });
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération Vote by Id
        public VoteEntity GetOne(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Votes WHERE Id = @Id AND Actif = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            VoteEntity S = new VoteEntity()
                            {
                                Id = (int)Tab["Id"],
                                EnregistrementId = (int)Tab["EnregistrementId"],
                                Vote = (int)Tab["Vote"],
                                UtilisateurId = (int)Tab["UtilisateurId"],
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

        #region Update Vote by Id
        public void Update(int Id, VoteEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T.UtilisateurId != 0 && T.EnregistrementId != 0 && Id != 0 && T.UtilisateurId != 0)
                    {
                        cmd.CommandText = "SP_ModifVote";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter UtilisateurId = new SqlParameter("UtilisateurId", T.UtilisateurId);
                        SqlParameter Vote = new SqlParameter("Vote", T.Vote);
                        SqlParameter EnregistrementId = new SqlParameter("EnregistrementId", T.EnregistrementId);
                        SqlParameter PId = new SqlParameter("Id", Id);
                        cmd.Parameters.Add(UtilisateurId);
                        cmd.Parameters.Add(Vote);
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