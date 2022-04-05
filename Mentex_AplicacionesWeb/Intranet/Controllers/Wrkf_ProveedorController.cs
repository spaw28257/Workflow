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
    public class Wrkf_ProveedorController : Controller
    {
        /// <summary>
        /// Lista todos los proveedores
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ListadoProveedores_todos()
        {
            List<Wrkf_Proveedores> lstProveedores;
            Wrkf_DbProveedor objProveedor = new Wrkf_DbProveedor();
            lstProveedores = objProveedor.GetListadoProveedor();
            return Json(new { data = lstProveedores });
        }

        /// <summary>
        /// Obtiene los datos del proveedor por id
        /// </summary>
        /// <param name="vendorid"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetListadoProveedorPorId(string vendorid)
        {
            List<Wrkf_Proveedores> lstProveedores;
            Wrkf_DbProveedor objProveedor = new Wrkf_DbProveedor();
            lstProveedores = objProveedor.GetListadoProveedorPorId(vendorid);
            return Json(lstProveedores, JsonRequestBehavior.AllowGet);
        }
    }
}