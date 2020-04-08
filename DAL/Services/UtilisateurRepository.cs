using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Services
{
    public class UtilisateurRepository
    {
        #region Ajout Utilisateur
        public void Create(UtilisateurEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T.Mail != null && T.Password != null && T.Pseudo != null)
                    {
                        cmd.CommandText = "SP_AjoutUtilisateur";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter Pseudo = new SqlParameter("Pseudo", T.Pseudo);
                        SqlParameter Mail = new SqlParameter("Mail", T.Mail);
                        SqlParameter Password = new SqlParameter("Password", T.Password);
                        cmd.Parameters.Add(Pseudo);
                        cmd.Parameters.Add(Mail);
                        cmd.Parameters.Add(Password);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Suppression Utilisateur by Id
        public void Delete(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Utilisateurs WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    c.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region Récupération Utilisateur
        public List<UtilisateurEntity> GetAll()
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Utilisateurs WHERE Actif = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<UtilisateurEntity> L = new List<UtilisateurEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new UtilisateurEntity()
                            {
                                Id = (int)Tab["Id"],
                                Pseudo = Tab["Pseudo"].ToString(),
                                Mail = Tab["Mail"].ToString(),
                                Password = Tab["Password"].ToString(),
                                Role = Tab["Role"].ToString(),
                                Actif = (int)Tab["Actif"],
                                ActivationToken = Tab["ActivationToken"].ToString()
                            }); ;
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération Utilisateur by Id
        public UtilisateurEntity GetOne(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Utilisateurs WHERE Id = @Id AND Actif = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            UtilisateurEntity S = new UtilisateurEntity()
                            {
                                Id = (int)Tab["Id"],
                                Pseudo = Tab["Pseudo"].ToString(),
                                Mail = Tab["Mail"].ToString(),
                                Password = Tab["Password"].ToString(),
                                Role = Tab["Role"].ToString(),
                                Actif = (int)Tab["Actif"],
                                ActivationToken = Tab["ActivationToken"].ToString()
                            };
                            return S;
                        }
                        else return null;
                    }
                }
            }
        }
        #endregion

        #region Récupération Utilisateur by Pseudo
        public UtilisateurEntity GetOneByPseudo(string pseudo)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                 using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Utilisateurs WHERE Pseudo = @Pseudo";
                    cmd.Parameters.AddWithValue("Pseudo", pseudo);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            UtilisateurEntity S = new UtilisateurEntity()
                            {
                                Id = (int)Tab["Id"],
                                Pseudo = Tab["Pseudo"].ToString(),
                                Mail = Tab["Mail"].ToString(),
                                Password = Tab["Password"].ToString(),
                                Role = Tab["Role"].ToString(),
                                Actif = (int)Tab["Actif"],
                                ActivationToken = Tab["ActivationToken"].ToString()
                            };
                            return S;
                        }
                        else return null;
                    }
                }
            }
        }
        #endregion

        #region Récupération Utilisateur by Mail
        public UtilisateurEntity GetOneByMail(string Mail)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (Mail != null)
                    {
                        cmd.CommandText = "SELECT * FROM Utilisateurs WHERE Mail = @Mail";
                        cmd.Parameters.AddWithValue("Mail", Mail);
                        c.Open();
                        using (SqlDataReader Tab = cmd.ExecuteReader())
                        {
                            if (Tab.Read())
                            {
                                UtilisateurEntity S = new UtilisateurEntity()
                                {
                                    Id = (int)Tab["Id"],
                                    Pseudo = Tab["Pseudo"].ToString(),
                                    Mail = Tab["Mail"].ToString(),
                                    Password = Tab["Password"].ToString(),
                                    Role = Tab["Role"].ToString(),
                                    Actif = (int)Tab["Actif"],
                                    ActivationToken = Tab["ActivationToken"].ToString()
                                };
                                return S;
                            }
                            else return null;
                        }
                    }
                    else return null;
                }
            }
        }
        #endregion

        #region Update Utilisateur by Id
        public void Update(int Id, UtilisateurEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T.Mail != null && T.Password != null && T.Pseudo != null && (T.Role == "User" || T.Role == "Admin") && Id != 0)
                    {
                        cmd.CommandText = "SP_ModifUtilisateur";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter Pseudo = new SqlParameter("Pseudo", T.Pseudo);
                        SqlParameter Password = new SqlParameter("Password", T.Password);
                        SqlParameter Mail = new SqlParameter("Mail", T.Mail);
                        SqlParameter Role = new SqlParameter("Role", T.Role);
                        SqlParameter PId = new SqlParameter("Id", Id);
                        cmd.Parameters.Add(Pseudo);
                        cmd.Parameters.Add(Password);
                        cmd.Parameters.Add(Mail);
                        cmd.Parameters.Add(Role);
                        cmd.Parameters.Add(PId);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Check Utilisateur
        public bool Check(string login, string mdp)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SP_VerifUtilisateur";
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter Pseudo = new SqlParameter("Pseudo", login);
                    SqlParameter Password = new SqlParameter("Password", mdp);
                    cmd.Parameters.Add(Pseudo);
                    cmd.Parameters.Add(Password);
                    cmd.Parameters.Add("@IsOk", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    c.Open();
                    cmd.ExecuteNonQuery();
                    return Convert.ToBoolean(cmd.Parameters["@IsOk"].Value);
                }
            }
        }
        #endregion

        #region Update Token
        public bool UpdateToken(int Id, string Token)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (Token != null && Id != 0)
                    {
                        cmd.CommandText = "SP_UpdateToken";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter ActivationToken = new SqlParameter("ActivationToken", Token);
                        SqlParameter PId = new SqlParameter("Id", Id);
                        cmd.Parameters.Add(ActivationToken);
                        cmd.Parameters.Add(PId);
                        cmd.Parameters.Add("@IsOk", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                    return Convert.ToBoolean(cmd.Parameters["@IsOk"].Value);
                }
            }
        }
        #endregion

        #region Renvoi Token
        public void RenvoiToken(int Id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SP_RenvoiToken";
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter PId = new SqlParameter("Id", Id);
                    cmd.Parameters.Add(PId);
                    c.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region Update Password
        public bool UpdatePassword(int Id, string NewPassword)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (NewPassword != null && Id != 0)
                    {
                        cmd.CommandText = "SP_ChangePassword";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter PNewPassword = new SqlParameter("NewPassword", NewPassword);
                        SqlParameter PId = new SqlParameter("Id", Id);
                        cmd.Parameters.Add(PNewPassword);
                        cmd.Parameters.Add(PId);
                        cmd.Parameters.Add("@IsOk", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                    return Convert.ToBoolean(cmd.Parameters["@IsOk"].Value);
                }
            }
        }
        #endregion

        #region Update Password
        public bool NouveauPassword(string Mail)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (Mail != "")
                    {
                        cmd.CommandText = "SP_NouveauPassword";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter PMail = new SqlParameter("Mail", Mail);
                        cmd.Parameters.Add(PMail);
                        cmd.Parameters.Add("@IsOk", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                    return Convert.ToBoolean(cmd.Parameters["@IsOk"].Value);
                }
            }
        }
        #endregion
        
    }
}