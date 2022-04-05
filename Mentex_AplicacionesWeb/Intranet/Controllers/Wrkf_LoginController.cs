using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Ado.DbContent;
using Intranet.Models;
using EncryptDecrypt;

namespace Intranet.Controllers
{
    public class Wrkf_LoginController : Controller
    {
        // GET: Wrkf_Login
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Validar los datosdel usuario en la aplicación
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ValidarUsuario(string pUSERID, string pClaveAcceso)
        {
            Wrkf_DbUsuario objDbUsuario = new Wrkf_DbUsuario();
            Wrkf_DbMensajeError objDbMensajeError = new Wrkf_DbMensajeError();
            Wrkf_Usuario objUsuario = new Wrkf_Usuario();
            MensajeError objMensajeError;

            try
            {
                //Encriptar la clave de acceso
                pClaveAcceso = EncriptadorMD5.Encrypt(pClaveAcceso);

                //verifica los datos delusuario
                objUsuario = objDbUsuario.DatAutenticarUsuario(pUSERID, pClaveAcceso);

                //Si los datos del usuario son correctos se crea la sesion y se ingresa a la interfaz de la aplicación
                if (string.IsNullOrEmpty(objUsuario.Codigox))
                {
                    //creamos las variables de sesión
                    Session["sUsuario_Id"] = objUsuario.USERID.ToUpper().Trim();
                    Session["sRol_Id"] = objUsuario.Rol_Id.ToUpper().Trim();
                }
                else
                {
                    Session["sUsuario_Id"] = "";
                    Session["sRol_Id"] = "";
                }
            }
            catch (Exception ex)
            {
                objMensajeError = objDbMensajeError.GetObtenerMensajeError("EXC999", "EXCEPCION");
                objUsuario.Codigox = objMensajeError.Codigox;
                objUsuario.Mensajex = objMensajeError.Mensajex;
                objUsuario.Tipox = objMensajeError.Tipox;
                objUsuario.Titulox = objMensajeError.Titulox;

                objDbMensajeError.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), pUSERID, "Wrkf_LoginController/ValidarUsuario");
            }

            return Json(new { Usuario = objUsuario });
        }

        /// <summary>
        /// Método para cerrar la sesión de la aplicación
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            return View();
        }
    }
}