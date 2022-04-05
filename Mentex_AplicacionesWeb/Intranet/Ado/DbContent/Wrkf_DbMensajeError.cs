using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Intranet.Utilities;
using Intranet.Models;

namespace Intranet.Ado.DbContent
{
    /// <summary>
    /// Clase permite acceder a la tabla [Workflow].[tbl_MensajeError]
    /// </summary>
    public class Wrkf_DbMensajeError
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Wrkf_DbMensajeError()
        {
        }

        /// <summary>
        /// El método obtiene el mensaje de error segun el código de error y el módulo
        /// </summary>
        /// <param name="Codigo"></param>
        /// <param name="Modulo"></param>
        /// <returns></returns>
        public MensajeError GetObtenerMensajeError(string Codigo, string Modulo)
        {
            MensajeError objMensajeError = new MensajeError();

            try
            {
                //Ejecutar la consulta SQL
                SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@Codigo", Codigo),
                    new SqlParameter("@Modulo", Modulo)
                });

                string sqlQuery = "select Mensaje_Id, Codigo, Mensaje, Modulo, Tipo, Titulo from Workflow.tbl_MensajeError where Codigo = @Codigo and Modulo = @Modulo";

                //optener los resultados de la consulta SQL
                DataTable DtMensajeError = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

                //verifica el procedimiento genero algun resultado
                int total_registros = DtMensajeError.Rows.Count;

                if (total_registros > 0)
                {
                    objMensajeError.Mensaje_Idx = Convert.ToInt32(DtMensajeError.Rows[0]["Mensaje_Id"]);
                    objMensajeError.Codigox = Convert.ToString(DtMensajeError.Rows[0]["Codigo"]);
                    objMensajeError.Mensajex = Convert.ToString(DtMensajeError.Rows[0]["Mensaje"]);
                    objMensajeError.Modulox = Convert.ToString(DtMensajeError.Rows[0]["Modulo"]);
                    objMensajeError.Tipox = Convert.ToString(DtMensajeError.Rows[0]["Tipo"]);
                    objMensajeError.Titulox = Convert.ToString(DtMensajeError.Rows[0]["Titulo"]);
                }
                else
                {
                    objMensajeError.Mensaje_Idx = -1;
                    objMensajeError.Codigox = "999";
                    objMensajeError.Mensajex = "Mensaje Indefinido";
                    objMensajeError.Modulox = "";
                    objMensajeError.Tipox = "error";
                    objMensajeError.Titulox = "Mensaje no Registrado";
                }
            }
            catch (Exception ex)
            {
                objMensajeError.Codigox = ex.HResult.ToString();
                objMensajeError.Mensajex = ex.Message.ToString();
                objMensajeError.Modulox = "MensajeError";
                objMensajeError.Tipox = "error";
                objMensajeError.Titulox = "Obtener Mensaje de Error";
            }

            return objMensajeError;
        }

        /// <summary>
        /// Registra una traza de los errores generados en el sistema
        /// </summary>
        /// <param name="logcodigoerror"></param>
        /// <param name="Logmensajeerror"></param>
        /// <param name="logusuarioerror"></param>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion RegistrarLogErrores(int logcodigoerror, string Logmensajeerror, string logusuarioerror, string logmetodocontroladorerror)
        {
            SqlTransaction transaccion = null;
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            SqlConnection sqlconexion = Sqlprovider.SqlConnection();
            Wrkf_RespuestaOperacion wrkf_respuestaoperacion = new Wrkf_RespuestaOperacion();

            try
            {
                //inicio de la trasaccion
                transaccion = Sqlprovider.InicioTransaccion("RegistrarLogErrores", sqlconexion);

                //Ejecutar la consulta SQL
                Sqlprovider.Oparameters = new List<SqlParameter>();
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@LogCodigoError", logcodigoerror),
                    new SqlParameter("@LogMensajeError", Logmensajeerror),
                    new SqlParameter("@LogFechaError", DateTime.Now),
                    new SqlParameter("@LogUsuarioError", logusuarioerror),
                    new SqlParameter("@LogMetodoControladorError", logmetodocontroladorerror)
                });

                string sqlQuery = "insert into Workflow.LogErrores (LogCodigoError, LogMensajeError, LogFechaError, LogUsuarioError, LogMetodoControladorError) ";
                sqlQuery += "values (@LogCodigoError, @LogMensajeError, @LogFechaError, @LogUsuarioError, @LogMetodoControladorError)";

                int resultado = Sqlprovider.ExecuteTransactionSqlString(sqlQuery, CommandType.Text, sqlconexion, transaccion);

                if (resultado > 0)
                {
                    wrkf_respuestaoperacion.RegistrosProcesadosx = resultado;
                    transaccion.Commit();
                }
                else
                {
                    wrkf_respuestaoperacion.Codigox = "RLE001";
                    wrkf_respuestaoperacion.Mensajex = "Ocurrio un error al momento de registrar el registro de la traza de la transacción";
                    wrkf_respuestaoperacion.Tipox = "error";
                    wrkf_respuestaoperacion.Titulox = "Registrar Log de la Aplicación";
                    transaccion.Rollback();
                }
            }
            catch (Exception ex)
            {
                wrkf_respuestaoperacion.Codigox = ex.HResult.ToString();
                wrkf_respuestaoperacion.Mensajex = ex.Message.ToString();
                wrkf_respuestaoperacion.Tipox = "error";
                wrkf_respuestaoperacion.Titulox = "Registrar Log de la Aplicación";
                transaccion.Rollback();
            }

            return wrkf_respuestaoperacion;
        }
    }
}