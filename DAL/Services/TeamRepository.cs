using DAL.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Services
{
    public class TeamRepository
    {
        #region Add MesCharactersConfigurations
        public void Create(TeamEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.TeamName != null && T.CharactersConfigurationId != 0 && T.UserId != 0 && T.ZoneId != 0)
                    {
                        cmd.CommandText = "SP_AddTeam";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter CharactersConfigurationId = new SqlParameter("CharactersConfigurationId", T.CharactersConfigurationId);
                        SqlParameter TeamName = new SqlParameter("TeamName", T.TeamName);
                        SqlParameter UserId = new SqlParameter("UserId", T.UserId);
                        SqlParameter ZoneId = new SqlParameter("ZoneId", T.ZoneId);
                        cmd.Parameters.Add(CharactersConfigurationId);
                        cmd.Parameters.Add(TeamName);
                        cmd.Parameters.Add(UserId);
                        cmd.Parameters.Add(ZoneId);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Suppression CharactersConfiguration by Id
        public void Delete(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Teams WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    c.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region Récupération CharactersConfiguration
        public List<TeamEntity> GetAll()
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Teams WHERE Active = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<TeamEntity> L = new List<TeamEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new TeamEntity()
                            {
                                Id = (int)Tab["Id"],
                                CharactersConfigurationId = (int)Tab["CharactersConfigurationId"],
                                UserId = (int)Tab["UserId"],
                                TeamName = Tab["TeamName"].ToString(),
                                ZoneId = (int)Tab["ZoneId"],
                                Active = (int)Tab["Active"]
                            }) ;
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération CharactersConfigurations pour un User
        public List<TeamEntity> GetAll(int UserId)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Teams WHERE UserId = @UserId AND Active = 1";
                    cmd.Parameters.AddWithValue("UserId", UserId);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<TeamEntity> L = new List<TeamEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new TeamEntity()
                            {
                                Id = (int)Tab["Id"],
                                CharactersConfigurationId = (int)Tab["CharactersConfigurationId"],
                                UserId = (int)Tab["UserId"],
                                TeamName = Tab["TeamName"].ToString(),
                                ZoneId = (int)Tab["ZoneId"],
                                Active = (int)Tab["Active"]
                            });
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération CharactersConfiguration by Id
        public TeamEntity GetOne(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Teams WHERE Id = @Id AND Active = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            TeamEntity S = new TeamEntity()
                            {
                                Id = (int)Tab["Id"],
                                CharactersConfigurationId = (int)Tab["CharactersConfigurationId"],
                                UserId = (int)Tab["UserId"],
                                TeamName = Tab["TeamName"].ToString(),
                                ZoneId = (int)Tab["ZoneId"],
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

        #region Update CharactersConfiguration by Id
        public void Update(int id, TeamEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.TeamName != null && T.CharactersConfigurationId != 0 && T.UserId != 0 && T.ZoneId != 0 && id != 0)
                    {
                        cmd.CommandText = "SP_UpdateTeam";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter CharactersConfigurationId = new SqlParameter("CharactersConfigurationId", T.CharactersConfigurationId);
                        SqlParameter UserId = new SqlParameter("UserId", T.UserId);
                        SqlParameter TeamName = new SqlParameter("TeamName", T.TeamName);
                        SqlParameter ZoneId = new SqlParameter("ZoneId", T.ZoneId);
                        SqlParameter PId = new SqlParameter("Id", id);
                        cmd.Parameters.Add(CharactersConfigurationId);
                        cmd.Parameters.Add(UserId);
                        cmd.Parameters.Add(TeamName);
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