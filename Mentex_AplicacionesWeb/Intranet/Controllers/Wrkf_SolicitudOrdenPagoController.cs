using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Ado.DbContent;
using Intranet.Models;
using Intranet.Utilities;
using EncryptDecrypt;

namespace Intranet.Controllers
{
    public class Wrkf_SolicitudOrdenPagoController : Controller
    {
        // GET: Wrkf_SolicitudOrdenPago

        /// <summary>
        /// Muestra la vista para registrar la solicitud de orden de pago
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SolicitudOrdenPago()
        {
            List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
            Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
            List<Wrkf_FormaPago> lstformapago;
            Wrkf_DbFormaPago objdbformapago = new Wrkf_DbFormaPago();
            List<Wrkf_TipoDocumento> lsttipodocumento;
            Wrkf_DbTipoDocumento objdbtipodocumento = new Wrkf_DbTipoDocumento();
            List<GestionPago_EntMtxConcepto> lstConcepto;
            GestionPago_DatMtxConcepto objDbConceptos = new GestionPago_DatMtxConcepto();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            Wrkf_SolicitudOrdenPagoPlantillaController objSolicitudOrdenPagoPlantilla = new Wrkf_SolicitudOrdenPagoPlantillaController();

            //Verificar que la sesión de usuario no esta activa cierra la sesion del usuario
            if ((Session["sUsuario_Id"] == null) || (Session["sUsuario_Id"].ToString() == ""))
            {
                return RedirectToAction("CerrarSesion", "Wrkf_Login");
            }
            else
            {
                try
                {
                    //Obtener una lista con las opciones de menu
                    lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());
                    ViewBag.listaropcionesmenu = lstopcionesmenuitem;

                    //Obtener una lista de las formas de pago
                    lstformapago = objdbformapago.GetFormasPago();
                    ViewBag.listaformapago = lstformapago;

                    //Obtener una lista de los tipos de documentos
                    lsttipodocumento = objdbtipodocumento.GetTipoDocumentos();
                    ViewBag.listatipodocumento = lsttipodocumento;

                    lstConcepto = objDbConceptos.ListarConceptosTodos();
                    ViewBag.listaconceptos = lstConcepto;

                }
                catch (Exception ex)
                {
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Session["sUsuario_Id"].ToString(), "Wrkf_SolicitudOrdenPago/SolicitudOrdenPago");
                }
            }

            return View();
        }

        /// <summary>
        /// Genera un reporte con el detalle de la solicitud de orden de pago enviada a CxP
        /// </summary>
        /// <param name="Solicitudordenpago_Id"></param>
        /// <returns></returns>
        public ActionResult ReporteSolicitudEnviadaCxP(int Solicitudordenpago_Id)
        {
            List<Wrkf_ListaPagosPorRubroId> lst_wrkflistapagosporrubroid = new List<Wrkf_ListaPagosPorRubroId>();
            Wrkf_ListaPagosPorRubroId wrkf_listapagosporrubroid = new Wrkf_ListaPagosPorRubroId();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            Wrkf_DbSolicitudOrdenPago wrkf_dbsolicitudordenpago = new Wrkf_DbSolicitudOrdenPago();

            //Verificar que la sesión de usuario este activa
            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                wrkf_listapagosporrubroid.Codigox = mensajeerror.Codigox;
                wrkf_listapagosporrubroid.Mensajex = mensajeerror.Mensajex;
                wrkf_listapagosporrubroid.Tipox = mensajeerror.Tipox;
                wrkf_listapagosporrubroid.Titulox = mensajeerror.Titulox;

                lst_wrkflistapagosporrubroid.Add(wrkf_listapagosporrubroid);
            }
            else
            {
                try
                {
                    lst_wrkflistapagosporrubroid = wrkf_dbsolicitudordenpago.ReporteSolicitudEnviadaCxP(Solicitudordenpago_Id);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    wrkf_listapagosporrubroid.Codigox = mensajeerror.Codigox;
                    wrkf_listapagosporrubroid.Mensajex = mensajeerror.Mensajex;
                    wrkf_listapagosporrubroid.Tipox = mensajeerror.Tipox;
                    wrkf_listapagosporrubroid.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoController/ReporteSolicitudEnviadaCxP");

                    lst_wrkflistapagosporrubroid.Add(wrkf_listapagosporrubroid);
                }
            }

            ViewBag.listapagosrubroid = lst_wrkflistapagosporrubroid;

            return View();
        }

        /// <summary>
        /// El método registra y actuliza los datos de la solicitud de la orden de pago
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddItemPago(string[] Item)
        {
            //genera una lista con los id generados del encabezado y detalle de la solicitud de pago
            List<Wrkf_RespuestaOperacion> lstRespuestaOperacion = new List<Wrkf_RespuestaOperacion>();
            Wrkf_SolicitudOrdenPago objSolicitudOrdenPago = new Wrkf_SolicitudOrdenPago();
            Wrkf_DbSolicitudOrdenPago objDbSolicitudOrdenPago = new Wrkf_DbSolicitudOrdenPago();
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();
            ConvertExtension objconvertextension = new ConvertExtension();
            GestionPago_DbDiaFeriado objdiaferiado = new GestionPago_DbDiaFeriado();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            //GAP
            string vCodigoSolicitud = Item[20].Trim();
            string vCodigoPlantilla = Item[0].Trim();
            double vMonto = Convert.ToDouble(Item[18].Trim());
            string vDescripcionFactura = Item[8].Trim();
            string vFechaFactura = Item[11].Trim();
            string vFechaPago = Item[12].Trim();
            string vNroFactura = Item[9].Trim();
            string vNroControl = Item[10].Trim();
            int vCodigoConcepto = Convert.ToInt32(Item[21].Trim());
            string vIdProveedor = Item[3].Trim();
            double vMontoExento = Convert.ToDouble(Item[14].Trim());


            //workflow
            int vCodigoGrupoRubro = Convert.ToInt32(Item[1].Trim());
            string vCodigoRubro = Item[2].Trim();
            int vTipoDocumento = Convert.ToInt32(Item[7].Trim());
            int vtipoPago = Convert.ToInt32(Item[13].Trim());
            double vBaseIvaGE = Convert.ToDouble(Item[16].Trim());
            double vBaseIvaRe = Convert.ToDouble(Item[17].Trim());
            double vBaseIvaAd = Convert.ToDouble(Item[22].Trim());
            string vPlanImpuesto = Item[6].Trim();
            string vCodigoMoneda = Item[15].Trim();
            string vObservaciones = Item[19].Trim();
            string UsuarioSesion = Session["sUsuario_Id"].ToString().Trim();

            bool requerido = false;

            //Verificar que la sesión de usuario este activa
            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                objRespuestaOperacion.Codigox = mensajeerror.Codigox;
                objRespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                objRespuestaOperacion.Tipox = mensajeerror.Tipox;
                objRespuestaOperacion.Titulox = mensajeerror.Titulox;
            }
            else
            {
                try
                {
                    //verificar si la fecha de pago es un dia sabado o domingo Item[3] representa la fecha de pago
                    if (requerido == false)
                    {
                        if (objconvertextension.VerificarSabadoDomingo(Convert.ToDateTime(vFechaPago)))
                        {
                            mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("ADD002", "AddItemPago");
                            objRespuestaOperacion.Codigox = mensajeerror.Codigox;
                            objRespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                            objRespuestaOperacion.Tipox = mensajeerror.Tipox;
                            objRespuestaOperacion.Titulox = mensajeerror.Titulox;
                            requerido = true;
                        }
                    }

                    //verifica si la fecha es un dia feriado Item[3] representa la fecha de pago
                    if (requerido == false)
                    {

                        if (objdiaferiado.GetDiaFeriado(Convert.ToDateTime(vFechaPago)))
                        {
                            mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("ADD003", "AddItemPago");
                            objRespuestaOperacion.Codigox = mensajeerror.Codigox;
                            objRespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                            objRespuestaOperacion.Tipox = mensajeerror.Tipox;
                            objRespuestaOperacion.Titulox = mensajeerror.Titulox;
                            requerido = true;
                        }
                    }

                    //verifica que la fecha de la factura no sea mayor a la fecha de pago
                    if (requerido == false)
                    {
                        if (objconvertextension.CompararFechas(Convert.ToDateTime(vFechaFactura), Convert.ToDateTime(vFechaPago)) > 0)
                        {
                            mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("ADD001", "AddItemPago");
                            objRespuestaOperacion.Codigox = mensajeerror.Codigox;
                            objRespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                            objRespuestaOperacion.Tipox = mensajeerror.Tipox;
                            objRespuestaOperacion.Titulox = mensajeerror.Titulox;
                            requerido = true;
                        }
                    }

                    //Se registra la solicitud de orden de pago
                    if (requerido == false)
                    {
                        //obtiene el proximo correlativo de la solicitud de orden de pago
                        if (string.IsNullOrEmpty(vCodigoSolicitud))
                        {
                            string strProximoCorrelativo = objDbSolicitudOrdenPago.ProximoCorrelativo(vCodigoPlantilla);
                            objSolicitudOrdenPago.Codigo = strProximoCorrelativo;
                        }
                        else
                        {
                            objSolicitudOrdenPago.Codigo = vCodigoSolicitud;
                        }

                        //GAP
                        objSolicitudOrdenPago.GAPCodigoPlantilla= vCodigoPlantilla;
                        objSolicitudOrdenPago.GAPMonto = vMonto;
                        objSolicitudOrdenPago.GAPDescripcionFactura = vDescripcionFactura;
                        objSolicitudOrdenPago.GAPFechaFactura = vFechaFactura;
                        objSolicitudOrdenPago.GAPFechaPago = vFechaPago;
                        objSolicitudOrdenPago.GAPEstatus = 2; //Aprobado
                        objSolicitudOrdenPago.GAPNroFactura = vNroFactura;
                        objSolicitudOrdenPago.GAPNroControl = vNroControl;
                        objSolicitudOrdenPago.GAPTipoPago = "T"; //Transferencia
                        objSolicitudOrdenPago.GAPCodigoConcepto = vCodigoConcepto;
                        objSolicitudOrdenPago.GAPIdProveedor = vIdProveedor;
                        objSolicitudOrdenPago.GAPProveedor = "";
                        objSolicitudOrdenPago.GAPRIF = "";
                        objSolicitudOrdenPago.GAPCuentaBancaria = "";
                        objSolicitudOrdenPago.GAPEmail = "";
                        objSolicitudOrdenPago.GAPPorcentajeRetencion = 0;
                        objSolicitudOrdenPago.GAPMontoExento = vMontoExento;
                        objSolicitudOrdenPago.GAPBaseImpIVAAdicional = vBaseIvaAd;
                        objSolicitudOrdenPago.GAPBaseImpIVAReducido = vBaseIvaRe;
                        objSolicitudOrdenPago.GAPBaseImpIVAGeneral = vBaseIvaGE;
                        objSolicitudOrdenPago.GAPTienda = "";
                        objSolicitudOrdenPago.GAPUsuarioAprueba = UsuarioSesion;
                        objSolicitudOrdenPago.GAPFechaAprobacion = objconvertextension.FormatoFechayyyyMMdd(DateTime.Now).ToString();

                        //workflow
                        objSolicitudOrdenPago.GrupoRubro_Id = vCodigoGrupoRubro;
                        objSolicitudOrdenPago.Rubro_Id = vCodigoRubro;
                        objSolicitudOrdenPago.Tipodocumento = vTipoDocumento;
                        objSolicitudOrdenPago.TipoPago = vtipoPago;
                        objSolicitudOrdenPago.MontoIva = 0.00;
                        objSolicitudOrdenPago.MontoRetenido = 0.00;
                        objSolicitudOrdenPago.PlanImpuesto = vPlanImpuesto;
                        objSolicitudOrdenPago.CodigoMoneda = vCodigoMoneda;
                        objSolicitudOrdenPago.Observaciones = vObservaciones;

                        objRespuestaOperacion = objDbSolicitudOrdenPago.AddSolicitudOrdenPago(objSolicitudOrdenPago, UsuarioSesion);
                    }

                    lstRespuestaOperacion.Add(objRespuestaOperacion);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    objRespuestaOperacion.Codigox = mensajeerror.Codigox;
                    objRespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                    objRespuestaOperacion.Tipox = mensajeerror.Tipox;
                    objRespuestaOperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), UsuarioSesion, "Wrkf_SolicitudOrdenPagoController/AddItemPago");
                }
            }

            return Json(lstRespuestaOperacion, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// El método lista todas las solicitudes de las ordenes de pagos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetSolicitudOrdenPagoTodas()
        {
            List<Wrkf_SolicitudOrdenPago> lstSolicitudOrdenPago = new List<Wrkf_SolicitudOrdenPago>();
            Wrkf_DbSolicitudOrdenPago objDbSolicitudOrdenPago = new Wrkf_DbSolicitudOrdenPago();
            Wrkf_SolicitudOrdenPago objwrkfsolicitudordenpago = new Wrkf_SolicitudOrdenPago();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            //Verificar que la sesión de usuario este activa
            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                objwrkfsolicitudordenpago.Codigox = mensajeerror.Codigox;
                objwrkfsolicitudordenpago.Mensajex = mensajeerror.Mensajex;
                objwrkfsolicitudordenpago.Tipox = mensajeerror.Tipox;
                objwrkfsolicitudordenpago.Titulox = mensajeerror.Titulox;

                lstSolicitudOrdenPago.Add(objwrkfsolicitudordenpago);
            } 
            else
            {
                try
                {
                    lstSolicitudOrdenPago = objDbSolicitudOrdenPago.GetSolicitudOrdenPagoTodas(Convert.ToString(Session["sUsuario_Id"]));
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                    objwrkfsolicitudordenpago.Codigox = mensajeerror.Codigox;
                    objwrkfsolicitudordenpago.Mensajex = mensajeerror.Mensajex;
                    objwrkfsolicitudordenpago.Tipox = mensajeerror.Tipox;
                    objwrkfsolicitudordenpago.Titulox = mensajeerror.Titulox;

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoController/GetSolicitudOrdenPagoTodas");

                    lstSolicitudOrdenPago.Add(objwrkfsolicitudordenpago);
                }
            }

            return Json(new { ListadoSolicitudPago = lstSolicitudOrdenPago });
        }

        /// <summary>
        /// El método lista el encabezado de la solicitud de la orden de pago
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetSolicitudOrdenPagoPorId(int solicitud_orden_pago_id)
        {
            List<Wrkf_SolicitudOrdenPago> lstSolicitudOrdenPago = new List<Wrkf_SolicitudOrdenPago>();
            Wrkf_DbSolicitudOrdenPago objDbSolicitudOrdenPago = new Wrkf_DbSolicitudOrdenPago();
            Wrkf_SolicitudOrdenPago wrkf_solicitudordenpago = new Wrkf_SolicitudOrdenPago();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            //Verificar que la sesión de usuario este activa
            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");

                wrkf_solicitudordenpago.Codigox = mensajeerror.Codigox;
                wrkf_solicitudordenpago.Mensajex = mensajeerror.Mensajex;
                wrkf_solicitudordenpago.Tipox = mensajeerror.Tipox;
                wrkf_solicitudordenpago.Titulox = mensajeerror.Titulox;

                lstSolicitudOrdenPago.Add(wrkf_solicitudordenpago);
            }
            else
            {
                try
                {
                    //lstSolicitudOrdenPago = objDbSolicitudOrdenPago.GetSolicitudOrdenPagoPorId(solicitud_orden_pago_id);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                    wrkf_solicitudordenpago.Codigox = mensajeerror.Codigox;
                    wrkf_solicitudordenpago.Mensajex = mensajeerror.Mensajex;
                    wrkf_solicitudordenpago.Tipox = mensajeerror.Tipox;
                    wrkf_solicitudordenpago.Titulox = mensajeerror.Titulox;

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoController/GetSolicitudOrdenPagoPorId");

                    lstSolicitudOrdenPago.Add(wrkf_solicitudordenpago);
                }
            }

            return Json(lstSolicitudOrdenPago, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// El método lista el detalle de la solicitud de la orden de pago
        /// </summary>
        /// <param name="solicitud_orden_pago_id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetDetalleOrdenPagoPorId(int solicitud_orden_pago_id)
        {
            List<Wrkf_SolicitudOrdenPagoDetalle> lstSolicitudOrdenPagoDetalle = new List<Wrkf_SolicitudOrdenPagoDetalle>();
            Wrkf_DbSolicitudOrdenPago objDbSolicitudOrdenPago = new Wrkf_DbSolicitudOrdenPago();
            Wrkf_SolicitudOrdenPagoDetalle objwrkfsolicitudordenpagodetalle = new Wrkf_SolicitudOrdenPagoDetalle();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            //Verificar que la sesión de usuario este activa
            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");

                objwrkfsolicitudordenpagodetalle.Codigox = mensajeerror.Codigox;
                objwrkfsolicitudordenpagodetalle.Mensajex = mensajeerror.Mensajex;
                objwrkfsolicitudordenpagodetalle.Tipox = mensajeerror.Tipox;
                objwrkfsolicitudordenpagodetalle.Titulox = mensajeerror.Titulox;

                lstSolicitudOrdenPagoDetalle.Add(objwrkfsolicitudordenpagodetalle);
            }
            else
            {
                try
                {
                    lstSolicitudOrdenPagoDetalle = objDbSolicitudOrdenPago.GetDetalleOrdenPagoPorId(solicitud_orden_pago_id);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                    objwrkfsolicitudordenpagodetalle.Codigox = mensajeerror.Codigox;
                    objwrkfsolicitudordenpagodetalle.Mensajex = mensajeerror.Mensajex;
                    objwrkfsolicitudordenpagodetalle.Tipox = mensajeerror.Tipox;
                    objwrkfsolicitudordenpagodetalle.Titulox = mensajeerror.Titulox;

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoController/GetDetalleOrdenPagoPorId");

                    lstSolicitudOrdenPagoDetalle.Add(objwrkfsolicitudordenpagodetalle);
                }
            }

            return Json(lstSolicitudOrdenPagoDetalle, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Calcular los montos de la solicitud de la orden de pago
        /// </summary>
        /// <param name="calculariva"></param>
        /// <param name="calcularretencion"></param>
        /// <param name="cantidad"></param>
        /// <param name="preciounitario"></param>
        /// <param name="anticipo"></param>
        /// <param name="porcentajeretencion"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCalcularMontos(double pMontoDocumento, double pBaseIvaGe, double pBaseIvaRe, double pBaseIvaAd, string pPlanImpuesto)
        {
            List<Wrkf_CalcularMontos> lstMontos = new List<Wrkf_CalcularMontos>();
            Wrkf_DbCalcularMontoSolicitud objdbcalcularmontosolicitud = new Wrkf_DbCalcularMontoSolicitud();
            Wrkf_CalcularMontos objwrkfcalcularmontos = new Wrkf_CalcularMontos();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            //Verificar que la sesión de usuario este activa
            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");

                objwrkfcalcularmontos.Codigox = mensajeerror.Codigox;
                objwrkfcalcularmontos.Mensajex = mensajeerror.Mensajex;
                objwrkfcalcularmontos.Tipox = mensajeerror.Tipox;
                objwrkfcalcularmontos.Titulox = mensajeerror.Titulox;

                lstMontos.Add(objwrkfcalcularmontos);
            }
            else
            {
                try
                {
                    lstMontos = objdbcalcularmontosolicitud.CalcularMontosSolicitud(pMontoDocumento, pBaseIvaGe, pBaseIvaRe, pBaseIvaAd,  pPlanImpuesto);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                    objwrkfcalcularmontos.Codigox = mensajeerror.Codigox;
                    objwrkfcalcularmontos.Mensajex = mensajeerror.Mensajex;
                    objwrkfcalcularmontos.Tipox = mensajeerror.Tipox;
                    objwrkfcalcularmontos.Titulox = mensajeerror.Titulox;

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoController/GetCalcularMontos");

                    lstMontos.Add(objwrkfcalcularmontos);
                }
            }

            return Json(lstMontos, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Subir los soportes de pagos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadSoportesPago()
        {
            Wrkf_DbParametros wrkf_dbparametros = new Wrkf_DbParametros();
            Wrkf_Parametros wrkf_parametros;
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            ConvertExtension ObjConvertExtension = new ConvertExtension();
            string ruta, vFechaDirectorio;

            try
            {
                //obtener la ruta donde se guardan los soportes digitales del pago
                wrkf_parametros = wrkf_dbparametros.SeleccionarParametroCodigo("RUTSOP", Session["sUsuario_Id"].ToString().Trim().ToUpper());

                ruta = wrkf_parametros.ValorAlfaNumerico1.Trim();

                string vPlantilla = Request["CodigoPlantilla"];
                string vCodigoPago = Request["CodigoPago"];
                string vCodigoItemPago = Request["CodigoItemPago"];
                string vFechaPagoItem = Request["FechaPagoItem"];

                vFechaDirectorio = ObjConvertExtension.FormatoFechayyyyMMdd(Convert.ToDateTime(vFechaPagoItem)).ToString();

                /*ruta donde se guardan los adjuntos del pago*/
                ruta += vPlantilla + "\\" + vFechaDirectorio + "\\" + vCodigoItemPago;

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                    int fileSize = file.ContentLength;
                    string fileName = file.FileName;
                    string mimeType = file.ContentType;
                    System.IO.Stream fileContent = file.InputStream;

                    //crear el directorio para guardar los soportes de la solicitud
                    if (!Directory.Exists(ruta))
                    {
                        System.IO.Directory.CreateDirectory(ruta);
                    }

                    //guardar los soportes
                    file.SaveAs(@ruta + "\\" + fileName);
                }
            }
            catch (Exception ex)
            {
                Wrkf_RespuestaOperacion wrkf_respuestaoperacion = new Wrkf_RespuestaOperacion();
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                wrkf_respuestaoperacion.Codigox = mensajeerror.Codigox;
                wrkf_respuestaoperacion.Mensajex = mensajeerror.Mensajex;
                wrkf_respuestaoperacion.Tipox = mensajeerror.Tipox;
                wrkf_respuestaoperacion.Titulox = mensajeerror.Titulox;

                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoController/UploadSoportesPago");
            }

            return Json("Uploaded " + Request.Files.Count + " files");
        }

        /// <summary>
        /// Obtiene la fecha actual del sistema
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetFechaActual()
        {
            string dia, mes;
            List<FechaHora> lstFecha = new List<FechaHora>();
            FechaHora objFechaHora = new FechaHora();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            //verificar la sesion del usuario
            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");

                objFechaHora.Codigox = mensajeerror.Codigox;
                objFechaHora.Mensajex = mensajeerror.Mensajex;
                objFechaHora.Tipox = mensajeerror.Tipox;
                objFechaHora.Titulox = mensajeerror.Titulox;

                lstFecha.Add(objFechaHora);
            }
            else
            {
                try
                {
                    if (DateTime.Now.Day <= 9)
                    {
                        dia = "0" + Convert.ToString(DateTime.Now.Day);
                    }
                    else
                    {
                        dia = Convert.ToString(DateTime.Now.Day);
                    }

                    if (DateTime.Now.Month <= 9)
                    {
                        mes = "0" + Convert.ToString(DateTime.Now.Month);
                    }
                    else
                    {
                        mes = Convert.ToString(DateTime.Now.Month);
                    }

                    objFechaHora.Fecha_actual = Convert.ToString(DateTime.Now.Year) + "-" + mes + "-" + dia;

                    lstFecha.Add(objFechaHora);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                    objFechaHora.Codigox = mensajeerror.Codigox;
                    objFechaHora.Mensajex = mensajeerror.Mensajex;
                    objFechaHora.Tipox = mensajeerror.Tipox;
                    objFechaHora.Titulox = mensajeerror.Titulox;

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoController/GetFechaActual");

                    lstFecha.Add(objFechaHora);
                }
            }

            return Json(lstFecha, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Descargar el soporte del pago
        /// </summary>
        /// <param name="soporte_id"></param>
        /// <returns></returns>
        [HttpGet]
        public FileResult Download(int soporte_id)
        {
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            var ruta2= "";

            //verificar la sesion del usuario
            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                wrkf_dbmensajeerror.RegistrarLogErrores(Convert.ToInt32(mensajeerror.Codigox), mensajeerror.Mensajex, Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoController/Download");
            }
            else
            {
                try
                {
                    ruta2 = Server.MapPath("~/Soporte/");
                }
                catch (Exception ex)
                {
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoController/Download");
                }
            }

            return File(ruta2, "application/pdf", "file.pdf");
        }

        /// <summary>
        /// Anula la solicitud de orden de pago que se encuentra en estatus 1 en el GAP
        /// </summary>
        /// <param name="pCodigo"></param>
        /// <returns></returns>
        public JsonResult AnularSolicitudOrdenPago(string pCodigo)
        {
            Wrkf_RespuestaOperacion objrespuestaoperacion = new Wrkf_RespuestaOperacion();
            Wrkf_DbSolicitudOrdenPago objdbsolicitudordenpago = new Wrkf_DbSolicitudOrdenPago();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            //verificar la sesion del usuario
            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");

                objrespuestaoperacion.Codigox = mensajeerror.Codigox;
                objrespuestaoperacion.Mensajex = mensajeerror.Mensajex;
                objrespuestaoperacion.Tipox = mensajeerror.Tipox;
                objrespuestaoperacion.Titulox = mensajeerror.Titulox;
            }
            else
            {
                try
                {
                    objrespuestaoperacion = objdbsolicitudordenpago.AnularSolicitudOrdenPago(pCodigo, Convert.ToString(Session["sUsuario_Id"]));
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                    objrespuestaoperacion.Codigox = mensajeerror.Codigox;
                    objrespuestaoperacion.Mensajex = mensajeerror.Mensajex;
                    objrespuestaoperacion.Tipox = mensajeerror.Tipox;
                    objrespuestaoperacion.Titulox = mensajeerror.Titulox;

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoController/AnularSolicitudOrdenPago");
                }
            }

            return Json(objrespuestaoperacion, JsonRequestBehavior.AllowGet);
        }
    }
}