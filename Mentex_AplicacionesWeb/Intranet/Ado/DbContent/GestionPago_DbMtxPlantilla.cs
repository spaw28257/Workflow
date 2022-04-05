using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Intranet.Utilities;
using Intranet.Models;
using System.Globalization;
using EncryptDecrypt;

namespace Intranet.Ado.DbContent
{
    public class GestionPago_DbMtxPlantilla
    {
        //Constructor
        public GestionPago_DbMtxPlantilla()
        {
        }

        /// <summary>
        /// Listar las plantillas activas para seleccionar
        /// </summary>
        /// <returns></returns>
        public List<GestionPago_MtxPlantilla> ListarPlantillasActivas(string pUsuario, bool pNoFrecuente)
        {
            List<GestionPago_MtxPlantilla> lstgestionPago_MtxPlantillas = new List<GestionPago_MtxPlantilla>();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@pUsuario", pUsuario),
                    new SqlParameter("@pNoFrecuente", pNoFrecuente)
            });

            DataTable DtPlantillas = Sqlprovider.ExecuteStoredProcedure("GestionPago.PL_Sel_MTX_Plantilla_all", CommandType.StoredProcedure);

            int vTotalRegistro = DtPlantillas.Rows.Count;

            if (vTotalRegistro > 0)
            {
                for (int i = 0; i < vTotalRegistro; i++)
                {
                    GestionPago_MtxPlantilla gestionPago_MtxPlantilla = new GestionPago_MtxPlantilla()
                    {
                        Codigo = DtPlantillas.Rows[i]["Codigo"].ToString().Trim(),
                        Nombre = DtPlantillas.Rows[i]["Nombre"].ToString().Trim(),
                        CodigoSubgrupo = DtPlantillas.Rows[i]["CodigoSubgrupo"].ToString().Trim(),
                        Frecuencia = DtPlantillas.Rows[i]["Frecuencia"].ToString().Trim(),
                        Sensibilidad = Convert.ToInt32(DtPlantillas.Rows[i]["Sensibilidad"]),
                        Estatus = Convert.ToBoolean(DtPlantillas.Rows[i]["Estatus"]),
                        IdClaseProveedor = DtPlantillas.Rows[i]["IdClaseProveedor"].ToString().Trim()
                    };

                    lstgestionPago_MtxPlantillas.Add(gestionPago_MtxPlantilla);
                }
            }

            return lstgestionPago_MtxPlantillas;
        }

        /// <summary>
        /// Selecciona los datos de la plantilla por código
        /// </summary>
        /// <returns></returns>
        public GestionPago_MtxPlantilla SeleccionarPlantillaPorCodigo(string pCodigo)
        {
            GestionPago_MtxPlantilla gestionPago_MtxPlantillas = new GestionPago_MtxPlantilla();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError objMensajeError;

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@pCodigo", pCodigo)
                });

            DataTable DtPlantilla = Sqlprovider.ExecuteStoredProcedure("GestionPago.PL_Sel_MTX_Plantilla_key", CommandType.StoredProcedure);

            int vTotalRegistro = DtPlantilla.Rows.Count;

            if (vTotalRegistro > 0)
            {
                gestionPago_MtxPlantillas.Codigo = DtPlantilla.Rows[0]["Codigo"].ToString().Trim();
                gestionPago_MtxPlantillas.Nombre = DtPlantilla.Rows[0]["nombre"].ToString().Trim();
            }
            else
            {
                objMensajeError = wrkf_dbmensajeerror.GetObtenerMensajeError("PLA001", "PLANTILLA");

                gestionPago_MtxPlantillas.Codigox = objMensajeError.Codigox;
                gestionPago_MtxPlantillas.Mensajex = objMensajeError.Mensajex;
                gestionPago_MtxPlantillas.Tipox = objMensajeError.Tipox;
                gestionPago_MtxPlantillas.Titulox = objMensajeError.Titulox;
            }

            return gestionPago_MtxPlantillas;
        }

        /// <summary>
        /// Select the payments by template from GP then run the method InsertPaymentByTemplate
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="fechapagodesde"></param>
        /// <param name="fechapagohasta"></param>
        /// <param name="proceso"></param>
        /// <param name="usuario"></param>
        /// <param name="checkuncheck"></param>
        /// <returns></returns>
        public List<Wrkf_PagosPlantillaFecha> LoadPaymentByTemplateGP(string codigo, string fechapagodesde, string fechapagohasta, string proceso, string usuario, bool cargainicial)
        {
            List<Wrkf_PagosPlantillaFecha> lstwrkf_pagosplantillas;
            Wrkf_RespuestaOperacion objrespuestaoperacion;
            Wrkf_DbProveedor objdbproveedor = new Wrkf_DbProveedor();

            if (cargainicial == true)
            {
                //check if the template's code exits
                objrespuestaoperacion = VerifyTemplateExistsByCode(codigo, usuario);

                if (objrespuestaoperacion.RegistrosProcesadosx > 0)
                {
                    //delete template's code
                    DeleteTemplateByCode(codigo, usuario);
                }

                SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@pCodigoPlantilla", codigo),
                    new SqlParameter("@pFechaDesde", fechapagodesde),
                    new SqlParameter("@pFechaHasta", fechapagohasta),
                    new SqlParameter("@pProceso", proceso)
                });

                DataTable DtRegistros = Sqlprovider.ExecuteStoredProcedure("GestionPago.PL_Sel_MTX_Pago_filter_PlantillaFecha", CommandType.StoredProcedure);

                int total_registros = DtRegistros.Rows.Count;

                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_PagosPlantillaFecha wrkf_PagosPlantillaFecha = new Wrkf_PagosPlantillaFecha()
                    {
                        Codigo1 = DtRegistros.Rows[i]["Codigo"].ToString(),
                        CodigoItem1 = DtRegistros.Rows[i]["CodigoItem"].ToString(),
                        CodigoPlantilla1 = DtRegistros.Rows[i]["CodigoPlantilla"].ToString(),
                        Monto1 = DtRegistros.Rows[i]["Monto"].ToString(),
                        FechaFactura1 = DtRegistros.Rows[i]["FechaFactura"].ToString(),
                        FechaVencimiento1 = DtRegistros.Rows[i]["FechaVencimiento"].ToString(),
                        FechaPago1 = DtRegistros.Rows[i]["FechaPago"].ToString(),
                        NroFactura1 = DtRegistros.Rows[i]["NroFactura"].ToString(),
                        TipoPago1 = DtRegistros.Rows[i]["TipoPago"].ToString(),
                        IdProveedor1 = DtRegistros.Rows[i]["IdProveedor"].ToString(),
                        VENDNAME1 = DtRegistros.Rows[i]["VENDNAME"].ToString(),
                        LOCATNID1 = DtRegistros.Rows[i]["LOCATNID"].ToString(),
                        CUSTNAME1 = DtRegistros.Rows[i]["CUSTNAME"].ToString(),
                        UltimoPago11 = DtRegistros.Rows[i]["UltimoPago1"].ToString(),
                        UltimoPago21 = DtRegistros.Rows[i]["UltimoPago2"].ToString(),
                        UltimoPago31 = DtRegistros.Rows[i]["UltimoPago3"].ToString(),
                        IdChequera1 = DtRegistros.Rows[i]["IdChequera"].ToString(),
                        ACNMVNDR1 = DtRegistros.Rows[i]["ACNMVNDR"].ToString(),
                        Estatus1 = DtRegistros.Rows[i]["Estatus"].ToString(),
                        ComprobanteIVA1 = DtRegistros.Rows[i]["ComprobanteIVA"].ToString(),
                        ComprobanteISLR1 = DtRegistros.Rows[i]["ComprobanteISLR"].ToString(),
                        NroControl1 = DtRegistros.Rows[i]["NroControl"].ToString(),
                        DescripcionFactura1 = DtRegistros.Rows[i]["DescripcionFactura"].ToString(),
                        MontoServicio1 = DtRegistros.Rows[i]["MontoServicio"].ToString(),
                        Referencia1 = DtRegistros.Rows[i]["Referencia"].ToString(),
                        ComplementoFactura1 = DtRegistros.Rows[i]["Referencia"].ToString(),
                        CartaSolicitud1 = DtRegistros.Rows[i]["CartaSolicitud"].ToString(),
                        Tienda1 = DtRegistros.Rows[i]["Tienda"].ToString(),
                        MontoAdelanto1 = DtRegistros.Rows[i]["MontoAdelanto"].ToString(),
                        Rif1 = objdbproveedor.GetTaxRegistrationNumber(DtRegistros.Rows[i]["IdProveedor"].ToString().Trim())
                    };
                    //Insert Payment by template
                    InsertPaymentByTemplate(wrkf_PagosPlantillaFecha, usuario);
                }
            }

            //insert the template's payments in the list.
            lstwrkf_pagosplantillas = GetPaymentsByTemplate(codigo, fechapagodesde, fechapagohasta, usuario);

            return lstwrkf_pagosplantillas;
        }

        /// <summary>
        /// This method checks if the code of the template in process exists
        /// </summary>
        /// <param name="templatecode"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private Wrkf_RespuestaOperacion VerifyTemplateExistsByCode(string templatecode, string user)
        {
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            Wrkf_RespuestaOperacion objrespuestaoperacion = new Wrkf_RespuestaOperacion();

            try
            {
                SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@CodigoPlantilla", templatecode)
                });

                string SqlQuery = "select CodigoPlantilla from workflow.PlantillaTmp where CodigoPlantilla = @CodigoPlantilla";

                DataTable DtRegistros = Sqlprovider.ExecuteStoredProcedure(SqlQuery, CommandType.Text);

                objrespuestaoperacion.RegistrosProcesadosx = DtRegistros.Rows.Count;
            }
            catch(Exception ex)
            {
                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), user, "GestionPago_DbMtxPlantilla/VerifyTemplateExistsByCode");
            }

            return objrespuestaoperacion;
        }
        
        /// <summary>
        /// This method insert the payment by template in a temporary table for be proccessed
        /// </summary>
        /// <param name="objwrkf_pagosplantillafecha"></param>
        /// <returns></returns>
        private Wrkf_RespuestaOperacion InsertPaymentByTemplate(Wrkf_PagosPlantillaFecha objwrkf_pagosplantillafecha, string usuario)
        {
            SqlTransaction transaccion = null;
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            SqlConnection sqlconexion = Sqlprovider.SqlConnection();
            Wrkf_RespuestaOperacion wrkf_respuestaoperacion = new Wrkf_RespuestaOperacion();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            ConvertExtension convertextension = new ConvertExtension();

            try
            {
                //inicio de la trasaccion
                transaccion = Sqlprovider.InicioTransaccion("InsertPaymentByTemplate", sqlconexion);

                //Ejecutar la consulta SQL
                Sqlprovider.Oparameters = new List<SqlParameter>();
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@Codigo", convertextension.ValidateNumberCharacters(objwrkf_pagosplantillafecha.Codigo1,50)),
                    new SqlParameter("@CodigoItem", convertextension.ValidateNumberCharacters(objwrkf_pagosplantillafecha.CodigoItem1,50)),
                    new SqlParameter("@CodigoPlantilla", convertextension.ValidateNumberCharacters(objwrkf_pagosplantillafecha.CodigoPlantilla1,6)),
                    new SqlParameter("@Monto", Convert.ToDouble(objwrkf_pagosplantillafecha.Monto1)),
                    new SqlParameter("@FechaFactura", Convert.ToDateTime(objwrkf_pagosplantillafecha.FechaFactura1)),
                    new SqlParameter("@FechaVencimiento", Convert.ToDateTime(objwrkf_pagosplantillafecha.FechaVencimiento1)),
                    new SqlParameter("@FechaPago", Convert.ToDateTime(objwrkf_pagosplantillafecha.FechaPago1)),
                    new SqlParameter("@NroFactura", convertextension.ValidateNumberCharacters(objwrkf_pagosplantillafecha.NroFactura1,100)),
                    new SqlParameter("@TipoPago", convertextension.ValidateNumberCharacters(objwrkf_pagosplantillafecha.TipoPago1,100)),
                    new SqlParameter("@IdProveedor", convertextension.ValidateNumberCharacters(objwrkf_pagosplantillafecha.IdProveedor1,100)),
                    new SqlParameter("@Vendname", convertextension.ValidateNumberCharacters(objwrkf_pagosplantillafecha.VENDNAME1,100)),
                    new SqlParameter("@Locatnid", convertextension.ValidateNumberCharacters(objwrkf_pagosplantillafecha.LOCATNID1,6)),
                    new SqlParameter("@Custname", convertextension.ValidateNumberCharacters(objwrkf_pagosplantillafecha.CUSTNAME1,100)),
                    new SqlParameter("@Ultimopago1",  Convert.ToDouble(objwrkf_pagosplantillafecha.UltimoPago11)),
                    new SqlParameter("@Ultimopago2",  Convert.ToDouble(objwrkf_pagosplantillafecha.UltimoPago21)),
                    new SqlParameter("@Ultimopago3",  Convert.ToDouble(objwrkf_pagosplantillafecha.UltimoPago31)),
                    new SqlParameter("@Idchequera", convertextension.ValidateNumberCharacters(objwrkf_pagosplantillafecha.IdChequera1,100)),
                    new SqlParameter("@Acnmvndr", convertextension.ValidateNumberCharacters(objwrkf_pagosplantillafecha.ACNMVNDR1,100)),
                    new SqlParameter("@Estatus", convertextension.ValidateNumberCharacters(objwrkf_pagosplantillafecha.Estatus1,100)),
                    new SqlParameter("@Comprobanteiva", convertextension.ValidateNumberCharacters(objwrkf_pagosplantillafecha.ComprobanteIVA1,100)),
                    new SqlParameter("@Comprobanteislr", convertextension.ValidateNumberCharacters(objwrkf_pagosplantillafecha.ComprobanteISLR1,100)),
                    new SqlParameter("@Nrocontrol", convertextension.ValidateNumberCharacters(objwrkf_pagosplantillafecha.NroControl1,100)),
                    new SqlParameter("@Descripcionfactura", convertextension.ValidateNumberCharacters(objwrkf_pagosplantillafecha.DescripcionFactura1,100)),
                    new SqlParameter("@Montoservicio",  Convert.ToDouble(objwrkf_pagosplantillafecha.MontoServicio1)),
                    new SqlParameter("@Referencia", convertextension.ValidateNumberCharacters(objwrkf_pagosplantillafecha.Referencia1,100)),
                    new SqlParameter("@Complementofactura", convertextension.ValidateNumberCharacters(objwrkf_pagosplantillafecha.ComplementoFactura1,100)),
                    new SqlParameter("@CartaSolicitud", convertextension.ValidateNumberCharacters(objwrkf_pagosplantillafecha.CartaSolicitud1,100)),
                    new SqlParameter("@Tienda", convertextension.ValidateNumberCharacters(objwrkf_pagosplantillafecha.Tienda1,6)),
                    new SqlParameter("@Montoadelanto",  Convert.ToDouble(objwrkf_pagosplantillafecha.MontoAdelanto1)),
                    new SqlParameter("@Usuario", convertextension.ValidateNumberCharacters(usuario,15)),
                    new SqlParameter("@Rif", convertextension.ValidateNumberCharacters(objwrkf_pagosplantillafecha.Rif1,30))
                });

                //building the sql query for the insert
                StringBuilder SqlQuery = new StringBuilder("insert into workflow.PlantillaTmp ");
                SqlQuery.Append("(Codigo, CodigoItem, CodigoPlantilla, Monto, FechaFactura, FechaVencimiento, ");
                SqlQuery.Append("FechaPago, NroFactura, TipoPago, IdProveedor, Vendname, Locatnid, Custname, ");
                SqlQuery.Append("Ultimopago1, Ultimopago2, Ultimopago3, Idchequera, Acnmvndr, Estatus, ");
                SqlQuery.Append("Comprobanteiva, Comprobanteislr, Nrocontrol, Descripcionfactura, Montoservicio, Referencia, ");
                SqlQuery.Append("Complementofactura, CartaSolicitud, Tienda, Montoadelanto, Usuario, Rif) ");
                SqlQuery.Append("values (@Codigo, @CodigoItem, @CodigoPlantilla, @Monto, @FechaFactura, @FechaVencimiento, ");
                SqlQuery.Append("@FechaPago, @NroFactura, @TipoPago, @IdProveedor, @Vendname, @Locatnid, @Custname, @Ultimopago1, @Ultimopago2, ");
                SqlQuery.Append("@Ultimopago3, @Idchequera, @Acnmvndr, @Estatus, @Comprobanteiva, @Comprobanteislr, @Nrocontrol, @Descripcionfactura, ");
                SqlQuery.Append("@Montoservicio, @Referencia, @Complementofactura, @CartaSolicitud, @Tienda, @Montoadelanto, @Usuario, @Rif)");

                //run the sql query
                wrkf_respuestaoperacion.RegistrosProcesadosx = Sqlprovider.ExecuteTransactionSqlString(SqlQuery.ToString(), CommandType.Text, sqlconexion, transaccion);

                //doing commit the transaction
                if (wrkf_respuestaoperacion.RegistrosProcesadosx > 0)
                {
                    transaccion.Commit();
                }
                else
                {
                    transaccion.Rollback();
                    wrkf_dbmensajeerror.RegistrarLogErrores(600, string.Format("El item de pago {0} no se logro registrar", objwrkf_pagosplantillafecha.CodigoItem1), usuario, "GestionPago_DbMtxPlantilla/InsertPaymentByTemplate");
                }
            }
            catch(Exception ex)
            {
                transaccion.Rollback();
                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), usuario, "GestionPago_DbMtxPlantilla/InsertPaymentByTemplate");
            }

            Sqlprovider.CerrarConexionDB(sqlconexion);

            return wrkf_respuestaoperacion;
        }

        /// <summary>
        /// This private method get template's detail and showing by a web page
        /// </summary>
        /// <param name="templatecode"></param>
        /// <param name="paymentdatefrom"></param>
        /// <param name="paymentdateuntil"></param>
        /// <returns></returns>
        private List<Wrkf_PagosPlantillaFecha> GetPaymentsByTemplate(string templatecode, string paymentdatefrom, string paymentdateuntil, string usuario)
        {
            List<Wrkf_PagosPlantillaFecha> lstwrkf_pagosplantillasfecha = new List<Wrkf_PagosPlantillaFecha>();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            try
            {
                SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@pCodigoPlantilla", templatecode),
                    new SqlParameter("@pFechaDesde", paymentdatefrom),
                    new SqlParameter("@pFechaHasta", paymentdateuntil),
                    new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                    new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                    new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                    new SqlParameter("@pTituloError", SqlDbType.VarChar, 60)
                });

                Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
                Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;
                Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;
                Sqlprovider.Oparameters[6].Direction = ParameterDirection.Output;

                DataTable DtRegistros = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("workflow.PL_Sel_PagoPorPlantilla", 
                                                                                                CommandType.StoredProcedure, 
                                                                                                out Dictionary<string, string> outparam);

                int total_registros = DtRegistros.Rows.Count;

                if (total_registros > 0)
                {
                    for (int i = 0; i < total_registros; i++)
                    {
                        double Montoaux = Convert.ToDouble(DtRegistros.Rows[i]["Monto"]);
                        double UltimoPago1aux = Convert.ToDouble(DtRegistros.Rows[i]["UltimoPago1"]);
                        double UltimoPago2aux = Convert.ToDouble(DtRegistros.Rows[i]["UltimoPago2"]);
                        double UltimoPago3aux = Convert.ToDouble(DtRegistros.Rows[i]["UltimoPago3"]);
                        double MontoServicioaux = Convert.ToDouble(DtRegistros.Rows[i]["MontoServicio"]);
                        double MontoAdelanto1aux = Convert.ToDouble(DtRegistros.Rows[i]["MontoAdelanto"]);

                        Wrkf_PagosPlantillaFecha wrkf_PagosPlantillaFecha = new Wrkf_PagosPlantillaFecha()
                        {
                            Codigo1 = DtRegistros.Rows[i]["Codigo"].ToString(),
                            CodigoItem1 = DtRegistros.Rows[i]["CodigoItem"].ToString(),
                            CodigoPlantilla1 = DtRegistros.Rows[i]["CodigoPlantilla"].ToString(),
                            Monto1 = Montoaux.ToString("N", new CultureInfo("is-IS")),
                            FechaFactura1 = DtRegistros.Rows[i]["FechaFactura"].ToString(),
                            FechaVencimiento1 = DtRegistros.Rows[i]["FechaVencimiento"].ToString(),
                            FechaPago1 = DtRegistros.Rows[i]["FechaPago"].ToString(),
                            NroFactura1 = DtRegistros.Rows[i]["NroFactura"].ToString(),
                            TipoPago1 = DtRegistros.Rows[i]["TipoPago"].ToString(),
                            IdProveedor1 = DtRegistros.Rows[i]["IdProveedor"].ToString(),
                            VENDNAME1 = DtRegistros.Rows[i]["VENDNAME"].ToString(),
                            LOCATNID1 = DtRegistros.Rows[i]["LOCATNID"].ToString(),
                            CUSTNAME1 = DtRegistros.Rows[i]["CUSTNAME"].ToString(),
                            UltimoPago11 = UltimoPago1aux.ToString("N", new CultureInfo("is-IS")),
                            UltimoPago21 = UltimoPago2aux.ToString("N", new CultureInfo("is-IS")),
                            UltimoPago31 = UltimoPago3aux.ToString("N", new CultureInfo("is-IS")),
                            IdChequera1 = DtRegistros.Rows[i]["IdChequera"].ToString(),
                            ACNMVNDR1 = DtRegistros.Rows[i]["ACNMVNDR"].ToString(),
                            Estatus1 = DtRegistros.Rows[i]["Estatus"].ToString(),
                            ComprobanteIVA1 = DtRegistros.Rows[i]["ComprobanteIVA"].ToString(),
                            ComprobanteISLR1 = DtRegistros.Rows[i]["ComprobanteISLR"].ToString(),
                            NroControl1 = DtRegistros.Rows[i]["NroControl"].ToString(),
                            DescripcionFactura1 = DtRegistros.Rows[i]["DescripcionFactura"].ToString(),
                            MontoServicio1 = MontoServicioaux.ToString("N", new CultureInfo("is-IS")),
                            Referencia1 = DtRegistros.Rows[i]["Referencia"].ToString(),
                            ComplementoFactura1 = DtRegistros.Rows[i]["Referencia"].ToString(),
                            CartaSolicitud1 = DtRegistros.Rows[i]["CartaSolicitud"].ToString(),
                            Tienda1 = DtRegistros.Rows[i]["Tienda"].ToString(),
                            MontoAdelanto1 = MontoAdelanto1aux.ToString("N", new CultureInfo("is-IS")),
                            Seleccionado1 = Convert.ToBoolean(DtRegistros.Rows[i]["Seleccionado"].ToString())
                        };

                        lstwrkf_pagosplantillasfecha.Add(wrkf_PagosPlantillaFecha);
                    }
                }
            }
            catch (Exception ex)
            {
                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), usuario, "GestionPago_DbMtxPlantilla/GetPaymentsByTemplate");
            }

            return lstwrkf_pagosplantillasfecha;
        }

        /// <summary>
        /// Selecciona los pagos en la base de datos que seran enviados a cuentas por pagar
        /// </summary>
        /// <param name="codigoitem"></param>
        /// <param name="codigoplantilla"></param>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion SeleccionarPagosPlantillas(string codigo, string codigoitem, string codigoplantilla, bool seleccionado, string usuario)
        {
            SqlTransaction transaccion = null;
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            SqlConnection sqlconexion = Sqlprovider.SqlConnection();
            Wrkf_RespuestaOperacion wrkf_respuestaoperacion = new Wrkf_RespuestaOperacion();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            try
            {
                //inicio de la trasaccion
                transaccion = Sqlprovider.InicioTransaccion("SeleccionarPagosPlantillas", sqlconexion);

                Sqlprovider.Oparameters = new List<SqlParameter>();
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@Codigo", codigo),
                    new SqlParameter("@CodigoItem", codigoitem),
                    new SqlParameter("@CodigoPlantilla", codigoplantilla),
                    new SqlParameter("@Seleccionado", seleccionado),
                    new SqlParameter("@Usuario", usuario.ToUpper().Trim())
                });

                StringBuilder SqlQuery = new StringBuilder("update workflow.PlantillaTmp ");
                SqlQuery.Append("set Seleccionado = @Seleccionado ");
                SqlQuery.Append("where codigo = @Codigo and CodigoItem = @CodigoItem and CodigoPlantilla = @CodigoPlantilla");

                wrkf_respuestaoperacion.RegistrosProcesadosx = Sqlprovider.ExecuteTransactionSqlString(SqlQuery.ToString(), CommandType.Text, sqlconexion, transaccion);

                if (wrkf_respuestaoperacion.RegistrosProcesadosx > 0)
                {
                    transaccion.Commit();
                }
                else
                {
                    transaccion.Rollback();
                    wrkf_respuestaoperacion.Codigox = "DBPT001";
                    wrkf_respuestaoperacion.Mensajex = string.Format("El registro de la plantilla {0} con el código {1} y código de item {2} no se logro actualizar", codigoplantilla, codigo, codigoitem);
                    wrkf_respuestaoperacion.Tipox = "error";
                    wrkf_respuestaoperacion.Titulox = "Seleccionar los Pagos de Plantillas";
                }
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                wrkf_respuestaoperacion.Codigox = "EXCEPTION";
                wrkf_respuestaoperacion.Mensajex = string.Format("El registro de la plantilla {0} con el código {1} y código de item {2} no se logro actualizar", codigoplantilla, codigo, codigoitem);
                wrkf_respuestaoperacion.Tipox = "error";
                wrkf_respuestaoperacion.Titulox = "Seleccionar los Pagos de Plantillas";

                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), usuario, "GestionPago_DbMtxPlantilla/SeleccionarPagosPlantillas");
            }

            Sqlprovider.CerrarConexionDB(sqlconexion);

            return wrkf_respuestaoperacion;
        }

        /// <summary>
        /// The method selects and remove all the payment items of the template
        /// </summary>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion SelectRemoveAllPaymentItems(string templatecode, bool select_remove, string user)
        {
            SqlTransaction transaccion;
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            SqlConnection sqlconexion = Sqlprovider.SqlConnection();
            Wrkf_RespuestaOperacion wrkf_respuestaoperacion = new Wrkf_RespuestaOperacion();

            //inicio de la trasaccion
            transaccion = Sqlprovider.InicioTransaccion("SelectAllPaymentItems", sqlconexion);

            Sqlprovider.Oparameters = new List<SqlParameter>();
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@CodigoPlantilla", templatecode),
                new SqlParameter("@select_remove", select_remove)
            });

            string SqlQuery = "update workflow.PlantillaTmp set Seleccionado = @select_remove where CodigoPlantilla = @CodigoPlantilla";

            wrkf_respuestaoperacion.RegistrosProcesadosx = Sqlprovider.ExecuteTransactionSqlString(SqlQuery, CommandType.Text, sqlconexion, transaccion);

            if (wrkf_respuestaoperacion.RegistrosProcesadosx > 0)
            {
                transaccion.Commit();
            }
            else
            {
                transaccion.Rollback();
            }

            Sqlprovider.CerrarConexionDB(sqlconexion);

            return wrkf_respuestaoperacion;
        }

        /// <summary>
        /// This method delete the template by code from the table [workflow].[PlantillaTmp]
        /// </summary>
        /// <param name="templatecode"></param>
        /// <returns></returns>
        private Wrkf_RespuestaOperacion DeleteTemplateByCode(string templatecode, string usuario)
        {
            SqlTransaction transaccion = null;
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            SqlConnection sqlconexion = Sqlprovider.SqlConnection();
            Wrkf_RespuestaOperacion wrkf_respuestaoperacion = new Wrkf_RespuestaOperacion();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            try
            {
                //inicio de la trasaccion
                transaccion = Sqlprovider.InicioTransaccion("DeleteTemplateByCode", sqlconexion);

                Sqlprovider.Oparameters = new List<SqlParameter>();
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@CodigoPlantilla", templatecode)
                });

                string SqlQuery = "delete from workflow.PlantillaTmp where CodigoPlantilla = @CodigoPlantilla";

                wrkf_respuestaoperacion.RegistrosProcesadosx = Sqlprovider.ExecuteTransactionSqlString(SqlQuery, CommandType.Text, sqlconexion, transaccion);

                if (wrkf_respuestaoperacion.RegistrosProcesadosx > 0)
                {
                    transaccion.Commit();
                }
                else
                {
                    transaccion.Rollback();
                    wrkf_respuestaoperacion.Codigox = "DBPT002";
                    wrkf_respuestaoperacion.Mensajex = string.Format("El registro de la plantilla {0} no se logro eliminar", templatecode);
                    wrkf_respuestaoperacion.Tipox = "error";
                    wrkf_respuestaoperacion.Titulox = "Eliminar Plantillas";
                }
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                wrkf_respuestaoperacion.Codigox = "EXCEPTION";
                wrkf_respuestaoperacion.Mensajex = string.Format("El registro de la plantilla {0} no se logro eliminar", templatecode);
                wrkf_respuestaoperacion.Tipox = "error";
                wrkf_respuestaoperacion.Titulox = "Eliminar Plantillas";

                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), usuario, "GestionPago_DbMtxPlantilla/DeleteTemplateByCode");
            }

            Sqlprovider.CerrarConexionDB(sqlconexion);

            return wrkf_respuestaoperacion;
        }

        /// <summary>
        /// Envia los pagos de las plantillas al workflow para la aprobación de cuentas por pagar
        /// </summary>
        /// <param name="Codigoplantilla"></param>
        /// <param name="Nombreplantilla"></param>
        /// <param name="Gruporubro_Id"></param>
        /// <param name="Rubro_Id"></param>
        /// <param name="Usuario"></param>
        /// <returns></returns>
        public Wrkf_RespuestaOperacion EnviarPlantillaCxP(string Codigoplantilla, string Nombreplantilla, int Gruporubro_Id, string Rubro_Id, string curncyid, string Usuario)
        {
            Wrkf_RespuestaOperacion wrkf_RespuestaOperacion = new Wrkf_RespuestaOperacion();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            try
            {
                SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@pCodigoplantilla", Codigoplantilla.Trim()),
                    new SqlParameter("@pNombreplantilla", Nombreplantilla.Trim().ToUpper()),
                    new SqlParameter("@pGruporubro_Id", Gruporubro_Id),
                    new SqlParameter("@pRubro_Id", Rubro_Id.Trim().ToUpper()),
                    new SqlParameter("@pcurncyid", curncyid),
                    new SqlParameter("@pUsuarioregistro", Usuario.Trim().ToUpper()),
                    new SqlParameter("@pSolicitudordenpago_Id", SqlDbType.Int),
                    new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                    new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                    new SqlParameter("@pTipo", SqlDbType.VarChar, 20),
                    new SqlParameter("@pTitulo", SqlDbType.VarChar, 60)
                });

                Sqlprovider.Oparameters[6].Direction = ParameterDirection.Output;
                Sqlprovider.Oparameters[7].Direction = ParameterDirection.Output;
                Sqlprovider.Oparameters[8].Direction = ParameterDirection.Output;
                Sqlprovider.Oparameters[9].Direction = ParameterDirection.Output;
                Sqlprovider.Oparameters[10].Direction = ParameterDirection.Output;

                int result = Sqlprovider.ExecuteStoredProcedureWithOutputParameter2("workflow.PL_InsUpd_EnviarPlantillaCxP", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

                if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
                {
                    wrkf_RespuestaOperacion.Codigox = outparam["@pCodigoError"];
                    wrkf_RespuestaOperacion.Mensajex = outparam["@pMensajeError"];
                    wrkf_RespuestaOperacion.Tipox = outparam["@pTipo"];
                    wrkf_RespuestaOperacion.Titulox = outparam["@pTitulo"];
                }
                else
                {
                    wrkf_RespuestaOperacion.NumeroRegistroOrdenPagox = Convert.ToInt32(outparam["@pSolicitudordenpago_Id"]);
                }
            }
            catch (Exception ex)
            {
                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Usuario, "GestionPago_DbMtxPlantilla/EnviarPlantillaCxP");
            }

            return wrkf_RespuestaOperacion;
        }
    }
}