using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Ado.DbContent;
using Intranet.Models;

namespace Intranet.Controllers
{
    public class Wrkf_RubroController : Controller
    {
        // GET: Wrkf_Rubro
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Método para obtener el listado de rubros por departamento o grupo
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetRubros_All(int pGrupoRubro_Id)
        {
            List<Wrkf_Rubro> lstRubro = new List<Wrkf_Rubro>();
            Wrkf_Rubro objRubro = new Wrkf_Rubro();
            Wrkf_DatRubro objDatRubro = new Wrkf_DatRubro();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            //Verificar que la sesión de usuario este activa
            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");

                objRubro.Codigox = mensajeerror.Codigox;
                objRubro.Mensajex = mensajeerror.Mensajex;
                objRubro.Tipox = mensajeerror.Tipox;
                objRubro.Titulox = mensajeerror.Titulox;

                lstRubro.Add(objRubro);
            }
            else
            {
                try
                {
                    lstRubro = objDatRubro.GetRubro_All(pGrupoRubro_Id);
                }
                catch(Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                    objRubro.Codigox = mensajeerror.Codigox;
                    objRubro.Mensajex = mensajeerror.Mensajex;
                    objRubro.Tipox = mensajeerror.Tipox;
                    objRubro.Titulox = mensajeerror.Titulox;

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_RubroController/GetRubros_All");

                    lstRubro.Add(objRubro);
                }
            }

            return Json(new { ListadoRubro = lstRubro });
        }

        /// <summary>
        /// Selecciona los datos del Rubro
        /// </summary>
        /// <param name="pGrupoRubro_Id"></param>
        /// <param name="pRubro_Id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetRubros_Key(int pGrupoRubro_Id, string pRubro_Id)
        {
            Wrkf_Rubro objRubro = new Wrkf_Rubro();
            Wrkf_DatRubro objDatRubro = new Wrkf_DatRubro();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            //Verificar que la sesión de usuario este activa
            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");

                objRubro.Codigox = mensajeerror.Codigox;
                objRubro.Mensajex = mensajeerror.Mensajex;
                objRubro.Tipox = mensajeerror.Tipox;
                objRubro.Titulox = mensajeerror.Titulox;
            }
            else
            {
                try
                {
                    objRubro = objDatRubro.GetRubro_Key(pGrupoRubro_Id, pRubro_Id);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                    objRubro.Codigox = mensajeerror.Codigox;
                    objRubro.Mensajex = mensajeerror.Mensajex;
                    objRubro.Tipox = mensajeerror.Tipox;
                    objRubro.Titulox = mensajeerror.Titulox;

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_RubroController/GetRubros_Key");
                }
            }

            return Json(new { DatosRubro = objRubro });
        }
    }
}