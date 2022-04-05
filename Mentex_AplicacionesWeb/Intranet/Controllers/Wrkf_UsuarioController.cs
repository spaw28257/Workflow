using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Ado.DbContent;
using Intranet.Models;
using Intranet.Utilities;
using System.Data;

namespace Intranet.Controllers
{
    public class Wrkf_UsuarioController : Controller
    {
        /// <summary>
        /// Muestra una vista con el listado de los usuarios registrados
        /// </summary>
        /// <returns></returns>
        // GET: Wrkf_Usuario
        public ActionResult ListadoUsuario()
        {
            //Verificar que la sesión de usuario no esta activa cierra la sesion del usuario
            if (Session["sUsuario_Id"] == null)
            {
                return RedirectToAction("CerrarSesion", "Wrkf_Login");
            }
            else
            {
                //Obtener una lista con las opciones de menu
                List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
                Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
                Wrkf_DbMensajeError objDbMensajeError = new Wrkf_DbMensajeError();

                try
                {
                    lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());
                    ViewBag.listaropcionesmenu = lstopcionesmenuitem;
                }
                catch (Exception ex)
                {
                    //registrar el log de errores
                    objDbMensajeError.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Session["sUsuario_Id"].ToString(), "Wrkf_UsuarioController/ListadoUsuario");
                }
            }

            return View();
        }
    }
}