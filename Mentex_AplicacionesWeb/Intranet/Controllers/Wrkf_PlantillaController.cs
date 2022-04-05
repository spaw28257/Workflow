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
    public class Wrkf_PlantillaController : Controller
    {
        // GET: Wrkf_Plantilla
        public ActionResult Index()
        {
            //Obtener una lista con las opciones de menu
            List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
            Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();

            //Verificar que la sesión de usuario no esta activa cierra la sesion del usuario
            if ((Session["sUsuario_Id"] == null) || (Session["sUsuario_Id"].ToString() == ""))
            {
                return RedirectToAction("CerrarSesion", "Wrkf_Login");
            }
            else
            {
                lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());
                ViewBag.listaropcionesmenu = lstopcionesmenuitem;
            }

            return View();
        }

        /// <summary>
        /// Lista las plantillas de pagos no frecuentes
        /// </summary>
        /// <returns></returns>
        public JsonResult ListarPlantillasPagoNofrecuente()
        {
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            List<GestionPago_MtxPlantilla> lstPlantilla = new List<GestionPago_MtxPlantilla>();
            GestionPago_DbMtxPlantilla objDatPlantilla = new GestionPago_DbMtxPlantilla();
            GestionPago_MtxPlantilla objEntPlantilla = new GestionPago_MtxPlantilla();
            try
            {
                //Verificar que la sesión de usuario este activa
                if ((Session["sUsuario_Id"] == null) || (Session["sUsuario_Id"].ToString() == ""))
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                    objEntPlantilla.Codigox = mensajeerror.Codigox;
                    objEntPlantilla.Mensajex = mensajeerror.Mensajex;
                    objEntPlantilla.Tipox = mensajeerror.Tipox;
                    objEntPlantilla.Titulox = mensajeerror.Titulox;

                    lstPlantilla.Add(objEntPlantilla);
                }
                else
                {
                    //listar plantillas de pago no frecuentes
                    lstPlantilla = objDatPlantilla.ListarPlantillasActivas(Session["sUsuario_Id"].ToString(), true);
                }
            }
            catch (Exception ex)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("EXC999", "EXCEPCION");
                objEntPlantilla.Codigox = mensajeerror.Codigox;
                objEntPlantilla.Mensajex = mensajeerror.Mensajex;
                objEntPlantilla.Tipox = mensajeerror.Tipox;
                objEntPlantilla.Titulox = mensajeerror.Titulox;

                lstPlantilla.Add(objEntPlantilla);

                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Session["sUsuario_Id"].ToString(), "Wrkf_PlantillaController/ListarPlantillasPagoNofrecuente");
            }

            return Json(new { Plantillas = lstPlantilla });
        }
    }
}