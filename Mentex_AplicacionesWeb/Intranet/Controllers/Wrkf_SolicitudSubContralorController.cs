using System;
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
    /// <summary>
    /// Clase para ejecutar las acciones del subcontralor al momento de aprobar o rechazar los pagos
    /// </summary>
    public class Wrkf_SolicitudSubContralorController : Controller
    {
        /// <summary>
        /// Muestra una vista con el listado degrupo de rubros con pagos pendientes por aprobar subcontraloria 
        /// </summary>
        /// <returns></returns>
        public ActionResult ListaGrupoRubroSubcontraloria()
        {
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();
            //Verificar que la sesión de usuario este activa
            if (Session["sUsuario_Id"] == null)
            {
                return RedirectToAction("CerrarSesion", "Wrkf_Login");
            }
            else
            {
                try
                {
                    //Obtener una lista con las opciones de menu
                    List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem = new List<Wrkf_OpcionesMenuItem>();
                    Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
                    lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());
                    ViewBag.listaropcionesmenu = lstopcionesmenuitem;

                    //Obtener los grupo de rubros con los pagos asociados
                    Wrkf_DbSolicitudSubContralor objsolicitudsubcontralor = new Wrkf_DbSolicitudSubContralor();
                    List<Wrkf_Departamento> lstgruporubro = objsolicitudsubcontralor.lstGrupoRubroSubContralor();
                    ViewBag.listagruporubrospagos = lstgruporubro;

                    //obtener los grupo de rubros con las notas de creditos pendientes
                    List<Wrkf_Departamento> lstgruporubronotacreditossubcont = objsolicitudsubcontralor.lstGrupoRubroNotasCreditoSubContralor();
                    ViewBag.listagruporubronotacreditosubcontr = lstgruporubronotacreditossubcont;
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    objRespuestaOperacion.Codigox = mensajeerror.Codigox;
                    objRespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                    objRespuestaOperacion.Tipox = mensajeerror.Tipox;
                    objRespuestaOperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudSubContralorController/ListaGrupoRubroSubcontraloria");
                }
            }

            return View();
        }

        /// <summary>
        /// Muestra una vista con el listado de rubros con los pagos asociados para la revición de SubContraloria.
        /// </summary>
        /// <returns></returns>
        public ActionResult ListarRubrosPagosSubContraloria(int gruporubro_id)
        {
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();

            //Verificar que la sesión de usuario este activa
            if (Session["sUsuario_Id"] == null)
            {
                return RedirectToAction("CerrarSesion", "Wrkf_Login");
            }
            else
            {
                try
                {
                    //Obtener una lista con las opciones de menu
                    List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
                    Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
                    lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());
                    ViewBag.listaropcionesmenu = lstopcionesmenuitem;

                    //Obtener los rubros con los pagos asociados
                    Wrkf_DbSolicitudSubContralor objsolicitudsubcontralor = new Wrkf_DbSolicitudSubContralor();
                    List<Wrkf_Rubro> lstrubro = objsolicitudsubcontralor.lstRubrosSubContraloria(gruporubro_id);
                    ViewBag.listarubrospagos = lstrubro;

                    //Obtener los rubros con notas de creditos
                    List<Wrkf_Rubro> lstrubronotascreditosubcontr = objsolicitudsubcontralor.lstRubrosNotasCreditosSubContraloria(gruporubro_id);
                    ViewBag.listarubronotacreditos = lstrubronotascreditosubcontr;
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    objRespuestaOperacion.Codigox = mensajeerror.Codigox;
                    objRespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                    objRespuestaOperacion.Tipox = mensajeerror.Tipox;
                    objRespuestaOperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudSubContralorController/ListarRubrosPagosSubContraloria");
                }
            }

            return View();
        }

        /// <summary>
        /// Muestra una vista con el listado de la solicitud de pago verifica si el tipo de documento es una nota de credito
        /// </summary>
        /// <returns></returns>
        public ActionResult ListaPagosPorRubroIdSubContraloria(string rubro_id, string fechadesde, string fechahasta, string tipodocumento)
        {
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();
            List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
            Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
            Wrkf_DiaIFSemana wrkfdiaifsemana;
            ConvertExtension objconvertextension = new ConvertExtension();
            List<Wrkf_ListaPagosPorRubroId> lstpagosporrubro;
            Wrkf_DbSolicitudSubContralor objsolicitudsubcontralor = new Wrkf_DbSolicitudSubContralor();

            //Verificar que la sesión de usuario este activa
            if (Session["sUsuario_Id"] == null)
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

                    string format = "yyyy-MM-dd";
                    var now = DateTime.Now.ToString(format);

                    //verificar los rangos de fecha
                    if ((fechadesde == null || fechadesde == "") && (fechahasta == null || fechahasta == ""))
                    {
                        wrkfdiaifsemana = objconvertextension.ObtenerPrimerDiaSemana(Convert.ToDateTime(now));
                        fechadesde = wrkfdiaifsemana.Primerdiasemana;
                        fechahasta = wrkfdiaifsemana.Ultimodiasemana;
                    }

                    ViewBag.rubro_id = rubro_id.Trim();
                    ViewBag.fechadesde = fechadesde.Trim();
                    ViewBag.fechahasta = fechahasta.Trim();
                    ViewBag.tipodocu = tipodocumento.Trim();

                    //verificar el tipo de documento
                    if (tipodocumento.Trim() != "NC")
                    {
                        lstpagosporrubro = objsolicitudsubcontralor.ListaPagosPorRubroIdSubContraloriaDb(rubro_id, Convert.ToDateTime(fechadesde), Convert.ToDateTime(fechahasta));
                    }
                    else
                    {
                        lstpagosporrubro = objsolicitudsubcontralor.ListaNotaCreditosPorRubroIdSubContraloria(rubro_id, Convert.ToDateTime(fechadesde), Convert.ToDateTime(fechahasta));
                    }

                    ViewBag.listarpagossubcontraloria = lstpagosporrubro;
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    objRespuestaOperacion.Codigox = mensajeerror.Codigox;
                    objRespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                    objRespuestaOperacion.Tipox = mensajeerror.Tipox;
                    objRespuestaOperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudSubContralorController/ListaPagosPorRubroIdSubContraloria");
                }
            }

            return View();
        }

        /// <summary>
        /// Muestra una vista con el listado de la solicitud de pago post verifica si el tipo de documento es una nota de credito
        /// </summary>
        /// <returns></returns>
        public ActionResult ListaPagosPorRubroIdSubContraloriaPost()
        {
            //Obtener una lista con las opciones de menu
            List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
            Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
            Wrkf_DiaIFSemana wrkfdiaifsemana;
            ConvertExtension objconvertextension = new ConvertExtension();
            List<Wrkf_ListaPagosPorRubroId> lstpagosporrubro;

            //Verificar que la sesión de usuario este activa
            if (Session["sUsuario_Id"] == null)
            {
                return RedirectToAction("CerrarSesion", "Wrkf_Login");
            }
            else
            {
                try
                {
                    string rubro_id;
                    string fechadesde;
                    string fechahasta;
                    string tipodocumento;

                    lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());
                    ViewBag.listaropcionesmenu = lstopcionesmenuitem;

                    string format = "yyyy-MM-dd";
                    var now = DateTime.Now.ToString(format);

                    rubro_id = Request.Form["rubro_id"].Trim();
                    fechadesde = objconvertextension.FormatoFecha2(Convert.ToDateTime(Request.Form["txtfechadesde"])).Trim();
                    fechahasta = objconvertextension.FormatoFecha2(Convert.ToDateTime(Request.Form["txtfechahasta"])).Trim();
                    tipodocumento = Request.Form["tipodocumento"].Trim();

                    //verificar los rangos de fecha
                    if ((fechadesde == null || fechadesde == "") && (fechahasta == null || fechahasta == ""))
                    {
                        wrkfdiaifsemana = objconvertextension.ObtenerPrimerDiaSemana(Convert.ToDateTime(now));
                        fechadesde = wrkfdiaifsemana.Primerdiasemana;
                        fechahasta = wrkfdiaifsemana.Ultimodiasemana;
                    }

                    ViewBag.rubro_id = rubro_id;
                    ViewBag.fechadesde = fechadesde;
                    ViewBag.fechahasta = fechahasta;
                    ViewBag.tipodocu = tipodocumento;

                    //Obtener los pagos asociados al rubro seleccionado
                    Wrkf_DbSolicitudSubContralor objsolicitudsubcontralor = new Wrkf_DbSolicitudSubContralor();

                    if (tipodocumento != "NC")
                    {
                        lstpagosporrubro = objsolicitudsubcontralor.ListaPagosPorRubroIdSubContraloriaDb(rubro_id, Convert.ToDateTime(fechadesde), Convert.ToDateTime(fechahasta));
                    }
                    else
                    {
                        lstpagosporrubro = objsolicitudsubcontralor.ListaNotaCreditosPorRubroIdSubContraloria(rubro_id, Convert.ToDateTime(fechadesde), Convert.ToDateTime(fechahasta));
                    }

                    ViewBag.listarpagossubcontraloria = lstpagosporrubro;
                }
                catch (Exception ex)
                {
                    ViewBag.CodigoError = ex.HResult.ToString();
                    ViewBag.MensajeError = ex.Message.ToString();
                    ViewBag.TipoError = "error";
                    ViewBag.TituloError = "Listado Solicitudes Pagos Por Rubro Id Sub Contraloria";
                }
            }

            return View("ListaPagosPorRubroIdSubContraloria");
        }

        /// <summary>
        /// Aprobar y rechazar las solictudes de pago
        /// </summary>
        /// <returns></returns>
        public JsonResult AprobarRechazarSolicitudSubContraloria(string listapagosaprobados, string listapagosrechazados)
        {
            Wrkf_DbSolicitudSubContralor objdbsolicitudsubcontralor = new Wrkf_DbSolicitudSubContralor();
            Wrkf_RespuestaOperacion objrespuestaoperacion = new Wrkf_RespuestaOperacion();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

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
                    //aprobar o rechazar las solicitudes de pago
                    objrespuestaoperacion = objdbsolicitudsubcontralor.AprobarRechazarSolicitudSubContraloria(listapagosaprobados, listapagosrechazados, Session["sUsuario_Id"].ToString());
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    objrespuestaoperacion.Codigox = mensajeerror.Codigox;
                    objrespuestaoperacion.Mensajex = mensajeerror.Mensajex;
                    objrespuestaoperacion.Tipox = mensajeerror.Tipox;
                    objrespuestaoperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudSubContralorController/AprobarRechazarSolicitudSubContraloria");
                }
            }

            return Json(objrespuestaoperacion, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Actualiza las observaciones del de rechazo de la solicitud por sub contraloria
        /// </summary>
        /// <param name="Solicitudordenpagodetalle_Id"></param>
        /// <param name="Solicitudordenpago_Id"></param>
        /// <param name="observaciones"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public JsonResult ObservacionesRechazoSubcontaloria(int Solicitudordenpagodetalle_Id, int Solicitudordenpago_Id, string observaciones)
        {
            Wrkf_DbSolicitudSubContralor objdbsolicitudsubcontralor = new Wrkf_DbSolicitudSubContralor();
            Wrkf_RespuestaOperacion objrespuestaoperacion = new Wrkf_RespuestaOperacion();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

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
                    objrespuestaoperacion = objdbsolicitudsubcontralor.ObservacionesRechazoSubcontaloria(Solicitudordenpagodetalle_Id, Solicitudordenpago_Id, observaciones, Session["sUsuario_Id"].ToString());
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    objrespuestaoperacion.Codigox = mensajeerror.Codigox;
                    objrespuestaoperacion.Mensajex = mensajeerror.Mensajex;
                    objrespuestaoperacion.Tipox = mensajeerror.Tipox;
                    objrespuestaoperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudSubContralorController/ObservacionesRechazoSubcontaloria");
                }
            }

            return Json(objrespuestaoperacion, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Asignar el pago como urgente para su aprobación
        /// </summary>
        /// <param name="Solicitudordenpago_Id"></param>
        /// <param name="Solicitudordenpagodetalle_Id"></param>
        /// <param name="pagourgente"></param>
        public JsonResult AsignarPagoUrgente(int Solicitudordenpago_Id, int Solicitudordenpagodetalle_Id, bool pagourgente)
        {
            Wrkf_DbSolicitudSubContralor objdbsolicitudsubcontralor = new Wrkf_DbSolicitudSubContralor();
            Wrkf_RespuestaOperacion objrespuestaoperacion = new Wrkf_RespuestaOperacion();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

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
                    objrespuestaoperacion = objdbsolicitudsubcontralor.AsignarPagoUrgente(Solicitudordenpago_Id, Solicitudordenpagodetalle_Id, Session["sUsuario_Id"].ToString(), pagourgente);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    objrespuestaoperacion.Codigox = mensajeerror.Codigox;
                    objrespuestaoperacion.Mensajex = mensajeerror.Mensajex;
                    objrespuestaoperacion.Tipox = mensajeerror.Tipox;
                    objrespuestaoperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudSubContralorController/AsignarPagoUrgente");
                }
            }

            return Json(objrespuestaoperacion, JsonRequestBehavior.AllowGet);
        }
    }
}