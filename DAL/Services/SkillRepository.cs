using DAL.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Services
{
    public class SkillRepository
    {
        #region Ajout Skill
        public void Create(SkillEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.ImagePath != null && T.NomEN != null && T.NomFR != null)
                    {
                        cmd.CommandText = "SP_AjoutSkill";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter NomEN = new SqlParameter("NomEN", T.NomEN);
                        SqlParameter NomFR = new SqlParameter("NomFR", T.NomFR);
                        SqlParameter ImagePath = new SqlParameter("ImagePath", T.ImagePath);
                        SqlParameter ClasseId = new SqlParameter("ClasseId", T.ClasseId);
                        cmd.Parameters.Add(NomEN);
                        cmd.Parameters.Add(NomFR);
                        cmd.Parameters.Add(ImagePath);
                        cmd.Parameters.Add(ClasseId);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Suppression Skill by Id
        public void Delete(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Skills WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    c.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region Récupération Skills
        public List<SkillEntity> GetAll()
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM SKills WHERE Actif = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<SkillEntity> L = new List<SkillEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new SkillEntity()
                            {
                                Id = (int)Tab["Id"],
                                NomEN = Tab["NomEN"].ToString(),
                                NomFR = Tab["NomFR"].ToString(),
                                ImagePath = Tab["ImagePath"].ToString(),
                                ClasseId = (int)Tab["ClasseId"],
                                Actif = (int)Tab["Actif"]
                            });
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération Skills by Classe
        public List<SkillEntity> GetAll(int ClasseId)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Skills WHERE Actif = 1 AND ClasseId = @ClasseId";
                    cmd.Parameters.AddWithValue("ClasseId", ClasseId);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<SkillEntity> L = new List<SkillEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new SkillEntity()
                            {
                                Id = (int)Tab["Id"],
                                NomEN = Tab["NomEN"].ToString(),
                                NomFR = Tab["NomFR"].ToString(),
                                ImagePath = Tab["ImagePath"].ToString(),
                                ClasseId = (int)Tab["ClasseId"],
                                Actif = (int)Tab["Actif"]
                            });
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération Skill by Id
        public SkillEntity GetOne(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Skills WHERE Id = @Id AND Actif = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            SkillEntity S = new SkillEntity()
                            {
                                Id = (int)Tab["Id"],
                                NomEN = Tab["NomEN"].ToString(),
                                NomFR = Tab["NomFR"].ToString(),
                                ImagePath = Tab["ImagePath"].ToString(),
                                ClasseId = (int)Tab["ClasseId"],
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

        #region Update Skill by Id
        public void Update(int Id, SkillEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.ImagePath != null && T.NomEN != null && T.NomFR != null && Id != 0)
                    {
                        cmd.CommandText = "SP_ModifSkill";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter NomEN = new SqlParameter("NomEN", T.NomEN);
                        SqlParameter NomFR = new SqlParameter("NomFR", T.NomFR);
                        SqlParameter ImagePath = new SqlParameter("ImagePath", T.ImagePath);
                        SqlParameter ClasseId = new SqlParameter("ClasseId", T.ClasseId);
                        SqlParameter PId = new SqlParameter("Id", Id);
                        cmd.Parameters.Add(NomFR);
                        cmd.Parameters.Add(NomEN);
                        cmd.Parameters.Add(ImagePath);
                        cmd.Parameters.Add(ClasseId);
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