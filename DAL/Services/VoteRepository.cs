using DAL.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Services
{
    public class VoteRepository
    {
        #region Add Vote
        public void Create(VoteEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.UserId != 0 && T.StrategyId != 0)
                    {
                        cmd.CommandText = "SP_AddVote";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter UserId = new SqlParameter("UserId", T.UserId);
                        SqlParameter StrategyId = new SqlParameter("StrategyId", T.StrategyId);
                        SqlParameter Vote = new SqlParameter("Vote", T.Vote);
                        cmd.Parameters.Add(UserId);
                        cmd.Parameters.Add(StrategyId);
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
                    cmd.CommandText = "SELECT * FROM Votes WHERE Active = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<VoteEntity> L = new List<VoteEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new VoteEntity()
                            {
                                Id = (int)Tab["Id"],
                                StrategyId = (int)Tab["StrategyId"],
                                Vote = (int)Tab["Vote"],
                                UserId = (int)Tab["UserId"],
                                Active = (int)Tab["Active"]
                            });
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération Votes by UserId
        public List<VoteEntity> GetAllbyUserId(int UserId)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Votes WHERE Active = 1 AND UserId = @UserId";
                    cmd.Parameters.AddWithValue("UserId", UserId);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<VoteEntity> L = new List<VoteEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new VoteEntity()
                            {
                                Id = (int)Tab["Id"],
                                StrategyId = (int)Tab["StrategyId"],
                                Vote = (int)Tab["Vote"],
                                UserId = (int)Tab["UserId"],
                                Active = (int)Tab["Active"]
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
                    cmd.CommandText = "SELECT * FROM Votes WHERE Id = @Id AND Active = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            VoteEntity S = new VoteEntity()
                            {
                                Id = (int)Tab["Id"],
                                StrategyId = (int)Tab["StrategyId"],
                                Vote = (int)Tab["Vote"],
                                UserId = (int)Tab["UserId"],
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

        #region Update Vote by Id
        public void Update(int Id, VoteEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T.UserId != 0 && T.StrategyId != 0 && Id != 0 && T.UserId != 0)
                    {
                        cmd.CommandText = "SP_UpdateVote";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter UserId = new SqlParameter("UserId", T.UserId);
                        SqlParameter Vote = new SqlParameter("Vote", T.Vote);
                        SqlParameter StrategyId = new SqlParameter("StrategyId", T.StrategyId);
                        SqlParameter PId = new SqlParameter("Id", Id);
                        cmd.Parameters.Add(UserId);
                        cmd.Parameters.Add(Vote);
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