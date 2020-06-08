using DAL.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Services
{
    public class SkillRepository
    {
        #region Add Skill
        public void Create(SkillEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.ImagePath != null && T.NameEN != null && T.NameFR != null)
                    {
                        cmd.CommandText = "SP_AddSkill";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter NameEN = new SqlParameter("NameEN", T.NameEN);
                        SqlParameter NameFR = new SqlParameter("NameFR", T.NameFR);
                        SqlParameter DescriptionFR = new SqlParameter("DescriptionFR", T.DescriptionFR);
                        SqlParameter DescriptionEN = new SqlParameter("DescriptionEN", T.DescriptionEN);
                        SqlParameter Location = new SqlParameter("Location", T.Location);
                        SqlParameter Cost = new SqlParameter("Cost", T.Cost);
                        SqlParameter ImagePath = new SqlParameter("ImagePath", T.ImagePath);
                        SqlParameter ClasseId = new SqlParameter("ClasseId", T.ClasseId);
                        cmd.Parameters.Add(NameEN);
                        cmd.Parameters.Add(NameFR);
                        cmd.Parameters.Add(DescriptionFR);
                        cmd.Parameters.Add(DescriptionEN);
                        cmd.Parameters.Add(Location);
                        cmd.Parameters.Add(Cost);
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
                    cmd.CommandText = "SELECT * FROM SKills WHERE Active = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<SkillEntity> L = new List<SkillEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new SkillEntity()
                            {
                                Id = (int)Tab["Id"],
                                NameEN = Tab["NameEN"].ToString(),
                                NameFR = Tab["NameFR"].ToString(),
                                DescriptionFR = Tab["DescriptionFR"].ToString(),
                                DescriptionEN = Tab["DescriptionEN"].ToString(),
                                Location = Tab["Location"].ToString(),
                                Cost = (int)Tab["Cost"],
                                ImagePath = Tab["ImagePath"].ToString(),
                                ClasseId = (int)Tab["ClasseId"],
                                Active = (int)Tab["Active"]
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
                    cmd.CommandText = "SELECT * FROM Skills WHERE Active = 1 AND ClasseId = @ClasseId";
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
                                NameEN = Tab["NameEN"].ToString(),
                                NameFR = Tab["NameFR"].ToString(),
                                DescriptionFR = Tab["DescriptionFR"].ToString(),
                                DescriptionEN = Tab["DescriptionEN"].ToString(),
                                Location = Tab["Location"].ToString(),
                                Cost = (int)Tab["Cost"],
                                ImagePath = Tab["ImagePath"].ToString(),
                                ClasseId = (int)Tab["ClasseId"],
                                Active = (int)Tab["Active"]
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
                    cmd.CommandText = "SELECT * FROM Skills WHERE Id = @Id AND Active = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            SkillEntity S = new SkillEntity()
                            {
                                Id = (int)Tab["Id"],
                                NameEN = Tab["NameEN"].ToString(),
                                NameFR = Tab["NameFR"].ToString(),
                                DescriptionFR = Tab["DescriptionFR"].ToString(),
                                DescriptionEN = Tab["DescriptionEN"].ToString(),
                                Location = Tab["Location"].ToString(),
                                Cost = (int)Tab["Cost"],
                                ImagePath = Tab["ImagePath"].ToString(),
                                ClasseId = (int)Tab["ClasseId"],
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

        #region Update Skill by Id
        public void Update(int Id, SkillEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.ImagePath != null && T.NameEN != null && T.NameFR != null && Id != 0)
                    {
                        cmd.CommandText = "SP_UpdateSkill";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter NameEN = new SqlParameter("NameEN", T.NameEN);
                        SqlParameter NameFR = new SqlParameter("NameFR", T.NameFR);
                        SqlParameter DescriptionFR = new SqlParameter("DescriptionFR", T.DescriptionFR);
                        SqlParameter DescriptionEN = new SqlParameter("DescriptionEN", T.DescriptionEN);
                        SqlParameter Location = new SqlParameter("Location", T.Location);
                        SqlParameter Cost = new SqlParameter("Cost", T.Cost);
                        SqlParameter ImagePath = new SqlParameter("ImagePath", T.ImagePath);
                        SqlParameter ClasseId = new SqlParameter("ClasseId", T.ClasseId);
                        SqlParameter PId = new SqlParameter("Id", Id);
                        cmd.Parameters.Add(NameFR);
                        cmd.Parameters.Add(NameEN);
                        cmd.Parameters.Add(DescriptionFR);
                        cmd.Parameters.Add(DescriptionEN);
                        cmd.Parameters.Add(Location);
                        cmd.Parameters.Add(Cost);
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