using DAL.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Services
{
    public class EnregistrementRepository
    {
        #region Ajout Enregistrement
        public void Create(EnregistrementEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.ImagePath1 != null && T.ImagePath2 != null && T.ImagePath3 != null && T.ImagePath4 != null && T.TeamId != 0 && T.BossZoneId != 0 && T.UtilisateurId != 0)
                    {
                        cmd.CommandText = "SP_AjoutEnregistrement";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter BossZoneId = new SqlParameter("BossZoneId", T.BossZoneId);
                        SqlParameter TeamId = new SqlParameter("TeamId", T.TeamId);
                        SqlParameter ImagePath1 = new SqlParameter("ImagePath1", T.ImagePath1);
                        SqlParameter ImagePath2 = new SqlParameter("ImagePath2", T.ImagePath2);
                        SqlParameter ImagePath3 = new SqlParameter("ImagePath3", T.ImagePath3);
                        SqlParameter ImagePath4 = new SqlParameter("ImagePath4", T.ImagePath4);
                        SqlParameter UtilisateurId = new SqlParameter("UtilisateurId", T.UtilisateurId);
                        cmd.Parameters.Add(BossZoneId);
                        cmd.Parameters.Add(TeamId);
                        cmd.Parameters.Add(ImagePath1);
                        cmd.Parameters.Add(ImagePath2);
                        cmd.Parameters.Add(ImagePath3);
                        cmd.Parameters.Add(ImagePath4);
                        cmd.Parameters.Add(UtilisateurId);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Suppression Enregistrement by Id
        public void Delete(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Enregistrements WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    c.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region Récupération Enregistrements
        public List<EnregistrementEntity> GetAll()
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Enregistrements WHERE Actif = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<EnregistrementEntity> L = new List<EnregistrementEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new EnregistrementEntity()
                            {
                                Id = (int)Tab["Id"],
                                BossZoneId = (int)Tab["BossZoneId"],
                                TeamId = (int)Tab["TeamId"],
                                ImagePath1 = Tab["ImagePath1"].ToString(),
                                ImagePath2 = Tab["ImagePath2"].ToString(),
                                ImagePath3 = Tab["ImagePath3"].ToString(),
                                ImagePath4 = Tab["ImagePath4"].ToString(),
                                UtilisateurId = (int)Tab["UtilisateurId"],
                                Note = (int)Tab["Note"],
                                Actif = (int)Tab["Actif"]
                            });
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération Enregistrement by Id
        public EnregistrementEntity GetOne(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Enregistrements WHERE Id = @Id AND Actif = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            EnregistrementEntity S = new EnregistrementEntity()
                            {
                                Id = (int)Tab["Id"],
                                BossZoneId = (int)Tab["BossZoneId"],
                                TeamId = (int)Tab["TeamId"],
                                ImagePath1 = Tab["ImagePath1"].ToString(),
                                ImagePath2 = Tab["ImagePath2"].ToString(),
                                ImagePath3 = Tab["ImagePath3"].ToString(),
                                ImagePath4 = Tab["ImagePath4"].ToString(),
                                UtilisateurId = (int)Tab["UtilisateurId"],
                                Note = (int)Tab["Note"],
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

        #region Update Enregistrement by Id
        public void Update(int Id, EnregistrementEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T != null && T.ImagePath1 != null && T.ImagePath2 != null && T.ImagePath3 != null && T.ImagePath4 != null && T.TeamId != 0 && T.BossZoneId != 0 && T.UtilisateurId != 0 && Id != 0)
                    { 
                        cmd.CommandText = "SP_ModifEnregistrement";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter BossZoneId = new SqlParameter("BossZoneId", T.BossZoneId);
                        SqlParameter TeamId = new SqlParameter("TeamId", T.TeamId);
                        SqlParameter ImagePath1 = new SqlParameter("ImagePath1", T.ImagePath1);
                        SqlParameter ImagePath2 = new SqlParameter("ImagePath2", T.ImagePath2);
                        SqlParameter ImagePath3 = new SqlParameter("ImagePath3", T.ImagePath3);
                        SqlParameter ImagePath4 = new SqlParameter("ImagePath4", T.ImagePath4);
                        SqlParameter Note = new SqlParameter("Note", T.Note);
                        SqlParameter UtilisateurId = new SqlParameter("UtilisateurId", T.UtilisateurId);
                        SqlParameter PId = new SqlParameter("Id", Id);
                        cmd.Parameters.Add(BossZoneId);
                        cmd.Parameters.Add(TeamId);
                        cmd.Parameters.Add(ImagePath1);
                        cmd.Parameters.Add(ImagePath2);
                        cmd.Parameters.Add(ImagePath3);
                        cmd.Parameters.Add(ImagePath4);
                        cmd.Parameters.Add(Note);
                        cmd.Parameters.Add(UtilisateurId);
                        cmd.Parameters.Add(PId);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Récupération Enregistrements en fonction de plusieurs éléments facultatifs
        public List<EnregistrementEntity> GetAllByInfos(int? U, int? BZ, int? C1, int? C2, int? C3, int? C4)
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
                    if (U != null) sU= " AND UtilisateurId = "+U ;
                    if (BZ != null) sBZ = " AND BossZoneId = "+BZ;
                    if (C1 != null) sC1 = $" AND (T.ClasseId1 = {C1} OR T.ClasseId2 = {C1} OR T.ClasseId3 = {C1} OR T.ClasseId4 = {C1})";
                    if (C2 != null) sC2 = $" AND (T.ClasseId1 = {C2} OR T.ClasseId2 = {C2} OR T.ClasseId3 = {C2} OR T.ClasseId4 = {C2})";
                    if (C3 != null) sC3 = $" AND (T.ClasseId1 = {C3} OR T.ClasseId2 = {C3} OR T.ClasseId3 = {C3} OR T.ClasseId4 = {C3})";
                    if (C4 != null) sC4 = $" AND (T.ClasseId1 = {C4} OR T.ClasseId2 = {C4} OR T.ClasseId3 = {C4} OR T.ClasseId4 = {C4})";
                    cmd.CommandText = "SELECT * FROM Enregistrements E JOIN Teams T ON E.TeamId = T.Id WHERE E.Actif = 1" +sU+sBZ+sC1+sC2+sC3+sC4+" ORDER BY Note DESC";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<EnregistrementEntity> L = new List<EnregistrementEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new EnregistrementEntity()
                            {
                                Id = (int)Tab["Id"],
                                BossZoneId = (int)Tab["BossZoneId"],
                                TeamId = (int)Tab["TeamId"],
                                ImagePath1 = Tab["ImagePath1"].ToString(),
                                ImagePath2 = Tab["ImagePath2"].ToString(),
                                ImagePath3 = Tab["ImagePath3"].ToString(),
                                ImagePath4 = Tab["ImagePath4"].ToString(),
                                UtilisateurId = (int)Tab["UtilisateurId"],
                                Note = (int)Tab["Note"],
                                Actif = (int)Tab["Actif"],
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