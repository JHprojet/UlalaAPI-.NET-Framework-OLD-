using DAL.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Services
{
    public class MesTeamsRepository
    {
        #region Ajout MesTeams
        public void Create(MesTeamsEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.NomTeam != null && T.TeamId != 0 && T.UtilisateurId != 0 && T.ZoneId != 0)
                    {
                        cmd.CommandText = "SP_AjoutMaTeam";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter TeamId = new SqlParameter("TeamId", T.TeamId);
                        SqlParameter NomTeam = new SqlParameter("NomTeam", T.NomTeam);
                        SqlParameter UtilisateurId = new SqlParameter("UtilisateurId", T.UtilisateurId);
                        SqlParameter ZoneId = new SqlParameter("ZoneId", T.ZoneId);
                        cmd.Parameters.Add(TeamId);
                        cmd.Parameters.Add(NomTeam);
                        cmd.Parameters.Add(UtilisateurId);
                        cmd.Parameters.Add(ZoneId);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Suppression Team by Id
        public void Delete(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM MesTeams WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    c.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region Récupération Team
        public List<MesTeamsEntity> GetAll()
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM MesTeams WHERE Actif = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<MesTeamsEntity> L = new List<MesTeamsEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new MesTeamsEntity()
                            {
                                Id = (int)Tab["Id"],
                                TeamId = (int)Tab["TeamId"],
                                UtilisateurId = (int)Tab["UtilisateurId"],
                                NomTeam = Tab["NomTeam"].ToString(),
                                ZoneId = (int)Tab["ZoneId"],
                                Actif = (int)Tab["Actif"]
                            }) ;
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération Teams pour un utilisateur
        public List<MesTeamsEntity> GetAll(int UtilisateurId)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM MesTeams WHERE UtilisateurId = @UtilisateurId AND Actif = 1";
                    cmd.Parameters.AddWithValue("UtilisateurId", UtilisateurId);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<MesTeamsEntity> L = new List<MesTeamsEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new MesTeamsEntity()
                            {
                                Id = (int)Tab["Id"],
                                TeamId = (int)Tab["TeamId"],
                                UtilisateurId = (int)Tab["UtilisateurId"],
                                NomTeam = Tab["NomTeam"].ToString(),
                                ZoneId = (int)Tab["ZoneId"],
                                Actif = (int)Tab["Actif"]
                            });
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération Team by Id
        public MesTeamsEntity GetOne(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM MesTeams WHERE Id = @Id AND Actif = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            MesTeamsEntity S = new MesTeamsEntity()
                            {
                                Id = (int)Tab["Id"],
                                TeamId = (int)Tab["TeamId"],
                                UtilisateurId = (int)Tab["UtilisateurId"],
                                NomTeam = Tab["NomTeam"].ToString(),
                                ZoneId = (int)Tab["ZoneId"],
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

        #region Update Team by Id
        public void Update(int id, MesTeamsEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.NomTeam != null && T.TeamId != 0 && T.UtilisateurId != 0 && T.ZoneId != 0 && id != 0)
                    {
                        cmd.CommandText = "SP_ModifMaTeam";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter TeamId = new SqlParameter("TeamId", T.TeamId);
                        SqlParameter UtilisateurId = new SqlParameter("UtilisateurId", T.UtilisateurId);
                        SqlParameter NomTeam = new SqlParameter("NomTeam", T.NomTeam);
                        SqlParameter ZoneId = new SqlParameter("ZoneId", T.ZoneId);
                        SqlParameter PId = new SqlParameter("Id", id);
                        cmd.Parameters.Add(TeamId);
                        cmd.Parameters.Add(UtilisateurId);
                        cmd.Parameters.Add(NomTeam);
                        cmd.Parameters.Add(ZoneId);
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