using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Intranet.Utilities;
using Intranet.Models;
using EncryptDecrypt;

namespace Intranet.Ado.DbContent
{
    /// <summary>
    /// La clase permite acceder a las solicitudes para aprobar por el VP
    /// </summary>
    public class Wrkf_DbSolicitudVP
    {
        /// <summary>
        /// Listar los grupo de rubros que tienen rubros con pagos pendientes por aprobar por el VP
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_GrupoRubro> lstGrupoRubroVP()
        {
            List<Wrkf_GrupoRubro> lstgruporubro = new List<Wrkf_GrupoRubro>();
            Wrkf_DbMensajeError objdbmensajeerror = new Wrkf_DbMensajeError();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@pTituloError", SqlDbType.VarChar, 60)
            });

            Sqlprovider.Oparameters[0].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[1].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;

            string vSqlProcedureName = "WorkFlow.PL_Sel_GrupoRubrosVpFinanza";

            DataTable DtGrupoRubros = Sqlprovider.ExecuteStoredProcedureWithOutputParameter(vSqlProcedureName, CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            int total_registros = DtGrupoRubros.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_GrupoRubro objgruporubro = new Wrkf_GrupoRubro()
                    {
                        GrupoRubroIdEncript = EncriptadorMD5.Encrypt(DtGrupoRubros.Rows[i]["GrupoRubro_Id"].ToString()),
                        GrupoRubro_Id = Convert.ToInt32(DtGrupoRubros.Rows[i]["GrupoRubro_Id"]),
                        Descripcion = Convert.ToString(DtGrupoRubros.Rows[i]["Descripcion"]),
                        TotalGrupoRubros = Convert.ToInt32(DtGrupoRubros.Rows[i]["TotalGrupoRubro"]),
                        PagoUrgente = Convert.ToInt32(DtGrupoRubros.Rows[i]["PagoUrgente"])
                    };

                    lstgruporubro.Add(objgruporubro);
                }
            }
            else
            {
                Wrkf_GrupoRubro objgruporubro = new Wrkf_GrupoRubro();
                lstgruporubro.Add(objgruporubro);
            }

            return lstgruporubro;
        }

        /// <summary>
        /// Lista los pagos por rubro pendientes por aprobar por VP
        /// </summary>
        /// <param name="pgrid"></param>
        /// <param name="pprioridadpago"></param>
        /// <returns></returns>
        public List<Wrkf_Rubro> LstRubroVP(int pgrid, int pprioridadpago)
        {
            List<Wrkf_Rubro> lstrubro = new List<Wrkf_Rubro>();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pGrupoRubro_Id", pgrid),
                new SqlParameter("@pPrioridadPago", pprioridadpago),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@pTituloError", SqlDbType.VarChar, 60)
            });

            Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;

            string vSqlProcedureName = "WorkFlow.PL_Sel_RubrosVpFinanza";

            DataTable DtRubrosConPagos = Sqlprovider.ExecuteStoredProcedureWithOutputParameter(vSqlProcedureName, CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            int total_registros = DtRubrosConPagos.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_Rubro objrubro = new Wrkf_Rubro()
                    {
                        Rubro_IdEncript = EncriptadorMD5.Encrypt(Convert.ToString(DtRubrosConPagos.Rows[i]["Rubro_Id"])),
                        Rubro_Id = Convert.ToString(DtRubrosConPagos.Rows[i]["Rubro_Id"]),
                        Descripcion = Convert.ToString(DtRubrosConPagos.Rows[i]["Descripcion"]),
                        TotalRubros = Convert.ToInt32(DtRubrosConPagos.Rows[i]["TotalRubros"]),
                        PagoUrgente = Convert.ToInt32(DtRubrosConPagos.Rows[i]["PagoUrgente"])
                    };

                    lstrubro.Add(objrubro);
                }
            }
            else
            {
                Wrkf_Rubro objrubro = new Wrkf_Rubro();
                lstrubro.Add(objrubro);
            }

            return lstrubro;
        }

        /// <summary>
        /// Muestra un listado de los pagos por rubros pendientes por revisar por VP
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_SolicitudOrdenPago> ListaPagosPorRubroIdVpDb(Int32 pgruporubro_id, string prubro_id, string pfechadesde, string pfechahasta, int pprioridadpago)
        {
            List<Wrkf_SolicitudOrdenPago> lstSolicitudOrdenPago = new List<Wrkf_SolicitudOrdenPago>();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pGrupoRubro_Id", pgruporubro_id),
                new SqlParameter("@pRubro_Id", prubro_id),
                new SqlParameter("@pFechaPagoDesde", pfechadesde),
                new SqlParameter("@pFechaPagoHasta", pfechahasta),
                new SqlParameter("@pPrioridadPago", pprioridadpago),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@pTituloError", SqlDbType.VarChar, 60)
            });

            Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[6].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[7].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[8].Direction = ParameterDirection.Output;

            string vSqlProcedureName = "WorkFlow.PL_Sel_ListaPagosPorRubroIdVpFinanza";

            DataTable DtSolicitudOrdenPago = Sqlprovider.ExecuteStoredProcedureWithOutputParameter(vSqlProcedureName, CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            int total_registros = DtSolicitudOrdenPago.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_SolicitudOrdenPago Objwrkf_SolicitudOrdenPago = new Wrkf_SolicitudOrdenPago()
                    {
                        Codigo = DtSolicitudOrdenPago.Rows[i]["Codigo"].ToString(),
                        GrupoRubro_Id = Convert.ToInt32(DtSolicitudOrdenPago.Rows[i]["GrupoRubro_Id"]),
                        GrupoRubroDescripcion = DtSolicitudOrdenPago.Rows[i]["GrupoRubro"].ToString(),
                        Rubro_Id = DtSolicitudOrdenPago.Rows[i]["Rubro_Id"].ToString(),
                        RubroDescripcion = DtSolicitudOrdenPago.Rows[i]["Rubro"].ToString(),
                        DescripcionDocumento = DtSolicitudOrdenPago.Rows[i]["TipoDocumento"].ToString(),
                        CodigoMoneda = DtSolicitudOrdenPago.Rows[i]["CodigoMoneda"].ToString(),
                        GAPFechaPago = DtSolicitudOrdenPago.Rows[i]["FechaPago"].ToString(),
                        GAPFechaFactura = DtSolicitudOrdenPago.Rows[i]["FechaFactura"].ToString(),
                        GAPCodigoItem = DtSolicitudOrdenPago.Rows[i]["CodigoItem"].ToString(),
                        GAPCodigoPlantilla = DtSolicitudOrdenPago.Rows[i]["CodigoPlantilla"].ToString(),
                        GAPMonto = Convert.ToDouble(DtSolicitudOrdenPago.Rows[i]["Monto"]),
                        GAPIdProveedor = DtSolicitudOrdenPago.Rows[i]["IdProveedor"].ToString(),
                        GAPIdChequera = DtSolicitudOrdenPago.Rows[i]["IdChequera"].ToString(),
                        GAPProveedor = DtSolicitudOrdenPago.Rows[i]["VENDNAME"].ToString(),
                        GAPNroFactura = DtSolicitudOrdenPago.Rows[i]["NroFactura"].ToString(),
                        PagoUrgente = Convert.ToBoolean(DtSolicitudOrdenPago.Rows[i]["PagoUrgente"])
                    };

                    lstSolicitudOrdenPago.Add(Objwrkf_SolicitudOrdenPago);
                }
            }
            else
            {
                Wrkf_SolicitudOrdenPago Objwrkf_SolicitudOrdenPago = new Wrkf_SolicitudOrdenPago();
                lstSolicitudOrdenPago.Add(Objwrkf_SolicitudOrdenPago);
            }

            return lstSolicitudOrdenPago;
        }

        /// <summary>
        /// Aprobar y rechazar las solicitudes de pagos por subcontraloria
        /// </summary>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion AprobarRechazarSolicitudVp(string pcodigo, string pusuario, string pobservaciones, string paccion)
        {
            Wrkf_RespuestaOperacion objrespuestaoperacion = new Wrkf_RespuestaOperacion();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pCodigo", pcodigo),
                new SqlParameter("@pUsuario", pusuario),
                new SqlParameter("@pObservaciones", pobservaciones),
                new SqlParameter("@pAccion", paccion),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@pTituloError", SqlDbType.VarChar, 60)
            });

            Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[6].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[7].Direction = ParameterDirection.Output;

            Sqlprovider.ExecuteStoredProcedureWithOutputParameter2("WorkFlow.PL_Up_AprobarRechazarSolicitudVp_key", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            //Obtiene El Error Generado Desde El Procedimiento Almacenado
            if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
            {
                objrespuestaoperacion.Codigox = outparam["@pCodigoError"];
                objrespuestaoperacion.Mensajex = outparam["@pMensajeError"];
                objrespuestaoperacion.Tipox = outparam["@pTipoError"];
                objrespuestaoperacion.Titulox = outparam["@pTituloError"];
            }
            else
            {
                objrespuestaoperacion.Codigox = string.Empty;
                objrespuestaoperacion.Mensajex = string.Empty;
                objrespuestaoperacion.Tipox = string.Empty;
                objrespuestaoperacion.Titulox = string.Empty;
            }

            return objrespuestaoperacion;
        }
    }
}