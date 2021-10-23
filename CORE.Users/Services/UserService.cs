using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using CORE.Users.Interfaces;
using CORE.Users.Models;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using Newtonsoft.Json;
using CORE.Users.Tools;
using Net.Core.Connection.Interfaces;
using Dapper;

namespace CORE.Users.Services
{
    public class UserService : IUser, IDisposable
    {
        bool disposedValue;
        //Implementa la interfaz 
        IConnectionDB<UserModel> _conn;
        Dapper.DynamicParameters _parameters = new Dapper.DynamicParameters();
        public UserService(IConnectionDB<UserModel> conn)
        {
            _conn = conn;
        }
        public List<Models.UserModel> GetUsers()
        {
            List<UserModel> list = new List<UserModel>();          
            try
            {
                _conn.PrepararProcedimiento("GetAll", null, CommandType.StoredProcedure); //Models.TipoDato.Cadena              
                foreach (var _user in _conn.Query())
                {
                    list.Add(new UserModel()
                    {
                        Identificador = _user.Identificador,
                        Name = _user.Name,
                        LastName = _user.LastName,
                        Nick = _user.Nick,
                        CreateDate = DateTime.Parse(_user.CreateDate.ToString())
                    });              
                }
                return list;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message);
            }
            catch (MySql.Data.MySqlClient.MySqlException mysqlEx)
            {
                throw new Exception(mysqlEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _conn.Dispose();
            }
        }
        public List<Models.UserModel> GetUser(int ID)
        {
            List<UserModel> list = new List<UserModel>();
            _conn.PrepararProcedimiento("EXEC GetUser @Id ="+Convert.ToString(ID), null, CommandType.Text);        
            try
            {
                foreach (var _user in _conn.Query())
                {
                    list.Add(new UserModel()
                    {
                        Identificador = _user.Identificador,
                        Name = _user.Name,
                        LastName = _user.LastName,
                        Nick = _user.Nick,
                        CreateDate = DateTime.Parse(_user.CreateDate.ToString())
                    });
                }
                return list;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message);
            }
            catch (MySql.Data.MySqlClient.MySqlException mysqlEx)
            {
                throw new Exception(mysqlEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _conn.Dispose();
            }
        }
        public long AddUser(Models.UserModel model)
        {
            long id = 0;           
            string Queryable = "DECLARE	@return_value int, @id int EXEC	@return_value = [dbo].[ADD_USER] @id = @id OUTPUT, @Name ="+model.Name+ ", @LastName ="+model.LastName+ ", @CreateDate = '"+Convert.ToString(DateTime.Now)+ "', @Status = 1, @Nick ="+model.Nick+ ", @Password = "+model.Password+"  SELECT	@id as N'@id'";
            _conn.PrepararProcedimiento(Queryable, null, CommandType.Text);
            
            try
            {
                var created_user = _conn.Query();
                if (created_user.Count() > 0)
                {
                    id = 1;
                }
                return id;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                _conn.Dispose();
            }
        }
        public bool UpdateUser(Models.UserModel model)
        {
            bool updated_user = false;
            string Queryable = "EXEC [dbo].[UPDATEUSER] @Id = "+Convert.ToString(model.Identificador)+", @Name = "+model.Name+ ", @LastName = "+model.LastName+", @CreateDate ='"+ Convert.ToString(DateTime.Now) + "', @Status = "+model.Status+", @Nick ="+model.Nick+", @Password ="+model.Password+";";
            _conn.PrepararProcedimiento(Queryable, null, CommandType.Text);
            try
            {
                var user = _conn.Query();

                    updated_user = true; ;

                return updated_user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                _conn.Dispose();
            }
        }
        public void DeleteUser(int ID)
        {
            bool updated_user = false;
            string Queryable = "EXEC [dbo].[DELETEUSER] @Id = " + Convert.ToString(ID) + ";";
            _conn.PrepararProcedimiento(Queryable, null, CommandType.Text);
            try
            {
                _conn.Query();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                _conn.Dispose();
            }
        }

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _conn.Dispose();// TODO: eliminar el estado administrado (objetos administrados)
                }

                // TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
                // TODO: establecer los campos grandes como NULL
                disposedValue = true;
            }
        }

        // // TODO: reemplazar el finalizador solo si "Dispose(bool disposing)" tiene código para liberar los recursos no administrados
        // ~MinervaService()
        // {
        //     // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}