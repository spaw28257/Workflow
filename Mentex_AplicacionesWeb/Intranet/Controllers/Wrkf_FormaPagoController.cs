using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Ado.DbContent;
using Intranet.Models;

namespace Intranet.Controllers
{
    public class Wrkf_FormaPagoController : Controller
    {
        /// <summary>
        /// Controlador para listar las formas de pagos
        /// </summary>
        /// <returns></returns>
        public JsonResult ListarFormaPago()
        {
            List<Wrkf_FormaPago> lstwrkf_formapago = new List<Wrkf_FormaPago>();
            Wrkf_FormaPago wrkf_formapago = new Wrkf_FormaPago();
            Wrkf_DbFormaPago wrkf_dbformapago = new Wrkf_DbFormaPago();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError mensajeerror = new MensajeError();

            try
            {
                lstwrkf_formapago = wrkf_dbformapago.GetFormasPago();
            }
            catch(Exception ex)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                wrkf_formapago.Codigox = mensajeerror.Codigox;
                wrkf_formapago.Mensajex = mensajeerror.Mensajex;
                wrkf_formapago.Titulox = mensajeerror.Titulox;
                wrkf_formapago.Tipox = mensajeerror.Tipox;

                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Session["sUsuario_Id"].ToString(), "Wrkf_FormaPagoController/ListarFormaPago");
            }

            return Json(new { formapago = lstwrkf_formapago });
        }

        /// <summary>
        /// Controlador para obtener la formadepago por id
        /// </summary>
        /// <param name="formapago_id"></param>
        /// <returns></returns>
        public JsonResult ObtenerFormaPago_Id(int formapago_id)
        {
            List<Wrkf_FormaPago> lstwrkf_formapago = new List<Wrkf_FormaPago>();
            Wrkf_FormaPago wrkf_formapago = new Wrkf_FormaPago();
            Wrkf_DbFormaPago wrkf_dbformapago = new Wrkf_DbFormaPago();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError mensajeerror = new MensajeError();

            try
            {
                wrkf_formapago = wrkf_dbformapago.GetFormasPagoId(formapago_id, Session["sUsuario_Id"].ToString());
            }
            catch (Exception ex)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                wrkf_formapago.Codigox = mensajeerror.Codigox;
                wrkf_formapago.Mensajex = mensajeerror.Mensajex;
                wrkf_formapago.Tipox = mensajeerror.Tipox;
                wrkf_formapago.Titulox = mensajeerror.Titulox;

                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Session["sUsuario_Id"].ToString(), "Wrkf_FormaPagoController/ObtenerFormaPago_Id");
            }

            return Json(wrkf_formapago, JsonRequestBehavior.AllowGet);
        }
    }
}