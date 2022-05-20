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
                    List<Wrkf_GrupoRubro> lstgruporubro = objsolicitudsubcontralor.lstGrupoRubroSubContralor();
                    ViewBag.listagruporubrospagos = lstgruporubro;
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
        public ActionResult ListarRubrosPagosSubContraloria(string pgruporubro_id)
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
                    int vGrupoRubro_Id = Convert.ToInt32(EncriptadorMD5.Decrypt(pgruporubro_id));
                    Wrkf_DbSolicitudSubContralor objsolicitudsubcontralor = new Wrkf_DbSolicitudSubContralor();
                    List<Wrkf_Rubro> lstrubro = objsolicitudsubcontralor.lstRubrosSubContraloria(vGrupoRubro_Id);
                    ViewBag.gruporubroencript = pgruporubro_id;
                    ViewBag.listarubrospagos = lstrubro;
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
        public ActionResult ListaPagosPorRubroIdSubContraloria(string pgruporubro_id, string prubro_id)
        {
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();
            List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
            Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
            Wrkf_DiaIFSemana wrkfdiaifsemana;
            ConvertExtension objconvertextension = new ConvertExtension();

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


                    wrkfdiaifsemana = objconvertextension.ObtenerPrimerDiaSemana(Convert.ToDateTime(now));
                   string vFechadesde = wrkfdiaifsemana.Primerdiasemana;
                   string vFechahasta = wrkfdiaifsemana.Ultimodiasemana;

                    ViewBag.gruporubro_id = pgruporubro_id.Trim();
                    ViewBag.rubro_id = prubro_id.Trim();
                    ViewBag.fechadesde = vFechadesde.Trim();
                    ViewBag.fechahasta = vFechahasta.Trim();
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
        /// Listar la solicitudes de pago por aprobar subcontralor
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerPagosPorAprobarSubcontraloria(string pgruporubro_id, string prubro_id, string pfechapagodesde, string pfechapagohasta)
        {
            Wrkf_DbSolicitudSubContralor objdbsolicitudsubcontralor = new Wrkf_DbSolicitudSubContralor();
            Wrkf_SolicitudOrdenPago wrkf_SolicitudOrdenPago = new Wrkf_SolicitudOrdenPago();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            List<Wrkf_SolicitudOrdenPago> listwrkf_SolicitudOrdenPagos = new List<Wrkf_SolicitudOrdenPago>();
            ConvertExtension convertExtension = new ConvertExtension();

            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                wrkf_SolicitudOrdenPago.Codigox = mensajeerror.Codigox;
                wrkf_SolicitudOrdenPago.Mensajex = mensajeerror.Mensajex;
                wrkf_SolicitudOrdenPago.Tipox = mensajeerror.Tipox;
                wrkf_SolicitudOrdenPago.Titulox = mensajeerror.Titulox;
                listwrkf_SolicitudOrdenPagos.Add(wrkf_SolicitudOrdenPago);
            }
            else
            {
                try
                {
                    Int32 vGrupoRubro_Id = Convert.ToInt32(EncriptadorMD5.Decrypt(pgruporubro_id));
                    string vRubro_Id = EncriptadorMD5.Decrypt(prubro_id).ToString();
                    string vFechaPagoDesde = convertExtension.FormatoFechayyyyMMdd(Convert.ToDateTime(pfechapagodesde)).ToString();
                    string vFechaPagoHasta = convertExtension.FormatoFechayyyyMMdd(Convert.ToDateTime(pfechapagohasta)).ToString();

                    //aprobar o rechazar las solicitudes de pago
                    listwrkf_SolicitudOrdenPagos = objdbsolicitudsubcontralor.ListaPagosPorRubroIdSubContraloriaDb(vGrupoRubro_Id, vRubro_Id, vFechaPagoDesde, vFechaPagoHasta);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    wrkf_SolicitudOrdenPago.Codigox = mensajeerror.Codigox;
                    wrkf_SolicitudOrdenPago.Mensajex = mensajeerror.Mensajex;
                    wrkf_SolicitudOrdenPago.Tipox = mensajeerror.Tipox;
                    wrkf_SolicitudOrdenPago.Titulox = mensajeerror.Titulox;

                    listwrkf_SolicitudOrdenPagos.Add(wrkf_SolicitudOrdenPago);

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudSubContralorController/AprobarRechazarSolicitudSubContraloria");
                }
            }

            return Json(listwrkf_SolicitudOrdenPagos, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Aprobar y rechazar las solictudes de pago
        /// </summary>
        /// <returns></returns>
        public JsonResult AprobarRechazarSolicitudSubContraloria(string pcodigo, string pobservaciones, string paccion)
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
                    objrespuestaoperacion = objdbsolicitudsubcontralor.AprobarRechazarSolicitudSubContraloria(pcodigo, Session["sUsuario_Id"].ToString(), pobservaciones, paccion);
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
    }
}