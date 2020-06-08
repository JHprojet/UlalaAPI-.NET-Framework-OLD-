using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Services
{
    public class UserRepository
    {
        #region Add User
        public void Create(UserEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T.Mail != null && T.Password != null && T.Username != null)
                    {
                        cmd.CommandText = "SP_AddOneUser";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter Username = new SqlParameter("Username", T.Username);
                        SqlParameter Mail = new SqlParameter("Mail", T.Mail);
                        SqlParameter Password = new SqlParameter("Password", T.Password);
                        cmd.Parameters.Add(Username);
                        cmd.Parameters.Add(Mail);
                        cmd.Parameters.Add(Password);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Suppression User by Id
        public void Delete(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Users WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    c.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region Récupération User
        public List<UserEntity> GetAll()
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Users WHERE Active = 1";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<UserEntity> L = new List<UserEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new UserEntity()
                            {
                                Id = (int)Tab["Id"],
                                Username = Tab["Username"].ToString(),
                                Mail = Tab["Mail"].ToString(),
                                Password = "*",
                                Role = Tab["Role"].ToString(),
                                Active = (int)Tab["Active"],
                                ActivationToken = Tab["ActivationToken"].ToString()
                            }); ;
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération User Admin
        public List<UserEntity> GetAllAdmin()
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Users";
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        List<UserEntity> L = new List<UserEntity>();
                        while (Tab.Read())
                        {
                            L.Add(new UserEntity()
                            {
                                Id = (int)Tab["Id"],
                                Username = Tab["Username"].ToString(),
                                Mail = Tab["Mail"].ToString(),
                                Password = "*",
                                Role = Tab["Role"].ToString(),
                                Active = (int)Tab["Active"],
                                ActivationToken = Tab["ActivationToken"].ToString()
                            }); ;
                        }
                        return L;
                    }
                }
            }
        }
        #endregion

        #region Récupération User by Id
        public UserEntity GetOne(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Users WHERE Id = @Id AND Active = 1";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            UserEntity S = new UserEntity()
                            {
                                Id = (int)Tab["Id"],
                                Username = Tab["Username"].ToString(),
                                Mail = Tab["Mail"].ToString(),
                                Password = "*",
                                Role = Tab["Role"].ToString(),
                                Active = (int)Tab["Active"],
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

        #region Récupération User by Id [Admin]
        public UserEntity GetOneAdmin(int id)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Users WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("Id", id);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            UserEntity S = new UserEntity()
                            {
                                Id = (int)Tab["Id"],
                                Username = Tab["Username"].ToString(),
                                Mail = Tab["Mail"].ToString(),
                                Password = "*",
                                Role = Tab["Role"].ToString(),
                                Active = (int)Tab["Active"],
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

        #region Récupération User by Username
        public UserEntity GetOneByUsername(string Username)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                 using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Users WHERE Username = @Username";
                    cmd.Parameters.AddWithValue("Username", Username);
                    c.Open();
                    using (SqlDataReader Tab = cmd.ExecuteReader())
                    {
                        if (Tab.Read())
                        {
                            UserEntity S = new UserEntity()
                            {
                                Id = (int)Tab["Id"],
                                Username = Tab["Username"].ToString(),
                                Mail = Tab["Mail"].ToString(),
                                Password = "*",
                                Role = Tab["Role"].ToString(),
                                Active = (int)Tab["Active"],
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

        #region Récupération User by Mail
        public UserEntity GetOneByMail(string Mail)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (Mail != null)
                    {
                        cmd.CommandText = "SELECT * FROM Users WHERE Mail = @Mail";
                        cmd.Parameters.AddWithValue("Mail", Mail);
                        c.Open();
                        using (SqlDataReader Tab = cmd.ExecuteReader())
                        {
                            if (Tab.Read())
                            {
                                UserEntity S = new UserEntity()
                                {
                                    Id = (int)Tab["Id"],
                                    Username = Tab["Username"].ToString(),
                                    Mail = Tab["Mail"].ToString(),
                                    Password = "*",
                                    Role = Tab["Role"].ToString(),
                                    Active = (int)Tab["Active"],
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

        #region Update User by Id
        public void Update(int Id, UserEntity T)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (T.Mail != null && T.Username != null && (T.Role == "User" || T.Role == "Admin") && Id != 0)
                    {
                        cmd.CommandText = "SP_UpdateUser";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter Username = new SqlParameter("Username", T.Username);
                        SqlParameter Mail = new SqlParameter("Mail", T.Mail);
                        SqlParameter Role = new SqlParameter("Role", T.Role);
                        SqlParameter Active = new SqlParameter("Active", T.Active);
                        SqlParameter PId = new SqlParameter("Id", Id);
                        cmd.Parameters.Add(Username);
                        cmd.Parameters.Add(Mail);
                        cmd.Parameters.Add(Role);
                        cmd.Parameters.Add(PId);
                        cmd.Parameters.Add(Active);
                        c.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Check User
        public bool Check(string login, string mdp)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "SP_CheckUser";
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter Username = new SqlParameter("Username", login);
                    SqlParameter Password = new SqlParameter("Password", mdp);
                    cmd.Parameters.Add(Username);
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
                    cmd.CommandText = "SP_SendActivationToken";
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

        #region Nouveau Password
        public bool NouveauPassword(string Mail)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (Mail != "")
                    {
                        cmd.CommandText = "SP_NewPassword";
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

        #region Retrieve Username
        public bool RetrieveUsername(string Mail)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["API"].ConnectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    if (Mail != "")
                    {
                        cmd.CommandText = "SP_RetrieveUsername";
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