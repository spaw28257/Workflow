using System;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Intranet.Utilities;
using Intranet.Models;
using EncryptDecrypt;

namespace Intranet.Ado.DbContent
{
    public class Wrkf_DbSolicitudSubContralor
    {
        /// <summary>
        /// Lista el grupo de rubros que contiene pagos pendientes por revisar por el subcontralor
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_GrupoRubro> lstGrupoRubroSubContralor()
        {
            List<Wrkf_GrupoRubro> lstgruporubro = new List<Wrkf_GrupoRubro>();

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

            string vSqlProcedureName = "WorkFlow.PL_Sel_GrupoRubrosSubContralor";

            DataTable DtGrupoRubrosConPagos = Sqlprovider.ExecuteStoredProcedureWithOutputParameter(vSqlProcedureName, CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            int total_registros = DtGrupoRubrosConPagos.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_GrupoRubro objgruporubro = new Wrkf_GrupoRubro()
                    {
                        GrupoRubroIdEncript = EncriptadorMD5.Encrypt(DtGrupoRubrosConPagos.Rows[i]["GrupoRubro_Id"].ToString()),
                        GrupoRubro_Id = Convert.ToInt32(DtGrupoRubrosConPagos.Rows[i]["GrupoRubro_Id"]),
                        Descripcion = Convert.ToString(DtGrupoRubrosConPagos.Rows[i]["Descripcion"]),
                        TotalGrupoRubros = Convert.ToInt32(DtGrupoRubrosConPagos.Rows[i]["TotalGrupoRubro"])
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
        /// Lista los rubros que tienen pagos pendientes por revisar por SubContraloria 
        /// </summary>
        /// <param name = "gruporubro_id" ></ param >
        /// < returns ></ returns >
        public List<Wrkf_Rubro> lstRubrosSubContraloria(int pGruporubro_id)
        {
            List<Wrkf_Rubro> lstrubro = new List<Wrkf_Rubro>();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pGruporubro_Id", pGruporubro_id),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@pTituloError", SqlDbType.VarChar, 60)
            });

            Sqlprovider.Oparameters[1].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;

            string vSqlProcedureName = "workflow.PL_Sel_RubrosConPagosSubContraloria";

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
                        TotalRubros = Convert.ToInt32(DtRubrosConPagos.Rows[i]["TotalRubros"])
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
        /// Muestra un listado de los pagos por rubros pendientes por revisar por subcontraloria
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_SolicitudOrdenPago> ListaPagosPorRubroIdSubContraloriaDb(Int32 pgruporubro_id, string prubro_id, string pfechadesde, string pfechahasta)
        {
            List<Wrkf_SolicitudOrdenPago> lstSolicitudOrdenPago = new List<Wrkf_SolicitudOrdenPago>();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pGrupoRubro_Id", pgruporubro_id),
                new SqlParameter("@pRubro_Id", prubro_id),
                new SqlParameter("@pFechaPagoDesde", pfechadesde),
                new SqlParameter("@pFechaPagoHasta", pfechahasta),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@pTituloError", SqlDbType.VarChar, 60)
            });

            Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[6].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[7].Direction = ParameterDirection.Output;

            string vSqlProcedureName = "workflow.PL_Sel_ListaPagosPorRubroIdSubContraloria";

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
                        GAPNroFactura = DtSolicitudOrdenPago.Rows[i]["NroFactura"].ToString()
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
        public Wrkf_RespuestaOperacion AprobarRechazarSolicitudSubContraloria(string pcodigo, string pusuario, string pobservaciones, string paccion)
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

            Sqlprovider.ExecuteStoredProcedureWithOutputParameter2("WorkFlow.PL_Up_AprobarRechazarSolicitudSubContraloria_key", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            //Obtiene El Error Generado Desde El Procedimiento Almacenado [Workflow].[sp_up_AprobarRechazarSolicitudSubContraloria].
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