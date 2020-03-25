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
    public class ZoneRepository
    {
        #region Ajout Zone
        public void Create(ZoneEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T.ContinentFR != null && T.ContinentEN != null && T.ZoneFR != null && T.ZoneEN != null && T.NbZones != 0)
                    {
                        cmd.CommandText = "SP_AjoutZone";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter ContinentEN = new SqlParameter("ContinentEN", T.ContinentEN);
                        SqlParameter ContinentFR = new SqlParameter("ContinentFR", T.ContinentFR);
                        SqlParameter ZoneEN = new SqlParameter("ZoneEN", T.ZoneEN);
                        SqlParameter ZoneFR = new SqlParameter("ZoneFR", T.ZoneFR);
                        SqlParameter NbZones = new SqlParameter("NbZones", T.NbZones);
                        cmd.Parameters.Add(ContinentEN);
                        cmd.Parameters.Add(ContinentFR);
                        cmd.Parameters.Add(ZoneEN);
                        cmd.Parameters.Add(ZoneFR);
                        cmd.Parameters.Add(NbZones);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Suppression Zone by Id
        public void Delete(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Zones WHERE Id = @Id AND Actif = 1";
                    cmd.Parameters.AddWithValue("@Id", id);
                    c.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region Récupération Zones
        public List<ZoneEntity> GetAll()
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Zones WHERE Actif = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<ZoneEntity> L = new List<ZoneEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new ZoneEntity()
                            {
                                Id = (int)Tab["Id"],
                                ContinentEN = Tab["ContinentEN"].ToString(),
                                ContinentFR = Tab["ContinentFR"].ToString(),
                                ZoneEN = Tab["ZoneEN"].ToString(),
                                ZoneFR = Tab["ZoneFR"].ToString(),
                                NbZones = (int)Tab["NbZones"],
                                Actif = (int)Tab["Actif"]
                            });
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération Zone by Id
        public ZoneEntity GetOne(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Zones WHERE Id = @Id AND Actif = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            ZoneEntity S = new ZoneEntity()
                            {
                                Id = (int)Tab["Id"],
                                ContinentEN = Tab["ContinentEN"].ToString(),
                                ContinentFR = Tab["ContinentFR"].ToString(),
                                ZoneEN = Tab["ZoneEN"].ToString(),
                                ZoneFR = Tab["ZoneFR"].ToString(),
                                NbZones = (int)Tab["NbZones"],
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

        #region Update Zone by Id
        public void Update(int Id, ZoneEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T.ContinentFR != null && T.ContinentEN != null && T.ZoneFR != null && T.ZoneEN != null && T.NbZones != 0 && Id != 0)
                    {
                        cmd.CommandText = "SP_ModifZone";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter ContinentEN = new SqlParameter("ContinentEN", T.ContinentEN);
                        SqlParameter ContinentFR = new SqlParameter("ContinentFR", T.ContinentFR);
                        SqlParameter ZoneEN = new SqlParameter("ZoneEN", T.ZoneEN);
                        SqlParameter ZoneFR = new SqlParameter("ZoneFR", T.ZoneFR);
                        SqlParameter NbZones = new SqlParameter("NbZones", T.NbZones);
                        SqlParameter PId = new SqlParameter("Id", Id);
                        cmd.Parameters.Add(ContinentEN);
                        cmd.Parameters.Add(ContinentFR);
                        cmd.Parameters.Add(ZoneEN);
                        cmd.Parameters.Add(ZoneFR);
                        cmd.Parameters.Add(NbZones);
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