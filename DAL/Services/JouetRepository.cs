using DAL.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Services
{
    public class ToyRepository
    {
        #region Add Toy
        public void Create(ToyEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.NameEN != null && T.NameFR != null && T.ImagePath != null)
                    {
                        cmd.CommandText = "SP_AddToy";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter NameEN = new SqlParameter("NameEN", T.NameEN);
                        SqlParameter NameFR = new SqlParameter("NameFR", T.NameFR);
                        SqlParameter ImagePath = new SqlParameter("ImagePath", T.ImagePath);
                        cmd.Parameters.Add(NameEN);
                        cmd.Parameters.Add(NameFR);
                        cmd.Parameters.Add(ImagePath);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Suppression Toy by Id
        public void Delete(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Toys WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    c.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region Récupération Toys
        public List<ToyEntity> GetAll()
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Toys WHERE Active = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<ToyEntity> L = new List<ToyEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new ToyEntity()
                            {
                                Id = (int)Tab["Id"],
                                NameEN = Tab["NameEN"].ToString(),
                                NameFR = Tab["NameFR"].ToString(),
                                ImagePath = Tab["ImagePath"].ToString(),
                                Active = (int)Tab["Active"]
                            });
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération Toy by Id
        public ToyEntity GetOne(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Toys WHERE Id = @Id AND Active = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            ToyEntity S = new ToyEntity()
                            {
                                Id = (int)Tab["Id"],
                                NameEN = Tab["NameEN"].ToString(),
                                NameFR = Tab["NameFR"].ToString(),
                                ImagePath = Tab["ImagePath"].ToString(),
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

        #region Update Toy by Id
        public void Update(int Id, ToyEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.NameEN != null && T.NameFR != null && T.ImagePath != null && Id != 0)
                    {
                        cmd.CommandText = "SP_UpdateToy";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter NameEN = new SqlParameter("NameEN", T.NameEN);
                        SqlParameter NameFR = new SqlParameter("NameFR", T.NameFR);
                        SqlParameter ImagePath = new SqlParameter("ImagePath", T.ImagePath);
                        SqlParameter PId = new SqlParameter("Id", Id);
                        cmd.Parameters.Add(NameFR);
                        cmd.Parameters.Add(NameEN);
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