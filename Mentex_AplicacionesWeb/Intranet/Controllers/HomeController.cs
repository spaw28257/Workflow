using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Ado.DbContent;
using Intranet.Models;

namespace Intranet.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Ejecuta la vista principal de la aplicación
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                if (Session["sUsuario_Id"] == null || Session["sUsuario_Id"] == "")
                {
                    return RedirectToAction("CerrarSesion", "Wrkf_Login");
                }
                else
                {
                    //Obtener una lista con las opciones de menu
                    List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem = new List<Wrkf_OpcionesMenuItem>();
                    Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
                    lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString().Trim());
                    ViewBag.listaropcionesmenu = lstopcionesmenuitem;
                }
            }
            catch (Exception ex)
            {
                Wrkf_DbMensajeError wrkfdbmensajeerror = new Wrkf_DbMensajeError();
                wrkfdbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Session["sUsuario_Id"].ToString(), "HomeController");
            }

            return View();
        }
    }
}