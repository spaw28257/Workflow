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
    /// Contolador para el detalle del plan de impuesto
    /// </summary>
    public class Tx00201Controller : Controller
    {
        /// <summary>
        /// Listar el detalle del plan de impuesto
        /// </summary>
        /// <returns></returns>
        public JsonResult ListarDetalleImpuesto()
        {
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            List<Tx00201> lstdetalleimpuesto = new List<Tx00201>();

            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                Tx00201 detalleimpuesto = new Tx00201()
                {
                    Codigox = mensajeerror.Codigox,
                    Mensajex = mensajeerror.Mensajex,
                    Tipox = mensajeerror.Tipox,
                    Titulox = mensajeerror.Titulox
                };

                lstdetalleimpuesto.Add(detalleimpuesto);
            }
            else
            {
                try
                {

                }
                catch (Exception ex)
                {

                }
            }

            return Json(lstdetalleimpuesto, JsonRequestBehavior.AllowGet);
        }
    }
}