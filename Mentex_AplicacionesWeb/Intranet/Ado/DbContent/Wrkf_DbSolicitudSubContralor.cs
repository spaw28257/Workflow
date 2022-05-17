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

namespace Intranet.Ado.DbContent
{
    public class Wrkf_DbSolicitudSubContralor
    {
        /// <summary>
        /// Lista el grupo de rubros que contiene pagos pendientes por revisar por el subcontralor
        /// </summary>
        /// <returns></returns>
        //public List<Wrkf_GrupoRubro> lstGrupoRubroSubContralor()
        //{
        //    List<Wrkf_GrupoRubro> lstgruporubro = new List<Wrkf_GrupoRubro>();

        //    SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
        //    Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
        //    });

        //    DataTable DtGrupoRubrosConPagos = Sqlprovider.ExecuteStoredProcedure("workflow.PL_Sel_GrupoRubrosSubContralor", CommandType.StoredProcedure);

        //    int total_registros = DtGrupoRubrosConPagos.Rows.Count;

        //    if (total_registros > 0)
        //    {
        //        for (int i = 0; i < total_registros; i++)
        //        {
        //            Wrkf_GrupoRubro objgruporubro = new Wrkf_GrupoRubro()
        //            {
        //                Departamento_Idx = Convert.ToInt32(DtGrupoRubrosConPagos.Rows[i]["Gruporubro_Id"]),
        //                Departamentox = Convert.ToString(DtGrupoRubrosConPagos.Rows[i]["Departamento"]),
        //                TotalRubrosx = Convert.ToInt32(DtGrupoRubrosConPagos.Rows[i]["TotalRubros"])
        //            };

        //            lstgruporubro.Add(objgruporubro);
        //        }
        //    }
        //    else
        //    {
        //        Wrkf_GrupoRubro objgruporubro = new Wrkf_GrupoRubro();
        //        lstgruporubro.Add(objgruporubro);
        //    }

        //    return lstgruporubro;
        //}

        ///// <summary>
        ///// Lista el grupo de rubros que contiene Notas de créditos pendientes por revisar por el subcontralor
        ///// </summary>
        ///// <returns></returns>
        //public List<Wrkf_GrupoRubro> lstGrupoRubroNotasCreditoSubContralor()
        //{
        //    List<Wrkf_GrupoRubro> lstgruporubro = new List<Wrkf_GrupoRubro>();

        //    SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
        //    Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
        //    });

        //    DataTable DtGrupoRubrosConPagos = Sqlprovider.ExecuteStoredProcedure("workflow.PL_Sel_GrupoRubrosNotasCreditoSubContralor", CommandType.StoredProcedure);

        //    int total_registros = DtGrupoRubrosConPagos.Rows.Count;

        //    if (total_registros > 0)
        //    {
        //        for (int i = 0; i < total_registros; i++)
        //        {
        //            Wrkf_GrupoRubro objgruporubro = new Wrkf_GrupoRubro()
        //            {
        //                Departamento_Idx = Convert.ToInt32(DtGrupoRubrosConPagos.Rows[i]["Gruporubro_Id"]),
        //                Departamentox = Convert.ToString(DtGrupoRubrosConPagos.Rows[i]["Departamento"]),
        //                TotalRubrosx = Convert.ToInt32(DtGrupoRubrosConPagos.Rows[i]["TotalRubros"])
        //            };

        //            lstgruporubro.Add(objgruporubro);
        //        }
        //    }
        //    else
        //    {
        //        Wrkf_GrupoRubro objgruporubro = new Wrkf_GrupoRubro();
        //        lstgruporubro.Add(objgruporubro);
        //    }

        //    return lstgruporubro;
        //}

        /// <summary>
        /// Lista los rubros que tienen pagos pendientes por revisar por SubContraloria 
        /// </summary>
        /// <param name="gruporubro_id"></param>
        /// <returns></returns>
        //public List<Wrkf_Rubro> lstRubrosSubContraloria(int gruporubro_id)
        //{
        //    List<Wrkf_Rubro> lstrubro = new List<Wrkf_Rubro>();

        //    SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
        //    Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
        //        new SqlParameter("@pGruporubro_Id", gruporubro_id)
        //    });

        //    DataTable DtRubrosConPagos = Sqlprovider.ExecuteStoredProcedure("workflow.PL_Sel_RubrosConPagosSubContraloria", CommandType.StoredProcedure);

        //    int total_registros = DtRubrosConPagos.Rows.Count;

        //    if (total_registros > 0)
        //    {
        //        for (int i = 0; i < total_registros; i++)
        //        {
        //            Wrkf_Rubro objrubro = new Wrkf_Rubro()
        //            {
        //                Rubro_Idx = Convert.ToString(DtRubrosConPagos.Rows[i]["Rubro_Id"]),
        //                Descripcionx = Convert.ToString(DtRubrosConPagos.Rows[i]["Descripcion"]),
        //                Cantidad_Pagosx = Convert.ToInt32(DtRubrosConPagos.Rows[i]["CantidadPago"])
        //            };

        //            lstrubro.Add(objrubro);
        //        }
        //    }
        //    else
        //    {
        //        Wrkf_Rubro objrubro = new Wrkf_Rubro();
        //        lstrubro.Add(objrubro);
        //    }

        //    return lstrubro;
        //}

        /// <summary>
        /// Lista los rubros que tienen notas de creditos pendientes por revisar por SubContraloria 
        /// </summary>
        /// <param name="gruporubro_id"></param>
        /// <returns></returns>
        //public List<Wrkf_Rubro> lstRubrosNotasCreditosSubContraloria(int gruporubro_id)
        //{
        //    List<Wrkf_Rubro> lstrubro = new List<Wrkf_Rubro>();

        //    SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
        //    Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
        //        new SqlParameter("@pGruporubro_Id", gruporubro_id)
        //    });

        //    DataTable DtRubrosConPagos = Sqlprovider.ExecuteStoredProcedure("Workflow.PL_Sel_RubrosConNotasCreditoSubContraloria", CommandType.StoredProcedure);

        //    int total_registros = DtRubrosConPagos.Rows.Count;

        //    if (total_registros > 0)
        //    {
        //        for (int i = 0; i < total_registros; i++)
        //        {
        //            Wrkf_Rubro objrubro = new Wrkf_Rubro()
        //            {
        //                Rubro_Idx = Convert.ToString(DtRubrosConPagos.Rows[i]["Rubro_Id"]),
        //                Descripcionx = Convert.ToString(DtRubrosConPagos.Rows[i]["Descripcion"]),
        //                Cantidad_Pagosx = Convert.ToInt32(DtRubrosConPagos.Rows[i]["CantidadPago"])
        //            };

        //            lstrubro.Add(objrubro);
        //        }
        //    }
        //    else
        //    {
        //        Wrkf_Rubro objrubro = new Wrkf_Rubro();
        //        lstrubro.Add(objrubro);
        //    }

        //    return lstrubro;
        //}

        /// <summary>
        /// Muestra un listado de los pagos por rubros pendientes por revisar por subcontraloria
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_ListaPagosPorRubroId> ListaPagosPorRubroIdSubContraloriaDb(string rubro_id, DateTime fechadesde, DateTime fechahasta)
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

            DataTable Dtlistapagosporrubroid = Sqlprovider.ExecuteStoredProcedure("workflow.PL_Sel_ListaPagosPorRubroIdSubContraloria", CommandType.StoredProcedure);

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
        /// Muestra un listado las notas de creditos por rubros pendientes por revisar por subcontraloria
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_ListaPagosPorRubroId> ListaNotaCreditosPorRubroIdSubContraloria(string rubro_id, DateTime fechadesde, DateTime fechahasta)
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

            DataTable Dtlistapagosporrubroid = Sqlprovider.ExecuteStoredProcedure("workflow.PL_Sel_ListaNotaCreditosPorRubroIdSubContraloria", CommandType.StoredProcedure);

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
        /// Aprobar y rechazar las solicitudes de pagos por subcontraloria
        /// </summary>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion AprobarRechazarSolicitudSubContraloria(string listapagosaprobados, string listapagosrechazados, string usuario)
        {
            Wrkf_RespuestaOperacion objrespuestaoperacion = new Wrkf_RespuestaOperacion();

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

            int Result = Sqlprovider.ExecuteStoredProcedureWithOutputParameter2("Workflow.sp_up_AprobarRechazarSolicitudSubContraloria", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            //Obtiene El Error Generado Desde El Procedimiento Almacenado [Workflow].[sp_up_AprobarRechazarSolicitudSubContraloria].
            if (!string.IsNullOrEmpty(outparam["@CodigoError"]))
            {
                objrespuestaoperacion.NumeroRegistroOrdenPagox = -1;
                objrespuestaoperacion.NumeroRegistroDetallePagox = -1;
                objrespuestaoperacion.RegistrosProcesadosx = 0;
                objrespuestaoperacion.Codigox = outparam["@CodigoError"];
                objrespuestaoperacion.Mensajex = outparam["@MensajeError"];
                objrespuestaoperacion.Tipox = outparam["@Tipo"];
                objrespuestaoperacion.Titulox = outparam["@Titulo"];
            }
            else
            {
                objrespuestaoperacion.RegistrosProcesadosx = Result;
                objrespuestaoperacion.Codigox = string.Empty;
                objrespuestaoperacion.Mensajex = string.Empty;
                objrespuestaoperacion.Tipox = string.Empty;
                objrespuestaoperacion.Titulox = string.Empty;
            }

            return objrespuestaoperacion;
        }

        /// <summary>
        /// Registra la observacion del rechazo por subcontraloria 
        /// </summary>
        /// <param name="Solicitudordenpagodetalle_Id"></param>
        /// <param name="Solicitudordenpago_Id"></param>
        /// <param name="observaciones"></param>
        public Wrkf_RespuestaOperacion ObservacionesRechazoSubcontaloria(int Solicitudordenpagodetalle_Id, int Solicitudordenpago_Id, string observaciones, string usuario)
        {
            Wrkf_RespuestaOperacion objrespuestaoperacion = new Wrkf_RespuestaOperacion();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@Solicitudordenpagodetalle_Id", Solicitudordenpagodetalle_Id),
                new SqlParameter("@Solicitudordenpago_Id", Solicitudordenpago_Id),
                new SqlParameter("@ObservacionRechaSubContra", observaciones),
                new SqlParameter("@usuariomodifico", usuario),
                new SqlParameter("@CodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@MensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@Tipo", SqlDbType.VarChar, 20),
                new SqlParameter("@Titulo", SqlDbType.VarChar, 50)
            });

            Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[6].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[7].Direction = ParameterDirection.Output;

            int Result = Sqlprovider.ExecuteStoredProcedureWithOutputParameter2("Workflow.sp_up_ObservacionRechaSubContraloria", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            //Obtiene El Error Generado Desde El Procedimiento Almacenado [Workflow].[sp_up_AprobarRechazarSolicitudSubContraloria].
            if (!string.IsNullOrEmpty(outparam["@CodigoError"]))
            {
                objrespuestaoperacion.NumeroRegistroOrdenPagox = -1;
                objrespuestaoperacion.NumeroRegistroDetallePagox = -1;
                objrespuestaoperacion.RegistrosProcesadosx = 0;
                objrespuestaoperacion.Codigox = outparam["@CodigoError"];
                objrespuestaoperacion.Mensajex = outparam["@MensajeError"];
                objrespuestaoperacion.Tipox = outparam["@Tipo"];
                objrespuestaoperacion.Titulox = outparam["@Titulo"];
            }
            else
            {
                objrespuestaoperacion.RegistrosProcesadosx = Result;
                objrespuestaoperacion.Codigox = string.Empty;
                objrespuestaoperacion.Mensajex = string.Empty;
                objrespuestaoperacion.Tipox = string.Empty;
                objrespuestaoperacion.Titulox = string.Empty;
            }

            return objrespuestaoperacion;
        }

        /// <summary>
        /// Colocar pagos urgentes para su aprobación
        /// </summary>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion AsignarPagoUrgente(int Solicitudordenpago_Id, int Solicitudordenpagodetalle_Id, string usuario, bool pagourgente)
        {
            SqlTransaction transaccion = null;
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            SqlConnection sqlconexion = Sqlprovider.SqlConnection();
            Wrkf_RespuestaOperacion wrkf_respuestaoperacion = new Wrkf_RespuestaOperacion();

            try
            {
                //inicio de la trasaccion
                transaccion = Sqlprovider.InicioTransaccion("AsignarPagoUrgente", sqlconexion);

                //Ejecutar la consulta SQL
                Sqlprovider.Oparameters = new List<SqlParameter>();
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@Solicitudordenpagodetalle_Id", Solicitudordenpagodetalle_Id),
                    new SqlParameter("@Solicitudordenpago_Id", Solicitudordenpago_Id),
                    new SqlParameter("@usuariomodifico", usuario),
                    new SqlParameter("@PagoUrgente", pagourgente),
                });

                StringBuilder sqlQuery = new StringBuilder("update Workflow.SolicitudOrdenPagoDetalle ");
                sqlQuery.Append("set Usuariomodifico = @Usuariomodifico, ");
                sqlQuery.Append("Fechamodifico = getdate(), ");
                sqlQuery.Append("PagoUrgente = @PagoUrgente ");
                sqlQuery.Append("where Solicitudordenpagodetalle_Id = @Solicitudordenpagodetalle_Id and ");
                sqlQuery.Append("Solicitudordenpago_Id = @Solicitudordenpago_Id");

                int resultado = Sqlprovider.ExecuteTransactionSqlString(sqlQuery.ToString(), CommandType.Text, sqlconexion, transaccion);

                if (resultado > 0)
                {
                    wrkf_respuestaoperacion.RegistrosProcesadosx = resultado;
                    transaccion.Commit();
                }
                else
                {
                    wrkf_respuestaoperacion.Codigox = "APU001";
                    wrkf_respuestaoperacion.Mensajex = "Ocurrio un error al momento de asignar el pago como urgente";
                    wrkf_respuestaoperacion.Tipox = "error";
                    wrkf_respuestaoperacion.Titulox = "Asignar el pago como urgente";
                    transaccion.Rollback();
                }
            }
            catch (Exception ex)
            {
                wrkf_respuestaoperacion.Codigox = ex.HResult.ToString();
                wrkf_respuestaoperacion.Mensajex = ex.Message.ToString();
                wrkf_respuestaoperacion.Tipox = "error";
                wrkf_respuestaoperacion.Titulox = "Asignar el pago como urgente";
                transaccion.Rollback();
            }

            Sqlprovider.CerrarConexionDB(sqlconexion);

            return wrkf_respuestaoperacion;
        }
    }
}