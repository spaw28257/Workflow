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
    /// <summary>
    /// La clase permite realizar las operaciones en la base de datos
    /// </summary>
    public class Wrkf_DbSolicitudOrdenPago
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Wrkf_DbSolicitudOrdenPago()
        {
        }

        /// <summary>
        /// Obtiene el proximo correlativo para asignarlo a la solicitud de orden de pago
        /// </summary>
        /// <param name="CodigoPlantilla"></param>
        /// <returns></returns>
        public string ProximoCorrelativo(string CodigoPlantilla)
        {
            string ProximoCorrelativo;

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@CodigoPlantilla", CodigoPlantilla)
            });

            string sqlQuery = "SELECT GestionPago.FU_ProximoCorrelativoPagoNoFrecuente(@CodigoPlantilla)";

            DataTable DtProximoCorrelativo = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

            if (DtProximoCorrelativo.Rows.Count > 0)
            {
                ProximoCorrelativo = DtProximoCorrelativo.Rows[0][0].ToString();
            }
            else
            {
                ProximoCorrelativo = string.Empty;
            }

            return ProximoCorrelativo;
        }

        /// <summary>
        /// Obtiene el proximo correlativo del item para los pagos no frecuentes
        /// </summary>
        /// <returns></returns>
        public string ProximoCorrelativoItemNoFrecuente()
        {
            string ProximoCorrelativoItemnoFrecuente;

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
            });

            string sqlQuery = "SELECT GestionPago.FU_ProximoCorrelativoItemNoFrecuente()";

            DataTable DtProximoCorrelativoItemNoFrecuente = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

            if (DtProximoCorrelativoItemNoFrecuente.Rows.Count > 0)
            {
                ProximoCorrelativoItemnoFrecuente = DtProximoCorrelativoItemNoFrecuente.Rows[0][0].ToString();
            }
            else
            {
                ProximoCorrelativoItemnoFrecuente = string.Empty;
            }

            return ProximoCorrelativoItemnoFrecuente;
        }

        /// <summary>
        /// Registra la solicitud de la orden del pago
        /// </summary>
        /// <param name="OrdenPago"></param>
        /// <param name="OrdenPagoDetalle"></param>
        /// <param name="Usuario"></param>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion AddSolicitudOrdenPago(Wrkf_SolicitudOrdenPago OrdenPago, string Usuario)
        {
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();
            ConvertExtension objConvertExtension = new ConvertExtension();

            string vFechaFactura = objConvertExtension.FormatoFechayyyyMMdd(Convert.ToDateTime(OrdenPago.GAPFechaFactura)).ToString();
            string vFechaPago = objConvertExtension.FormatoFechayyyyMMdd(Convert.ToDateTime(OrdenPago.GAPFechaPago)).ToString();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pCodigo", OrdenPago.Codigo),
                new SqlParameter("@pGAPCodigoPlantilla", OrdenPago.GAPCodigoPlantilla),
                new SqlParameter("@pGAPMonto", OrdenPago.GAPMonto),
                new SqlParameter("@pGAPDescripcionFactura", OrdenPago.GAPDescripcionFactura),
                new SqlParameter("@pGAPFechaFactura", vFechaFactura),
                new SqlParameter("@pGAPFechaPago", vFechaPago),
                new SqlParameter("@pGAPEstatus", OrdenPago.GAPEstatus),
                new SqlParameter("@pGAPNroFactura", OrdenPago.GAPNroFactura),
                new SqlParameter("@pGAPNroControl", OrdenPago.GAPNroControl),
                new SqlParameter("@pGAPTipoPago", OrdenPago.GAPTipoPago),
                new SqlParameter("@pGAPCodigoConcepto", OrdenPago.GAPCodigoConcepto),
                new SqlParameter("@pGAPIdProveedor", OrdenPago.GAPIdProveedor),
                new SqlParameter("@pGAPProveedor", OrdenPago.GAPProveedor),
                new SqlParameter("@pGAPRIF", OrdenPago.GAPRIF),
                new SqlParameter("@pGAPCuentaBancaria", OrdenPago.GAPCuentaBancaria),
                new SqlParameter("@pGAPEmail", OrdenPago.GAPEmail),
                new SqlParameter("@pGAPPorcentajeRetencion", OrdenPago.GAPPorcentajeRetencion),
                new SqlParameter("@pGAPMontoExento", OrdenPago.GAPMontoExento),
                new SqlParameter("@pGAPBaseImpIVAAdicional", OrdenPago.GAPBaseImpIVAAdicional),
                new SqlParameter("@pGAPBaseImpIVAReducido", OrdenPago.GAPBaseImpIVAReducido),
                new SqlParameter("@pGAPBaseImpIVAGeneral", OrdenPago.GAPBaseImpIVAGeneral),
                new SqlParameter("@pGAPTienda", OrdenPago.GAPTienda),
                new SqlParameter("@pGAPUsuarioAprueba", OrdenPago.GAPUsuarioAprueba),
                new SqlParameter("@pGAPFechaAprobacion", OrdenPago.GAPFechaAprobacion),
                new SqlParameter("@pGrupoRubro_Id", OrdenPago.GrupoRubro_Id),
                new SqlParameter("@pRubro_Id", OrdenPago.Rubro_Id),
                new SqlParameter("@pTipodocumento", OrdenPago.Tipodocumento),
                new SqlParameter("@pTipoPago", OrdenPago.TipoPago),
                new SqlParameter("@pMontoIva", OrdenPago.MontoIva),
                new SqlParameter("@pMontoRetenido", OrdenPago.MontoRetenido),
                new SqlParameter("@pPlanImpuesto", OrdenPago.PlanImpuesto),
                new SqlParameter("@pCodigoMoneda", OrdenPago.CodigoMoneda),
                new SqlParameter("@pObservaciones", OrdenPago.Observaciones),
                new SqlParameter("@pUsuarioCreacion", Usuario),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@pTituloError", SqlDbType.VarChar, 60),
                new SqlParameter("@pNumeroSolicitudPago", SqlDbType.VarChar, 11),
                new SqlParameter("@pNumeroItem", SqlDbType.VarChar, 7)
            });

            Sqlprovider.Oparameters[34].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[35].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[36].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[37].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[38].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[39].Direction = ParameterDirection.Output;

            //optener los resultados del procedimiento almacenado
            //Dictionary<string, string> outparam; = new Dictionary<string, string>();
            int Result = Sqlprovider.ExecuteStoredProcedureWithOutputParameter2("WorkFlow.PL_InsUpd_SolicitudOrdenPago_key", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            //Obtiene El Error Generado Desde El Procedimiento Almacenado Workflow.sp_insup_solicitudordenpago.
            if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
            {
                objRespuestaOperacion.NumeroRegistroOrdenPagox = 0;
                objRespuestaOperacion.NumeroRegistroDetallePagox = 0;
                objRespuestaOperacion.Codigox = outparam["@pCodigoError"];
                objRespuestaOperacion.Mensajex = outparam["@pMensajeError"];
                objRespuestaOperacion.Tipox = outparam["@pTipoError"];
                objRespuestaOperacion.Titulox = outparam["@pTituloError"];
            }
            else
            {
                objRespuestaOperacion.NumeroRegistroOrdenPagox = Convert.ToInt64(outparam["@pNumeroSolicitudPago"].Trim());
                objRespuestaOperacion.NumeroRegistroDetallePagox = Convert.ToInt64(outparam["@pNumeroItem"].Trim());
                objRespuestaOperacion.Codigox = string.Empty;
                objRespuestaOperacion.Mensajex = string.Empty;
                objRespuestaOperacion.Tipox = string.Empty;
                objRespuestaOperacion.Titulox = string.Empty;
            }

            return objRespuestaOperacion;
        }

        /// <summary>
        /// Lista las solicitudes de ordenes de pagos registradas por el usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public List<Wrkf_SolicitudOrdenPago> GetSolicitudOrdenPagoTodas(string pusuario)
        {
            List<Wrkf_SolicitudOrdenPago> lstSolicitudOrdenPago = new List<Wrkf_SolicitudOrdenPago>();
            Wrkf_DbMensajeError objdbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError objmensajeerror;

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pUsuario",pusuario),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@pTituloError", SqlDbType.VarChar, 60)
            });

            Sqlprovider.Oparameters[1].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;

            DataTable DtListadoSolicitudOrdenPago = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("WorkFlow.PL_Sel_SolicitudOrdenPago_All", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            //verifica que el procedimiento almacenado al momento de ejecutar no tenga errores
            if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
            {
                Wrkf_SolicitudOrdenPago objSolicitudOrdenPago = new Wrkf_SolicitudOrdenPago()
                {
                    Codigox = outparam["@pCodigoError"],
                    Mensajex = outparam["@pMensajeError"],
                    Tipox = outparam["@pTipoError"],
                    Titulox = outparam["@pTituloError"]
                };

                lstSolicitudOrdenPago.Add(objSolicitudOrdenPago);
            }
            else
            {
                int total_registros = DtListadoSolicitudOrdenPago.Rows.Count;

                if (total_registros > 0)
                {
                    for (int i = 0; i < total_registros; i++)
                    {
                        Wrkf_SolicitudOrdenPago objSolicitudOrdenPago = new Wrkf_SolicitudOrdenPago()
                        {
                            Codigo = DtListadoSolicitudOrdenPago.Rows[i]["Codigo"].ToString(),
                            GAPCodigoItem = DtListadoSolicitudOrdenPago.Rows[i]["CodigoItem"].ToString(),
                            GAPCodigoPlantilla = DtListadoSolicitudOrdenPago.Rows[i]["CodigoPlantilla"].ToString(),
                            GAPProveedor = DtListadoSolicitudOrdenPago.Rows[i]["VENDNAME"].ToString(),
                            GAPIdProveedor = DtListadoSolicitudOrdenPago.Rows[i]["IdProveedor"].ToString(),
                            GAPNroFactura = DtListadoSolicitudOrdenPago.Rows[i]["NroFactura"].ToString(),
                            GAPDescripcionFactura = DtListadoSolicitudOrdenPago.Rows[i]["DescripcionFactura"].ToString(),
                            GAPFechaFactura = DtListadoSolicitudOrdenPago.Rows[i]["FechaFactura"].ToString(),
                            GAPFechaPago = DtListadoSolicitudOrdenPago.Rows[i]["FechaPago"].ToString(),
                            GAPMonto = Convert.ToDouble(DtListadoSolicitudOrdenPago.Rows[i]["Monto"]),
                            GAPIdChequera = DtListadoSolicitudOrdenPago.Rows[i]["IdChequera"].ToString(),
                            GrupoRubro_Id = Convert.ToInt32(DtListadoSolicitudOrdenPago.Rows[i]["GrupoRubro_Id"]),
                            GrupoRubroDescripcion = DtListadoSolicitudOrdenPago.Rows[i]["GrupoRubroDescripcion"].ToString(),
                            Rubro_Id = DtListadoSolicitudOrdenPago.Rows[i]["Rubro_Id"].ToString(),
                            RubroDescripcion = DtListadoSolicitudOrdenPago.Rows[i]["rubroDescripcion"].ToString(),
                            CodigoMoneda = DtListadoSolicitudOrdenPago.Rows[i]["CodigoMoneda"].ToString()
                        };

                        lstSolicitudOrdenPago.Add(objSolicitudOrdenPago);
                    }
                }
                else
                {
                    //busca el mensaje de error en la base de datos
                    objmensajeerror = objdbmensajeerror.GetObtenerMensajeError("SOP003", "SOLIORPAGO");

                    Wrkf_SolicitudOrdenPago objSolicitudOrdenPago = new Wrkf_SolicitudOrdenPago()
                    {
                        Codigox = objmensajeerror.Codigox,
                        Mensajex = objmensajeerror.Mensajex,
                        Tipox = objmensajeerror.Tipox,
                        Titulox = objmensajeerror.Titulox
                    };

                    lstSolicitudOrdenPago.Add(objSolicitudOrdenPago);
                }
            }

            return lstSolicitudOrdenPago;
        }

        ///// <summary>
        ///// El método retorna una lista con el encabezado de la solicitud de la orden de pago
        ///// </summary>
        ///// <param name="solicitud_pago_id"></param>
        ///// <returns></returns>
        //public List<Wrkf_SolicitudOrdenPago> GetSolicitudOrdenPagoPorId(int solicitud_pago_id)
        //{
        //    List<Wrkf_SolicitudOrdenPago> lstSolicitudOrdenPago = new List<Wrkf_SolicitudOrdenPago>();
        //    Wrkf_DbMensajeError objdbmensajeerror = new Wrkf_DbMensajeError();
        //    MensajeError objmensajeerror;

        //    SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
        //    Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
        //        new SqlParameter("@pSolicitudordenpago_Id", solicitud_pago_id),
        //        new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
        //        new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
        //        new SqlParameter("@pTipo", SqlDbType.VarChar, 20),
        //        new SqlParameter("@pTitulo", SqlDbType.VarChar, 30)
        //    });

        //    Sqlprovider.Oparameters[1].Direction = ParameterDirection.Output;
        //    Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
        //    Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
        //    Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;

        //    DataTable DtListadoSolicitudOrdenPago = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("WorkFlow.PL_Sel_solicitudordenpago", 
        //                                                                                                    CommandType.StoredProcedure, 
        //                                                                                                    out Dictionary<string, string> outparam);

        //    //verificar si el procedimiento almacenado genero algun error
        //    if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
        //    {
        //        Wrkf_SolicitudOrdenPago objSolicitudOrdenPago = new Wrkf_SolicitudOrdenPago()
        //        {
        //            Codigox = outparam["@pCodigoError"],
        //            Mensajex = outparam["@pMensajeError"],
        //            Tipox = outparam["@pTipo"],
        //            Titulox = outparam["@pTitulo"]
        //        };

        //        lstSolicitudOrdenPago.Add(objSolicitudOrdenPago);
        //    }
        //    else
        //    {
        //        int total_registros = DtListadoSolicitudOrdenPago.Rows.Count;

        //        if (total_registros > 0)
        //        {
        //            for (int i = 0; i < total_registros; i++)
        //            {
        //                Wrkf_SolicitudOrdenPago objSolicitudOrdenPago = new Wrkf_SolicitudOrdenPago()
        //                {
        //                    Solicitudordenpago_Idx = Convert.ToInt32(DtListadoSolicitudOrdenPago.Rows[i]["Solicitudordenpago_Id"]),
        //                    Codigoplantillax = Convert.ToString(DtListadoSolicitudOrdenPago.Rows[i]["Codigoplantilla"]),
        //                    Nombreplantillax = Convert.ToString(DtListadoSolicitudOrdenPago.Rows[i]["Nombreplantilla"]),
        //                    Recibidocxpx = Convert.ToBoolean(DtListadoSolicitudOrdenPago.Rows[i]["Recibidocxp"]),
        //                    Aprobadocontraloriax = Convert.ToBoolean(DtListadoSolicitudOrdenPago.Rows[i]["Aprobadocontraloria"]),
        //                    Aprobadovpx = Convert.ToBoolean(DtListadoSolicitudOrdenPago.Rows[i]["Aprobadovp"]),
        //                    Aplicadotesoreriax = Convert.ToBoolean(DtListadoSolicitudOrdenPago.Rows[i]["Aplicadotesoreria"]),
        //                    Anuladax = Convert.ToBoolean(DtListadoSolicitudOrdenPago.Rows[i]["Anulada"]),
        //                    Usuarioregistrox = Convert.ToString(DtListadoSolicitudOrdenPago.Rows[i]["Usuarioregistro"]),
        //                    Fechamodificacionx = Convert.ToDateTime(DtListadoSolicitudOrdenPago.Rows[i]["Fechamodificacion"]),
        //                    Usuariomodificox = Convert.ToString(DtListadoSolicitudOrdenPago.Rows[i]["Usuariomodifico"]),
        //                    curncyidx = Convert.ToString(DtListadoSolicitudOrdenPago.Rows[i]["curncyid"]),
        //                    Urgentex = Convert.ToBoolean(DtListadoSolicitudOrdenPago.Rows[i]["Urgente"]),
        //                    FechaRegx = Convert.ToString(DtListadoSolicitudOrdenPago.Rows[i]["Fecharegistro"]),
        //                };

        //                lstSolicitudOrdenPago.Add(objSolicitudOrdenPago);
        //            }
        //        }
        //        else
        //        {
        //            //busca el mensaje de error en la base de datos
        //            objmensajeerror = objdbmensajeerror.GetObtenerMensajeError("SOP003", "SOLIORPAGO");

        //            Wrkf_SolicitudOrdenPago objSolicitudOrdenPago = new Wrkf_SolicitudOrdenPago()
        //            {
        //                Codigox = objmensajeerror.Codigox,
        //                Mensajex = objmensajeerror.Mensajex,
        //                Tipox = objmensajeerror.Tipox,
        //                Titulox = objmensajeerror.Titulox
        //            };

        //            lstSolicitudOrdenPago.Add(objSolicitudOrdenPago);
        //        }
        //    }

        //    return lstSolicitudOrdenPago;
        //}

        /// <summary>
        /// El método retorna una lista con el detalle de la solicitud de orden de pago por solicitud_pago_id
        /// </summary>
        /// <param name="solicitud_pago_id"></param>
        /// <returns></returns>
        public List<Wrkf_SolicitudOrdenPagoDetalle> GetDetalleOrdenPagoPorId(int solicitud_pago_id)
        {
            double Cantidadaux = 0.00;
            double Preciounitarioaux = 0.00;
            double Subtotalaux = 0.00;
            double Anticipoaux = 0.00;
            double Totalaux = 0.00;
            double Porcentajeivaaux = 0.00;
            double Montoivaaux = 0.00;
            double Porcentajeretencionaux = 0.00;
            double Totalretenidoaux = 0.00;

            List<Wrkf_SolicitudOrdenPagoDetalle> lstSolicitudOrdenPagoDetalle = new List<Wrkf_SolicitudOrdenPagoDetalle>();
            Wrkf_DbMensajeError objwrkfdbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError objmensajeerror = new MensajeError();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@Solicitudordenpago_Id", solicitud_pago_id)
            });

            DataTable DtListadoDetalle = Sqlprovider.ExecuteStoredProcedure("Workflow.sp_sel_solicitudordenpagodetalle", CommandType.StoredProcedure);

            int total_registros = DtListadoDetalle.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    Cantidadaux = Convert.ToDouble(DtListadoDetalle.Rows[i]["Cantidad"]);
                    Preciounitarioaux = Convert.ToDouble(DtListadoDetalle.Rows[i]["Preciounitario"]);
                    Subtotalaux = Convert.ToDouble(DtListadoDetalle.Rows[i]["Subtotal"]);
                    Anticipoaux = Convert.ToDouble(DtListadoDetalle.Rows[i]["Anticipo"]);
                    Totalaux = Convert.ToDouble(DtListadoDetalle.Rows[i]["Total"]);
                    Porcentajeivaaux = Convert.ToDouble(DtListadoDetalle.Rows[i]["Porcentajeiva"]);
                    Montoivaaux = Convert.ToDouble(DtListadoDetalle.Rows[i]["Montoiva"]);
                    Porcentajeretencionaux = Convert.ToDouble(DtListadoDetalle.Rows[i]["Porcentajeretencion"]);
                    Totalretenidoaux = Convert.ToDouble(DtListadoDetalle.Rows[i]["Totalretenido"]);

                    Wrkf_SolicitudOrdenPagoDetalle objDetalleOrdenPago = new Wrkf_SolicitudOrdenPagoDetalle()
                    {
                        Solicitudordenpagodetalle_Idx = Convert.ToInt32(DtListadoDetalle.Rows[i]["Solicitudordenpagodetalle_Id"]),
                        Solicitudordenpago_Idx = Convert.ToInt32(DtListadoDetalle.Rows[i]["Solicitudordenpago_Id"]),
                        Rifx = Convert.ToString(DtListadoDetalle.Rows[i]["Rif"]).Trim(),
                        Proveedorx = Convert.ToString(DtListadoDetalle.Rows[i]["Proveedor"]).Trim(),
                        Descripcionx = Convert.ToString(DtListadoDetalle.Rows[i]["Descripcion"]).Trim(),
                        Numerodocumentox = Convert.ToString(DtListadoDetalle.Rows[i]["Numerodocumento"]).Trim(),
                        Cantidadx = Cantidadaux.ToString("N", new CultureInfo("is-IS")),
                        Preciounitariox = Preciounitarioaux.ToString("N", new CultureInfo("is-IS")),
                        Subtotalx = Subtotalaux.ToString("N", new CultureInfo("is-IS")),
                        Anticipox = Anticipoaux.ToString("N", new CultureInfo("is-IS")),
                        Totalx = Totalaux.ToString("N", new CultureInfo("is-IS")),
                        Aprobadox = Convert.ToBoolean(DtListadoDetalle.Rows[i]["Aprobado"]),
                        TipoDocumentox = Convert.ToInt32(DtListadoDetalle.Rows[i]["Tipodocumento"]),
                        Calculaivax = Convert.ToBoolean(DtListadoDetalle.Rows[i]["Calculaiva"]),
                        Realizaretencionx = Convert.ToBoolean(DtListadoDetalle.Rows[i]["Realizaretencion"]),
                        Porcentajeivax = Porcentajeivaaux.ToString("N", new CultureInfo("is-IS")),
                        Montoivax = Montoivaaux.ToString("N", new CultureInfo("is-IS")),
                        Porcentajeretencionx = Porcentajeretencionaux.ToString("N", new CultureInfo("is-IS")),
                        Totalretenidox = Totalretenidoaux.ToString("N", new CultureInfo("is-IS")),
                        Gruporubro_Idx = Convert.ToInt32(DtListadoDetalle.Rows[i]["Gruporubro_Id"]),
                        Rubro_Idx = Convert.ToString(DtListadoDetalle.Rows[i]["Rubro_Id"]),
                        FechaPagx = Convert.ToString(DtListadoDetalle.Rows[i]["Fechapago"]),
                        Formapago_Idx = Convert.ToInt32(DtListadoDetalle.Rows[i]["formapago_Id"]),
                        Observacionesx = Convert.ToString(DtListadoDetalle.Rows[i]["observaciones"]),
                        FechaDocux = Convert.ToString(DtListadoDetalle.Rows[i]["FechaDocumento"]),
                        IdProveedorx = Convert.ToString(DtListadoDetalle.Rows[i]["IdProveedor"])
                    };

                    lstSolicitudOrdenPagoDetalle.Add(objDetalleOrdenPago);
                }
            }
            else
            {
                Wrkf_SolicitudOrdenPagoDetalle objDetalleOrdenPago = new Wrkf_SolicitudOrdenPagoDetalle();
                objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("SOP004", "SOLIORPAGO");
                objDetalleOrdenPago.Codigox = objmensajeerror.Codigox;
                objDetalleOrdenPago.Mensajex = objmensajeerror.Mensajex;
                objDetalleOrdenPago.Tipox = objmensajeerror.Tipox;
                objDetalleOrdenPago.Titulox = objmensajeerror.Titulox;
                lstSolicitudOrdenPagoDetalle.Add(objDetalleOrdenPago);
            }

            return lstSolicitudOrdenPagoDetalle;
        }

        /// <summary>
        /// El método obtiene un listado de los soportes asociados a la solicitud de la orden de pago
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_SolicitudOrdenPagoSoporte> GetListarSoportes(int Solicitudordenpago_Id, int Solicitudordenpagodetalle_Id)
        {
            List<Wrkf_SolicitudOrdenPagoSoporte> lstSoportePagos = new List<Wrkf_SolicitudOrdenPagoSoporte>();
            Wrkf_SolicitudOrdenPagoSoporte objwrkfsolicitudordenpagosoporte = new Wrkf_SolicitudOrdenPagoSoporte();
            string sqlQuery, codigoplantilla; 

            try
            {
                SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);

                //verificar si la solicitud de pago es por plantilla o manual
                Sqlprovider.Oparameters = new List<SqlParameter>();
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@Solicitudordenpago_Id", Solicitudordenpago_Id),
                });

                sqlQuery = "select Codigoplantilla from Workflow.SolicitudOrdenPago where Solicitudordenpago_Id = @Solicitudordenpago_Id";
                DataTable DtCodPlantilla = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

                if (DtCodPlantilla.Rows.Count > 0)
                {
                    codigoplantilla = DtCodPlantilla.Rows[0]["Codigoplantilla"].ToString();
                }
                else
                {
                    codigoplantilla = "";
                }

                //si los soportes son de una solicitud de pago manual
                if (codigoplantilla == "")
                {
                    Sqlprovider.Oparameters = new List<SqlParameter>();
                    Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                        new SqlParameter("@Solicitudordenpago_Id", Solicitudordenpago_Id),
                        new SqlParameter("@Solicitudordenpagodetalle_Id", Solicitudordenpagodetalle_Id)
                    });
                }
                else // si los soportes son de una plantilla
                {
                    Solicitudordenpagodetalle_Id = 0;

                    Sqlprovider.Oparameters = new List<SqlParameter>();
                    Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                        new SqlParameter("@Solicitudordenpago_Id", Solicitudordenpago_Id),
                        new SqlParameter("@Solicitudordenpagodetalle_Id", Solicitudordenpagodetalle_Id)
                    });
                }

                sqlQuery = "select Soporte_id, Solicitudordenpago_Id, Solicitudordenpagodetalle_Id, RutaDirectorio, NombreArchivo ";
                sqlQuery += "from Workflow.SolicitudOrdenPagoSoporte ";
                sqlQuery += "where Solicitudordenpago_Id = @Solicitudordenpago_Id and ";
                sqlQuery += "Solicitudordenpagodetalle_Id = @Solicitudordenpagodetalle_Id";

                //optener los resultados del procedimiento almacenado
                DataTable DtSoportePagos = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

                //verifica si la consulta genero algun resultado
                int total_registros = DtSoportePagos.Rows.Count;

                if (total_registros > 0)
                {
                    for (int i = 0; i < total_registros; i++)
                    {
                        Wrkf_SolicitudOrdenPagoSoporte objsoportepagos = new Wrkf_SolicitudOrdenPagoSoporte()
                        {
                            Soporte_idx = Convert.ToInt32(DtSoportePagos.Rows[i]["Soporte_id"]),
                            Solicitudordenpago_Idx = Convert.ToInt32(DtSoportePagos.Rows[i]["Soporte_id"]),
                            Solicitudordenpagodetalle_Idx = Convert.ToInt32(DtSoportePagos.Rows[i]["Solicitudordenpagodetalle_Id"]),
                            RutaDirectoriox = Convert.ToString(DtSoportePagos.Rows[i]["RutaDirectorio"]),
                            NombreArchivox = Convert.ToString(DtSoportePagos.Rows[i]["NombreArchivo"])
                        };
                        lstSoportePagos.Add(objsoportepagos);
                    }
                }
                else
                {
                    Wrkf_SolicitudOrdenPagoSoporte objsoportepagos = new Wrkf_SolicitudOrdenPagoSoporte();
                    lstSoportePagos.Add(objsoportepagos);
                }
            }
            catch (Exception ex)
            {
                objwrkfsolicitudordenpagosoporte.Codigox = ex.HResult.ToString();
                objwrkfsolicitudordenpagosoporte.Mensajex = ex.Message.ToString();
                objwrkfsolicitudordenpagosoporte.Tipox = "error";
                objwrkfsolicitudordenpagosoporte.Titulox = "Solicitud Orden de Pago";

                lstSoportePagos.Add(objwrkfsolicitudordenpagosoporte);
            }

            return lstSoportePagos;
        }

        /// <summary>
        /// Anula las solicitudes de pagos realizadas desde workflow y que se encuentran en estatus 1 en el GAP
        /// </summary>
        /// <param name="pCodigo"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion AnularSolicitudOrdenPago(string pCodigo, string pUsuario)
        {
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pCodigo", pCodigo),
                new SqlParameter("@pUsuarioAnulacion", pUsuario),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@pTituloError", SqlDbType.VarChar, 60)
            });

            Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;

            //optener los resultados del procedimiento almacenado
            Dictionary<string, string> outparam = new Dictionary<string, string>();
            int AnularSolicitudOrdenPago = Sqlprovider.ExecuteStoredProcedureWithOutputParameter2("WorkFlow.PL_Up_AnularPagoEspecifico", CommandType.StoredProcedure, out outparam);

            //Obtiene El Error Generado Desde El Procedimiento Almacenado.
            if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
            {
                objRespuestaOperacion.RegistrosProcesadosx = AnularSolicitudOrdenPago;
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
        /// El método verifica si la solicitud ya esta registrada
        /// </summary>
        /// <param name="Solicitudordenpago_Id"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion ExisteSolicitud(int Solicitudordenpago_Id, string usuario)
        {
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);

            try
            {
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@Solicitudordenpago_Id", Solicitudordenpago_Id),
                    new SqlParameter("@Usuarioregistro", usuario)
                });

                string sqlQuery = "select top 1 Solicitudordenpago_Id ";
                sqlQuery += "from Workflow.SolicitudOrdenPago ";
                sqlQuery += "where Solicitudordenpago_Id = @Solicitudordenpago_Id and Usuarioregistro = @Usuarioregistro";

                DataTable DtExisteSolicitud = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

                //verifica si la consulta genero algun resultado
                int total_registros = DtExisteSolicitud.Rows.Count;

                if (total_registros > 0)
                {
                    //indica que la solicitud esta registrada
                    objRespuestaOperacion.RespuestaSioNox = true;
                }
                else
                {
                    objRespuestaOperacion.RespuestaSioNox = false;
                }
            }
            catch (Exception ex)
            {
                objRespuestaOperacion.Codigox = "EXC000";
                objRespuestaOperacion.Mensajex = ex.Message.ToString().Trim();
                objRespuestaOperacion.Tipox = "error";
                objRespuestaOperacion.Titulox = "Existe La Solicitud";
            }

            return objRespuestaOperacion;
        }

        /// <summary>
        /// El método verifica que los pagos de la solicitud solo tengan una sola moneda.
        /// </summary>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion VerificarMonedaSolicitud(int Solicitudordenpago_Id, string curncyid, string usuario)
        {
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);

            try
            {
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@Solicitudordenpago_Id", Solicitudordenpago_Id),
                    new SqlParameter("@curncyid", curncyid),
                    new SqlParameter("@Usuarioregistro", usuario),
                });

                string sqlQuery = "select top 1 Solicitudordenpago_Id ";
                sqlQuery += "from Workflow.SolicitudOrdenPago ";
                sqlQuery += "where Solicitudordenpago_Id = @Solicitudordenpago_Id and ";
                sqlQuery += "curncyid = @curncyid and Usuarioregistro = @Usuarioregistro";

                DataTable DtVerificaMonedaSolictiud = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

                //verifica si la consulta genero algun resultado
                int total_registros = DtVerificaMonedaSolictiud.Rows.Count;

                //si no devuelve registros la consulta 
                if (total_registros > 0)
                {
                    //indica que la moneda registrada coincide con la de los pago previamente registrados
                    objRespuestaOperacion.RespuestaSioNox = true;
                }
                else
                {
                    //indica que esta registrando un tipo de moneda diferente al seleccionado anteriormente
                    objRespuestaOperacion.RespuestaSioNox = false;
                }
            }
            catch (Exception ex)
            {
                objRespuestaOperacion.Codigox = "EXC000";
                objRespuestaOperacion.Mensajex = ex.Message.ToString().Trim();
                objRespuestaOperacion.Tipox = "error";
                objRespuestaOperacion.Titulox = "Verifición Moneda de la Solicitud";
            }

            return objRespuestaOperacion;
        }

        /// <summary>
        /// Genera un reporte por pantalla con los pagos de la solictud de la orden de pago
        /// </summary>
        /// <param name="solicitudordenpago_id"></param>
        /// <returns></returns>
        public List<Wrkf_SolicitudOrdenPagoEncabDetal> SolicitudOrdenPagoReporte(int solicitudordenpago_id)
        {
            List<Wrkf_SolicitudOrdenPagoEncabDetal> lstwrkf_SolicitudOrdenPagoEncabDetals = new List<Wrkf_SolicitudOrdenPagoEncabDetal>();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@Solicitudordenpago_Id", solicitudordenpago_id),
                new SqlParameter("@CodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@MensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@Tipo", SqlDbType.VarChar, 20),
                new SqlParameter("@Titulo", SqlDbType.VarChar, 30)
            });

            Sqlprovider.Oparameters[1].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;

            //optener los resultados del procedimiento almacenado
            Dictionary<string, string> outparam;
            DataTable DtReporte= Sqlprovider.ExecuteStoredProcedureWithOutputParameter("Workflow.sp_sel_SolicitudOrdenPagoReporte", CommandType.StoredProcedure, out outparam);

            //Obtiene El Error Generado Desde El Procedimiento Almacenado
            if (!string.IsNullOrEmpty(outparam["@CodigoError"]))
            {
                Wrkf_SolicitudOrdenPagoEncabDetal wrkf_solicitudOrdenPagoEncabDetal = new Wrkf_SolicitudOrdenPagoEncabDetal()
                {
                    Codigox = outparam["@CodigoError"].ToString(),
                    Mensajex = outparam["@MensajeError"].ToString(),
                    Tipox = outparam["@Tipo"].ToString(),
                    Titulox = outparam["@Titulo"].ToString()
                };

                lstwrkf_SolicitudOrdenPagoEncabDetals.Add(wrkf_solicitudOrdenPagoEncabDetal);
            }
            else
            {
                int total_registros = DtReporte.Rows.Count;

                if (total_registros > 0)
                {
                    for (int i = 0; i < total_registros; i++)
                    {
                        double totalsolicitudpagoaux = Convert.ToDouble(DtReporte.Rows[i]["TotalSolicitudPago"]);
                        double Totalaux = Convert.ToDouble(DtReporte.Rows[i]["Total"]);

                        Wrkf_SolicitudOrdenPagoEncabDetal wrkf_solicitudOrdenPagoEncabDetal = new Wrkf_SolicitudOrdenPagoEncabDetal()
                        {

                            Solicitudordenpago_Idx = DtReporte.Rows[i]["Solicitudordenpago_Id"].ToString(),
                            Codigoplantillax = DtReporte.Rows[i]["Codigoplantilla"].ToString(),
                            Nombreplantillax = DtReporte.Rows[i]["Nombreplantilla"].ToString(),
                            Fecharegistrox = DtReporte.Rows[i]["Fecharegistro"].ToString(),
                            Usuarioregistrox = DtReporte.Rows[i]["Usuarioregistro"].ToString(),
                            Curncyid = DtReporte.Rows[i]["curncyid"].ToString(),
                            Enviadoacxpx = DtReporte.Rows[i]["Enviadoacxp"].ToString(),
                            TotalSolicitudPagox = totalsolicitudpagoaux.ToString("N", new CultureInfo("is-IS")),
                            Rifx = DtReporte.Rows[i]["Rif"].ToString(),
                            Proveedorx = DtReporte.Rows[i]["Proveedor"].ToString(),
                            Numerodocumentox = DtReporte.Rows[i]["Numerodocumento"].ToString(),
                            Descripcionx = DtReporte.Rows[i]["Descripcion"].ToString(),
                            Totalx = Totalaux.ToString("N", new CultureInfo("is-IS")),
                            DescripGrupoRubrox = DtReporte.Rows[i]["DescripGrupoRubro"].ToString(),
                            DescripRubrox = DtReporte.Rows[i]["DescripRubro"].ToString(),
                            DescripFormaPagox = DtReporte.Rows[i]["DescripFormaPago"].ToString(),
                            DescripTipoDocumentox = DtReporte.Rows[i]["documento"].ToString(),
                            Chekbkidx = DtReporte.Rows[i]["Chekbkid"].ToString(),
                            Fechapagox = DtReporte.Rows[i]["Fechapago"].ToString(),
                        };

                        lstwrkf_SolicitudOrdenPagoEncabDetals.Add(wrkf_solicitudOrdenPagoEncabDetal);
                    }

                }
            }

            return lstwrkf_SolicitudOrdenPagoEncabDetals;
        }
        
        /// <summary>
        /// genera un reporte con el detalle de la solicitud orden de pago
        /// </summary>
        /// <param name="solicitudordenpago_id"></param>
        /// <returns></returns>
        public List<Wrkf_ListaPagosPorRubroId> ReporteSolicitudEnviadaCxP(int solicitudordenpago_id)
        {
            List<Wrkf_ListaPagosPorRubroId> lst_wrkflistapagosporrubroid = new List<Wrkf_ListaPagosPorRubroId>();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pSolicitudordenpago_Id", solicitudordenpago_id),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@pTituloError", SqlDbType.VarChar, 30)
            });

            Sqlprovider.Oparameters[1].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;

            //optener los resultados del procedimiento almacenado
            Dictionary<string, string> outparam = new Dictionary<string, string>();
            DataTable DtReporte = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("workflow.PL_Sel_ReporteSolicitudEnviadaCxP", CommandType.StoredProcedure, out outparam);

            //Obtiene El Error Generado Desde El Procedimiento Almacenado
            if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
            {
                Wrkf_ListaPagosPorRubroId wrkf_listapagosporrubroid = new Wrkf_ListaPagosPorRubroId()
                {
                    Codigox = outparam["@pCodigoError"].ToString(),
                    Mensajex = outparam["@pMensajeError"].ToString(),
                    Tipox = outparam["@pTipo"].ToString(),
                    Titulox = outparam["@pTitulo"].ToString()
                };

                lst_wrkflistapagosporrubroid.Add(wrkf_listapagosporrubroid);
            }
            else
            {
                int total_registros = DtReporte.Rows.Count;

                if (total_registros > 0)
                {
                    for (int i = 0; i < total_registros; i++)
                    {
                        double totalsolicitudpagoaux = Convert.ToDouble(DtReporte.Rows[i]["TotalSolicitudPago"]);
                        double preciounitario = Convert.ToDouble(DtReporte.Rows[i]["Preciounitario"]);
                        double anticipo = Convert.ToDouble(DtReporte.Rows[i]["Anticipo"]);
                        double subtotal = Convert.ToDouble(DtReporte.Rows[i]["Subtotal"]);
                        double total = Convert.ToDouble(DtReporte.Rows[i]["Total"]);
                        double porcentajeiva = Convert.ToDouble(DtReporte.Rows[i]["Porcentajeiva"]);
                        double montoiva = Convert.ToDouble(DtReporte.Rows[i]["Montoiva"]);
                        double porcentajeretencion = Convert.ToDouble(DtReporte.Rows[i]["Porcentajeretencion"]);
                        double totalretenido = Convert.ToDouble(DtReporte.Rows[i]["Totalretenido"]);

                        Wrkf_ListaPagosPorRubroId wrkf_listapagosporrubroid = new Wrkf_ListaPagosPorRubroId()
                        {
                            Solicitudordenpago_Idx = Convert.ToInt32(DtReporte.Rows[i]["Solicitudordenpago_Id"]),
                            Codigoplantillax = DtReporte.Rows[i]["Codigoplantilla"].ToString(),
                            Nombreplantillax = DtReporte.Rows[i]["Nombreplantilla"].ToString(),
                            FechaRegistrox = DtReporte.Rows[i]["Fecharegistro"].ToString(),
                            Usuarioregistrox = DtReporte.Rows[i]["Usuarioregistro"].ToString(),
                            Curncyidx = DtReporte.Rows[i]["curncyid"].ToString(),
                            TotalGlobalAPagarx = totalsolicitudpagoaux.ToString("N", new CultureInfo("is-IS")),
                            Rifx = DtReporte.Rows[i]["Rif"].ToString(),
                            Proveedorx = DtReporte.Rows[i]["Proveedor"].ToString(),
                            Descripcionx = DtReporte.Rows[i]["Descripcion"].ToString(),
                            Numerodocumentox = DtReporte.Rows[i]["Numerodocumento"].ToString(),
                            Preciounitariox = preciounitario.ToString("N", new CultureInfo("is-IS")),
                            Anticipox = anticipo.ToString("N", new CultureInfo("is-IS")),
                            Subtotalx = subtotal.ToString("N", new CultureInfo("is-IS")),
                            Totalx = total.ToString("N", new CultureInfo("is-IS")),
                            FechaPagox = DtReporte.Rows[i]["Fechapago"].ToString(),
                            Porcentajeivax = porcentajeiva.ToString("N", new CultureInfo("is-IS")),
                            Montoivax = montoiva.ToString("N", new CultureInfo("is-IS")),
                            Porcentajeretencionx = porcentajeretencion.ToString("N", new CultureInfo("is-IS")),
                            Totalretenidox = totalretenido.ToString("N", new CultureInfo("is-IS")),
                            Documentox = DtReporte.Rows[i]["documento"].ToString(),
                            formadepagox = DtReporte.Rows[i]["formadepago"].ToString(),
                            Observacionesx = DtReporte.Rows[i]["observaciones"].ToString(),
                            FechaDocumentox = DtReporte.Rows[i]["FechaDocumento"].ToString()
                        };

                        lst_wrkflistapagosporrubroid.Add(wrkf_listapagosporrubroid);
                    }

                }
            }

            return lst_wrkflistapagosporrubroid;
        }
    }
}