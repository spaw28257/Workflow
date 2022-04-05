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
    public class Wrkf_MtxTiendaController : Controller
    {
        //Lista las tiendas registradas
        public JsonResult Listartiendas()
        {
            List<IntegradorVentas_MtxTienda> lstintegradorventas_mtxtienda = new List<IntegradorVentas_MtxTienda>();
            IntegradorVentas_DbMtxTienda integradorventas_dbmtxtienda = new IntegradorVentas_DbMtxTienda();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError mensajeerror;

            try
            {
                lstintegradorventas_mtxtienda = integradorventas_dbmtxtienda.Listartiendas(Session["sUsuario_Id"].ToString());
            }
            catch (Exception ex)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                IntegradorVentas_MtxTienda integradorventas_mtxtienda = new IntegradorVentas_MtxTienda()
                {
                    Codigox = mensajeerror.Codigox,
                    Mensajex = mensajeerror.Mensajex,
                    Tipox = mensajeerror.Tipox,
                    Titulox = mensajeerror.Titulox
                };

                lstintegradorventas_mtxtienda.Add(integradorventas_mtxtienda);

                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Session["sUsuario_Id"].ToString(), "Wrkf_MtxTiendaController/Listartiendas");
            }

            return Json(new { tienda = lstintegradorventas_mtxtienda });
        }

        /// <summary>
        /// Selecciona la tienda por código
        /// </summary>
        /// <param name="tienda"></param>
        /// <returns></returns>
        public JsonResult Seleccionartienda(string tienda)
        {
            IntegradorVentas_MtxTienda integradorventas_mtxtienda = new IntegradorVentas_MtxTienda();
            IntegradorVentas_DbMtxTienda integradorventas_dbmtxtienda = new IntegradorVentas_DbMtxTienda();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError mensajeerror;

            try
            {
                integradorventas_mtxtienda = integradorventas_dbmtxtienda.Seleccionartienda(tienda, Session["sUsuario_Id"].ToString());
            }
            catch (Exception ex)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                integradorventas_mtxtienda.Codigox = mensajeerror.Codigox;
                integradorventas_mtxtienda.Mensajex = mensajeerror.Mensajex;
                integradorventas_mtxtienda.Tipox = mensajeerror.Tipox;
                integradorventas_mtxtienda.Titulox = mensajeerror.Titulox;

                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Session["sUsuario_Id"].ToString(), "Wrkf_MtxTiendaController/Listartiendas");
            }

            return Json(integradorventas_mtxtienda, JsonRequestBehavior.AllowGet);
        }
    }
}