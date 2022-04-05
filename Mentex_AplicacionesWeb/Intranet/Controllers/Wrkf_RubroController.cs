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
        public JsonResult GetRubros(int Departamento_Id)
        {
            List<Wrkf_Rubro> lstRubro;
            Wrkf_DbRubro objRubro = new Wrkf_DbRubro();
            lstRubro = objRubro.GetRubro(Departamento_Id);
            return Json(lstRubro, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Método para obtener el listado de rubros por departamento o grupo
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetRubros2(int Departamento_Id)
        {
            List<Wrkf_Rubro> lstRubro;
            Wrkf_DbRubro objRubro = new Wrkf_DbRubro();
            lstRubro = objRubro.GetRubro(Departamento_Id);
            return Json(new { data = lstRubro });
        }

        /// <summary>
        /// Método para obtener el rubro por los campos Rubro_Id y Departamento_Id
        /// </summary>
        /// <param name="Rubro_Id"></param>
        /// <param name="Departamento_Id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetRubroPorId(string Rubro_Id, int Departamento_Id)
        {
            List<Wrkf_Rubro> lstRubro;
            Wrkf_DbRubro objRubro = new Wrkf_DbRubro();
            lstRubro = objRubro.GetRubroPorId(Rubro_Id, Departamento_Id);
            return Json(lstRubro, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Método para obtener el rubro por los campos descripcion y departamento_id
        /// </summary>
        /// <param name="Rubro_Id"></param>
        /// <param name="Departamento_Id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetRubroPorNombreDepartamento(string Descripcion, int Departamento_Id)
        {
            List<Wrkf_Rubro> lstRubro;
            Wrkf_DbRubro objRubro = new Wrkf_DbRubro();
            lstRubro = objRubro.GetRubroPorNombre(Descripcion, Departamento_Id);
            return Json(lstRubro, JsonRequestBehavior.AllowGet);
        }
    }
}