using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Intranet.Models;

/// <summary>
/// Descripción: Espacios de nombre para las clases que representan un uso global en el sistema.
/// Creado Por: Pablo Aponte
/// Fecha: 24/08/2021
/// </summary>
namespace Intranet.Utilities
{
    /// <summary>
    /// Descripción: Se especifican todos los metodos que ejecutan el acceso a la base de datos y retornan un DataTable, DataSet o un un valor escalar.
    /// </summary>
    public class SQLClient : Setting
    {
        /// <summary>
        /// Atributo de la clase para identificar la base de datos ala cual se debe conectar
        /// </summary>
        private readonly int db;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public SQLClient(int basedatos)
        {
            Oparameters = new List<SqlParameter>();
            this.db = basedatos;
        }

        /// <summary>
        /// Lista de parametros
        /// </summary>
        public List<SqlParameter> Oparameters { get; set; }

        /// <summary>
        /// Realiza la conexión a la base de datos CORP
        /// </summary>
        /// <returns></returns>
        public SqlConnection SqlConnection()
        {
            try
            {
                //base de datos mentex
                SqlConnection SQLCon = new SqlConnection
                {
                    ConnectionString = ConfigurationSetting.SQLConnection
                };

                //base de datos dynamics
                if (this.db == Convert.ToInt16(BasedeDatos.DYNAMICS))
                {
                    SQLCon.ConnectionString = ConfigurationSetting.SQLConnectionDY;
                }

                //base de datos corp
                if (this.db == Convert.ToInt16(BasedeDatos.CORP))
                {
                    SQLCon.ConnectionString = ConfigurationSetting.SQLConnectionCORP;
                }

                SQLCon.Open();
                return SQLCon;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Inicia una transacción de sql server
        /// </summary>
        /// <param name="nombre_transaccion"></param>
        /// <returns></returns>
        public SqlTransaction InicioTransaccion(string nombre_transaccion, SqlConnection SqlConexion)
        {
            SqlTransaction transaction;
            transaction = SqlConexion.BeginTransaction(nombre_transaccion);

            return transaction;
        }

        /// <summary>
        /// Se especifica una sentencia SQL SERVER para ser ejecutada insert, update o delete retorna un valor entero.
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public int InUpDel(string script)
        {
            int filasafectadad = 0;
            SqlConnection SqlConexion = SqlConnection();
            try
            {
                List<SqlParameter> parameters = Oparameters;
                SqlCommand SQLComand = new SqlCommand
                {
                    CommandText = script,
                    CommandTimeout = ConfigurationSetting.Timeout,
                    CommandType = CommandType.Text,
                    Connection = SqlConexion
                };
                SQLComand.Parameters.AddRange(parameters.ToArray());
                filasafectadad = SQLComand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SqlConexion.Close();
                SqlConexion.Dispose();
            }
            return filasafectadad;
        }

        /// <summary>
        /// Se especifica el nombre de un procedimiento almacenado
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="CommandType"></param>
        /// <returns></returns>
        public DataTable ExecuteStoredProcedure(string Name, CommandType CommandType)
        {
            //int Timeout
            DataTable dtData = new DataTable();
            SqlConnection SqlConexion = SqlConnection();

            try
            {
                List<SqlParameter> parameters = Oparameters;
                SqlCommand SQLComand = new SqlCommand
                {
                    CommandText = Name,
                    CommandTimeout = ConfigurationSetting.Timeout,
                    CommandType = CommandType,
                    Connection = SqlConexion
                };
                SQLComand.Parameters.AddRange(parameters.ToArray());

                SqlDataAdapter SqlDataAdapter = new SqlDataAdapter(SQLComand);
                SqlDataAdapter.Fill(dtData);
                return dtData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SqlConexion.Close();
                SqlConexion.Dispose();
            }

        }

        /// <summary>
        /// ExecuteStoredProcedureWithOutputParameter
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="CommandType"></param>
        /// <param name="OutParameter"></param>
        /// <returns></returns>
        public DataTable ExecuteStoredProcedureWithOutputParameter(string Name, CommandType CommandType, out Dictionary<string, string> OutParameter)
        {
            //int Timeout
            DataTable dtData = new DataTable();
            SqlConnection SqlConexion = SqlConnection();

            try
            {
                List<SqlParameter> parameters = Oparameters;
                SqlCommand SQLComand = new SqlCommand();
                SQLComand.CommandText = Name;
                SQLComand.CommandTimeout = ConfigurationSetting.Timeout;
                SQLComand.CommandType = CommandType;
                SQLComand.Connection = SqlConexion;
                SQLComand.Parameters.AddRange(parameters.ToArray());
                //SQLComand.ExecuteNonQuery();
                SqlDataAdapter SQLDataAdapter = new SqlDataAdapter(SQLComand);
                SQLDataAdapter.Fill(dtData);
                OutParameter = SQLComand.Parameters.OfType<SqlParameter>().ToDictionary((Key) => Key.ParameterName, (value) => value.Value.ToString().Trim());
                return dtData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SqlConexion.Close();
                SqlConexion.Dispose();
            }
        }

        /// <summary>
        /// ExecuteStoredProcedureWithOutputParameter
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="CommandType"></param>
        /// <param name="OutParameter"></param>
        /// <returns></returns>
        public int ExecuteStoredProcedureWithOutputParameter2(string Name, CommandType CommandType, out Dictionary<string, string> OutParameter)
        {
            //int Timeout
            DataTable dtData = new DataTable();
            SqlConnection SqlConexion = SqlConnection();
            int Result;

            try
            {
                List<SqlParameter> parameters = Oparameters;
                SqlCommand SQLComand = new SqlCommand();
                SQLComand.CommandText = Name;
                SQLComand.CommandTimeout = ConfigurationSetting.Timeout;
                SQLComand.CommandType = CommandType;
                SQLComand.Connection = SqlConexion;
                SQLComand.Parameters.AddRange(parameters.ToArray());
                Result = SQLComand.ExecuteNonQuery();
                OutParameter = SQLComand.Parameters.OfType<SqlParameter>().ToDictionary((Key) => Key.ParameterName, (value) => value.Value.ToString().Trim());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SqlConexion.Close();
                SqlConexion.Dispose();
            }
            return Result;
        }

        /// <summary>
        /// El Método ejecuta un string sql 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public int ExecuteTransactionSqlString(string name, CommandType commandType, SqlConnection SqlConexion, SqlTransaction transaction)
        {
            int Result;

            try
            {
                List<SqlParameter> parameters = Oparameters;
                SqlCommand SQLComand = new SqlCommand();
                SQLComand.CommandText = name;
                SQLComand.CommandTimeout = ConfigurationSetting.Timeout;
                SQLComand.CommandType = commandType;
                SQLComand.Connection = SqlConexion;
                SQLComand.Transaction = transaction;
                SQLComand.Parameters.AddRange(parameters.ToArray());
                Result = SQLComand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Result;
        }

        /// <summary>
        /// El Método ejecuta un string sql 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public DataTable ExecuteTransactionSqlDataTable(string name, CommandType commandType, SqlConnection SqlConexion, SqlTransaction transaction)
        {
            DataTable dtData = new DataTable();
            try
            {
                List<SqlParameter> parameters = Oparameters;
                SqlCommand SQLComand = new SqlCommand();
                SQLComand.CommandText = name;
                SQLComand.CommandTimeout = ConfigurationSetting.Timeout;
                SQLComand.CommandType = commandType;
                SQLComand.Connection = SqlConexion;
                SQLComand.Transaction = transaction;
                SQLComand.Parameters.AddRange(parameters.ToArray());
                SqlDataAdapter SQLDataAdapter = new SqlDataAdapter(SQLComand);
                SQLDataAdapter.Fill(dtData);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtData;
        }

        /// <summary>
        /// Cierra la conexión de la base de datos despues de finalizar la operación
        /// </summary>
        /// <param name="SqlConexion"></param>
        public void CerrarConexionDB(SqlConnection SqlConexion)
        {
            try
            {
                SqlConexion.Close();
                SqlConexion.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}