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
        public JsonResult ListarDetalleImpuesto(string TaxScheduleID)
        {
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            DbTx00201 objDbTx00201 = new DbTx00201();
            List<Tx00201> lstdetalleimpuesto = new List<Tx00201>();
            Tx00201 objTx00201 = new Tx00201();

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
                    lstdetalleimpuesto = objDbTx00201.ListaPlanImpuesto_filter_TaxSchedule(TaxScheduleID);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("EXC999", "EXCEPCION");
                    objTx00201.Codigox = mensajeerror.Codigox;
                    objTx00201.Mensajex = mensajeerror.Mensajex;
                    objTx00201.Tipox = mensajeerror.Tipox;
                    objTx00201.Titulox = mensajeerror.Titulox;

                    lstdetalleimpuesto.Add(objTx00201);

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Session["sUsuario_Id"].ToString(), "Tx00201Controller/ListarDetalleImpuesto");
                }
            }

            return Json(lstdetalleimpuesto, JsonRequestBehavior.AllowGet);
        }
    }
}