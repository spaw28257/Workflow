using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Ado.DbContent;
using Intranet.Models;
using Intranet.Utilities;

namespace Intranet.Controllers
{
    public class Wrkf_SolicitudOrdenPagoPlantillaController : Controller
    {
        /// <summary>
        /// Especifica la plantilla que se va a utilizar para ser enviada a cuentas por pagar
        /// </summary>
        /// <returns></returns>
        public ActionResult SolicitudOrdenPagoPlantilla()
        {
            List<Wrkf_OpcionesMenuItem> lstwrkf_OpcionesMenuItems;
            List<GestionPago_MtxPlantilla> lstgestionPago_MtxPlantillas;
            GestionPago_DbMtxPlantilla GestionPago_DbMtxPlantilla = new GestionPago_DbMtxPlantilla();
            Wrkf_DbOpcionesMenu wrkf_DbOpcionesMenu = new Wrkf_DbOpcionesMenu();
            Wrkf_DbMensajeError wrkf_DbMensajeError = new Wrkf_DbMensajeError();
            MensajeError mensajeError;
            Wrkf_DiaIFSemana wrkfdiaifsemana;
            ConvertExtension objconvertextension = new ConvertExtension();

            //Verificar que la sesión de usuario no esta activa cierra la sesion del usuario
            if ((Session["sUsuario_Id"] == null) || (Session["sUsuario_Id"].ToString() == ""))
            {
                return RedirectToAction("CerrarSesion", "Wrkf_Login");
            }
            else
            {
                try
                {
                    lstwrkf_OpcionesMenuItems = wrkf_DbOpcionesMenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());

                    //verificar si la lista tiene las opciones del menu del controlar para mostrar en la vista
                    if (lstwrkf_OpcionesMenuItems.Count > 0)
                    {
                        ViewBag.listaropcionesmenu = lstwrkf_OpcionesMenuItems;
                    }
                    else
                    {
                        mensajeError = wrkf_DbMensajeError.GetObtenerMensajeError("MNU001", "MENUOPCION");
                        ViewBag.CodigoError = mensajeError.Codigox;
                        ViewBag.MensajeError = mensajeError.Mensajex;
                        ViewBag.TipoError = mensajeError.Tipox;
                        ViewBag.TituloError = mensajeError.Titulox;
                    }

                    //listar las plantillas de pagos fruentes
                    lstgestionPago_MtxPlantillas = GestionPago_DbMtxPlantilla.ListarPlantillasActivas(Session["sUsuario_Id"].ToString(), false);
                    ViewBag.ListadoPlantilla = lstgestionPago_MtxPlantillas.ToList();

                    //obtener fecha actual del sistema
                    string format = "yyyy-MM-dd";
                    var now = DateTime.Now.ToString(format);

                    wrkfdiaifsemana = objconvertextension.ObtenerPrimerDiaSemana(Convert.ToDateTime(now));
                    string fechadesde = wrkfdiaifsemana.Primerdiasemana;
                    string fechahasta = wrkfdiaifsemana.Ultimodiasemana;

                    ViewBag.fechadesde = fechadesde;
                    ViewBag.fechahasta = fechahasta;
                }
                catch (Exception ex)
                {
                    mensajeError = wrkf_DbMensajeError.GetObtenerMensajeError("EXC999", "EXCEPCION");
                    ViewBag.CodigoError = mensajeError.Codigox;
                    ViewBag.MensajeMnu = mensajeError.Mensajex;
                    ViewBag.TipoMnu = mensajeError.Tipox;
                    ViewBag.TituloMnu = mensajeError.Titulox;

                    wrkf_DbMensajeError.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Session["sUsuario_Id"].ToString(), "Wrkf_SolicitudOrdenPagoPlantillaController/SolicitudOrdenPagoPlantilla");
                }
            }

            return View();
        }

        [HttpPost]
        public JsonResult ListarPlantillasActivas2()
        {
            List<GestionPago_MtxPlantilla> lstgestionPago_MtxPlantillas = new List<GestionPago_MtxPlantilla>();
            GestionPago_DbMtxPlantilla GestionPago_DbMtxPlantilla = new GestionPago_DbMtxPlantilla();
            Wrkf_DbMensajeError wrkf_DbMensajeError = new Wrkf_DbMensajeError();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();

            //Verificar que la sesión de usuario este activa
            if ((Session["sUsuario_Id"] == null) || (Session["sUsuario_Id"].ToString() == ""))
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
                    //listar las plantillas de pagos fruentes
                    lstgestionPago_MtxPlantillas = GestionPago_DbMtxPlantilla.ListarPlantillasActivas(Session["sUsuario_Id"].ToString(), false);
                }
                catch (Exception ex)
                {
                    //se obtiene el mensaje de error
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("EXC999", "EXCEPCION");

                    GestionPago_MtxPlantilla gestionpago_mtxplantilla = new GestionPago_MtxPlantilla()
                    {
                        Codigox = mensajeerror.Codigox,
                        Mensajex = mensajeerror.Mensajex,
                        Tipox = mensajeerror.Tipox,
                        Titulox = mensajeerror.Titulox
                    };

                    lstgestionPago_MtxPlantillas.Add(gestionpago_mtxplantilla);

                    wrkf_DbMensajeError.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Session["sUsuario_Id"].ToString(), "Wrkf_SolicitudOrdenPagoPlantillaController/ListarPlantillasActivas2");
                }
            }

            return Json(new { data = lstgestionPago_MtxPlantillas });
        }

        [HttpPost]
        public ActionResult SolicitudOrdenPagoPlantillaItems()
        {
            List<Wrkf_OpcionesMenuItem> lstwrkf_OpcionesMenuItems;
            Wrkf_DbOpcionesMenu wrkf_DbOpcionesMenu = new Wrkf_DbOpcionesMenu();
            Wrkf_DbMensajeError wrkf_DbMensajeError = new Wrkf_DbMensajeError();
            MensajeError mensajeError;
            List<Wrkf_PagosPlantillaFecha> lstwrkf_PagosPlantillaFechas;
            GestionPago_DbMtxPlantilla GestionPago_DbMtxPlantilla = new GestionPago_DbMtxPlantilla();
            ConvertExtension objconvertextension = new ConvertExtension();

            //Verificar que la sesión de usuario no esta activa cierra la sesion del usuario
            if ((Session["sUsuario_Id"] == null) || (Session["sUsuario_Id"].ToString() == ""))
            {
                return RedirectToAction("CerrarSesion", "Wrkf_Login");
            }
            else
            {
                try
                {
                    lstwrkf_OpcionesMenuItems = wrkf_DbOpcionesMenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());

                    //verificar si la lista tiene las opciones del menu del controlar para mostrar en la vista
                    if (lstwrkf_OpcionesMenuItems.Count > 0)
                    {
                        ViewBag.listaropcionesmenu = lstwrkf_OpcionesMenuItems;
                    }
                    else
                    {
                        mensajeError = wrkf_DbMensajeError.GetObtenerMensajeError("MNU001", "MENUOPCION");
                        ViewBag.CodigoError = mensajeError.Codigox;
                        ViewBag.MensajeError = mensajeError.Mensajex;
                        ViewBag.TipoError = mensajeError.Tipox;
                        ViewBag.TituloError = mensajeError.Titulox;
                    }

                    string codigoplantilla = Request.Form["txtcodigoplantilla"];
                    string nombreplantilla = Request.Form["txtnombreplantilla"];
                    bool cargainicial = Convert.ToBoolean(Request.Form["hddcargainicial"]);
                    string fechadesde = objconvertextension.FormatoFechayyyyMMdd(Convert.ToDateTime(Request.Form["txtfechadesde"])).ToString();
                    string fechahasta = objconvertextension.FormatoFechayyyyMMdd(Convert.ToDateTime(Request.Form["txtfechahasta"])).ToString();
                    bool enviadoacxp = Convert.ToBoolean(Request.Form["hddenviadoacxp"]);

                    string proceso = "APROBAR";

                    ViewBag.codigoplantilla = codigoplantilla;
                    ViewBag.nombreplantilla = nombreplantilla;
                    ViewBag.fechadesde = Request.Form["txtfechadesde"].ToString();
                    ViewBag.fechahasta = Request.Form["txtfechahasta"].ToString();
                    ViewBag.proceso = proceso;

                    //obtiene los pagos asociados a la plantilla
                    lstwrkf_PagosPlantillaFechas = GestionPago_DbMtxPlantilla.LoadPaymentByTemplateGP(codigoplantilla, fechadesde, fechahasta, proceso, Session["sUsuario_Id"].ToString(), cargainicial);
                    ViewBag.listaitemspagos = lstwrkf_PagosPlantillaFechas.ToList();
                }
                catch (Exception ex)
                {
                    ViewBag.CodigoError = ex.HResult.ToString();
                    ViewBag.MensajeError = ex.Message.ToString();
                    ViewBag.TipoError = "error";
                    ViewBag.TituloError = "Solicitud Orden de Pago Plantillas Items";
                }
            }

            return View();
        }

        /// <summary>
        /// Selecciona los pagos  para ser enviados a cuentas por pagar
        /// </summary>
        /// <param name="codigoplantilla"></param>
        /// <param name="codigoplantilla"></param>
        /// <param name="seleccionado"></param>
        /// <returns></returns>
        public JsonResult SeleccionarPagosPlantillas(string codigo, string codigoitem, string codigoplantilla, bool seleccionado)
        {
            Wrkf_RespuestaOperacion wrkf_RespuestaOperacion = new Wrkf_RespuestaOperacion();
            GestionPago_DbMtxPlantilla gestionPago_DbMtxPlantilla = new GestionPago_DbMtxPlantilla();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();

            //Verificar que la sesión de usuario este activa
            if ((Session["sUsuario_Id"] == null) || (Session["sUsuario_Id"].ToString() == ""))
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
                    wrkf_RespuestaOperacion = gestionPago_DbMtxPlantilla.SeleccionarPagosPlantillas(codigo, codigoitem, codigoplantilla, seleccionado, Session["sUsuario_Id"].ToString());
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("EXC999", "EXCEPCION");
                    wrkf_RespuestaOperacion.Codigox = mensajeerror.Codigox;
                    wrkf_RespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                    wrkf_RespuestaOperacion.Tipox = mensajeerror.Tipox;
                    wrkf_RespuestaOperacion.Titulox = mensajeerror.Titulox;

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Session["sUsuario_Id"].ToString(), "Wrkf_SolicitudOrdenPagoPlantillaController/SeleccionarPagosPlantillas");
                }
            }

            return Json(wrkf_RespuestaOperacion, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Envia la plantilla a la aprobación de cuentas por pagar
        /// </summary>
        /// <param name="Codigoplantilla"></param>
        /// <param name="Nombreplantilla"></param>
        /// <param name="Gruporubro_Id"></param>
        /// <param name="Rubro_Id"></param>
        /// <returns></returns>
        public JsonResult EnviarPlantillaCxP(string Codigoplantilla, string Nombreplantilla, int Gruporubro_Id, string Rubro_Id, string curncyid)
        {
            Wrkf_RespuestaOperacion wrkf_RespuestaOperacion = new Wrkf_RespuestaOperacion();
            GestionPago_DbMtxPlantilla gestionPago_DbMtxPlantilla = new GestionPago_DbMtxPlantilla();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            try
            {
                wrkf_RespuestaOperacion = gestionPago_DbMtxPlantilla.EnviarPlantillaCxP(Codigoplantilla, Nombreplantilla, Gruporubro_Id, Rubro_Id, curncyid, Session["sUsuario_Id"].ToString());
            }
            catch (Exception ex)
            {
                wrkf_RespuestaOperacion.Codigox = ex.HResult.ToString();
                wrkf_RespuestaOperacion.Mensajex = ex.Message.ToString();
                wrkf_RespuestaOperacion.Tipox = "error";
                wrkf_RespuestaOperacion.Titulox = "Enviar Pagos a de Plantillas a CxP";

                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Session["sUsuario_Id"].ToString(), "GestionPago_DbMtxPlantilla/EnviarPlantillaCxP");
            }

            return Json(wrkf_RespuestaOperacion, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SolicitudOrdenPagoReporte(int solicitudordenpago_id)
        {
            Wrkf_DbSolicitudOrdenPago wrkf_DbSolicitudOrdenPago = new Wrkf_DbSolicitudOrdenPago();
            List<Wrkf_SolicitudOrdenPagoEncabDetal> lstwrkf_SolicitudOrdenPagoEncabDetals = new List<Wrkf_SolicitudOrdenPagoEncabDetal>();
            

            try
            {
                lstwrkf_SolicitudOrdenPagoEncabDetals = wrkf_DbSolicitudOrdenPago.SolicitudOrdenPagoReporte(solicitudordenpago_id);
            }
            catch (Exception ex)
            {
                Wrkf_SolicitudOrdenPagoEncabDetal wrkf_SolicitudOrdenPagoEncabDetal = new Wrkf_SolicitudOrdenPagoEncabDetal()
                {
                    Codigox = ex.HResult.ToString(),
                    Mensajex = ex.Message.ToString(),
                    Tipox = "error",
                    Titulox = "Reporte Plantilla Enviada a Cuentas Por Pagar"
                };
            }

            return View();
        }



        /// <summary>
        /// Obtiene los datos de la plantilla por código
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public JsonResult SeleccionarPlantillaPorCodigo2(string codigo)
        {
            GestionPago_DbMtxPlantilla gestionpago_dbmtxplantilla = new GestionPago_DbMtxPlantilla();
            GestionPago_MtxPlantilla gestionpago_mtxplantilla = new GestionPago_MtxPlantilla();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError objMensajeError;

            try
            {
                gestionpago_mtxplantilla = gestionpago_dbmtxplantilla.SeleccionarPlantillaPorCodigo(codigo);
            }
            catch (Exception ex)
            {
                objMensajeError = wrkf_dbmensajeerror.GetObtenerMensajeError("EXC999", "EXCEPCION");

                gestionpago_mtxplantilla.Codigox = objMensajeError.Codigox;
                gestionpago_mtxplantilla.Mensajex = objMensajeError.Mensajex;
                gestionpago_mtxplantilla.Tipox = objMensajeError.Tipox;
                gestionpago_mtxplantilla.Titulox = objMensajeError.Titulox;

                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Session["sUsuario_Id"].ToString(), "Wrkf_SolicitudOrdenPagoPlantilla/SeleccionarPlantillaPorCodigo2");
            }

            return Json(gestionpago_mtxplantilla, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Subir soportes de pagos de las plantillas
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadSoportesPago()
        {
            Wrkf_DbParametros wrkf_dbparametros = new Wrkf_DbParametros();
            Wrkf_Parametros wrkf_parametros = new Wrkf_Parametros();

            string numero_solicitud = Request["numero_solicitud"];
            string ruta;

            try
            {
                //obtener la ruta donde se guardan los soportes digitales del pago
                wrkf_parametros = wrkf_dbparametros.SeleccionarParametroCodigo("RUTSOP", Session["sUsuario_Id"].ToString().Trim().ToUpper());

                ruta = wrkf_parametros.ValorAlfaNumerico1.Trim();

                ruta += numero_solicitud;

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
            catch (Exception e)
            {
                Wrkf_RespuestaOperacion objrespuestaoperacion = new Wrkf_RespuestaOperacion();
                objrespuestaoperacion.Mensaje_Idx = -1;
                objrespuestaoperacion.Tipox = "error";
                objrespuestaoperacion.Codigox = "";
                objrespuestaoperacion.Mensajex = string.Format("Método UploadSoportesPago genero el siguiente error: {0}", e.Message.ToString().Trim());
            }

            return Json("Uploaded " + Request.Files.Count + " files");
        }

        /// <summary>
        /// The method selects and remove all the payment items of the template
        /// </summary>
        /// <param name="templatecode"></param>
        /// <param name="select_remove"></param>
        [HttpPost]
        public JsonResult SelectRemoveAllPaymentItems(string templatecode, bool select_remove)
        {
            GestionPago_DbMtxPlantilla gestionpago_dbmtxplantilla = new GestionPago_DbMtxPlantilla();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            Wrkf_RespuestaOperacion wrkf_respuestaoperacion = new Wrkf_RespuestaOperacion();

            try
            {
                wrkf_respuestaoperacion = gestionpago_dbmtxplantilla.SelectRemoveAllPaymentItems(templatecode, select_remove, Session["sUsuario_Id"].ToString().Trim().ToUpper());
            }
            catch(Exception ex)
            {
                wrkf_respuestaoperacion.Codigox = ex.HResult.ToString();
                wrkf_respuestaoperacion.Mensajex = "Error al momento de seleccionar todos los items de pago, verifique el Log de Errores";
                wrkf_respuestaoperacion.Tipox = "error";
                wrkf_respuestaoperacion.Titulox = "Seleccionando Todos los Items de Pago";
                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Session["sUsuario_Id"].ToString().Trim().ToUpper(), "SelectRemoveAllPaymentItems");
            }

            return Json(wrkf_respuestaoperacion, JsonRequestBehavior.AllowGet);
        }
    }
}