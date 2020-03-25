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
    public class BossZoneRepository
    {
        #region Ajout BossZone
        public void Create(BossZoneEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.BossId != 0 && T.ZoneId != 0)
                    {
                        cmd.CommandText = "SP_AjoutBossZone";
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
                    cmd.CommandText = "DELETE FROM BossZones WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    c.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region Récupération BossZones
        public List<BossZoneEntity> GetAll()
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM BossZones WHERE Actif = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<BossZoneEntity> L = new List<BossZoneEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new BossZoneEntity()
                            {
                                Id = (int)Tab["Id"],
                                BossId = (int)Tab["BossId"],
                                ZoneId = (int)Tab["ZoneId"],
                                Actif = (int)Tab["Actif"],
                            });
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération BossZone by Id
        public BossZoneEntity GetOne(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM BossZones WHERE Id = @Id AND Actif = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            BossZoneEntity S = new BossZoneEntity()
                            {
                                Id = (int)Tab["Id"],
                                BossId = (int)Tab["BossId"],
                                ZoneId = (int)Tab["ZoneId"],
                                Actif = (int)Tab["Id"]
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
        public void Update(int Id, BossZoneEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.BossId != 0 && T.ZoneId != 0 && Id != 0)
                    {
                        cmd.CommandText = "SP_ModifBossZone";
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