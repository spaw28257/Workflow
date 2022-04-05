using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Utilities;
using Intranet.Models;
using Intranet.Ado.DbContent;

namespace Intranet.Controllers
{
    public class ConvertExtensionController : Controller
    {
        /// <summary>
        /// Devuelve la fecha actual del sistema yyyy-mm-dd
        /// </summary>
        /// <returns></returns>
        public JsonResult FormatoFechayyymmdd()
        {
            string fechahora;
            ConvertExtension convertextension = new ConvertExtension();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            try
            {
                fechahora = convertextension.FormatoFecha2(DateTime.Now);
            }
            catch (Exception ex)
            {
                fechahora = "";
                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Session["sUsuario_Id"].ToString(), "ConvertExtensionController/FormatoFechayyymmdd");
            }

            return Json(new { fechahoraactual = fechahora });
        }
    }
}