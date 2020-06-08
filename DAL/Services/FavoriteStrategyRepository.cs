using DAL.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Services
{
    public class FavoriteStrategyRepository
    {
        #region Add Favoris
        public void Create(FavoriteStrategyEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.StrategyId != 0 && T.UserId != 0)
                    {
                        cmd.CommandText = "SP_AddFavoriteStrategy";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter StrategyId = new SqlParameter("StrategyId", T.StrategyId);
                        SqlParameter UserId = new SqlParameter("UserId", T.UserId);
                        cmd.Parameters.Add(StrategyId);
                        cmd.Parameters.Add(UserId);
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
                    cmd.CommandText = "DELETE FROM FavoriteStrategies WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    c.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region Récupération Favoris
        public List<FavoriteStrategyEntity> GetAll()
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM FavoriteStrategies WHERE Active = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<FavoriteStrategyEntity> L = new List<FavoriteStrategyEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new FavoriteStrategyEntity()
                            {
                                Id = (int)Tab["Id"],
                                UserId = (int)Tab["UserId"],
                                StrategyId = (int)Tab["StrategyId"],
                                Active = (int)Tab["Active"]
                            });
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération Favoris by Id
        public FavoriteStrategyEntity GetOne(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM FavoriteStrategies WHERE Id = @Id AND Active = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            FavoriteStrategyEntity S = new FavoriteStrategyEntity()
                            {
                                Id = (int)Tab["Id"],
                                UserId = (int)Tab["UserId"],
                                StrategyId = (int)Tab["StrategyId"],
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

        #region Récupération Favoris by UserId
        public List<FavoriteStrategyEntity> GetAllByUserId(int UserId)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM FavoriteStrategies WHERE UserId = @UserId AND Active = 1 ";
                    cmd.Parameters.AddWithValue("UserId", UserId);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<FavoriteStrategyEntity> L = new List<FavoriteStrategyEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new FavoriteStrategyEntity()
                            {
                                Id = (int)Tab["Id"],
                                UserId = (int)Tab["UserId"],
                                StrategyId = (int)Tab["StrategyId"],
                                Active = (int)Tab["Active"],
                            });
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Update Favoris by Id
        public void Update(int Id, FavoriteStrategyEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.StrategyId != 0 && T.UserId != 0 && Id != 0)
                    {
                        cmd.CommandText = "SP_UpdateFavoriteStrategy";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter UserId = new SqlParameter("UserId", T.UserId);
                        SqlParameter StrategyId = new SqlParameter("StrategyId", T.StrategyId);
                        SqlParameter PId = new SqlParameter("Id", Id);
                        cmd.Parameters.Add(UserId);
                        cmd.Parameters.Add(StrategyId);
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