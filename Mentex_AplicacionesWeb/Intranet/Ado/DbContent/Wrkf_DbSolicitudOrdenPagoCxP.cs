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
        public List<Wrkf_SolicitudOrdenPago> GetSolicitudOrdenPagoRevisarCxP()
        {
            double TotalSolicitudPagox;

            List<Wrkf_SolicitudOrdenPago> lstSolicitudOrdenPago = new List<Wrkf_SolicitudOrdenPago>();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
            });

            DataTable DtSolicitudRevisarCxP = Sqlprovider.ExecuteStoredProcedure("Workflow.sp_sel_SolicitudOrdenPagoRevisarCxP_Encab", CommandType.StoredProcedure);

            int total_registros = DtSolicitudRevisarCxP.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    TotalSolicitudPagox = Convert.ToDouble(DtSolicitudRevisarCxP.Rows[i]["TotalSolicitudPago"]);

                    Wrkf_SolicitudOrdenPago objsolicitudordenpago = new Wrkf_SolicitudOrdenPago
                    {
                        Solicitudordenpago_Idx = Convert.ToInt32(DtSolicitudRevisarCxP.Rows[i]["Solicitudordenpago_Id"]),
                        Codigoplantillax = Convert.ToString(DtSolicitudRevisarCxP.Rows[i]["Codigoplantilla"]),
                        Nombreplantillax = Convert.ToString(DtSolicitudRevisarCxP.Rows[i]["Nombreplantilla"]),
                        Recibidocxpx = Convert.ToBoolean(DtSolicitudRevisarCxP.Rows[i]["Recibidocxp"]),
                        Aprobadocontraloriax = Convert.ToBoolean(DtSolicitudRevisarCxP.Rows[i]["Aprobadocontraloria"]),
                        Aprobadovpx = Convert.ToBoolean(DtSolicitudRevisarCxP.Rows[i]["Aprobadovp"]),
                        Aplicadotesoreriax = Convert.ToBoolean(DtSolicitudRevisarCxP.Rows[i]["Aplicadotesoreria"]),
                        Anuladax = Convert.ToBoolean(DtSolicitudRevisarCxP.Rows[i]["Anulada"]),
                        Usuarioregistrox = Convert.ToString(DtSolicitudRevisarCxP.Rows[i]["Usuarioregistro"]),
                        FechaRegx = Convert.ToString(DtSolicitudRevisarCxP.Rows[i]["Fecharegistro"]),
                        curncyidx = Convert.ToString(DtSolicitudRevisarCxP.Rows[i]["curncyid"]),
                        TotalSolicitudPagox = TotalSolicitudPagox.ToString("N", new CultureInfo("is-IS"))
                    };
                    lstSolicitudOrdenPago.Add(objsolicitudordenpago);
                }
            }
            return lstSolicitudOrdenPago;
        }

        /// <summary>
        /// Obtiene un listado del detalle de las solicitudes de ordenes de pago.
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_SolicitudOrdenPagoDetalle> GetDetalleOrdenPagoPorRevisarCxP(int numerosolicitud_Id)
        {
            double Cantidadaux;
            double Preciounitarioaux;
            double Subtotalaux;
            double Anticipoaux;
            double Totalaux;
            double Porcentajeivaaux;
            double Montoivaaux;
            double Porcentajeretencionaux;
            double Totalretenidoaux;

            List<Wrkf_SolicitudOrdenPagoDetalle> lstSolicitudOrdenPagoDetalleCxP = new List<Wrkf_SolicitudOrdenPagoDetalle>();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@Solicitudordenpago_Id", numerosolicitud_Id),
            });

            DataTable DtListadoDetalle = Sqlprovider.ExecuteStoredProcedure("Workflow.sp_sel_SolicitudOrdenPagoRevisarCxP_Detalle", CommandType.StoredProcedure);

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
                        FechaPagx = Convert.ToString(DtListadoDetalle.Rows[i]["Fechapago"])
                    };

                    lstSolicitudOrdenPagoDetalleCxP.Add(objDetalleOrdenPago);
                }
            }
            else
            {
                Wrkf_SolicitudOrdenPagoDetalle objDetalleOrdenPago = new Wrkf_SolicitudOrdenPagoDetalle();
                lstSolicitudOrdenPagoDetalleCxP.Add(objDetalleOrdenPago);
            }

            return lstSolicitudOrdenPagoDetalleCxP;
        }

        /// <summary>
        /// Obtiene un listado de los grupos de rubros que tienen pagos asociados
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_Departamento> lstGrupoRubroPagosAsociados()
        {
            List<Wrkf_Departamento> lstgruporubro = new List<Wrkf_Departamento>();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
            });

            DataTable DtGrupoRubrosConPagos = Sqlprovider.ExecuteStoredProcedure("workflow.PL_Sel_GrupoRubrosConPagosPorAprobarCxP", CommandType.StoredProcedure);

            int total_registros = DtGrupoRubrosConPagos.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_Departamento objgruporubro = new Wrkf_Departamento()
                    {
                        Departamento_Idx = Convert.ToInt32(DtGrupoRubrosConPagos.Rows[i]["Gruporubro_Id"]),
                        Departamentox = Convert.ToString(DtGrupoRubrosConPagos.Rows[i]["Departamento"]),
                        TotalRubrosx = Convert.ToInt32(DtGrupoRubrosConPagos.Rows[i]["TotalRubros"])
                    };

                    lstgruporubro.Add(objgruporubro);
                }
            }
            else
            {
                Wrkf_Departamento objgruporubro = new Wrkf_Departamento();
                lstgruporubro.Add(objgruporubro);
            }

            return lstgruporubro;
        }

        /// <summary>
        /// Obtiene un listado de los grupos de rubros que tienen solamente las notas de créditos
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_Departamento> lstGrupoRubrosNotasCreditosAproCxP()
        {
            List<Wrkf_Departamento> lstgruporubro = new List<Wrkf_Departamento>();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
            });

            DataTable DtGrupoRubrosConPagos = Sqlprovider.ExecuteStoredProcedure("workflow.PL_Sel_GrupoRubrosConNotasCreditosPorAprobarCxP", CommandType.StoredProcedure);

            int total_registros = DtGrupoRubrosConPagos.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_Departamento objgruporubro = new Wrkf_Departamento()
                    {
                        Departamento_Idx = Convert.ToInt32(DtGrupoRubrosConPagos.Rows[i]["Gruporubro_Id"]),
                        Departamentox = Convert.ToString(DtGrupoRubrosConPagos.Rows[i]["Departamento"]),
                        TotalRubrosx = Convert.ToInt32(DtGrupoRubrosConPagos.Rows[i]["TotalRubros"])
                    };

                    lstgruporubro.Add(objgruporubro);
                }
            }
            else
            {
                Wrkf_Departamento objgruporubro = new Wrkf_Departamento();
                lstgruporubro.Add(objgruporubro);
            }

            return lstgruporubro;
        }

        /// <summary>
        /// Obtiene un listado de los rubros que tienen pagos pendientes por revisar por CxP
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_Rubro> lstRubrosPagosAsociados(int gruporubro_id)
        {
            List<Wrkf_Rubro> lstrubro = new List<Wrkf_Rubro>();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pGruporubro_Id", gruporubro_id)
            });

            DataTable DtRubrosConPagos = Sqlprovider.ExecuteStoredProcedure("workflow.PL_Sel_RubrosConPagosPorAprobarCxP", CommandType.StoredProcedure);

            int total_registros = DtRubrosConPagos.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_Rubro objrubro = new Wrkf_Rubro()
                    {
                        Rubro_Idx = Convert.ToString(DtRubrosConPagos.Rows[i]["Rubro_Id"]),
                        Descripcionx = Convert.ToString(DtRubrosConPagos.Rows[i]["Descripcion"]),
                        Cantidad_Pagosx = Convert.ToInt32(DtRubrosConPagos.Rows[i]["CantidadPago"])
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
        /// Obtiene un listado de los rubros que tienen notas de créditos pendientes por revisar por CxP
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_Rubro> lstRubrosConNotasCreditosAsociados(int gruporubro_id)
        {
            List<Wrkf_Rubro> lstrubro = new List<Wrkf_Rubro>();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pGruporubro_Id", gruporubro_id)
            });

            DataTable DtRubrosConPagos = Sqlprovider.ExecuteStoredProcedure("workflow.PL_Sel_RubrosConNotasCreditosPorAprobarCxP", CommandType.StoredProcedure);

            int total_registros = DtRubrosConPagos.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_Rubro objrubro = new Wrkf_Rubro()
                    {
                        Rubro_Idx = Convert.ToString(DtRubrosConPagos.Rows[i]["Rubro_Id"]),
                        Descripcionx = Convert.ToString(DtRubrosConPagos.Rows[i]["Descripcion"]),
                        Cantidad_Pagosx = Convert.ToInt32(DtRubrosConPagos.Rows[i]["CantidadPago"])
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
        /// El método lista los pagos asociados a un rubro especificado
        /// </summary>
        /// <param name="rubro_id"></param>
        /// <returns></returns>
        public List<Wrkf_ListaPagosPorRubroId> ListaPagosPorRubroId(string rubro_id, DateTime fechadesde, DateTime fechahasta)
        {

            List<Wrkf_ListaPagosPorRubroId> lstlistapagosporrubroid = new List<Wrkf_ListaPagosPorRubroId>();
            double totalapagarx;
            double totalglobalapagarx = 0;

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pRubro_Id", rubro_id),
                new SqlParameter("@pfechapago_desde", fechadesde),
                new SqlParameter("@pfechapago_hasta", fechahasta)
            });

            DataTable Dtlistapagosporrubroid = Sqlprovider.ExecuteStoredProcedure("workflow.PL_Sel_ListaPagosPorRubroId", CommandType.StoredProcedure);

            int total_registros = Dtlistapagosporrubroid.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    totalapagarx = Convert.ToDouble(Dtlistapagosporrubroid.Rows[i]["Total"]);
                    //totaliza el total a pagar
                    totalglobalapagarx = totalglobalapagarx + totalapagarx;

                    Wrkf_ListaPagosPorRubroId objlistapagosporrubroid = new Wrkf_ListaPagosPorRubroId()
                    {
                        Solicitudordenpagodetalle_Idx = Convert.ToInt32(Dtlistapagosporrubroid.Rows[i]["Solicitudordenpagodetalle_Id"]),
                        Solicitudordenpago_Idx = Convert.ToInt32(Dtlistapagosporrubroid.Rows[i]["Solicitudordenpago_Id"]),
                        Documentox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["documento"]),
                        Proveedorx = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["Proveedor"]),
                        Descripcionx = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["Descripcion"]),
                        Numerodocumentox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["Numerodocumento"]),
                        Totalx = totalapagarx.ToString("N", new CultureInfo("is-IS")),
                        TotalGlobalAPagarx = totalglobalapagarx.ToString("N", new CultureInfo("is-IS")),
                        DescripcionRubrox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["DescripcionRubro"]),
                        FechaRegistrox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["FechaRegistro"]),
                        FechaPagox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["Fechapago"]),
                        formadepagox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["formadepago"]),
                        formapago_Idx = Convert.ToInt32(Dtlistapagosporrubroid.Rows[i]["formapago_Id"]),
                        Observacionesx = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["TieneObservaciones"]),
                        Chequerax = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["Chekbkid"]),
                        DocumentoRecibidox = Convert.ToBoolean(Dtlistapagosporrubroid.Rows[i]["DocumentoRecibido"]),
                        TotalDocumentosx = Convert.ToInt32(Dtlistapagosporrubroid.Rows[i]["TotalDocumentos"]),
                        ObservacionRechaSubContrax = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["ObservacionRechaSubContra"]),
                        Curncyidx = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["curncyid"]),
                        FechaDocumentox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["FechaDocumento"])
                    };

                    lstlistapagosporrubroid.Add(objlistapagosporrubroid);
                }
            }
            else
            {
                Wrkf_ListaPagosPorRubroId objlistapagosporrubroid = new Wrkf_ListaPagosPorRubroId();
                lstlistapagosporrubroid.Add(objlistapagosporrubroid);
            }

            return lstlistapagosporrubroid;
        }

        /// <summary>
        /// El método lista las notas de creditos asociadas a un rubro especificado
        /// </summary>
        /// <param name="rubro_id"></param>
        /// <returns></returns>
        public List<Wrkf_ListaPagosPorRubroId> ListaNotasCreditoPorRubroId(string rubro_id, DateTime fechadesde, DateTime fechahasta)
        {

            List<Wrkf_ListaPagosPorRubroId> lstlistapagosporrubroid = new List<Wrkf_ListaPagosPorRubroId>();
            double totalapagarx;
            double totalglobalapagarx = 0;

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pRubro_Id", rubro_id),
                new SqlParameter("@pfechapago_desde", fechadesde),
                new SqlParameter("@pfechapago_hasta", fechahasta)
            });

            DataTable Dtlistapagosporrubroid = Sqlprovider.ExecuteStoredProcedure("workflow.PL_Sel_ListaNotaCreditoPorRubroId", CommandType.StoredProcedure);

            int total_registros = Dtlistapagosporrubroid.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    totalapagarx = Convert.ToDouble(Dtlistapagosporrubroid.Rows[i]["Total"]);
                    //totaliza el total a pagar
                    totalglobalapagarx = totalglobalapagarx + totalapagarx;

                    Wrkf_ListaPagosPorRubroId objlistapagosporrubroid = new Wrkf_ListaPagosPorRubroId()
                    {
                        Solicitudordenpagodetalle_Idx = Convert.ToInt32(Dtlistapagosporrubroid.Rows[i]["Solicitudordenpagodetalle_Id"]),
                        Solicitudordenpago_Idx = Convert.ToInt32(Dtlistapagosporrubroid.Rows[i]["Solicitudordenpago_Id"]),
                        Documentox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["documento"]),
                        Proveedorx = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["Proveedor"]),
                        Descripcionx = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["Descripcion"]),
                        Numerodocumentox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["Numerodocumento"]),
                        Totalx = totalapagarx.ToString("N", new CultureInfo("is-IS")),
                        TotalGlobalAPagarx = totalglobalapagarx.ToString("N", new CultureInfo("is-IS")),
                        DescripcionRubrox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["DescripcionRubro"]),
                        FechaRegistrox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["FechaRegistro"]),
                        FechaPagox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["Fechapago"]),
                        formadepagox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["formadepago"]),
                        formapago_Idx = Convert.ToInt32(Dtlistapagosporrubroid.Rows[i]["formapago_Id"]),
                        Observacionesx = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["TieneObservaciones"]),
                        Chequerax = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["Chekbkid"]),
                        DocumentoRecibidox = Convert.ToBoolean(Dtlistapagosporrubroid.Rows[i]["DocumentoRecibido"]),
                        TotalDocumentosx = Convert.ToInt32(Dtlistapagosporrubroid.Rows[i]["TotalDocumentos"]),
                        Curncyidx = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["curncyid"]),
                        FechaDocumentox = Convert.ToString(Dtlistapagosporrubroid.Rows[i]["FechaDocumento"])
                    };

                    lstlistapagosporrubroid.Add(objlistapagosporrubroid);
                }
            }
            else
            {
                Wrkf_ListaPagosPorRubroId objlistapagosporrubroid = new Wrkf_ListaPagosPorRubroId();
                lstlistapagosporrubroid.Add(objlistapagosporrubroid);
            }

            return lstlistapagosporrubroid;
        }

        /// <summary>
        /// Actualiza forma de pago enla revisión de la solicitud del pago por CxP
        /// </summary>
        /// <param name="forma_pago_id"></param>
        /// <param name="Solicitudordenpagodetalle_Id"></param>
        /// <param name="Solicitudordenpago_Id"></param>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion ActualizarFormaPagoRevisionCxP(int forma_pago_id, int Solicitudordenpagodetalle_Id, int Solicitudordenpago_Id)
        {
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@formapago_Id", forma_pago_id),
                new SqlParameter("@Solicitudordenpagodetalle_Id", Solicitudordenpagodetalle_Id),
                new SqlParameter("@Solicitudordenpago_Id", Solicitudordenpago_Id),
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
            int Result = Sqlprovider.ExecuteStoredProcedureWithOutputParameter2("Workflow.sp_up_ActualizarFormaPagoRevisionCxP", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

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