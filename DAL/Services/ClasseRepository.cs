using DAL.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Services
{
    public class ClasseRepository
    {
        #region Add Classe
        public void Create(ClasseEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.NameFR != null && T.NameEN != null)
                    {
                        cmd.CommandText = "SP_AddClasse";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter NameFR = new SqlParameter("NameFR", T.NameFR);
                        SqlParameter NameEN = new SqlParameter("NameEN", T.NameEN);
                        cmd.Parameters.Add(NameFR);
                        cmd.Parameters.Add(NameEN);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Suppression Classe by Id
        public void Delete(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Classes WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    c.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region Récupération Classes
        public List<ClasseEntity> GetAll()
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Classes WHERE Active = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<ClasseEntity> L = new List<ClasseEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new ClasseEntity()
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

        #region Récupération Classe by Id
        public ClasseEntity GetOne(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Classes WHERE Id = @Id AND Active = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            ClasseEntity S = new ClasseEntity()
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

        #region Update Classe by Id
        public void Update(int Id, ClasseEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.NameFR != null && T.NameEN != null && Id != 0)
                    {
                        cmd.CommandText = "SP_UpdateClasse";
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