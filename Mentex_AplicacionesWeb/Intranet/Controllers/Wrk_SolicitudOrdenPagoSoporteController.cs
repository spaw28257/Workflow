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
    public class Wrk_SolicitudOrdenPagoSoporteController : Controller
    {
        // GET: Wrk_SolicitudOrdenPagoSoporte
        /// <summary>
        /// Lista los soportes para ser descargados para su revisión
        /// </summary>
        /// <param name="Solicitudordenpago_Id"></param>
        /// <param name="Solicitudordenpagodetalle_Id"></param>
        /// <returns></returns>
        public ActionResult DescargarSoporte(int Solicitudordenpago_Id, int Solicitudordenpagodetalle_Id)
        {
            List<Wrkf_SolicitudOrdenPagoSoporte> lstSoportePago = new List<Wrkf_SolicitudOrdenPagoSoporte>();
            Wrkf_DbSolicitudOrdenPagoSoporte objsolicitudordenpago = new Wrkf_DbSolicitudOrdenPagoSoporte();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError objmensajeerror;
            Wrkf_SolicitudOrdenPagoSoporte wrkf_solicitudordenpagosoporte = new Wrkf_SolicitudOrdenPagoSoporte();

            if (Session["sUsuario_Id"] == null)
            {
                return RedirectToAction("CerrarSesion", "Wrkf_Login");
            }
            else
            {
                try
                {
                    lstSoportePago = objsolicitudordenpago.GetListarSoportes(Solicitudordenpago_Id, Solicitudordenpagodetalle_Id);
                }
                catch (Exception ex)
                {

                    objmensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    wrkf_solicitudordenpagosoporte.Codigox = objmensajeerror.Codigox;
                    wrkf_solicitudordenpagosoporte.Mensajex = objmensajeerror.Mensajex;
                    wrkf_solicitudordenpagosoporte.Tipox = objmensajeerror.Tipox;
                    wrkf_solicitudordenpagosoporte.Titulox = objmensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrk_SolicitudOrdenPagoSoporteController/DescargarSoporte");

                    lstSoportePago.Add(wrkf_solicitudordenpagosoporte);
                }

                ViewBag.listadosoporte = lstSoportePago;
            }

            return View();
        }

        /// <summary>
        /// Descarga el archivo de soporte
        /// </summary>
        /// <param name="ruta"></param>
        /// <param name="archivo"></param>
        /// <returns></returns>
        public ActionResult RevisarSoporte(string ruta, string archivo)
        {
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            string rutadescarga = ruta + archivo;

            if (Session["sUsuario_Id"] == null)
            {
                return RedirectToAction("CerrarSesion", "Wrkf_Login");
            }
            else
            {
                try
                {
                    return File(rutadescarga, archivo);
                }
                catch (Exception ex)
                { 
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrk_SolicitudOrdenPagoSoporteController/RevisarSoporte");
                }
            }

            return File(rutadescarga, archivo);
        }
    }
}