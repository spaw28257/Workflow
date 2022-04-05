using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Ado.DbContent;
using Intranet.Models;

namespace Intranet.Controllers
{
    public class Wrkf_MonedaController : Controller
    {
        [HttpPost]
        public ActionResult GetListarMonedas()
        {
            //listado de la moneda
            List<Wrkf_Moneda> lstConfiguracionMoneda;
            Wrkf_DbMoneda objDbConfiguracionMoneda = new Wrkf_DbMoneda();
            lstConfiguracionMoneda = objDbConfiguracionMoneda.GetMonedasId();
            return Json(lstConfiguracionMoneda, JsonRequestBehavior.AllowGet);
        }
    }
}