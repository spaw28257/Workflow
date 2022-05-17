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
    /// La clase permite acceder a las solicitudes para aprobar por el controlar
    /// </summary>
    public class Wrkf_DbSolicitudContralor
    {
        /// <summary>
        /// Listar los grupo de rubros que tienen rubros con pagos pendientes por aprobar por el contralor
        /// </summary>
        /// <returns></returns>
        //public List<Wrkf_GrupoRubro> lstGrupoRubroContralor(bool documento_nc)
        //{
        //    List<Wrkf_GrupoRubro> lstgruporubro = new List<Wrkf_GrupoRubro>();
        //    Wrkf_DbMensajeError objdbmensajeerror = new Wrkf_DbMensajeError();
        //    MensajeError objmensajeerror;

        //    try
        //    {
        //        SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
        //        Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
        //            new SqlParameter("@pDocumentoNC", documento_nc),
        //            new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
        //            new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
        //            new SqlParameter("@pTipo", SqlDbType.VarChar, 20),
        //            new SqlParameter("@pTitulo", SqlDbType.VarChar, 30)
        //        });

        //        Sqlprovider.Oparameters[1].Direction = ParameterDirection.Output;
        //        Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
        //        Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
        //        Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;

        //        DataTable DtGrupoRubrosContralor = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("workflow.PL_Sel_GrupoRubrosContralor", 
        //                                                                                                    CommandType.StoredProcedure, 
        //                                                                                                    out Dictionary<string, string> outparam);

        //        //verifica que el procedimiento almacenado al momento de ejecutar no tenga errores
        //        if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
        //        {
        //            Wrkf_GrupoRubro objgruporubro = new Wrkf_GrupoRubro()
        //            {
        //                Codigox = outparam["@pCodigoError"],
        //                Mensajex = outparam["@pMensajeError"],
        //                Tipox = outparam["@pTipo"],
        //                Titulox = outparam["@pTitulo"]
        //            };

        //            lstgruporubro.Add(objgruporubro);
        //        }
        //        else
        //        {
        //            int total_registros = DtGrupoRubrosContralor.Rows.Count;

        //            if (total_registros > 0)
        //            {
        //                for (int i = 0; i < total_registros; i++)
        //                {
        //                    //Wrkf_GrupoRubro objgruporubro = new Wrkf_GrupoRubro()
        //                    //{
        //                    //    DepartamentoEncript_Idx = EncriptadorMD5.Encrypt(Convert.ToString(DtGrupoRubrosContralor.Rows[i]["Gruporubro_Id"])),
        //                    //    Departamento_Idx = Convert.ToInt32(DtGrupoRubrosContralor.Rows[i]["Gruporubro_Id"]),
        //                    //    Departamentox = Convert.ToString(DtGrupoRubrosContralor.Rows[i]["Departamento"]),
        //                    //    TotalRubrosx = Convert.ToInt32(DtGrupoRubrosContralor.Rows[i]["TotalRubros"])
        //                    //};

        //                    /*lstgruporubro.Add(objgruporubro*/);
        //                }
        //            }
        //            else
        //            {
        //                objmensajeerror = objdbmensajeerror.GetObtenerMensajeError("SOP005", "SOLIORPAGO");

        //                Wrkf_GrupoRubro objgruporubro = new Wrkf_GrupoRubro()
        //                {
        //                    Codigox = objmensajeerror.Codigox,
        //                    Mensajex = objmensajeerror.Mensajex,
        //                    Tipox = "card card-info",
        //                    Titulox = objmensajeerror.Titulox
        //                };

        //                lstgruporubro.Add(objgruporubro);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Wrkf_GrupoRubro objwrkfdepartamento = new Wrkf_GrupoRubro()
        //        {
        //            Codigox = ex.HResult.ToString(),
        //            Mensajex = ex.Message.ToString(),
        //            Tipox = "error",
        //            Titulox = "Excepción Al Obtener los Grupo de Rubros"
        //        };

        //        lstgruporubro.Add(objwrkfdepartamento);
        //    }

        //    return lstgruporubro;
        //}

        ///// <summary>
        ///// El método lista los grupos de rubros que tienen pagos urgentes
        ///// </summary>
        ///// <returns></returns>
        //public List<Wrkf_GrupoRubro> lstGrupoRubroUrgentesContralor()
        //{
        //    List<Wrkf_GrupoRubro> lstgruporubro = new List<Wrkf_GrupoRubro>();
        //    Wrkf_DbMensajeError objdbmensajeerror = new Wrkf_DbMensajeError();
        //    MensajeError objmensajeerror;

        //    try
        //    {
        //        SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
        //        Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
        //            new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
        //            new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
        //            new SqlParameter("@pTipo", SqlDbType.VarChar, 20),
        //            new SqlParameter("@pTitulo", SqlDbType.VarChar, 30)
        //        });

        //        Sqlprovider.Oparameters[0].Direction = ParameterDirection.Output;
        //        Sqlprovider.Oparameters[1].Direction = ParameterDirection.Output;
        //        Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
        //        Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;

        //        DataTable DtGrupoRubrosUrgenteContralor = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("workflow.PL_Sel_GrupoRubrosContralor_Urgentes", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

        //        //verifica que el procedimiento almacenado al momento de ejecutar no tenga errores
        //        if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
        //        {
        //            Wrkf_GrupoRubro objgruporubro = new Wrkf_GrupoRubro()
        //            {
        //                Codigox = outparam["@pCodigoError"],
        //                Mensajex = outparam["@pMensajeError"],
        //                Tipox = outparam["@pTipo"],
        //                Titulox = outparam["@pTitulo"]
        //            };

        //            lstgruporubro.Add(objgruporubro);
        //        }
        //        else
        //        {
        //            int total_registros = DtGrupoRubrosUrgenteContralor.Rows.Count;

        //            if (total_registros > 0)
        //            {
        //                for (int i = 0; i < total_registros; i++)
        //                {
        //                    Wrkf_GrupoRubro objgruporubro = new Wrkf_GrupoRubro()
        //                    {
        //                        DepartamentoEncript_Idx = EncriptadorMD5.Encrypt(Convert.ToString(DtGrupoRubrosUrgenteContralor.Rows[i]["Gruporubro_Id"])),
        //                        Departamento_Idx = Convert.ToInt32(DtGrupoRubrosUrgenteContralor.Rows[i]["Gruporubro_Id"]),
        //                        Departamentox = Convert.ToString(DtGrupoRubrosUrgenteContralor.Rows[i]["Departamento"]),
        //                        TotalRubrosx = Convert.ToInt32(DtGrupoRubrosUrgenteContralor.Rows[i]["TotalRubros"])
        //                    };

        //                    lstgruporubro.Add(objgruporubro);
        //                }
        //            }
        //            else
        //            {
        //                objmensajeerror = objdbmensajeerror.GetObtenerMensajeError("SOP005", "SOLIORPAGO");

        //                Wrkf_GrupoRubro objgruporubro = new Wrkf_GrupoRubro()
        //                {
        //                    Codigox = objmensajeerror.Codigox,
        //                    Mensajex = objmensajeerror.Mensajex,
        //                    Tipox = "card card-info",
        //                    Titulox = objmensajeerror.Titulox
        //                };

        //                lstgruporubro.Add(objgruporubro);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Wrkf_GrupoRubro objwrkfdepartamento = new Wrkf_GrupoRubro()
        //        {
        //            Codigox = ex.HResult.ToString(),
        //            Mensajex = ex.Message.ToString(),
        //            Tipox = "error",
        //            Titulox = "Excepción Al Obtener los Grupo de Rubros"
        //        };

        //        lstgruporubro.Add(objwrkfdepartamento);
        //    }

        //    return lstgruporubro;
        //}

        /// <summary>
        /// Obtiene los rubros asociados 
        /// </summary>
        /// <param name="gruporubro_id"></param>
        /// <param name="documento_nc"></param>
        /// <returns></returns>
        //public List<Wrkf_Rubro> LstRubroContralor(int gruporubro_id, bool documento_nc)
        //{
        //    List<Wrkf_Rubro> lstrubro = new List<Wrkf_Rubro>();
        //    Wrkf_DbMensajeError objdbmensajeerror = new Wrkf_DbMensajeError();
        //    MensajeError objmensajeerror;

        //    try
        //    {
        //        SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
        //        Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
        //            new SqlParameter("@pGruporubro_Id", gruporubro_id),
        //            new SqlParameter("@pDocumentoNC", documento_nc),
        //            new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
        //            new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
        //            new SqlParameter("@pTipo", SqlDbType.VarChar, 20),
        //            new SqlParameter("@pTitulo", SqlDbType.VarChar, 30)
        //        });

        //        Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
        //        Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
        //        Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;
        //        Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;

        //        DataTable DtRubrosContralor = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("Workflow.PL_Sel_RubrosContraloria", 
        //                                                                                            CommandType.StoredProcedure, 
        //                                                                                            out Dictionary<string, string> outparam);

        //        //verifica que el procedimiento almacenado al momento de ejecutar no tenga errores
        //        if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
        //        {
        //            Wrkf_Rubro objrubro = new Wrkf_Rubro()
        //            {
        //                Codigox = outparam["@pCodigoError"],
        //                Mensajex = outparam["@pMensajeError"],
        //                Tipox = outparam["@pTipo"],
        //                Titulox = outparam["@pTitulo"]
        //            };

        //            lstrubro.Add(objrubro);
        //        }
        //        else
        //        {
        //            int total_registros = DtRubrosContralor.Rows.Count;

        //            if (total_registros > 0)
        //            {
        //                for (int i = 0; i < total_registros; i++)
        //                {
        //                    Wrkf_Rubro objrubro = new Wrkf_Rubro()
        //                    {
        //                        RubroEncript_Idx = EncriptadorMD5.Encrypt(Convert.ToString(DtRubrosContralor.Rows[i]["Rubro_Id"])),
        //                        Rubro_Idx = Convert.ToString(DtRubrosContralor.Rows[i]["Rubro_Id"]),
        //                        Descripcionx = Convert.ToString(DtRubrosContralor.Rows[i]["Descripcion"]),
        //                        Cantidad_Pagosx = Convert.ToInt32(DtRubrosContralor.Rows[i]["CantidadPago"]),
        //                        GruporubroEncript_Idx = EncriptadorMD5.Encrypt(gruporubro_id.ToString())
        //                    };

        //                    lstrubro.Add(objrubro);
        //                }
        //            }
        //            else
        //            {
        //                objmensajeerror = objdbmensajeerror.GetObtenerMensajeError("SOP007", "SOLIORPAGO");

        //                Wrkf_Rubro objrubro = new Wrkf_Rubro()
        //                {
        //                    Codigox = objmensajeerror.Codigox,
        //                    Mensajex = objmensajeerror.Mensajex,
        //                    Tipox = objmensajeerror.Tipox,
        //                    Titulox = objmensajeerror.Titulox
        //                };

        //                lstrubro.Add(objrubro);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Wrkf_Rubro objrubro = new Wrkf_Rubro()
        //        {
        //            Codigox = ex.HResult.ToString(),
        //            Mensajex = ex.Message.ToString() + " " + ex.StackTrace.ToString(),
        //            Tipox = "error",
        //            Titulox = "Excepción Al Obtener los pagos por Rubros"
        //        };

        //        lstrubro.Add(objrubro);
        //    }

        //    return lstrubro;
        //}

        /// <summary>
        /// El método lista los rubros que contienen pagos urgentes marcados.
        /// </summary>
        /// <param name="gruporubro_id"></param>
        /// <returns></returns>
        //public List<Wrkf_Rubro> LstRubroContralorUrgentes(int gruporubro_id)
        //{
        //    List<Wrkf_Rubro> lstrubroUrgentes = new List<Wrkf_Rubro>();
        //    Wrkf_DbMensajeError objdbmensajeerror = new Wrkf_DbMensajeError();
        //    MensajeError objmensajeerror;

        //    try
        //    {
        //        SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
        //        Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
        //            new SqlParameter("@pGruporubro_Id", gruporubro_id),
        //            new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
        //            new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
        //            new SqlParameter("@pTipo", SqlDbType.VarChar, 20),
        //            new SqlParameter("@pTitulo", SqlDbType.VarChar, 30)
        //        });

        //        Sqlprovider.Oparameters[1].Direction = ParameterDirection.Output;
        //        Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
        //        Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
        //        Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;

        //        DataTable DtRubrosContralorUrgentes = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("Workflow.PL_Sel_RubrosContraloria_Urgentes", 
        //                                                                                                        CommandType.StoredProcedure, 
        //                                                                                                        out Dictionary<string, string> outparam);

        //        //verifica que el procedimiento almacenado al momento de ejecutar no tenga errores
        //        if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
        //        {
        //            Wrkf_Rubro objrubro = new Wrkf_Rubro()
        //            {
        //                Codigox = outparam["@pCodigoError"],
        //                Mensajex = outparam["@pMensajeError"],
        //                Tipox = outparam["@pTipo"],
        //                Titulox = outparam["@pTitulo"]
        //            };

        //            lstrubroUrgentes.Add(objrubro);
        //        }
        //        else
        //        {
        //            int total_registros = DtRubrosContralorUrgentes.Rows.Count;

        //            if (total_registros > 0)
        //            {
        //                for (int i = 0; i < total_registros; i++)
        //                {
        //                    Wrkf_Rubro objrubro = new Wrkf_Rubro()
        //                    {
        //                        RubroEncript_Idx = EncriptadorMD5.Encrypt(Convert.ToString(DtRubrosContralorUrgentes.Rows[i]["Rubro_Id"])),
        //                        Rubro_Idx = Convert.ToString(DtRubrosContralorUrgentes.Rows[i]["Rubro_Id"]),
        //                        Descripcionx = Convert.ToString(DtRubrosContralorUrgentes.Rows[i]["Descripcion"]),
        //                        Cantidad_Pagosx = Convert.ToInt32(DtRubrosContralorUrgentes.Rows[i]["CantidadPago"]),
        //                        GruporubroEncript_Idx = EncriptadorMD5.Encrypt(gruporubro_id.ToString())
        //                    };

        //                    lstrubroUrgentes.Add(objrubro);
        //                }
        //            }
        //            else
        //            {
        //                objmensajeerror = objdbmensajeerror.GetObtenerMensajeError("SOP007", "SOLIORPAGO");

        //                Wrkf_Rubro objrubro = new Wrkf_Rubro()
        //                {
        //                    Codigox = objmensajeerror.Codigox,
        //                    Mensajex = objmensajeerror.Mensajex,
        //                    Tipox = objmensajeerror.Tipox,
        //                    Titulox = objmensajeerror.Titulox
        //                };

        //                lstrubroUrgentes.Add(objrubro);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Wrkf_Rubro objrubro = new Wrkf_Rubro()
        //        {
        //            Codigox = ex.HResult.ToString(),
        //            Mensajex = ex.Message.ToString() + " " + ex.StackTrace.ToString(),
        //            Tipox = "error",
        //            Titulox = "Excepción Al Obtener los pagos por Rubros"
        //        };

        //        lstrubroUrgentes.Add(objrubro);
        //    }

        //    return lstrubroUrgentes;
        //}

        /// <summary>
        /// Muestra un listado de los pagos por rubros pendientes por revisar por subcontraloria
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_ListaPagosPorRubroId> ListaPagosPorRubroIdContraloria(int gruporubro, string rubro_id, DateTime fechadesde, DateTime fechahasta, bool documento_nc)
        {
            List<Wrkf_ListaPagosPorRubroId> lstlistapagosporrubroid = new List<Wrkf_ListaPagosPorRubroId>();
            Wrkf_DbMensajeError objwrkfdbmensajeerror = new Wrkf_DbMensajeError();
            double totalapagarx;
            double totalglobalapagarx = 0;

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pGrupoRubro_Id", gruporubro),
                new SqlParameter("@pRubro_Id", rubro_id),
                new SqlParameter("@pfechapago_desde", fechadesde),
                new SqlParameter("@pfechapago_hasta", fechahasta),
                new SqlParameter("@pDocumentoNC", documento_nc),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipo", SqlDbType.VarChar, 20),
                new SqlParameter("@pTitulo", SqlDbType.VarChar, 30)
            });

            Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[6].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[7].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[8].Direction = ParameterDirection.Output;

            DataTable DtRubrosContralor = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("workflow.PL_Sel_ListaPagosPorRubroIdContraloria", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            //verifica que el procedimiento almacenado al momento de ejecutar no tenga errores
            if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
            {
                Wrkf_ListaPagosPorRubroId objwrkflistapagosrubro = new Wrkf_ListaPagosPorRubroId()
                {
                    Codigox = outparam["@pCodigoError"],
                    Mensajex = outparam["@pMensajeError"],
                    Tipox = outparam["@pTipo"],
                    Titulox = outparam["@pTitulo"]
                };

                lstlistapagosporrubroid.Add(objwrkflistapagosrubro);
            }
            else
            {
                int total_registros = DtRubrosContralor.Rows.Count;

                if (total_registros > 0)
                {
                    for (int i = 0; i < total_registros; i++)
                    {
                        totalapagarx = Convert.ToDouble(DtRubrosContralor.Rows[i]["Total"]);
                        //totaliza el total a pagar
                        totalglobalapagarx = totalglobalapagarx + totalapagarx;

                        Wrkf_ListaPagosPorRubroId objlistapagosporrubroid = new Wrkf_ListaPagosPorRubroId()
                        {
                            Solicitudordenpagodetalle_Idx = Convert.ToInt32(DtRubrosContralor.Rows[i]["Solicitudordenpagodetalle_Id"]),
                            Solicitudordenpago_Idx = Convert.ToInt32(DtRubrosContralor.Rows[i]["Solicitudordenpago_Id"]),
                            Documentox = Convert.ToString(DtRubrosContralor.Rows[i]["documento"]),
                            Proveedorx = Convert.ToString(DtRubrosContralor.Rows[i]["Proveedor"]),
                            Descripcionx = Convert.ToString(DtRubrosContralor.Rows[i]["Descripcion"]),
                            Numerodocumentox = Convert.ToString(DtRubrosContralor.Rows[i]["Numerodocumento"]),
                            Totalx = totalapagarx.ToString("N", new CultureInfo("is-IS")),
                            TotalGlobalAPagarx = totalglobalapagarx.ToString("N", new CultureInfo("is-IS")),
                            DescripcionRubrox = Convert.ToString(DtRubrosContralor.Rows[i]["DescripcionRubro"]),
                            FechaRegistrox = Convert.ToString(DtRubrosContralor.Rows[i]["FechaRegistro"]),
                            FechaPagox = Convert.ToString(DtRubrosContralor.Rows[i]["Fechapago"]),
                            formadepagox = Convert.ToString(DtRubrosContralor.Rows[i]["formadepago"]),
                            formapago_Idx = Convert.ToInt32(DtRubrosContralor.Rows[i]["formapago_Id"]),
                            Observacionesx = Convert.ToString(DtRubrosContralor.Rows[i]["TieneObservaciones"]),
                            Chequerax = Convert.ToString(DtRubrosContralor.Rows[i]["Chekbkid"]),
                            DocumentoRecibidox = Convert.ToBoolean(DtRubrosContralor.Rows[i]["DocumentoRecibido"]),
                            TotalDocumentosx = Convert.ToInt32(DtRubrosContralor.Rows[i]["TotalDocumentos"]),
                            Curncyidx = Convert.ToString(DtRubrosContralor.Rows[i]["curncyid"]),
                            FechaDocumentox = Convert.ToString(DtRubrosContralor.Rows[i]["FechaDocumento"]),
                            PagoUrgentex = Convert.ToBoolean(DtRubrosContralor.Rows[i]["PagoUrgente"])
                        };

                        lstlistapagosporrubroid.Add(objlistapagosporrubroid);
                    }
                }
                else
                {
                    MensajeError objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("SOP008", "SOLIORPAGO");
                    Wrkf_ListaPagosPorRubroId objlistapagosporrubroid = new Wrkf_ListaPagosPorRubroId()
                    {
                        Codigox = objmensajeerror.Codigox,
                        Mensajex = objmensajeerror.Mensajex,
                        Tipox = objmensajeerror.Tipox,
                        Titulox = objmensajeerror.Titulox
                    };

                    lstlistapagosporrubroid.Add(objlistapagosporrubroid);
                }
            }

            return lstlistapagosporrubroid;
        }

        /// <summary>
        /// Muestra un listado de los pagos por rubros pendientes por revisar por subcontraloria
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_ListaPagosPorRubroId> ListaPagosPorRubroIdContraloria_Urgentes(int gruporubro, string rubro_id, DateTime fechadesde, DateTime fechahasta)
        {
            List<Wrkf_ListaPagosPorRubroId> lstlistapagosporrubroid = new List<Wrkf_ListaPagosPorRubroId>();
            Wrkf_DbMensajeError objwrkfdbmensajeerror = new Wrkf_DbMensajeError();
            double totalapagarx;
            double totalglobalapagarx = 0;

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pGrupoRubro_Id", gruporubro),
                new SqlParameter("@pRubro_Id", rubro_id),
                new SqlParameter("@pfechapago_desde", fechadesde),
                new SqlParameter("@pfechapago_hasta", fechahasta),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipo", SqlDbType.VarChar, 20),
                new SqlParameter("@pTitulo", SqlDbType.VarChar, 30)
            });

            Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[6].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[7].Direction = ParameterDirection.Output;

            DataTable DtRubrosContralor = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("workflow.PL_Sel_ListaPagosPorRubroIdContraloria_Urgentes", 
                                                                                                CommandType.StoredProcedure, 
                                                                                                out Dictionary<string, string> outparam);

            //verifica que el procedimiento almacenado al momento de ejecutar no tenga errores
            if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
            {
                Wrkf_ListaPagosPorRubroId objwrkflistapagosrubro = new Wrkf_ListaPagosPorRubroId()
                {
                    Codigox = outparam["@pCodigoError"],
                    Mensajex = outparam["@pMensajeError"],
                    Tipox = outparam["@pTipo"],
                    Titulox = outparam["@pTitulo"]
                };

                lstlistapagosporrubroid.Add(objwrkflistapagosrubro);
            }
            else
            {
                int total_registros = DtRubrosContralor.Rows.Count;

                if (total_registros > 0)
                {
                    for (int i = 0; i < total_registros; i++)
                    {
                        totalapagarx = Convert.ToDouble(DtRubrosContralor.Rows[i]["Total"]);
                        //totaliza el total a pagar
                        totalglobalapagarx = totalglobalapagarx + totalapagarx;

                        Wrkf_ListaPagosPorRubroId objlistapagosporrubroid = new Wrkf_ListaPagosPorRubroId()
                        {
                            Solicitudordenpagodetalle_Idx = Convert.ToInt32(DtRubrosContralor.Rows[i]["Solicitudordenpagodetalle_Id"]),
                            Solicitudordenpago_Idx = Convert.ToInt32(DtRubrosContralor.Rows[i]["Solicitudordenpago_Id"]),
                            Documentox = Convert.ToString(DtRubrosContralor.Rows[i]["documento"]),
                            Proveedorx = Convert.ToString(DtRubrosContralor.Rows[i]["Proveedor"]),
                            Descripcionx = Convert.ToString(DtRubrosContralor.Rows[i]["Descripcion"]),
                            Numerodocumentox = Convert.ToString(DtRubrosContralor.Rows[i]["Numerodocumento"]),
                            Totalx = totalapagarx.ToString("N", new CultureInfo("is-IS")),
                            TotalGlobalAPagarx = totalglobalapagarx.ToString("N", new CultureInfo("is-IS")),
                            DescripcionRubrox = Convert.ToString(DtRubrosContralor.Rows[i]["DescripcionRubro"]),
                            FechaRegistrox = Convert.ToString(DtRubrosContralor.Rows[i]["FechaRegistro"]),
                            FechaPagox = Convert.ToString(DtRubrosContralor.Rows[i]["Fechapago"]),
                            formadepagox = Convert.ToString(DtRubrosContralor.Rows[i]["formadepago"]),
                            formapago_Idx = Convert.ToInt32(DtRubrosContralor.Rows[i]["formapago_Id"]),
                            Observacionesx = Convert.ToString(DtRubrosContralor.Rows[i]["TieneObservaciones"]),
                            Chequerax = Convert.ToString(DtRubrosContralor.Rows[i]["Chekbkid"]),
                            DocumentoRecibidox = Convert.ToBoolean(DtRubrosContralor.Rows[i]["DocumentoRecibido"]),
                            TotalDocumentosx = Convert.ToInt32(DtRubrosContralor.Rows[i]["TotalDocumentos"]),
                            Curncyidx = Convert.ToString(DtRubrosContralor.Rows[i]["curncyid"]),
                            FechaDocumentox = Convert.ToString(DtRubrosContralor.Rows[i]["FechaDocumento"]),
                            PagoUrgentex = Convert.ToBoolean(DtRubrosContralor.Rows[i]["PagoUrgente"])
                        };

                        lstlistapagosporrubroid.Add(objlistapagosporrubroid);
                    }
                }
                else
                {
                    MensajeError objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("SOP008", "SOLIORPAGO");
                    Wrkf_ListaPagosPorRubroId objlistapagosporrubroid = new Wrkf_ListaPagosPorRubroId()
                    {
                        Codigox = objmensajeerror.Codigox,
                        Mensajex = objmensajeerror.Mensajex,
                        Tipox = objmensajeerror.Tipox,
                        Titulox = objmensajeerror.Titulox
                    };

                    lstlistapagosporrubroid.Add(objlistapagosporrubroid);
                }
            }

            return lstlistapagosporrubroid;
        }

        /// <summary>
        /// Registra el motivo del rechazo del pago por parte del contralor
        /// </summary>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion RegistrarMotivoRechazo(int Solicitudordenpagodetalle_Id, int Solicitudordenpago_Id, string motivorechazo, string usuario)
        {
            Wrkf_RespuestaOperacion wrkf_RespuestaOperacion = new Wrkf_RespuestaOperacion();
            Wrkf_DbMensajeError wrkf_DbMensajeError = new Wrkf_DbMensajeError();
            MensajeError mensajeError = new MensajeError();
            SqlTransaction transaccion = null;
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            SqlConnection sqlconexion = Sqlprovider.SqlConnection();

            try
            {
                int Result;

                int longitudusuario = usuario.Trim().Length;

                if (longitudusuario > 15)
                {
                    longitudusuario = 14;
                }

                int longitudmotivorechazo = motivorechazo.Trim().Length;

                if (longitudmotivorechazo > 1000)
                {
                    longitudmotivorechazo = 1000;
                }

                //inicio de la trasaccion
                transaccion = Sqlprovider.InicioTransaccion("RegistrarMotivoRechazoContralor", sqlconexion);

                Sqlprovider.Oparameters = new List<SqlParameter>();
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@Solicitudordenpagodetalle_Id", Solicitudordenpagodetalle_Id),
                    new SqlParameter("@Solicitudordenpago_Id", Solicitudordenpago_Id),
                    new SqlParameter("@Usuariomodifico", usuario.Substring(0, longitudusuario)),
                    new SqlParameter("@Fechamodifico", DateTime.Now),
                    new SqlParameter("@ObservacionesContralor", motivorechazo.Substring(0, longitudmotivorechazo)),
                });

                string sqlQuery = "update Workflow.SolicitudOrdenPagoDetalle ";
                sqlQuery += "set ObservacionesContralor = @ObservacionesContralor, ";
                sqlQuery += "Usuariomodifico = @Usuariomodifico, ";
                sqlQuery += "Fechamodifico = @Fechamodifico ";
                sqlQuery += "where Solicitudordenpagodetalle_Id = @Solicitudordenpagodetalle_Id and ";
                sqlQuery += "Solicitudordenpago_Id = @Solicitudordenpago_Id";

                Result = Sqlprovider.ExecuteTransactionSqlString(sqlQuery, CommandType.Text, sqlconexion, transaccion);

                if (Result > 0)
                {
                    wrkf_RespuestaOperacion.RegistrosProcesadosx = Result;
                    transaccion.Commit();
                }
                else
                {
                    mensajeError = wrkf_DbMensajeError.GetObtenerMensajeError("SOP009", "SOLIORPAGO");
                    wrkf_RespuestaOperacion.Codigox = mensajeError.Codigox;
                    wrkf_RespuestaOperacion.Mensajex = mensajeError.Mensajex;
                    wrkf_RespuestaOperacion.Tipox = mensajeError.Tipox;
                    wrkf_RespuestaOperacion.Titulox = mensajeError.Titulox;
                    transaccion.Rollback();
                }
            }
            catch (Exception ex)
            {
                wrkf_RespuestaOperacion.Codigox = ex.HResult.ToString();
                wrkf_RespuestaOperacion.Mensajex = ex.Message.ToString();
                wrkf_RespuestaOperacion.Tipox = "error";
                wrkf_RespuestaOperacion.Titulox = "Solicitud Orden de Pago";
                transaccion.Rollback();
            }

            Sqlprovider.CerrarConexionDB(sqlconexion);

            return wrkf_RespuestaOperacion;
        }

        /// <summary>
        /// Seleccionar el motivo del rechazo del pago por parte del contralor
        /// </summary>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion SeleccionarMotivoRechazo(int Solicitudordenpagodetalle_Id, int Solicitudordenpago_Id)
        {
            Wrkf_RespuestaOperacion wrkf_RespuestaOperacion = new Wrkf_RespuestaOperacion();
            Wrkf_DbMensajeError wrkf_DbMensajeError = new Wrkf_DbMensajeError();
            MensajeError mensajeError = new MensajeError();
            SqlTransaction transaccion = null;
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            SqlConnection sqlconexion = Sqlprovider.SqlConnection();

            try
            {
                //inicio de la trasaccion
                transaccion = Sqlprovider.InicioTransaccion("RegistrarMotivoRechazoContralor", sqlconexion);

                Sqlprovider.Oparameters = new List<SqlParameter>();
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@Solicitudordenpagodetalle_Id", Solicitudordenpagodetalle_Id),
                    new SqlParameter("@Solicitudordenpago_Id", Solicitudordenpago_Id)
                });

                string sqlQuery = "select ObservacionesVPFinanza from Workflow.SolicitudOrdenPagoDetalle ";
                sqlQuery += "where Solicitudordenpagodetalle_Id = @Solicitudordenpagodetalle_Id and ";
                sqlQuery += "Solicitudordenpago_Id = @Solicitudordenpago_Id";

                DataTable DtMotivoRechazo = Sqlprovider.ExecuteTransactionSqlDataTable(sqlQuery, CommandType.Text, sqlconexion, transaccion);

                if (DtMotivoRechazo.Rows.Count > 0)
                {
                    wrkf_RespuestaOperacion.Observacionesx = DtMotivoRechazo.Rows[0]["ObservacionesVPFinanza"].ToString().Trim();
                    transaccion.Commit();
                }
                else
                {
                    mensajeError = wrkf_DbMensajeError.GetObtenerMensajeError("SOP010", "SOLIORPAGO");
                    wrkf_RespuestaOperacion.Codigox = mensajeError.Codigox;
                    wrkf_RespuestaOperacion.Mensajex = mensajeError.Mensajex;
                    wrkf_RespuestaOperacion.Tipox = mensajeError.Tipox;
                    wrkf_RespuestaOperacion.Titulox = mensajeError.Titulox;
                    transaccion.Rollback();
                }
            }
            catch (Exception ex)
            {
                wrkf_RespuestaOperacion.Codigox = ex.HResult.ToString();
                wrkf_RespuestaOperacion.Mensajex = ex.Message.ToString();
                wrkf_RespuestaOperacion.Tipox = "error";
                wrkf_RespuestaOperacion.Titulox = "Solicitud Orden de Pago";
                transaccion.Rollback();
            }

            Sqlprovider.CerrarConexionDB(sqlconexion);

            return wrkf_RespuestaOperacion;
        }

        /// <summary>
        /// Aprobar y rechazar las solicitudes de pagos por contraloria
        /// </summary>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion AprobarRechazarSolicitudContraloria(string listapagosaprobados, string listapagosrechazados, string usuario)
        {
            Wrkf_RespuestaOperacion wrkf_RespuestaOperacion = new Wrkf_RespuestaOperacion();

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

            int Result = Sqlprovider.ExecuteStoredProcedureWithOutputParameter2("Workflow.sp_up_AprobarRechazarSolicitudContraloria", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            //Obtiene El Error Generado Desde El Procedimiento Almacenado [Workflow].[sp_up_AprobarRechazarSolicitudSubContraloria].
            if (!string.IsNullOrEmpty(outparam["@CodigoError"]))
            {
                wrkf_RespuestaOperacion.RegistrosProcesadosx = Result;
                wrkf_RespuestaOperacion.Codigox = outparam["@CodigoError"];
                wrkf_RespuestaOperacion.Mensajex = outparam["@MensajeError"];
                wrkf_RespuestaOperacion.Tipox = outparam["@Tipo"];
                wrkf_RespuestaOperacion.Titulox = outparam["@Titulo"];
            }
            else
            {
                wrkf_RespuestaOperacion.RegistrosProcesadosx = Result;
                wrkf_RespuestaOperacion.Codigox = string.Empty;
                wrkf_RespuestaOperacion.Mensajex = string.Empty;
                wrkf_RespuestaOperacion.Tipox = string.Empty;
                wrkf_RespuestaOperacion.Titulox = string.Empty;
            }

            return wrkf_RespuestaOperacion;
        }
    }
}