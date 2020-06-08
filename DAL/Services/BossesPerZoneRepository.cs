using DAL.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Services
{
    public class BossesPerZoneRepository
    {
        #region Add BossZone
        public void Create(BossesZoneEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.BossId != 0 && T.ZoneId != 0)
                    {
                        cmd.CommandText = "SP_AddBossZone";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter BossId = new SqlParameter("BossId", T.BossId);
                        SqlParameter ZoneId = new SqlParameter("ZoneId", T.ZoneId);
                        cmd.Parameters.Add(BossId);
                        cmd.Parameters.Add(ZoneId);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Suppression BossZone by Id
        public void Delete(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM BossesPerZones WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    c.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region Récupération BossZones
        public List<BossesZoneEntity> GetAll()
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM BossesPerZones WHERE Active = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<BossesZoneEntity> L = new List<BossesZoneEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new BossesZoneEntity()
                            {
                                Id = (int)Tab["Id"],
                                BossId = (int)Tab["BossId"],
                                ZoneId = (int)Tab["ZoneId"],
                                Active = (int)Tab["Active"],
                            });
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération BossZone by Id
        public BossesZoneEntity GetOne(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM BossesPerZones WHERE Id = @Id AND Active = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            BossesZoneEntity S = new BossesZoneEntity()
                            {
                                Id = (int)Tab["Id"],
                                BossId = (int)Tab["BossId"],
                                ZoneId = (int)Tab["ZoneId"],
                                Active = (int)Tab["Id"]
                            };
                            return S;
                        }
                        else return null;
                    }
                }
            }
        }
        #endregion

        #region Update BossZone by Id
        public void Update(int Id, BossesZoneEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.BossId != 0 && T.ZoneId != 0 && Id != 0)
                    {
                        cmd.CommandText = "SP_UpdateBossZone";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter BossId = new SqlParameter("BossId", T.BossId);
                        SqlParameter ZoneId = new SqlParameter("ZoneId", T.ZoneId);
                        SqlParameter PId = new SqlParameter("Id", Id);
                        cmd.Parameters.Add(BossId);
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