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
        public JsonResult GetChequera_Key(string pchequera_id)
        {
            Wrkf_Chequera objchequera = new Wrkf_Chequera();
            Wrkf_DbChequera wrkf_dbchequera = new Wrkf_DbChequera();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError mensajeerror;

            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                objchequera.Codigox = mensajeerror.Codigox;
                objchequera.Mensajex = mensajeerror.Mensajex;
                objchequera.Tipox = mensajeerror.Tipox;
                objchequera.Titulox = mensajeerror.Titulox;
            }
            else
            {
                try
                {
                    objchequera = wrkf_dbchequera.GetChequera_Key(pchequera_id);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                    objchequera.Codigox = mensajeerror.Codigox;
                    objchequera.Mensajex = mensajeerror.Mensajex;
                    objchequera.Titulox = mensajeerror.Titulox;
                    objchequera.Tipox = mensajeerror.Tipox;

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Session["sUsuario_Id"].ToString(), " Wrkf_ChequeraController/GetChequera_Key");
                }
            }

            return Json(new { chequera = objchequera });
        }
    }
}