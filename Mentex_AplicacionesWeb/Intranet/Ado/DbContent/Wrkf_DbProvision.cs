using Intranet.Models;
using Intranet.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace Intranet.Ado.DbContent
{
    public class Wrkf_DbProvision
    {
        /// <summary>
        /// constructor de la clase
        /// </summary>
        public Wrkf_DbProvision()
        {

        }

        /// <summary>
        /// Genera la provisión para un rango de fecha y código de moneda especificado
        /// </summary>
        /// <param name="codigo_moneda"></param>
        /// <param name="fecha_desde"></param>
        /// <param name="fecha_hasta"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion GenerarProvision(string codigo_moneda, string fecha_desde, string fecha_hasta, string usuario, string listadoChequera)
        {
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();

            //Ejecutar el procedimiento almacenado
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pListadoChequera", listadoChequera),
                new SqlParameter("@pFechaPagoDesde", fecha_desde),
                new SqlParameter("@pFechaPagoHasta", fecha_hasta),
                new SqlParameter("@pCodigoMoneda", codigo_moneda),
                new SqlParameter("@pUsuario", usuario),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipo", SqlDbType.VarChar, 20),
                new SqlParameter("@pTitulo", SqlDbType.VarChar, 60)
            });

            Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[6].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[7].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[8].Direction = ParameterDirection.Output;

            Sqlprovider.ExecuteStoredProcedureWithOutputParameter("WorkFlow.PL_Ins_GenerarProvision", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
            {
                objRespuestaOperacion.Codigox = outparam["@pCodigoError"];
                objRespuestaOperacion.Mensajex = outparam["@pMensajeError"];
                objRespuestaOperacion.Tipox = outparam["@pTipo"];
                objRespuestaOperacion.Titulox = outparam["@pTitulo"];
            }
            else
            {
                objRespuestaOperacion.Codigox = "";
                objRespuestaOperacion.Mensajex = "";
                objRespuestaOperacion.Tipox = "";
                objRespuestaOperacion.Titulox = "";
            }
            
            return objRespuestaOperacion;
        }

        /// <summary>
        /// Muestra un listado de las provisiones
        /// </summary>
        /// <returns></returns>
        public DataTable ListadoProvisiones(string codigo_moneda, string fecha_desde, string fecha_hasta, string usuario, string listadoChequera)
        {
            //Ejecutar el procedimiento almacenado
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pListadoChequera", listadoChequera),
                new SqlParameter("@pFechaPagoDesde", fecha_desde),
                new SqlParameter("@pFechaPagoHasta", fecha_hasta),
                new SqlParameter("@pCodigoMoneda", codigo_moneda),
                new SqlParameter("@pUsuario", usuario),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipo", SqlDbType.VarChar, 20),
                new SqlParameter("@pTitulo", SqlDbType.VarChar, 60)
            });

            Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[6].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[7].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[8].Direction = ParameterDirection.Output;

            DataTable DtProvision = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("WorkFlow.PL_Sel_ListarProvision_filter_FechaMoneda", 
                                                                                            CommandType.StoredProcedure, 
                                                                                            out Dictionary<string, string> outparam);
            return DtProvision;
        }

        /// <summary>
        /// Registra o actualiza la provisión
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion RegistrarActualizarProvision(Wrkf_Provision body)
        {
            Wrkf_RespuestaOperacion wrkfrespuestaoperacion = new Wrkf_RespuestaOperacion();

            //Ejecutar el procedimiento almacenado
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pIdProvision", body.IdProvisionx),
                new SqlParameter("@pIdProveedor", body.IdProveedorx),
                new SqlParameter("@pCodigoPlantilla", body.CodigoPlantillax),
                new SqlParameter("@pIdGrupoRubro", body.IdGrupoRubrox),
                new SqlParameter("@pIdRubro", body.IdRubrox),
                new SqlParameter("@pTienda", body.Tiendax),
                new SqlParameter("@pCodigoMoneda", body.CodigoMonedax),
                new SqlParameter("@pIdChequera", body.IdChequerax),
                new SqlParameter("@pIdFormaPago", body.IdFormaPagox),
                new SqlParameter("@pMonto", body.Montox),
                new SqlParameter("@pFecha_pago", body.FechaPagox),
                new SqlParameter("@pObservaciones", body.Observacionesx),
                new SqlParameter("@pAnulada", body.Anuladax),
                new SqlParameter("@pUsuario", body.Usuariox),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipo", SqlDbType.VarChar, 20),
                new SqlParameter("@pTitulo", SqlDbType.VarChar, 60)
            });

            Sqlprovider.Oparameters[14].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[15].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[16].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[17].Direction = ParameterDirection.Output;

            //Ejecutar el procedimiento almacenado
            Sqlprovider.ExecuteStoredProcedureWithOutputParameter2("WorkFlow.PL_InsUpd_Provision", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            //Obtiene El Error Generado Desde El Procedimiento Almacenado WorkFlow.PL_InsUpd_Provision.
            if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
            {
                wrkfrespuestaoperacion.Codigox = outparam["@pCodigoError"];
                wrkfrespuestaoperacion.Mensajex = outparam["@pMensajeError"];
                wrkfrespuestaoperacion.Tipox = outparam["@pTipo"];
                wrkfrespuestaoperacion.Titulox = outparam["@pTitulo"];
            }
            else
            {
                wrkfrespuestaoperacion.Codigox = string.Empty;
                wrkfrespuestaoperacion.Mensajex = string.Empty;
                wrkfrespuestaoperacion.Tipox = string.Empty;
                wrkfrespuestaoperacion.Titulox = string.Empty;
            }

            return wrkfrespuestaoperacion;
        }

        /// <summary>
        /// El método realiza la anula la provisión
        /// </summary>
        /// <param name="codigo_provision"></param>
        /// <param name="usuario_modifico"></param>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion CancelProvision(int provision_id, string usuario)
        {
            SqlTransaction transaccion = null;
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            SqlConnection sqlconexion = Sqlprovider.SqlConnection();
            Wrkf_RespuestaOperacion wrkf_respuestaoperacion = new Wrkf_RespuestaOperacion();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            //inicio de la trasaccion
            transaccion = Sqlprovider.InicioTransaccion("CancelProvision", sqlconexion);

            //Ejecutar la consulta SQL
            Sqlprovider.Oparameters = new List<SqlParameter>();
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@provision_id", provision_id),
                new SqlParameter("@usuario_modifico", usuario)
            });

            string sqlQuery = "update workflow.Provision set anulada = 1, usuario_modifico = @usuario_modifico where provision_id = @provision_id";

            //run the sql query
            wrkf_respuestaoperacion.RegistrosProcesadosx = Sqlprovider.ExecuteTransactionSqlString(sqlQuery, CommandType.Text, sqlconexion, transaccion);

            //doing commit the transaction
            if (wrkf_respuestaoperacion.RegistrosProcesadosx > 0)
            {
                transaccion.Commit();
            }
            else
            {
                transaccion.Rollback();
            }

            return wrkf_respuestaoperacion;
        }
    }
}