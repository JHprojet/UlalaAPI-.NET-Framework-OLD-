using DAL.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Services
{
    public class StrategyRepository
    {
        #region Add Strategy
        public void Create(StrategyEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.ImagePath1 != null && T.ImagePath2 != null && T.ImagePath3 != null && T.ImagePath4 != null && T.CharactersConfigurationId != 0 && T.BossZoneId != 0 && T.UserId != 0)
                    {
                        cmd.CommandText = "SP_AddStrategy";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter BossZoneId = new SqlParameter("BossZoneId", T.BossZoneId);
                        SqlParameter CharactersConfigurationId = new SqlParameter("CharactersConfigurationId", T.CharactersConfigurationId);
                        SqlParameter ImagePath1 = new SqlParameter("ImagePath1", T.ImagePath1);
                        SqlParameter ImagePath2 = new SqlParameter("ImagePath2", T.ImagePath2);
                        SqlParameter ImagePath3 = new SqlParameter("ImagePath3", T.ImagePath3);
                        SqlParameter ImagePath4 = new SqlParameter("ImagePath4", T.ImagePath4);
                        SqlParameter UserId = new SqlParameter("UserId", T.UserId);
                        cmd.Parameters.Add(BossZoneId);
                        cmd.Parameters.Add(CharactersConfigurationId);
                        cmd.Parameters.Add(ImagePath1);
                        cmd.Parameters.Add(ImagePath2);
                        cmd.Parameters.Add(ImagePath3);
                        cmd.Parameters.Add(ImagePath4);
                        cmd.Parameters.Add(UserId);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Suppression Strategy by Id
        public void Delete(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Strategies WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    c.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region Récupération Strategys
        public List<StrategyEntity> GetAll()
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Strategies WHERE Active = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<StrategyEntity> L = new List<StrategyEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new StrategyEntity()
                            {
                                Id = (int)Tab["Id"],
                                BossZoneId = (int)Tab["BossZoneId"],
                                CharactersConfigurationId = (int)Tab["CharactersConfigurationId"],
                                ImagePath1 = Tab["ImagePath1"].ToString(),
                                ImagePath2 = Tab["ImagePath2"].ToString(),
                                ImagePath3 = Tab["ImagePath3"].ToString(),
                                ImagePath4 = Tab["ImagePath4"].ToString(),
                                UserId = (int)Tab["UserId"],
                                Note = (int)Tab["Note"],
                                Active = (int)Tab["Active"]
                            });
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération Strategy by Id
        public StrategyEntity GetOne(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Strategies WHERE Id = @Id AND Active = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            StrategyEntity S = new StrategyEntity()
                            {
                                Id = (int)Tab["Id"],
                                BossZoneId = (int)Tab["BossZoneId"],
                                CharactersConfigurationId = (int)Tab["CharactersConfigurationId"],
                                ImagePath1 = Tab["ImagePath1"].ToString(),
                                ImagePath2 = Tab["ImagePath2"].ToString(),
                                ImagePath3 = Tab["ImagePath3"].ToString(),
                                ImagePath4 = Tab["ImagePath4"].ToString(),
                                UserId = (int)Tab["UserId"],
                                Note = (int)Tab["Note"],
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

        #region Update Strategy by Id
        public void Update(int Id, StrategyEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.ImagePath1 != null && T.ImagePath2 != null && T.ImagePath3 != null && T.ImagePath4 != null && T.CharactersConfigurationId != 0 && T.BossZoneId != 0 && T.UserId != 0 && Id != 0)
                    { 
                        cmd.CommandText = "SP_UpdateStrategy";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter BossZoneId = new SqlParameter("BossZoneId", T.BossZoneId);
                        SqlParameter CharactersConfigurationId = new SqlParameter("CharactersConfigurationId", T.CharactersConfigurationId);
                        SqlParameter ImagePath1 = new SqlParameter("ImagePath1", T.ImagePath1);
                        SqlParameter ImagePath2 = new SqlParameter("ImagePath2", T.ImagePath2);
                        SqlParameter ImagePath3 = new SqlParameter("ImagePath3", T.ImagePath3);
                        SqlParameter ImagePath4 = new SqlParameter("ImagePath4", T.ImagePath4);
                        SqlParameter Note = new SqlParameter("Note", T.Note);
                        SqlParameter UserId = new SqlParameter("UserId", T.UserId);
                        SqlParameter PId = new SqlParameter("Id", Id);
                        cmd.Parameters.Add(BossZoneId);
                        cmd.Parameters.Add(CharactersConfigurationId);
                        cmd.Parameters.Add(ImagePath1);
                        cmd.Parameters.Add(ImagePath2);
                        cmd.Parameters.Add(ImagePath3);
                        cmd.Parameters.Add(ImagePath4);
                        cmd.Parameters.Add(Note);
                        cmd.Parameters.Add(UserId);
                        cmd.Parameters.Add(PId);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Récupération Strategys en fonction de plusieurs éléments facultatifs
        public List<StrategyEntity> GetAllByInfos(int? U, int? BZ, int? C1, int? C2, int? C3, int? C4)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    string sU = "";
                    string sBZ = "";
                    string sC1 = "";
                    string sC2 = "";
                    string sC3 = "";
                    string sC4 = "";
                    if (U != null) sU= " AND UserId = "+U ;
                    if (BZ != null) sBZ = " AND BossZoneId = "+BZ;
                    if (C1 != null) sC1 = $" AND (T.ClasseId1 = {C1} OR T.ClasseId2 = {C1} OR T.ClasseId3 = {C1} OR T.ClasseId4 = {C1})";
                    if (C2 != null) sC2 = $" AND (T.ClasseId1 = {C2} OR T.ClasseId2 = {C2} OR T.ClasseId3 = {C2} OR T.ClasseId4 = {C2})";
                    if (C3 != null) sC3 = $" AND (T.ClasseId1 = {C3} OR T.ClasseId2 = {C3} OR T.ClasseId3 = {C3} OR T.ClasseId4 = {C3})";
                    if (C4 != null) sC4 = $" AND (T.ClasseId1 = {C4} OR T.ClasseId2 = {C4} OR T.ClasseId3 = {C4} OR T.ClasseId4 = {C4})";
                    cmd.CommandText = "SELECT * FROM Strategies E JOIN CharactersConfigurations T ON E.CharactersConfigurationId = T.Id WHERE E.Active = 1" + sU+sBZ+sC1+sC2+sC3+sC4+" ORDER BY Note DESC";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<StrategyEntity> L = new List<StrategyEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new StrategyEntity()
                            {
                                Id = (int)Tab["Id"],
                                BossZoneId = (int)Tab["BossZoneId"],
                                CharactersConfigurationId = (int)Tab["CharactersConfigurationId"],
                                ImagePath1 = Tab["ImagePath1"].ToString(),
                                ImagePath2 = Tab["ImagePath2"].ToString(),
                                ImagePath3 = Tab["ImagePath3"].ToString(),
                                ImagePath4 = Tab["ImagePath4"].ToString(),
                                UserId = (int)Tab["UserId"],
                                Note = (int)Tab["Note"],
                                Active = (int)Tab["Active"],
                            });
                        }
                        return L;
                    }
                }
            }
        }
        #endregion
    }
}