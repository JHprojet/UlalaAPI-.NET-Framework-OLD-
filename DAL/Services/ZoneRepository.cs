using DAL.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Services
{
    public class ZoneRepository
    {
        #region Add Zone
        public void Create(ZoneEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T.ContinentFR != null && T.ContinentEN != null && T.ZoneFR != null && T.ZoneEN != null && T.ZoneQty != 0)
                    {
                        cmd.CommandText = "SP_AddZone";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter ContinentEN = new SqlParameter("ContinentEN", T.ContinentEN);
                        SqlParameter ContinentFR = new SqlParameter("ContinentFR", T.ContinentFR);
                        SqlParameter ZoneEN = new SqlParameter("ZoneEN", T.ZoneEN);
                        SqlParameter ZoneFR = new SqlParameter("ZoneFR", T.ZoneFR);
                        SqlParameter ZoneQty = new SqlParameter("ZoneQty", T.ZoneQty);
                        cmd.Parameters.Add(ContinentEN);
                        cmd.Parameters.Add(ContinentFR);
                        cmd.Parameters.Add(ZoneEN);
                        cmd.Parameters.Add(ZoneFR);
                        cmd.Parameters.Add(ZoneQty);
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
                    cmd.CommandText = "DELETE FROM Zones WHERE Id = @Id AND Active = 1";
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
                    cmd.CommandText = "SELECT * FROM Zones WHERE Active = 1";
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
                                ZoneQty = (int)Tab["ZoneQty"],
                                Active = (int)Tab["Active"]
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
                    cmd.CommandText = "SELECT * FROM Zones WHERE Id = @Id AND Active = 1";
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
                                ZoneQty = (int)Tab["ZoneQty"],
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

        #region Update Zone by Id
        public void Update(int Id, ZoneEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T.ContinentFR != null && T.ContinentEN != null && T.ZoneFR != null && T.ZoneEN != null && T.ZoneQty != 0 && Id != 0)
                    {
                        cmd.CommandText = "SP_UpdateZone";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter ContinentEN = new SqlParameter("ContinentEN", T.ContinentEN);
                        SqlParameter ContinentFR = new SqlParameter("ContinentFR", T.ContinentFR);
                        SqlParameter ZoneEN = new SqlParameter("ZoneEN", T.ZoneEN);
                        SqlParameter ZoneFR = new SqlParameter("ZoneFR", T.ZoneFR);
                        SqlParameter ZoneQty = new SqlParameter("ZoneQty", T.ZoneQty);
                        SqlParameter PId = new SqlParameter("Id", Id);
                        cmd.Parameters.Add(ContinentEN);
                        cmd.Parameters.Add(ContinentFR);
                        cmd.Parameters.Add(ZoneEN);
                        cmd.Parameters.Add(ZoneFR);
                        cmd.Parameters.Add(ZoneQty);
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