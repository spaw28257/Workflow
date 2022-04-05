using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Ado.DbContent;
using Intranet.Models;

namespace Intranet.Controllers
{
    public class Wrkf_ChequeraController : Controller
    {
        //Listar las chequeras
        public JsonResult ListarChequera()
        {
            List<Wrkf_Chequera> lstchequeras = new List<Wrkf_Chequera>();
            Wrkf_DbChequera wrkf_dbchequera = new Wrkf_DbChequera();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError mensajeerror;

            try
            {
                lstchequeras = wrkf_dbchequera.GetListadoChequera();
            }
            catch (Exception ex)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                Wrkf_Chequera wrkf_chequera = new Wrkf_Chequera()
                {
                    Codigox = mensajeerror.Codigox,
                    Mensajex = mensajeerror.Mensajex,
                    Titulox = mensajeerror.Titulox,
                    Tipox = mensajeerror.Tipox
                };

                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Session["sUsuario_Id"].ToString(), "Wrkf_ChequeraController/ListarChequera");

                lstchequeras.Add(wrkf_chequera);
            }

            return Json(new { chequera = lstchequeras });
        }
    }
}