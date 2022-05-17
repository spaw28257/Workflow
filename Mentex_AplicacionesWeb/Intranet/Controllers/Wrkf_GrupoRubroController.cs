using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Ado.DbContent;
using Intranet.Models;

namespace Intranet.Controllers
{
    public class Wrkf_GrupoRubroController : Controller
    {
        // GET: WrkfDepartamento
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Método para obtener el listado de departamentos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetGrupoRubros_All()
        {
            List<Wrkf_GrupoRubro> lstGruporubro = new List<Wrkf_GrupoRubro>();
            Wrkf_GrupoRubro objGrupoRubro = new Wrkf_GrupoRubro();
            Wrkf_DatGrupoRubro objDatGrupoRubro = new Wrkf_DatGrupoRubro();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            //Verificar que la sesión de usuario este activa
            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");

                objGrupoRubro.Codigox = mensajeerror.Codigox;
                objGrupoRubro.Mensajex = mensajeerror.Mensajex;
                objGrupoRubro.Tipox = mensajeerror.Tipox;
                objGrupoRubro.Titulox = mensajeerror.Titulox;

                lstGruporubro.Add(objGrupoRubro);
            }
            else
            {
                try
                {
                    lstGruporubro = objDatGrupoRubro.GetGrupoRubros();
                }
                catch(Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                    objGrupoRubro.Codigox = mensajeerror.Codigox;
                    objGrupoRubro.Mensajex = mensajeerror.Mensajex;
                    objGrupoRubro.Tipox = mensajeerror.Tipox;
                    objGrupoRubro.Titulox = mensajeerror.Titulox;

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_GrupoRubroController/GetGrupoRubros_All");

                    lstGruporubro.Add(objGrupoRubro);
                }
            }

            return Json(new { ListadoGrupoRubro = lstGruporubro });
        }

        [HttpPost]
        public JsonResult GetGrupoRubroPorId(int pGrupoRubro_Id)
        {
            Wrkf_GrupoRubro objGrupoRubro = new Wrkf_GrupoRubro();
            Wrkf_DatGrupoRubro objDatGrupoRubro = new Wrkf_DatGrupoRubro();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            //Verificar que la sesión de usuario este activa
            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");

                objGrupoRubro.Codigox = mensajeerror.Codigox;
                objGrupoRubro.Mensajex = mensajeerror.Mensajex;
                objGrupoRubro.Tipox = mensajeerror.Tipox;
                objGrupoRubro.Titulox = mensajeerror.Titulox;
            }
            else
            {
                try
                {
                    objGrupoRubro = objDatGrupoRubro.GetGrupoRubroPorId(pGrupoRubro_Id);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                    objGrupoRubro.Codigox = mensajeerror.Codigox;
                    objGrupoRubro.Mensajex = mensajeerror.Mensajex;
                    objGrupoRubro.Tipox = mensajeerror.Tipox;
                    objGrupoRubro.Titulox = mensajeerror.Titulox;

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_GrupoRubroController/GetGrupoRubroPorId");
                }
            }

            return Json(new { GrupoRubro = objGrupoRubro });
        }
    }
}