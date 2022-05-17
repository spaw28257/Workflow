using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Intranet.Utilities;
using Intranet.Models;

namespace Intranet.Ado.DbContent
{
    public class Wrkf_DbSolicitudOrdenPagoCxP
    {
        /// <summary>
        /// Obtiene un listado de las solicitudes de ordenes de pago.
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_SolicitudOrdenPago> GetPagosRevisionCxP(string pFechaPagoDesde, string pFechaPagoHasta)
        {
            List<Wrkf_SolicitudOrdenPago> lstSolicitudOrdenPago = new List<Wrkf_SolicitudOrdenPago>();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pFechaPagoDesde", pFechaPagoDesde),
                new SqlParameter("@pFechaPagoHasta", pFechaPagoHasta),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@pTituloError", SqlDbType.VarChar, 60)
            });

            Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;

            DataTable DtPagosRevisionCxP = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("WorkFlow.PL_Sel_RevisionCXP", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            int total_registros = DtPagosRevisionCxP.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_SolicitudOrdenPago objsolicitudordenpago = new Wrkf_SolicitudOrdenPago
                    {
                        Codigo = DtPagosRevisionCxP.Rows[i]["NroPago"].ToString(),
                        GAPCodigoItem = DtPagosRevisionCxP.Rows[i]["NroItem"].ToString(),
                        GAPCodigoPlantilla = DtPagosRevisionCxP.Rows[i]["CodigoPlantilla"].ToString(),
                        GAPProveedor = DtPagosRevisionCxP.Rows[i]["Beneficiario"].ToString(),
                        GAPIdProveedor = DtPagosRevisionCxP.Rows[i]["IdProveedor"].ToString(),
                        GAPNroFactura = DtPagosRevisionCxP.Rows[i]["NroFactura"].ToString(),
                        GAPDescripcionFactura = DtPagosRevisionCxP.Rows[i]["DescripcionFactura"].ToString(),
                        GAPFechaFactura = DtPagosRevisionCxP.Rows[i]["FechaFactura"].ToString(),
                        GAPFechaPago = DtPagosRevisionCxP.Rows[i]["FechaPago"].ToString(),
                        GAPMonto = Convert.ToDouble(DtPagosRevisionCxP.Rows[i]["Monto"]),
                        GAPIdChequera = DtPagosRevisionCxP.Rows[i]["IdChequera"].ToString(),
                        GrupoRubro_Id = Convert.ToInt32(DtPagosRevisionCxP.Rows[i]["GruporubroId"]),
                        GrupoRubroDescripcion = DtPagosRevisionCxP.Rows[i]["GrupoRubro"].ToString(),
                        Rubro_Id = DtPagosRevisionCxP.Rows[i]["RubroId"].ToString(),
                        RubroDescripcion = DtPagosRevisionCxP.Rows[i]["Rubro"].ToString(),
                        CodigoMoneda = DtPagosRevisionCxP.Rows[i]["CodigoMoneda"].ToString()
                    };
                    lstSolicitudOrdenPago.Add(objsolicitudordenpago);
                }
            }
            else
            {
                Wrkf_SolicitudOrdenPago objsolicitudordenpago = new Wrkf_SolicitudOrdenPago()
                {
                    Mensajex = "No existen registros para el rango de fecha especificado"
                };
                lstSolicitudOrdenPago.Add(objsolicitudordenpago);
            }
            return lstSolicitudOrdenPago;
        }

        /// <summary>
        /// Obtiene un listado de las solicitudes de ordenes de pago.
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_SolicitudOrdenPago> ListadoAprobacionRechazoPagoCxP(string pFechaPagoDesde, string pFechaPagoHasta)
        {
            List<Wrkf_SolicitudOrdenPago> lstSolicitudOrdenPago = new List<Wrkf_SolicitudOrdenPago>();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pFechaPagoDesde", pFechaPagoDesde),
                new SqlParameter("@pFechaPagoHasta", pFechaPagoHasta),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@pTituloError", SqlDbType.VarChar, 60)
            });

            Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;

            DataTable DtPagosAprobacionCxP = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("WorkFlow.PL_Sel_AprobacionCXP", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            int total_registros = DtPagosAprobacionCxP.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_SolicitudOrdenPago objsolicitudordenpago = new Wrkf_SolicitudOrdenPago
                    {
                        Codigo = DtPagosAprobacionCxP.Rows[i]["NroPago"].ToString(),
                        GAPCodigoItem = DtPagosAprobacionCxP.Rows[i]["NroItem"].ToString(),
                        GAPCodigoPlantilla = DtPagosAprobacionCxP.Rows[i]["CodigoPlantilla"].ToString(),
                        GAPProveedor = DtPagosAprobacionCxP.Rows[i]["Beneficiario"].ToString(),
                        GAPIdProveedor = DtPagosAprobacionCxP.Rows[i]["IdProveedor"].ToString(),
                        GAPNroFactura = DtPagosAprobacionCxP.Rows[i]["NroFactura"].ToString(),
                        GAPDescripcionFactura = DtPagosAprobacionCxP.Rows[i]["DescripcionFactura"].ToString(),
                        GAPFechaFactura = DtPagosAprobacionCxP.Rows[i]["FechaFactura"].ToString(),
                        GAPFechaPago = DtPagosAprobacionCxP.Rows[i]["FechaPago"].ToString(),
                        GAPMonto = Convert.ToDouble(DtPagosAprobacionCxP.Rows[i]["Monto"]),
                        GAPIdChequera = DtPagosAprobacionCxP.Rows[i]["IdChequera"].ToString(),
                        GrupoRubro_Id = Convert.ToInt32(DtPagosAprobacionCxP.Rows[i]["GruporubroId"]),
                        GrupoRubroDescripcion = DtPagosAprobacionCxP.Rows[i]["GrupoRubro"].ToString(),
                        Rubro_Id = DtPagosAprobacionCxP.Rows[i]["RubroId"].ToString(),
                        RubroDescripcion = DtPagosAprobacionCxP.Rows[i]["Rubro"].ToString(),
                        CodigoMoneda = DtPagosAprobacionCxP.Rows[i]["CodigoMoneda"].ToString()
                    };
                    lstSolicitudOrdenPago.Add(objsolicitudordenpago);
                }
            }
            else
            {
                Wrkf_SolicitudOrdenPago objsolicitudordenpago = new Wrkf_SolicitudOrdenPago()
                {
                    Mensajex = "No existen registros para el rango de fecha especificado"
                };
                lstSolicitudOrdenPago.Add(objsolicitudordenpago);
            }
            return lstSolicitudOrdenPago;
        }

        /// <summary>
        /// Obtiene un detalle de la solicitud de la orden de pago.
        /// </summary>
        /// <returns></returns>
        public Wrkf_SolicitudOrdenPago GetDetalleOrdenPagoPorRevisarCxP(string pCodigo)
        {

            Wrkf_SolicitudOrdenPago SolicitudOrdenPagoCxP = new Wrkf_SolicitudOrdenPago();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pCodigo", pCodigo),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@pTituloError", SqlDbType.VarChar, 60),
            });

            Sqlprovider.Oparameters[1].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;

            DataTable DtsolicitudOrdenPago = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("WorkFlow.PL_Sel_DetallePago_Key", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            int total_registros = DtsolicitudOrdenPago.Rows.Count;

            if (total_registros > 0)
            {
                SolicitudOrdenPagoCxP.Codigo = DtsolicitudOrdenPago.Rows[0]["Codigo"].ToString();
                SolicitudOrdenPagoCxP.GAPCodigoItem = DtsolicitudOrdenPago.Rows[0]["CodigoItem"].ToString();
                SolicitudOrdenPagoCxP.GAPCodigoPlantilla = DtsolicitudOrdenPago.Rows[0]["CodigoPlantilla"].ToString();
                SolicitudOrdenPagoCxP.GAPDescripcionPlantilla = DtsolicitudOrdenPago.Rows[0]["DescripcionPlantilla"].ToString();
                SolicitudOrdenPagoCxP.GAPMonto = Convert.ToDouble(DtsolicitudOrdenPago.Rows[0]["Monto"]);
                SolicitudOrdenPagoCxP.GAPFechaFactura = DtsolicitudOrdenPago.Rows[0]["FechaFactura"].ToString();
                SolicitudOrdenPagoCxP.GAPFechaPago = DtsolicitudOrdenPago.Rows[0]["FechaPago"].ToString();
                SolicitudOrdenPagoCxP.GAPEstatus = Convert.ToInt32(DtsolicitudOrdenPago.Rows[0]["Estatus"]);
                SolicitudOrdenPagoCxP.GAPFechaAprobacion = DtsolicitudOrdenPago.Rows[0]["FechaAprobacion"].ToString();
                SolicitudOrdenPagoCxP.GAPUsuarioAprueba = DtsolicitudOrdenPago.Rows[0]["UsuarioAprueba"].ToString();
                SolicitudOrdenPagoCxP.GAPFechaRechazo = DtsolicitudOrdenPago.Rows[0]["FechaRechazo"].ToString();
                SolicitudOrdenPagoCxP.GAPUsuarioRechaza = DtsolicitudOrdenPago.Rows[0]["UsuarioRechaza"].ToString();
                SolicitudOrdenPagoCxP.GAPNroFactura = DtsolicitudOrdenPago.Rows[0]["NroFactura"].ToString();
                SolicitudOrdenPagoCxP.GAPNroControl = DtsolicitudOrdenPago.Rows[0]["NroControl"].ToString();
                SolicitudOrdenPagoCxP.GAPTipoPago = DtsolicitudOrdenPago.Rows[0]["TipoPago"].ToString();
                SolicitudOrdenPagoCxP.GAPIdChequera = DtsolicitudOrdenPago.Rows[0]["IdChequera"].ToString();
                SolicitudOrdenPagoCxP.GAPDescripcionFactura = DtsolicitudOrdenPago.Rows[0]["DescripcionFactura"].ToString();
                SolicitudOrdenPagoCxP.GAPTienda = DtsolicitudOrdenPago.Rows[0]["Tienda"].ToString();
                SolicitudOrdenPagoCxP.GAPIdProveedor = DtsolicitudOrdenPago.Rows[0]["IdProveedor"].ToString();
                SolicitudOrdenPagoCxP.GAPProveedor = DtsolicitudOrdenPago.Rows[0]["VENDNAME"].ToString();
                SolicitudOrdenPagoCxP.GrupoRubro_Id =Convert.ToInt32(DtsolicitudOrdenPago.Rows[0]["GrupoRubro_Id"]);
                SolicitudOrdenPagoCxP.GrupoRubroDescripcion = DtsolicitudOrdenPago.Rows[0]["GrupoRubroDescripcion"].ToString();
                SolicitudOrdenPagoCxP.Rubro_Id = DtsolicitudOrdenPago.Rows[0]["Rubro_Id"].ToString();
                SolicitudOrdenPagoCxP.RubroDescripcion = DtsolicitudOrdenPago.Rows[0]["rubroDescripcion"].ToString();
                SolicitudOrdenPagoCxP.DescripcionDocumento = DtsolicitudOrdenPago.Rows[0]["documento"].ToString();
                SolicitudOrdenPagoCxP.DescripcionTipoPago = DtsolicitudOrdenPago.Rows[0]["formadepago"].ToString();
                SolicitudOrdenPagoCxP.ObservacionRevisionCxP = DtsolicitudOrdenPago.Rows[0]["ObservacionRevisionCxP"].ToString();
                SolicitudOrdenPagoCxP.CodigoMoneda = DtsolicitudOrdenPago.Rows[0]["CodigoMoneda"].ToString();
                SolicitudOrdenPagoCxP.UsuarioRevisionCxP = DtsolicitudOrdenPago.Rows[0]["UsuarioRevisionCxP"].ToString();
                SolicitudOrdenPagoCxP.FechaRevisionCxP = DtsolicitudOrdenPago.Rows[0]["FechaRevisionCxP"].ToString();
                SolicitudOrdenPagoCxP.ObservacionRevisionCxP = DtsolicitudOrdenPago.Rows[0]["ObservacionRevisionCxP"].ToString();
                SolicitudOrdenPagoCxP.Tipodocumento = Convert.ToInt32(DtsolicitudOrdenPago.Rows[0]["tipodocumento_Id"]);
                SolicitudOrdenPagoCxP.GAPCodigoConcepto = Convert.ToInt32(DtsolicitudOrdenPago.Rows[0]["CodigoConcepto"]);
                SolicitudOrdenPagoCxP.TipoPago = Convert.ToInt32(DtsolicitudOrdenPago.Rows[0]["formapago_Id"]);
                SolicitudOrdenPagoCxP.GAPBaseImpIVAGeneral = Convert.ToDouble(DtsolicitudOrdenPago.Rows[0]["BaseImpIVARegular"]);
                SolicitudOrdenPagoCxP.GAPBaseImpIVAReducido = Convert.ToDouble(DtsolicitudOrdenPago.Rows[0]["BaseImpIVAReducido"]);
                SolicitudOrdenPagoCxP.GAPBaseImpIVAAdicional = Convert.ToDouble(DtsolicitudOrdenPago.Rows[0]["BaseImpIVAAdicional"]);
                SolicitudOrdenPagoCxP.GAPMontoExento = Convert.ToDouble(DtsolicitudOrdenPago.Rows[0]["MontoExento"]);
                SolicitudOrdenPagoCxP.Observaciones = DtsolicitudOrdenPago.Rows[0]["Observaciones"].ToString();
            }

            return SolicitudOrdenPagoCxP;
        }

        /// <summary>
        /// Actualiza los datos del pago en la tabla del workflow
        /// </summary>
        /// <param name="pcodigo"></param>
        /// <param name="pgruporubro_id"></param>
        /// <param name="prubro_id"></param>
        /// <param name="psoportesfacturas"></param>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion ActualizarGrupoRubro(string pcodigo, int pgruporubro_id, string prubro_id, bool psoportesfacturas, string pusuario, string pobservacion)
        {
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pCodigo", pcodigo),
                new SqlParameter("@pSoportesRecibido", psoportesfacturas),
                new SqlParameter("@pGrupoRubro_Id", pgruporubro_id),
                new SqlParameter("@pRubro_Id", prubro_id),
                new SqlParameter("@pUsuarioRevisionCxP", pusuario),
                new SqlParameter("@pObservacionRevisionCxP", pobservacion),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@pTituloError", SqlDbType.VarChar, 60)
            });

            Sqlprovider.Oparameters[6].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[7].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[8].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[9].Direction = ParameterDirection.Output;

            //optener los resultados del procedimiento almacenado
            //Dictionary<string, string> outparam; = new Dictionary<string, string>();
            Sqlprovider.ExecuteStoredProcedureWithOutputParameter2("WorkFlow.PL_InsUpd_ActualizarGrupoRubro", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            //Obtiene El Error Generado Desde El Procedimiento Almacenado Workflow.sp_insup_solicitudordenpago.
            if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
            {
                objRespuestaOperacion.Codigox = outparam["@pCodigoError"];
                objRespuestaOperacion.Mensajex = outparam["@pMensajeError"];
                objRespuestaOperacion.Tipox = outparam["@pTipoError"];
                objRespuestaOperacion.Titulox = outparam["@pTituloError"];
            }
            else
            {
                objRespuestaOperacion.Codigox = string.Empty;
                objRespuestaOperacion.Mensajex = string.Empty;
                objRespuestaOperacion.Tipox = string.Empty;
                objRespuestaOperacion.Titulox = string.Empty;
            }

            return objRespuestaOperacion;
        }

        /// <summary>
        /// Actualiza los datos del pago en la tabla del workflow
        /// </summary>
        /// <param name="pcodigo"></param>
        /// <param name="pgruporubro_id"></param>
        /// <param name="prubro_id"></param>
        /// <param name="psoportesfacturas"></param>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion AprobarRechazarPagosCxP(string plistapagosaprobados, string plistapagosrechazados, string plistadopagossoportes, string pusuario, string pobservacion)
        {
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@plistadoaprobacion", plistapagosaprobados),
                new SqlParameter("@plistadorechazo", plistapagosrechazados),
                new SqlParameter("@plistadosoportes", plistadopagossoportes),
                new SqlParameter("@pUsuario", pusuario),
                new SqlParameter("@pObservacion", pobservacion),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@pTituloError", SqlDbType.VarChar, 60)
            });

            Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[6].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[7].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[8].Direction = ParameterDirection.Output;

            //optener los resultados del procedimiento almacenado
            //Dictionary<string, string> outparam; = new Dictionary<string, string>();
            Sqlprovider.ExecuteStoredProcedureWithOutputParameter2("WorkFlow.PL_Up_AprobarRechazarPagosCxP", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            //Obtiene El Error Generado Desde El Procedimiento Almacenado Workflow.sp_insup_solicitudordenpago.
            if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
            {
                objRespuestaOperacion.Codigox = outparam["@pCodigoError"];
                objRespuestaOperacion.Mensajex = outparam["@pMensajeError"];
                objRespuestaOperacion.Tipox = outparam["@pTipoError"];
                objRespuestaOperacion.Titulox = outparam["@pTituloError"];
            }
            else
            {
                objRespuestaOperacion.Codigox = string.Empty;
                objRespuestaOperacion.Mensajex = string.Empty;
                objRespuestaOperacion.Tipox = string.Empty;
                objRespuestaOperacion.Titulox = string.Empty;
            }

            return objRespuestaOperacion;
        }

        ///// <summary>
        ///// El método lista los pagos asociados a un rubro especificado
        ///// </summary>
        ///// <param name="rubro_id"></param>
        ///// <returns></returns>
        //public List<Wrkf_ListaPagosPorRubroId> ListaPagosPorRubroId(string rubro_id, DateTime fechadesde, DateTime fechahasta)
        //{

        //    List<Wrkf_ListaPagosPorRubroId> lstlistapagosporrubroid = new List<Wrkf_ListaPagosPorRubroId>();
        //    double totalapagarx;
        //    double totalglobalapagarx = 0;

        //    SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
        //    Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
        //        new SqlParameter("@pRubro_Id", rubro_id),
        //        new SqlParameter("@pfechapago_desde", fechadesde),
        //        new SqlParameter("@pfechapago_hasta", fechahasta)
        //    });

        //    DataTable Dtlistapagosporrubroid = Sqlprovider.ExecuteStoredProcedure("workflow.PL_Sel_ListaPagosPorRubroId", CommandType.StoredProcedure);

        //    int total_registros = Dtlistapagosporrubroid.Rows.Count;

        //    if (total_registros > 0)
        //    {
        //        for (int i = 0; i < total_registros; i++)
        //        {
        //            totalapagarx = Convert.ToDouble(Dtlistapagosporrubroid.Rows[i]["Total"]);
        //            //totaliza el total a pagar
        //            totalglobalapagarx = totalglobalapagarx + totalapagarx;

        //            Wrkf_ListaPagosPorRubroId objlistapagosporrubroid = new Wrkf_ListaPagosPorRubroId()
        //            {
        //                Solicitudordenpagodetalle_Idx = Convert.ToInt32(Dtlistapagosporrubroid.Rows[i]["Solicitudordenpagodetalle_Id"]),
        //                Solicitudordenpago_Idx = Convert.ToInt32(Dtlistapagosporrubroid.Rows[i]["Solicitudordenpago_Id"]),
        //                Documentox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["documento"]),
        //                Proveedorx = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["Proveedor"]),
        //                Descripcionx = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["Descripcion"]),
        //                Numerodocumentox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["Numerodocumento"]),
        //                Totalx = totalapagarx.ToString("N", new CultureInfo("is-IS")),
        //                TotalGlobalAPagarx = totalglobalapagarx.ToString("N", new CultureInfo("is-IS")),
        //                DescripcionRubrox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["DescripcionRubro"]),
        //                FechaRegistrox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["FechaRegistro"]),
        //                FechaPagox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["Fechapago"]),
        //                formadepagox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["formadepago"]),
        //                formapago_Idx = Convert.ToInt32(Dtlistapagosporrubroid.Rows[i]["formapago_Id"]),
        //                Observacionesx = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["TieneObservaciones"]),
        //                Chequerax = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["Chekbkid"]),
        //                DocumentoRecibidox = Convert.ToBoolean(Dtlistapagosporrubroid.Rows[i]["DocumentoRecibido"]),
        //                TotalDocumentosx = Convert.ToInt32(Dtlistapagosporrubroid.Rows[i]["TotalDocumentos"]),
        //                ObservacionRechaSubContrax = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["ObservacionRechaSubContra"]),
        //                Curncyidx = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["curncyid"]),
        //                FechaDocumentox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["FechaDocumento"])
        //            };

        //            lstlistapagosporrubroid.Add(objlistapagosporrubroid);
        //        }
        //    }
        //    else
        //    {
        //        Wrkf_ListaPagosPorRubroId objlistapagosporrubroid = new Wrkf_ListaPagosPorRubroId();
        //        lstlistapagosporrubroid.Add(objlistapagosporrubroid);
        //    }

        //    return lstlistapagosporrubroid;
        //}

        ///// <summary>
        ///// El método lista las notas de creditos asociadas a un rubro especificado
        ///// </summary>
        ///// <param name="rubro_id"></param>
        ///// <returns></returns>
        //public List<Wrkf_ListaPagosPorRubroId> ListaNotasCreditoPorRubroId(string rubro_id, DateTime fechadesde, DateTime fechahasta)
        //{

        //    List<Wrkf_ListaPagosPorRubroId> lstlistapagosporrubroid = new List<Wrkf_ListaPagosPorRubroId>();
        //    double totalapagarx;
        //    double totalglobalapagarx = 0;

        //    SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
        //    Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
        //        new SqlParameter("@pRubro_Id", rubro_id),
        //        new SqlParameter("@pfechapago_desde", fechadesde),
        //        new SqlParameter("@pfechapago_hasta", fechahasta)
        //    });

        //    DataTable Dtlistapagosporrubroid = Sqlprovider.ExecuteStoredProcedure("workflow.PL_Sel_ListaNotaCreditoPorRubroId", CommandType.StoredProcedure);

        //    int total_registros = Dtlistapagosporrubroid.Rows.Count;

        //    if (total_registros > 0)
        //    {
        //        for (int i = 0; i < total_registros; i++)
        //        {
        //            totalapagarx = Convert.ToDouble(Dtlistapagosporrubroid.Rows[i]["Total"]);
        //            //totaliza el total a pagar
        //            totalglobalapagarx = totalglobalapagarx + totalapagarx;

        //            Wrkf_ListaPagosPorRubroId objlistapagosporrubroid = new Wrkf_ListaPagosPorRubroId()
        //            {
        //                Solicitudordenpagodetalle_Idx = Convert.ToInt32(Dtlistapagosporrubroid.Rows[i]["Solicitudordenpagodetalle_Id"]),
        //                Solicitudordenpago_Idx = Convert.ToInt32(Dtlistapagosporrubroid.Rows[i]["Solicitudordenpago_Id"]),
        //                Documentox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["documento"]),
        //                Proveedorx = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["Proveedor"]),
        //                Descripcionx = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["Descripcion"]),
        //                Numerodocumentox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["Numerodocumento"]),
        //                Totalx = totalapagarx.ToString("N", new CultureInfo("is-IS")),
        //                TotalGlobalAPagarx = totalglobalapagarx.ToString("N", new CultureInfo("is-IS")),
        //                DescripcionRubrox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["DescripcionRubro"]),
        //                FechaRegistrox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["FechaRegistro"]),
        //                FechaPagox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["Fechapago"]),
        //                formadepagox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["formadepago"]),
        //                formapago_Idx = Convert.ToInt32(Dtlistapagosporrubroid.Rows[i]["formapago_Id"]),
        //                Observacionesx = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["TieneObservaciones"]),
        //                Chequerax = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["Chekbkid"]),
        //                DocumentoRecibidox = Convert.ToBoolean(Dtlistapagosporrubroid.Rows[i]["DocumentoRecibido"]),
        //                TotalDocumentosx = Convert.ToInt32(Dtlistapagosporrubroid.Rows[i]["TotalDocumentos"]),
        //                Curncyidx = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["curncyid"]),
        //                FechaDocumentox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["FechaDocumento"])
        //            };

        //            lstlistapagosporrubroid.Add(objlistapagosporrubroid);
        //        }
        //    }
        //    else
        //    {
        //        Wrkf_ListaPagosPorRubroId objlistapagosporrubroid = new Wrkf_ListaPagosPorRubroId();
        //        lstlistapagosporrubroid.Add(objlistapagosporrubroid);
        //    }

        //    return lstlistapagosporrubroid;
        //}

        /// <summary>
        /// Actualiza el número de chequera en la solicitud del pago
        /// </summary>
        /// <param name="Solicitudordenpagodetalle_Id"></param>
        /// <param name="Solicitudordenpago_Id"></param>
        /// <param name="Chekbkid"></param>
        /// <param name="Usuariomodifico"></param>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion ActualizarNumeroChequeraRevisionCxP(int Solicitudordenpagodetalle_Id, int Solicitudordenpago_Id, string Chekbkid, string CurrencyCode, string Usuariomodifico)
        {
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@Solicitudordenpagodetalle_Id", Solicitudordenpagodetalle_Id),
                new SqlParameter("@Solicitudordenpago_Id", Solicitudordenpago_Id),
                new SqlParameter("@Chekbkid", Chekbkid),
                new SqlParameter("@Currencycode", CurrencyCode),
                new SqlParameter("@Usuariomodifico", Usuariomodifico),
                new SqlParameter("@CodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@MensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@Tipo", SqlDbType.VarChar, 20),
                new SqlParameter("@Titulo", SqlDbType.VarChar, 50)
            });

            Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[6].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[7].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[8].Direction = ParameterDirection.Output;

            //optener los resultados del procedimiento almacenado
            //Dictionary<string, string> outparam; = new Dictionary<string, string>();
            int Result = Sqlprovider.ExecuteStoredProcedureWithOutputParameter2("Workflow.sp_up_ActualizarNumerochequeraCxP", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            //Obtiene El Error Generado Desde El Procedimiento Almacenado Workflow.sp_insup_solicitudordenpago.
            if (!string.IsNullOrEmpty(outparam["@CodigoError"]))
            {
                objRespuestaOperacion.NumeroRegistroOrdenPagox = -1;
                objRespuestaOperacion.NumeroRegistroDetallePagox = -1;
                objRespuestaOperacion.RegistrosProcesadosx = 0;
                objRespuestaOperacion.Codigox = outparam["@CodigoError"];
                objRespuestaOperacion.Mensajex = outparam["@MensajeError"];
                objRespuestaOperacion.Tipox = outparam["@Tipo"];
                objRespuestaOperacion.Titulox = outparam["@Titulo"];
            }
            else
            {
                objRespuestaOperacion.RegistrosProcesadosx = Result;
                objRespuestaOperacion.Codigox = string.Empty;
                objRespuestaOperacion.Mensajex = string.Empty;
                objRespuestaOperacion.Tipox = string.Empty;
                objRespuestaOperacion.Titulox = string.Empty;
            }

            return objRespuestaOperacion;
        }

        /// <summary>
        /// Obtiene las observaciones de la solicitud de pago seleccionada
        /// </summary>
        /// <param name="Solicitudordenpagodetalle_Id"></param>
        /// <param name="Solicitudordenpago_Id"></param>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion ObtenerObservacionesSolicitudPago(int Solicitudordenpagodetalle_Id, int Solicitudordenpago_Id)
        {
            Wrkf_RespuestaOperacion objrespuestaoperacion = new Wrkf_RespuestaOperacion();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@Solicitudordenpagodetalle_Id", Solicitudordenpagodetalle_Id),
                new SqlParameter("@Solicitudordenpago_Id", Solicitudordenpago_Id)
            });

            string strQuery = "select Proveedor +' : ' + observaciones as Observaciones ";
            strQuery += "from Workflow.SolicitudOrdenPagoDetalle where Solicitudordenpagodetalle_Id = @Solicitudordenpagodetalle_Id and Solicitudordenpago_Id = @Solicitudordenpago_Id";

            DataTable DtObservacionesSol = Sqlprovider.ExecuteStoredProcedure(strQuery, CommandType.Text);

            int total_registros = DtObservacionesSol.Rows.Count;

            if (total_registros > 0)
            {
                objrespuestaoperacion.Observacionesx = DtObservacionesSol.Rows[0]["Observaciones"].ToString().Trim();
            }

            return objrespuestaoperacion;
        }

        /// <summary>
        /// Devuelve una lista con los datos de la cabecera y el detalle de la solicitud de la orden de pago por ID
        /// </summary>
        /// <param name="Solicitudordenpagodetalle_Id"></param>
        /// <param name="Solicitudordenpago_Id"></param>
        /// <returns></returns>
        public List<Wrkf_SolicitudOrdenPagoEncabDetal> ObtenerSolicitudOrdenPagoEncabDetallePorId(int Solicitudordenpagodetalle_Id, int Solicitudordenpago_Id)
        {
            List<Wrkf_SolicitudOrdenPagoEncabDetal> lstencabezadodetalleid = new List<Wrkf_SolicitudOrdenPagoEncabDetal>();
            Wrkf_SolicitudOrdenPagoEncabDetal objsolicitudordenpagoencdetal = new Wrkf_SolicitudOrdenPagoEncabDetal();
            List<MensajeError> lstmensajeerror = new List<MensajeError>();
            MensajeError mensajeerror = new MensajeError();
            double TotalSolicitudPago;
            double Preciounitario;
            double Subtotal;
            double Anticipo;
            double Total;
            double MontoIva;
            double PorcentajeIva;
            double PorcentajeRetencion;
            double TotalRetenido;

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@Solicitudordenpago_Id", Solicitudordenpago_Id),
                new SqlParameter("@Solicitudordenpagodetalle_Id", Solicitudordenpagodetalle_Id),
                new SqlParameter("@CodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@MensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@Tipo", SqlDbType.VarChar, 20),
                new SqlParameter("@Titulo", SqlDbType.VarChar, 50)
            });

            Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;

            //optener los resultados del procedimiento almacenado
            //Dictionary<string, string> outparam; = new Dictionary<string, string>();
            DataTable DtEncabDetallePorId = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("Workflow.sp_sel_SolicitudOrdenPagoEncabDetallePorId", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            //Obtiene El Error Generado Desde El Procedimiento Almacenado Workflow.sp_insup_solicitudordenpago.
            if (!string.IsNullOrEmpty(outparam["@CodigoError"]))
            {
                mensajeerror.Codigox = outparam["@CodigoError"];
                mensajeerror.Mensajex = outparam["@MensajeError"];
                mensajeerror.Tipox = outparam["@Tipo"];
                mensajeerror.Titulox = outparam["@Titulo"];
                lstmensajeerror.Add(mensajeerror);
            }
            else
            {
                int total_elementos = DtEncabDetallePorId.Rows.Count;

                if (total_elementos > 0)
                {
                    objsolicitudordenpagoencdetal.Solicitudordenpago_Idx = DtEncabDetallePorId.Rows[0]["Solicitudordenpago_Id"].ToString();

                    objsolicitudordenpagoencdetal.Codigoplantillax = DtEncabDetallePorId.Rows[0]["Codigoplantilla"].ToString();

                    objsolicitudordenpagoencdetal.Nombreplantillax = DtEncabDetallePorId.Rows[0]["Nombreplantilla"].ToString();

                    objsolicitudordenpagoencdetal.Recibidocxpx = DtEncabDetallePorId.Rows[0]["Recibidocxp"].ToString();

                    objsolicitudordenpagoencdetal.Aprobadocontraloriax = DtEncabDetallePorId.Rows[0]["Aprobadocontraloria"].ToString();

                    objsolicitudordenpagoencdetal.Aprobadovpx = DtEncabDetallePorId.Rows[0]["Aprobadovp"].ToString();

                    objsolicitudordenpagoencdetal.Aplicadotesoreriax = DtEncabDetallePorId.Rows[0]["Aplicadotesoreria"].ToString();

                    objsolicitudordenpagoencdetal.Anuladax = DtEncabDetallePorId.Rows[0]["Anulada"].ToString();

                    objsolicitudordenpagoencdetal.Fecharegistrox = DtEncabDetallePorId.Rows[0]["Fecharegistro"].ToString();

                    objsolicitudordenpagoencdetal.Usuarioregistrox = DtEncabDetallePorId.Rows[0]["Usuarioregistro"].ToString();

                    objsolicitudordenpagoencdetal.Fechamodificacionx = DtEncabDetallePorId.Rows[0]["Fechamodificacion"].ToString();

                    objsolicitudordenpagoencdetal.Usuariomodificox = DtEncabDetallePorId.Rows[0]["Usuariomodifico"].ToString();

                    objsolicitudordenpagoencdetal.Curncyid = DtEncabDetallePorId.Rows[0]["curncyid"].ToString();

                    objsolicitudordenpagoencdetal.Urgentex = DtEncabDetallePorId.Rows[0]["Urgente"].ToString();

                    objsolicitudordenpagoencdetal.Enviadoacxpx = DtEncabDetallePorId.Rows[0]["Enviadoacxp"].ToString();

                    TotalSolicitudPago = Convert.ToDouble(DtEncabDetallePorId.Rows[0]["TotalSolicitudPago"]);
                    objsolicitudordenpagoencdetal.TotalSolicitudPagox = TotalSolicitudPago.ToString("N", new CultureInfo("is-IS"));

                    objsolicitudordenpagoencdetal.UsuarioAnulacionx = DtEncabDetallePorId.Rows[0]["UsuarioAnulacion"].ToString();

                    objsolicitudordenpagoencdetal.FechaAnulacionx = DtEncabDetallePorId.Rows[0]["FechaAnulacion"].ToString();

                    objsolicitudordenpagoencdetal.Solicitudordenpagodetalle_Idx = DtEncabDetallePorId.Rows[0]["Solicitudordenpagodetalle_Id"].ToString();

                    objsolicitudordenpagoencdetal.Rifx = DtEncabDetallePorId.Rows[0]["Rif"].ToString();

                    objsolicitudordenpagoencdetal.Proveedorx = DtEncabDetallePorId.Rows[0]["Proveedor"].ToString();

                    objsolicitudordenpagoencdetal.Descripcionx = DtEncabDetallePorId.Rows[0]["Descripcion"].ToString();

                    objsolicitudordenpagoencdetal.Numerodocumentox = DtEncabDetallePorId.Rows[0]["Numerodocumento"].ToString();

                    objsolicitudordenpagoencdetal.Cantidadx = DtEncabDetallePorId.Rows[0]["Cantidad"].ToString();

                    Preciounitario = Convert.ToDouble(DtEncabDetallePorId.Rows[0]["Preciounitario"]);
                    objsolicitudordenpagoencdetal.Preciounitariox = Preciounitario.ToString("N", new CultureInfo("is-IS"));

                    Subtotal = Convert.ToDouble(DtEncabDetallePorId.Rows[0]["Subtotal"]);
                    objsolicitudordenpagoencdetal.Subtotalx = Subtotal.ToString("N", new CultureInfo("is-IS"));

                    Anticipo = Convert.ToDouble(DtEncabDetallePorId.Rows[0]["Anticipo"]);
                    objsolicitudordenpagoencdetal.Anticipox = Anticipo.ToString("N", new CultureInfo("is-IS"));

                    Total = Convert.ToDouble(DtEncabDetallePorId.Rows[0]["Total"]);
                    objsolicitudordenpagoencdetal.Totalx = Total.ToString("N", new CultureInfo("is-IS"));

                    objsolicitudordenpagoencdetal.Aprobadox = DtEncabDetallePorId.Rows[0]["Aprobado"].ToString();

                    objsolicitudordenpagoencdetal.Tipodocumentox = DtEncabDetallePorId.Rows[0]["Tipodocumento"].ToString();

                    objsolicitudordenpagoencdetal.DescripTipoDocumentox = DtEncabDetallePorId.Rows[0]["documento"].ToString();

                    objsolicitudordenpagoencdetal.Calculaivax = DtEncabDetallePorId.Rows[0]["Calculaiva"].ToString();

                    objsolicitudordenpagoencdetal.Realizaretencionx = DtEncabDetallePorId.Rows[0]["Realizaretencion"].ToString();

                    PorcentajeIva = Convert.ToDouble(DtEncabDetallePorId.Rows[0]["Porcentajeiva"].ToString());
                    objsolicitudordenpagoencdetal.Porcentajeivax = PorcentajeIva.ToString("N", new CultureInfo("is-IS"));

                    MontoIva = Convert.ToDouble(DtEncabDetallePorId.Rows[0]["Montoiva"]);
                    objsolicitudordenpagoencdetal.MontoIvax = MontoIva.ToString("N", new CultureInfo("is-IS"));

                    PorcentajeRetencion = Convert.ToDouble(DtEncabDetallePorId.Rows[0]["Porcentajeretencion"]);
                    objsolicitudordenpagoencdetal.Porcentajeretencionx = PorcentajeRetencion.ToString("N", new CultureInfo("is-IS"));

                    TotalRetenido = Convert.ToDouble(DtEncabDetallePorId.Rows[0]["Totalretenido"]);
                    objsolicitudordenpagoencdetal.Totalretenidox = TotalRetenido.ToString("N", new CultureInfo("is-IS"));

                    objsolicitudordenpagoencdetal.Gruporubro_Idx = DtEncabDetallePorId.Rows[0]["Gruporubro_Id"].ToString();

                    objsolicitudordenpagoencdetal.DescripGrupoRubrox = DtEncabDetallePorId.Rows[0]["DescripGrupoRubro"].ToString();

                    objsolicitudordenpagoencdetal.Rubro_Idx = DtEncabDetallePorId.Rows[0]["Rubro_Id"].ToString();

                    objsolicitudordenpagoencdetal.DescripRubrox = DtEncabDetallePorId.Rows[0]["DescripRubro"].ToString();

                    objsolicitudordenpagoencdetal.Fechapagox = DtEncabDetallePorId.Rows[0]["Fechapago"].ToString();

                    objsolicitudordenpagoencdetal.Formapago_Idx = DtEncabDetallePorId.Rows[0]["formapago_Id"].ToString();

                    objsolicitudordenpagoencdetal.DescripFormaPagox = DtEncabDetallePorId.Rows[0]["DescripFormaPago"].ToString();

                    objsolicitudordenpagoencdetal.Observacionesx = DtEncabDetallePorId.Rows[0]["observaciones"].ToString();

                    objsolicitudordenpagoencdetal.anuladaDetallex = DtEncabDetallePorId.Rows[0]["AnuladaDetalle"].ToString();

                    objsolicitudordenpagoencdetal.UsuarioAnulacionDetallex = DtEncabDetallePorId.Rows[0]["UsuarioAnulacionDetalle"].ToString();

                    objsolicitudordenpagoencdetal.FechaAnulacionDetallex = DtEncabDetallePorId.Rows[0]["FechaAnulacionDetalle"].ToString();

                    objsolicitudordenpagoencdetal.Chekbkidx = DtEncabDetallePorId.Rows[0]["Chekbkid"].ToString();

                    objsolicitudordenpagoencdetal.UsuariomodificoDetallex = DtEncabDetallePorId.Rows[0]["UsuariomodificoDetalle"].ToString();

                    objsolicitudordenpagoencdetal.FechamodificoDetallex = DtEncabDetallePorId.Rows[0]["FechamodificoDetalle"].ToString();

                    objsolicitudordenpagoencdetal.DocumentoRecibidox = DtEncabDetallePorId.Rows[0]["DocumentoRecibido"].ToString();

                    lstencabezadodetalleid.Add(objsolicitudordenpagoencdetal);
                }

                mensajeerror.Codigox = string.Empty;
                mensajeerror.Mensajex = string.Empty;
                mensajeerror.Tipox = string.Empty;
                mensajeerror.Titulox = string.Empty;
                lstmensajeerror.Add(mensajeerror);
            }

            return lstencabezadodetalleid;
        }

        /// <summary>
        /// Corfirmar la recepción de los documentos de soporte por CxP
        /// </summary>
        /// <param name="Solicitudordenpagodetalle_Id"></param>
        /// <param name="DocumentoRecibido"></param>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion ConfirmarRecepcionSoporteCxP(int Solicitudordenpagodetalle_Id, bool DocumentoRecibido)
        {
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@Solicitudordenpagodetalle_Id", Solicitudordenpagodetalle_Id),
                new SqlParameter("@DocumentoRecibido", DocumentoRecibido),
                new SqlParameter("@CodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@MensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@Tipo", SqlDbType.VarChar, 20),
                new SqlParameter("@Titulo", SqlDbType.VarChar, 50)
            });

            Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;

            //optener los resultados del procedimiento almacenado
            //Dictionary<string, string> outparam; = new Dictionary<string, string>();
            int Result = Sqlprovider.ExecuteStoredProcedureWithOutputParameter2("Workflow.sp_up_ConfirmaRecepcionDocumento", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            //Obtiene El Error Generado Desde El Procedimiento Almacenado Workflow.sp_insup_solicitudordenpago.
            if (!string.IsNullOrEmpty(outparam["@CodigoError"]))
            {
                objRespuestaOperacion.NumeroRegistroOrdenPagox = -1;
                objRespuestaOperacion.NumeroRegistroDetallePagox = -1;
                objRespuestaOperacion.RegistrosProcesadosx = 0;
                objRespuestaOperacion.Codigox = outparam["@CodigoError"];
                objRespuestaOperacion.Mensajex = outparam["@MensajeError"];
                objRespuestaOperacion.Tipox = outparam["@Tipo"];
                objRespuestaOperacion.Titulox = outparam["@Titulo"];
            }
            else
            {
                objRespuestaOperacion.RegistrosProcesadosx = Result;
                objRespuestaOperacion.Codigox = string.Empty;
                objRespuestaOperacion.Mensajex = string.Empty;
                objRespuestaOperacion.Tipox = string.Empty;
                objRespuestaOperacion.Titulox = string.Empty;
            }

            return objRespuestaOperacion;
        }

        /// <summary>
        /// CxP Aprueba el pago que se encuentra en la solicitud 
        /// </summary>
        /// <param name="Solicitudordenpagodetalle_Id"></param>
        /// <param name="Aprobado"></param>
        /// <param name="UsuarioAprobacionCxP"></param>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion AprobacionPorCxP(string listapagosaprobados, string listapagosrechazados, string usuario)
        {
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@listadoaprobacion", listapagosaprobados),
                new SqlParameter("@listadorechazo", listapagosrechazados),
                new SqlParameter("@Usuarioaprobacion", usuario),
                new SqlParameter("@CodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@MensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@Tipo", SqlDbType.VarChar, 20),
                new SqlParameter("@Titulo", SqlDbType.VarChar, 50)
            });

            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[6].Direction = ParameterDirection.Output;

            //optener los resultados del procedimiento almacenado
            //Dictionary<string, string> outparam; = new Dictionary<string, string>();
            int Result = Sqlprovider.ExecuteStoredProcedureWithOutputParameter2("Workflow.sp_up_AprobacionPorCxP", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            //Obtiene El Error Generado Desde El Procedimiento Almacenado [Workflow].[sp_up_AprobarRechazarSolicitudSubContraloria].
            if (!string.IsNullOrEmpty(outparam["@CodigoError"]))
            {
                objRespuestaOperacion.NumeroRegistroOrdenPagox = -1;
                objRespuestaOperacion.NumeroRegistroDetallePagox = -1;
                objRespuestaOperacion.RegistrosProcesadosx = 0;
                objRespuestaOperacion.Codigox = outparam["@CodigoError"];
                objRespuestaOperacion.Mensajex = outparam["@MensajeError"];
                objRespuestaOperacion.Tipox = outparam["@Tipo"];
                objRespuestaOperacion.Titulox = outparam["@Titulo"];
            }
            else
            {
                objRespuestaOperacion.RegistrosProcesadosx = Result;
                objRespuestaOperacion.Codigox = string.Empty;
                objRespuestaOperacion.Mensajex = string.Empty;
                objRespuestaOperacion.Tipox = string.Empty;
                objRespuestaOperacion.Titulox = string.Empty;
            }

            return objRespuestaOperacion;
        }

        /// <summary>
        /// Obtiene el motivo del rechazo por cada solicitud
        /// </summary>
        /// <param name="Solicitudordenpagodetalle_Id"></param>
        /// <returns></returns>
        public List<Wrkf_RespuestaOperacion> ObtenerMotivoRechazoSubContraloria(int Solicitudordenpagodetalle_Id)
        {
            List<Wrkf_RespuestaOperacion> lstrespuestaoperacion = new List<Wrkf_RespuestaOperacion>();
            Wrkf_RespuestaOperacion objwrkfrespuestaoperacion = new Wrkf_RespuestaOperacion();

            try
            {
                SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@Solicitudordenpagodetalle_Id", Solicitudordenpagodetalle_Id)
                });

                string sqlQuery = "select ObservacionRechaSubContra from Workflow.SolicitudOrdenPagoDetalle where Solicitudordenpagodetalle_Id = @Solicitudordenpagodetalle_Id";

                DataTable DtObservacionesSol = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

                int total_registros = DtObservacionesSol.Rows.Count;

                if (total_registros > 0)
                {
                    objwrkfrespuestaoperacion.Observacionesx = DtObservacionesSol.Rows[0]["ObservacionRechaSubContra"].ToString().Trim();
                }

                lstrespuestaoperacion.Add(objwrkfrespuestaoperacion);
            }
            catch (Exception ex)
            {
                objwrkfrespuestaoperacion.Codigox = ex.HResult.ToString();
                objwrkfrespuestaoperacion.Mensajex = ex.Message.ToString();
                objwrkfrespuestaoperacion.Tipox = "error";
                objwrkfrespuestaoperacion.Titulox = "Aprobación o Rechazo de la Solicitud por Cuentas Por Pagar";

                lstrespuestaoperacion.Add(objwrkfrespuestaoperacion);
            }

            return lstrespuestaoperacion;
        }
    }
}