using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Ado.DbContent;
using Intranet.Models;
using Intranet.Utilities;

namespace Intranet.Controllers
{
    /// <summary>
    /// Controlador para generar los reportes
    /// </summary>
    public class Wrkf_ReporteController : Controller
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Wrkf_ReporteController()
        {
        }

        /// <summary>
        /// Página principal de reportes
        /// </summary>
        /// <returns></returns>
        public ActionResult TipoReportes()
        {
            List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
            Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError objmensajeerror;

            if (Session["sUsuario_Id"] == null)
            {
                return RedirectToAction("CerrarSesion", "Wrkf_Login");
            }
            else
            {
                try
                {
                    lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());

                    //verificar si la lista tiene las opciones del menu del controlar para mostrar en la vista
                    if (lstopcionesmenuitem.Count > 0)
                    {
                        ViewBag.listaropcionesmenu = lstopcionesmenuitem;
                    }
                    else
                    {
                        objmensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("MNU001", "MENUOPCION");
                        ViewBag.CodigoMnu = objmensajeerror.Codigox;
                        ViewBag.MensajeMnu = objmensajeerror.Mensajex;
                        ViewBag.TipoMnu = objmensajeerror.Tipox;
                        ViewBag.TituloMnu = objmensajeerror.Titulox;
                    }
                }
                catch (Exception ex)
                {
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_ReporteController/TipoReportes");
                }
            }

            return View();
        }

        /// <summary>
        /// Genera un reporte que muestra los pagos pendientes por aprobación por área
        /// </summary>
        /// <returns></returns>
        public ActionResult GenerarReportePagosPendientes()
        {
            List<Wrkf_ListaPagosPorRubroId> lstpagospendientesporarea = new List<Wrkf_ListaPagosPorRubroId>();
            Wrkf_DbReporte wrkf_dbreporte = new Wrkf_DbReporte();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            if (Session["sUsuario_Id"] == null)
            {
                return RedirectToAction("CerrarSesion", "Wrkf_Login");
            }
            else
            {
                try
                {
                    lstpagospendientesporarea = wrkf_dbreporte.GenerarReportePagosPendientes();

                    ViewBag.listapagospendientesporarea = lstpagospendientesporarea;
                }
                catch(Exception ex)
                {
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_ReporteController/GenerarReportePagosPendientes");
                }
            }

            return View();
        }

        /// <summary>
        /// Genera un reporte con las solicitudes anuladas por el usuario
        /// </summary>
        /// <returns></returns>
        public JsonResult GenerarReporteSolicitudesAnuladas()
        {
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            Wrkf_DbReporte wrkf_dbreporte = new Wrkf_DbReporte();
            Wrkf_ListaPagosPorRubroId wrkf_listapagosporrubroid = new Wrkf_ListaPagosPorRubroId();
            List<Wrkf_ListaPagosPorRubroId> lstsolicitudespagosanuladas = new List<Wrkf_ListaPagosPorRubroId>();

            //Verificar que la sesión de usuario este activa
            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                wrkf_listapagosporrubroid.Codigox = mensajeerror.Codigox;
                wrkf_listapagosporrubroid.Mensajex = mensajeerror.Mensajex;
                wrkf_listapagosporrubroid.Tipox = mensajeerror.Tipox;
                wrkf_listapagosporrubroid.Titulox = mensajeerror.Titulox;

                lstsolicitudespagosanuladas.Add(wrkf_listapagosporrubroid);
            }
            else
            {
                try
                {
                    lstsolicitudespagosanuladas = wrkf_dbreporte.GenerarReporteSolicitudesAnuladas();
                }
                catch (Exception ex)
                {

                }
            }

            return Json(lstsolicitudespagosanuladas, JsonRequestBehavior.AllowGet);
        }
    }
}