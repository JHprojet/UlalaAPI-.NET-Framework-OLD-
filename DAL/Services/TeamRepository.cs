using DAL.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Services
{
    public class TeamRepository
    {
        #region Ajout Team
        public void Create(TeamEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T.ClasseId1 != 0 && T.ClasseId2 != 0 && T.ClasseId3 != 0 && T.ClasseId4 != 0)
                    {
                        cmd.CommandText = "SP_AjoutTeam";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter ClasseId1 = new SqlParameter("ClasseId1", T.ClasseId1);
                        SqlParameter ClasseId2 = new SqlParameter("ClasseId2", T.ClasseId2);
                        SqlParameter ClasseId3 = new SqlParameter("ClasseId3", T.ClasseId3);
                        SqlParameter ClasseId4 = new SqlParameter("ClasseId4", T.ClasseId4);
                        cmd.Parameters.Add(ClasseId1);
                        cmd.Parameters.Add(ClasseId2);
                        cmd.Parameters.Add(ClasseId3);
                        cmd.Parameters.Add(ClasseId4);
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
                    cmd.CommandText = "DELETE FROM Teams WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    c.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region Récupération Teams
        public List<TeamEntity> GetAll()
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Teams WHERE Actif = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<TeamEntity> L = new List<TeamEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new TeamEntity()
                            {
                                Id = (int)Tab["Id"],
                                ClasseId1 = (int)Tab["ClasseId1"],
                                ClasseId2 = (int)Tab["ClasseId2"],
                                ClasseId3 = (int)Tab["ClasseId3"],
                                ClasseId4 = (int)Tab["ClasseId4"],
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
        public TeamEntity GetOne(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Teams WHERE Id = @Id AND Actif = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            TeamEntity S = new TeamEntity()
                            {
                                Id = (int)Tab["Id"],
                                ClasseId1 = (int)Tab["ClasseId1"],
                                ClasseId2 = (int)Tab["ClasseId2"],
                                ClasseId3 = (int)Tab["ClasseId3"],
                                ClasseId4 = (int)Tab["ClasseId4"],
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
        public void Update(int Id, TeamEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T.ClasseId1 != 0 && T.ClasseId2 != 0 && T.ClasseId3 != 0 && T.ClasseId4 != 0 && Id != 0)
                    {
                        cmd.CommandText = "SP_ModifTeam";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter PId = new SqlParameter("Id", T.Id);
                        SqlParameter ClasseId1 = new SqlParameter("ClasseId1", T.ClasseId1);
                        SqlParameter ClasseId2 = new SqlParameter("ClasseId2", T.ClasseId2);
                        SqlParameter ClasseId3 = new SqlParameter("ClasseId3", T.ClasseId3);
                        SqlParameter ClasseId4 = new SqlParameter("ClasseId4", T.ClasseId4);
                        cmd.Parameters.Add(ClasseId1);
                        cmd.Parameters.Add(ClasseId2);
                        cmd.Parameters.Add(ClasseId3);
                        cmd.Parameters.Add(ClasseId4);
                        cmd.Parameters.Add(PId);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Récupération Team by Id
        public TeamEntity GetTeamByClasses(int C1, int C2, int C3, int C4)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    string sC1 = "";
                    string sC2 = "";
                    string sC3 = "";
                    string sC4 = "";
                    if (C1 != null) sC1 = $" AND (ClasseId1 = {C1} OR ClasseId2 = {C1} OR ClasseId3 = {C1} OR ClasseId4 = {C1})";
                    if (C2 != null) sC2 = $" AND (ClasseId1 = {C2} OR ClasseId2 = {C2} OR ClasseId3 = {C2} OR ClasseId4 = {C2})";
                    if (C3 != null) sC3 = $" AND (ClasseId1 = {C3} OR ClasseId2 = {C3} OR ClasseId3 = {C3} OR ClasseId4 = {C3})";
                    if (C4 != null) sC4 = $" AND (ClasseId1 = {C4} OR ClasseId2 = {C4} OR ClasseId3 = {C4} OR ClasseId4 = {C4})";

                    cmd.CommandText = "SELECT * FROM Teams WHERE Actif = 1" + sC1 + sC2 + sC3 + sC4;

                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            TeamEntity S = new TeamEntity()
                            {
                                Id = (int)Tab["Id"],
                                ClasseId1 = (int)Tab["ClasseId1"],
                                ClasseId2 = (int)Tab["ClasseId2"],
                                ClasseId3 = (int)Tab["ClasseId3"],
                                ClasseId4 = (int)Tab["ClasseId4"],
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
    }
}
