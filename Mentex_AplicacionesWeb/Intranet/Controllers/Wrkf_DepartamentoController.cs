using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Ado.DbContent;
using Intranet.Models;

namespace Intranet.Controllers
{
    public class Wrkf_DepartamentoController : Controller
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
        public JsonResult GetDepartamentos()
        {
            List<Wrkf_Departamento> lstDepartamento = new List<Wrkf_Departamento>();
            Wrkf_DbDepartamento objDepartamento = new Wrkf_DbDepartamento();
            lstDepartamento = objDepartamento.GetDepartamentos();
            return Json(lstDepartamento, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDepartamentos2()
        {
            List<Wrkf_Departamento> lstDepartamento = new List<Wrkf_Departamento>();
            Wrkf_DbDepartamento objDepartamento = new Wrkf_DbDepartamento();
            lstDepartamento = objDepartamento.GetDepartamentos();
            return Json(new { data = lstDepartamento });
        }

        /// <summary>
        /// Método para obtener los datos del grupo del rubro por Id
        /// </summary>
        /// <param name="Departamento_Id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetDepartamentosPorId(int Departamento_Id)
        {
            List<Wrkf_Departamento> lstDepartamento = new List<Wrkf_Departamento>();
            Wrkf_DbDepartamento objDepartamento = new Wrkf_DbDepartamento();
            lstDepartamento = objDepartamento.GetDepartamentosPorId(Departamento_Id);
            return Json(lstDepartamento, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Método para obtener el departamento por nombre
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDepartamentosPorNombre(string departamento)
        {
            List<Wrkf_Departamento> lstDepartamento = new List<Wrkf_Departamento>();
            Wrkf_DbDepartamento objDepartamento = new Wrkf_DbDepartamento();
            lstDepartamento = objDepartamento.GetDepartamentosPorNombre(departamento);
            return Json(lstDepartamento, JsonRequestBehavior.AllowGet);
        }
    }
}