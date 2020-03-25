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
    public class ClasseRepository
    {
        #region Ajout Classe
        public void Create(ClasseEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.NomFR != null && T.NomEN != null)
                    {
                        cmd.CommandText = "SP_AjoutClasse";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter NomFR = new SqlParameter("NomFR", T.NomFR);
                        SqlParameter NomEN = new SqlParameter("NomEN", T.NomEN);
                        cmd.Parameters.Add(NomFR);
                        cmd.Parameters.Add(NomEN);
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
                    cmd.CommandText = "SELECT * FROM Classes WHERE Actif = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<ClasseEntity> L = new List<ClasseEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new ClasseEntity()
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

        #region Récupération Classe by Id
        public ClasseEntity GetOne(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Classes WHERE Id = @Id AND Actif = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            ClasseEntity S = new ClasseEntity()
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

        #region Update Classe by Id
        public void Update(int Id, ClasseEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.NomFR != null && T.NomEN != null && Id != 0)
                    {
                        cmd.CommandText = "SP_ModifClasse";
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