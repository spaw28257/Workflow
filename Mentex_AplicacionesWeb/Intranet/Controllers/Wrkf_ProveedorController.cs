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
            List<Wrkf_Proveedores> lstProveedor = new List<Wrkf_Proveedores>();
            Wrkf_Proveedores objProveedorModel = new Wrkf_Proveedores();
            Wrkf_DbProveedor objProveedorDat = new Wrkf_DbProveedor();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            //Verificar que la sesión de usuario este activa
            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");

                objProveedorModel.Codigox = mensajeerror.Codigox;
                objProveedorModel.Mensajex = mensajeerror.Mensajex;
                objProveedorModel.Tipox = mensajeerror.Tipox;
                objProveedorModel.Titulox = mensajeerror.Titulox;

                lstProveedor.Add(objProveedorModel);
            }
            else
            {
                try
                {
                    lstProveedor = objProveedorDat.GetListadoProveedor();
                }
                catch(Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                    objProveedorModel.Codigox = mensajeerror.Codigox;
                    objProveedorModel.Mensajex = mensajeerror.Mensajex;
                    objProveedorModel.Tipox = mensajeerror.Tipox;
                    objProveedorModel.Titulox = mensajeerror.Titulox;

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_ProveedorController/ListadoProveedores_todos");

                    lstProveedor.Add(objProveedorModel);
                }
            }

            return Json(new { ListadoProveedores = lstProveedor });
        }

        /// <summary>
        /// Obtiene los datos del proveedor por id
        /// </summary>
        /// <param name="vendorid"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetListadoProveedorPorId(string pVendorid)
        {
            List<Wrkf_Proveedores> lstProveedor = new List<Wrkf_Proveedores>();
            Wrkf_Proveedores objProveedorModel = new Wrkf_Proveedores();
            Wrkf_DbProveedor objProveedorDat = new Wrkf_DbProveedor();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            //Verificar que la sesión de usuario este activa
            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");

                objProveedorModel.Codigox = mensajeerror.Codigox;
                objProveedorModel.Mensajex = mensajeerror.Mensajex;
                objProveedorModel.Tipox = mensajeerror.Tipox;
                objProveedorModel.Titulox = mensajeerror.Titulox;

                lstProveedor.Add(objProveedorModel);
            }
            else
            {
                try
                {
                    lstProveedor = objProveedorDat.GetListadoProveedorPorId(pVendorid);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                    objProveedorModel.Codigox = mensajeerror.Codigox;
                    objProveedorModel.Mensajex = mensajeerror.Mensajex;
                    objProveedorModel.Tipox = mensajeerror.Tipox;
                    objProveedorModel.Titulox = mensajeerror.Titulox;

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_ProveedorController/GetListadoProveedorPorId");

                    lstProveedor.Add(objProveedorModel);
                }
            }

            return Json(new { ListadoProveedores = lstProveedor });
        }
    }
}