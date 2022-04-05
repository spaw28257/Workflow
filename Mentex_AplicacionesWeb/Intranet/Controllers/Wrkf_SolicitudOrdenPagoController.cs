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
            Wrkf_SolicitudOrdenPagoDetalle objSolicitudOrdenPagoDetalle = new Wrkf_SolicitudOrdenPagoDetalle();
            ConvertExtension objConvertExtension = new ConvertExtension();
            Wrkf_DbSolicitudOrdenPago objDbSolicitudOrdenPago = new Wrkf_DbSolicitudOrdenPago();
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();
            ConvertExtension objconvertextension = new ConvertExtension();
            GestionPago_DbDiaFeriado objdiaferiado = new GestionPago_DbDiaFeriado();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

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
                        if (objconvertextension.VerificarSabadoDomingo(Item[3]) == true)
                        {
                            mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("00004", "Wrkf_SolicitudOrdenPagoController/AddItemPago");
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
                        if (objdiaferiado.GetDiaFeriado(Item[3]) == true)
                        {
                            mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("00005", "Wrkf_SolicitudOrdenPagoController/AddItemPago");
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
                        //encabezado de la solicitud de orden pago
                        objSolicitudOrdenPago.curncyidx = Convert.ToString(Item[0]);
                        objSolicitudOrdenPagoDetalle.Rubro_Idx = Convert.ToString(Item[1]);
                        objSolicitudOrdenPagoDetalle.FechaDocumentox = Convert.ToDateTime(Item[2]);
                        objSolicitudOrdenPagoDetalle.Fechapagox = Convert.ToDateTime(Item[3]);

                        //detalle del encabezado de la solicitud orden pago
                        objSolicitudOrdenPagoDetalle.IdProveedorx = Convert.ToString(Item[4]);
                        objSolicitudOrdenPagoDetalle.Proveedorx = Convert.ToString(Item[5]);
                        objSolicitudOrdenPagoDetalle.Descripcionx = Convert.ToString(Item[6]);
                        objSolicitudOrdenPagoDetalle.Numerodocumentox = Convert.ToString(Item[7]);
                        objSolicitudOrdenPagoDetalle.Cantidadx = objConvertExtension.FormatoNumeroDecimal(Convert.ToDouble(Item[8])).ToString();
                        objSolicitudOrdenPagoDetalle.Preciounitariox = objConvertExtension.FormatoNumeroDecimal(Convert.ToDouble(Item[9])).ToString();
                        objSolicitudOrdenPagoDetalle.Anticipox = objConvertExtension.FormatoNumeroDecimal(Convert.ToDouble(Item[10])).ToString();
                        objSolicitudOrdenPagoDetalle.Subtotalx = objConvertExtension.FormatoNumeroDecimal(Convert.ToDouble(Item[11])).ToString();
                        objSolicitudOrdenPagoDetalle.Totalx = objConvertExtension.FormatoNumeroDecimal(Convert.ToDouble(Item[12])).ToString();
                        objSolicitudOrdenPagoDetalle.TipoDocumentox = Convert.ToInt32(Item[15]);
                        objSolicitudOrdenPagoDetalle.Calculaivax = Convert.ToBoolean(Item[16]);
                        objSolicitudOrdenPagoDetalle.Realizaretencionx = Convert.ToBoolean(Item[17]);
                        objSolicitudOrdenPagoDetalle.Porcentajeivax = objConvertExtension.FormatoNumeroDecimal(Convert.ToDouble(Item[18])).ToString();
                        objSolicitudOrdenPagoDetalle.Montoivax = objConvertExtension.FormatoNumeroDecimal(Convert.ToDouble(Item[19])).ToString();
                        objSolicitudOrdenPagoDetalle.Porcentajeretencionx = objConvertExtension.FormatoNumeroDecimal(Convert.ToDouble(Item[20])).ToString();
                        objSolicitudOrdenPagoDetalle.Totalretenidox = objConvertExtension.FormatoNumeroDecimal(Convert.ToDouble(Item[21])).ToString();
                        objSolicitudOrdenPagoDetalle.Gruporubro_Idx = Convert.ToInt32(Item[22]);
                        objSolicitudOrdenPago.Solicitudordenpago_Idx = Convert.ToInt32(Item[13]);
                        objSolicitudOrdenPagoDetalle.Solicitudordenpagodetalle_Idx = Convert.ToInt32(Item[14]);
                        objSolicitudOrdenPagoDetalle.Formapago_Idx = Convert.ToInt32(Item[23]);
                        objSolicitudOrdenPagoDetalle.Observacionesx = Convert.ToString(Item[24]);

                        //verificar si la solicitud ya esta creada
                        objRespuestaOperacion = objDbSolicitudOrdenPago.ExisteSolicitud(objSolicitudOrdenPago.Solicitudordenpago_Idx, Convert.ToString(Session["sUsuario_Id"]));

                        if (objRespuestaOperacion.RespuestaSioNox == true)
                        {
                            //verificar que todos los pagos de la solicitud tengan la misma moneda
                            objRespuestaOperacion = objDbSolicitudOrdenPago.VerificarMonedaSolicitud(objSolicitudOrdenPago.Solicitudordenpago_Idx, objSolicitudOrdenPago.curncyidx, Convert.ToString(Session["sUsuario_Id"]));

                            if (objRespuestaOperacion.RespuestaSioNox == false)
                            {
                                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("00006", "Wrkf_SolicitudOrdenPagoController/AddItemPago");
                                objRespuestaOperacion.Codigox = mensajeerror.Codigox;
                                objRespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                                objRespuestaOperacion.Tipox = mensajeerror.Tipox;
                                objRespuestaOperacion.Titulox = mensajeerror.Titulox;
                                requerido = true;
                            }
                            else
                            {
                                requerido = false;
                            }
                        }
                        else
                        {
                            requerido = false;
                        }

                        //registra los datos de la solicitud de orden de pago
                        if (requerido == false)
                        {
                            objRespuestaOperacion = objDbSolicitudOrdenPago.AddSolicitudOrdenPago(objSolicitudOrdenPago, objSolicitudOrdenPagoDetalle, Convert.ToString(Session["sUsuario_Id"]));
                        }
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
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoController/AddItemPago");
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

            return Json(lstSolicitudOrdenPago, JsonRequestBehavior.AllowGet);
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
                    lstSolicitudOrdenPago = objDbSolicitudOrdenPago.GetSolicitudOrdenPagoPorId(solicitud_orden_pago_id);
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
        public JsonResult GetCalcularMontos(bool calculariva, bool calcularretencion, double cantidad, double preciounitario, double anticipo, double porcentajeretencion)
        {
            List<Wrkf_CalcularMontos> lstMontos = new List<Wrkf_CalcularMontos>();
            Wrkf_DbCalcularMontoSolicitud objdbcalcularmontosolicitud = new Wrkf_DbCalcularMontoSolicitud();
            Wrkf_CalcularMontos objwrkfcalcularmontos = new Wrkf_CalcularMontos();
            ConvertExtension objConvertExtension = new ConvertExtension();
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
                    preciounitario = objConvertExtension.FormatoNumeroDecimal(preciounitario);
                    cantidad = objConvertExtension.FormatoNumeroDecimal(cantidad);
                    anticipo = objConvertExtension.FormatoNumeroDecimal(anticipo);

                    lstMontos = objdbcalcularmontosolicitud.CalcularMontosSolicitud(calculariva, calcularretencion, cantidad, preciounitario, anticipo, porcentajeretencion);
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
        /// Obtener a los proveedores por clase
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProveedoresPorClaseId()
        {
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            List<Wrkf_Proveedores> lstProveedores = new List<Wrkf_Proveedores>();
            Wrkf_DbProveedor objProveedor = new Wrkf_DbProveedor();
            Wrkf_Proveedores wrkf_proveedores = new Wrkf_Proveedores();
            

            //verificar la sesion del usuario
            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");

                wrkf_proveedores.Codigox = mensajeerror.Codigox;
                wrkf_proveedores.Mensajex = mensajeerror.Mensajex;
                wrkf_proveedores.Tipox = mensajeerror.Tipox;
                wrkf_proveedores.Titulox = mensajeerror.Titulox;

                lstProveedores.Add(wrkf_proveedores);
            }
            else
            {
                try
                {
                    lstProveedores = objProveedor.GetListadoProveedor();
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                    wrkf_proveedores.Codigox = mensajeerror.Codigox;
                    wrkf_proveedores.Mensajex = mensajeerror.Mensajex;
                    wrkf_proveedores.Tipox = mensajeerror.Tipox;
                    wrkf_proveedores.Titulox = mensajeerror.Titulox;

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoController/GetCalcularMontos");

                    lstProveedores.Add(wrkf_proveedores);
                }
            }

            return Json(lstProveedores, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Obtiene los proveedores por nombre
        /// </summary>
        /// <param name="vendname"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetListadoProveedorPorNombre(string vendname)
        {
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            List<Wrkf_Proveedores> lstProveedores = new List<Wrkf_Proveedores>();
            Wrkf_DbProveedor objProveedor = new Wrkf_DbProveedor();
            Wrkf_Proveedores wrkf_proveedores = new Wrkf_Proveedores();

            //verificar la sesion del usuario
            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");

                wrkf_proveedores.Codigox = mensajeerror.Codigox;
                wrkf_proveedores.Mensajex = mensajeerror.Mensajex;
                wrkf_proveedores.Tipox = mensajeerror.Tipox;
                wrkf_proveedores.Titulox = mensajeerror.Titulox;

                lstProveedores.Add(wrkf_proveedores);
            }
            else
            {
                try
                {
                    lstProveedores = objProveedor.GetListadoProveedorPorNombre(vendname);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                    wrkf_proveedores.Codigox = mensajeerror.Codigox;
                    wrkf_proveedores.Mensajex = mensajeerror.Mensajex;
                    wrkf_proveedores.Tipox = mensajeerror.Tipox;
                    wrkf_proveedores.Titulox = mensajeerror.Titulox;

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoController/GetCalcularMontos");

                    lstProveedores.Add(wrkf_proveedores);
                }
            }

            return Json(lstProveedores, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Subir los soportes de pagos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadSoportesPago()
        {
            Wrkf_DbSolicitudOrdenPago objsolicitudordenpago = new Wrkf_DbSolicitudOrdenPago();
            Wrkf_DbParametros wrkf_dbparametros = new Wrkf_DbParametros();
            Wrkf_Parametros wrkf_parametros = new Wrkf_Parametros();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            string directorio = Request["directorio"];
            string numero_solicitud = Request["numero_solicitud"];
            string numero_solicitud_det_id = Request["numero_solicitud_det_id"];
            string ruta;

            try
            {
                //obtener la ruta donde se guardan los soportes digitales del pago
                wrkf_parametros = wrkf_dbparametros.SeleccionarParametroCodigo("RUTSOP", Session["sUsuario_Id"].ToString().Trim().ToUpper());

                ruta = wrkf_parametros.ValorAlfaNumerico1.Trim();

                ruta += @directorio + "\\" + numero_solicitud;

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

                    //si el archivo se guardo en el directorio se guardan los datos en la base de datos
                    if (System.IO.File.Exists(@ruta + "\\" + fileName))
                    {
                        Wrkf_SolicitudOrdenPagoSoporte objsolicitudpagosoporte = new Wrkf_SolicitudOrdenPagoSoporte();
                        objsolicitudpagosoporte.Solicitudordenpago_Idx = Convert.ToInt32(numero_solicitud);
                        objsolicitudpagosoporte.Solicitudordenpagodetalle_Idx = Convert.ToInt32(numero_solicitud_det_id);
                        objsolicitudpagosoporte.RutaDirectoriox = @ruta + "\\";
                        objsolicitudpagosoporte.NombreArchivox = fileName;
                        Wrkf_RespuestaOperacion objrespuestaoperacion = new Wrkf_RespuestaOperacion();
                        objrespuestaoperacion = objsolicitudordenpago.AddSoporteSolicitudOrdenPago(objsolicitudpagosoporte);
                    }
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

        ///// <summary>
        ///// El método realiza una busqueda de los soportes de pagos asociados a la solicitud 
        ///// </summary>
        ///// <param name="directorio"></param>
        ///// <param name="numero_solicitud"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult ListarSoportePago(int Solicitudordenpago_Id, int Solicitudordenpagodetalle_Id)
        //{
        //    List<Wrkf_SolicitudOrdenPagoSoporte> lstSoportePago = new List<Wrkf_SolicitudOrdenPagoSoporte>();
        //    Wrkf_DbSolicitudOrdenPago objsolicitudordenpago = new Wrkf_DbSolicitudOrdenPago();
        //    Wrkf_SolicitudOrdenPagoSoporte objwrkfsolicitudordenpagosoporte = new Wrkf_SolicitudOrdenPagoSoporte();

        //    try
        //    {
        //        lstSoportePago = objsolicitudordenpago.GetListarSoportes(Solicitudordenpago_Id, Solicitudordenpagodetalle_Id);
        //    }
        //    catch (Exception ex)
        //    {
        //        objwrkfsolicitudordenpagosoporte.Codigox = ex.HResult.ToString();
        //        objwrkfsolicitudordenpagosoporte.Mensajex = ex.Message.ToString();
        //        objwrkfsolicitudordenpagosoporte.Tipox = "error";
        //        objwrkfsolicitudordenpagosoporte.Titulox = "Solicitud Orden de Pago";

        //        lstSoportePago.Add(objwrkfsolicitudordenpagosoporte);
        //    }

        //    return Json(lstSoportePago, JsonRequestBehavior.AllowGet);
        //}

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
        /// Método para registrar los datos de los soportes de pago Path.Combine(Server.MapPath("~/MyFiles") 
        /// </summary>
        /// <param name="SoportesPago"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddSoportePago(Wrkf_SolicitudOrdenPagoSoporte SoportesPago)
        {
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();
            Wrkf_DbSolicitudOrdenPago objAddSoportePago = new Wrkf_DbSolicitudOrdenPago();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            //verificar la sesion del usuario
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
                    objRespuestaOperacion = objAddSoportePago.AddSoporteSolicitudOrdenPago(SoportesPago);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                    objRespuestaOperacion.Codigox = mensajeerror.Codigox;
                    objRespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                    objRespuestaOperacion.Tipox = mensajeerror.Tipox;
                    objRespuestaOperacion.Titulox = mensajeerror.Titulox;

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoController/AddSoportePago");
                }
            }

            return Json(objRespuestaOperacion, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Método para enviar la solicitud de la orden de pago al jefe de cuenta por pagar
        /// </summary>
        /// <param name="Solicitudordenpago_Id"></param>
        /// <param name="Usuariomodifico"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EnviaraCXP(int Solicitudordenpago_Id)
        {
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();
            Wrkf_DbSolicitudOrdenPago objAddSoportePago = new Wrkf_DbSolicitudOrdenPago();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            //verificar la sesion del usuario
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
                    objRespuestaOperacion = objAddSoportePago.EnviaraCXP(Solicitudordenpago_Id, Convert.ToString(Session["sUsuario_Id"]));
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                    objRespuestaOperacion.Codigox = mensajeerror.Codigox;
                    objRespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                    objRespuestaOperacion.Tipox = mensajeerror.Tipox;
                    objRespuestaOperacion.Titulox = mensajeerror.Titulox;

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoController/EnviaraCXP");
                }
            }

            return Json(objRespuestaOperacion, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// El método anula la solicitud de la orden de pago
        /// </summary>
        /// <param name="Solicitudordenpago_Id"></param>
        /// <returns></returns>
        public JsonResult AnularSolicitudOrdenPago(int Solicitudordenpago_Id)
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
                    objrespuestaoperacion = objdbsolicitudordenpago.AnularSolicitudOrdenPago(Solicitudordenpago_Id, Convert.ToString(Session["sUsuario_Id"]));
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

        /// <summary>
        /// El método anula un pago en especifico de la solicitud.
        /// </summary>
        /// <param name="Solicitudordenpagodetalle_Id"></param>
        /// <param name="Solicitudordenpago_Id"></param>
        /// <param name="Usuariomodifico"></param>
        /// <returns></returns>
        public JsonResult AnularPagoEspecificoSolicitud(int Solicitudordenpagodetalle_Id, int Solicitudordenpago_Id)
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
                    objrespuestaoperacion = objdbsolicitudordenpago.AnularPagoEspecificoSolicitud(Solicitudordenpagodetalle_Id, Solicitudordenpago_Id, Convert.ToString(Session["sUsuario_Id"]));
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                    objrespuestaoperacion.Codigox = mensajeerror.Codigox;
                    objrespuestaoperacion.Mensajex = mensajeerror.Mensajex;
                    objrespuestaoperacion.Tipox = mensajeerror.Tipox;
                    objrespuestaoperacion.Titulox = mensajeerror.Titulox;

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoController/AnularPagoEspecificoSolicitud");
                }
            }

            return Json(objrespuestaoperacion, JsonRequestBehavior.AllowGet);
        }
    }
}