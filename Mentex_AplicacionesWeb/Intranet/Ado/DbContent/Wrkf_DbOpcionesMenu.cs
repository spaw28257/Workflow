using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Intranet.Utilities;
using Intranet.Models;

namespace Intranet.Ado.DbContent
{
    /// <summary>
    /// Descripcion: La clase se utiliza para acceder a los datos de la tabla Workflow.OpcionesMenu
    /// Creado Por: Pablo aponte
    /// Fecha: 03/09/2021
    /// </summary>
    public class Wrkf_DbOpcionesMenu
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Wrkf_DbOpcionesMenu()
        {
        }

        /// <summary>
        /// Descripción: El método devuelve una lista con las opciones del menu segun el rol especificado
        /// </summary>
        /// <param name="Rol_Id"></param>
        /// <returns></returns>
        public List<Wrkf_OpcionesMenuItem> Fn_ListarOpcionesMenuPorRol(string pUSERID)
        {
            List<Wrkf_OpcionesMenuItem> lstOpcionesMenuPorRol = new List<Wrkf_OpcionesMenuItem>();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError mensajeerror;

            try
            {
                //Ejecutar el procedimiento almacenado
                SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@pUSERID", pUSERID),
                    new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                    new SqlParameter("@pMensajEerror", SqlDbType.VarChar, 200),
                    new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                    new SqlParameter("@pTituloError", SqlDbType.VarChar, 60)
                });

                Sqlprovider.Oparameters[1].Direction = ParameterDirection.Output;
                Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
                Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
                Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;

                //optener los resultados del procedimiento almacenado
                DataTable DtOpcionesMenuPorRol = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("workflow.PL_Sel_OpcionesMenuPorRol",
                                                                                                        CommandType.StoredProcedure, 
                                                                                                        out Dictionary<string, string> outparam);

                if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("00007", "class_Wrkf_DbOpcionesMenu");

                    Wrkf_OpcionesMenuItem ObjOpcionesMenu = new Wrkf_OpcionesMenuItem()
                    {
                        Codigox = mensajeerror.Codigox,
                        Mensajex = mensajeerror.Mensajex,
                        Tipox = mensajeerror.Tipox,
                        Titulox = mensajeerror.Titulox
                    };

                    wrkf_dbmensajeerror.RegistrarLogErrores(Convert.ToInt32(outparam["@pCodigoError"]), outparam["@pMensajEerror"], pUSERID, "Wrkf_DbOpcionesMenu");

                    lstOpcionesMenuPorRol.Add(ObjOpcionesMenu);
                }
                else
                {
                    //verifica el procedimiento genero algun resultado
                    int total_registros = DtOpcionesMenuPorRol.Rows.Count;

                    if (total_registros > 0)
                    {
                        //ingresa los datos en la lista lista
                        for (int i = 0; i < total_registros; i++)
                        {
                            Wrkf_OpcionesMenuItem ObjOpcionesMenu = new Wrkf_OpcionesMenuItem()
                            {
                                Opcionmenu_Idx = Convert.ToInt32(DtOpcionesMenuPorRol.Rows[i]["Opcionmenu_Id"]),
                                Padre_Idx = Convert.ToInt32(DtOpcionesMenuPorRol.Rows[i]["Padre_Id"]),
                                Nivelx = Convert.ToInt32(DtOpcionesMenuPorRol.Rows[i]["Nivel"]),
                                Menux = Convert.ToString(DtOpcionesMenuPorRol.Rows[i]["Menu"]),
                                Accionx = Convert.ToString(DtOpcionesMenuPorRol.Rows[i]["Accion"]),
                                Controladorx = Convert.ToString(DtOpcionesMenuPorRol.Rows[i]["Controlador"]),
                                Titulox = Convert.ToString(DtOpcionesMenuPorRol.Rows[i]["Titulo"]),
                                Activox = Convert.ToBoolean(DtOpcionesMenuPorRol.Rows[i]["Activo"]),
                                Imagenx = Convert.ToString(DtOpcionesMenuPorRol.Rows[i]["Imagen"])
                            };

                            lstOpcionesMenuPorRol.Add(ObjOpcionesMenu);
                        }
                    }
                    else
                    {
                        mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("00007", "class_Wrkf_DbOpcionesMenu");

                        Wrkf_OpcionesMenuItem ObjOpcionesMenu = new Wrkf_OpcionesMenuItem()
                        {
                            Codigox = mensajeerror.Codigox,
                            Mensajex = mensajeerror.Mensajex,
                            Tipox = mensajeerror.Tipox,
                            Titulox = mensajeerror.Titulox
                        };

                        lstOpcionesMenuPorRol.Add(ObjOpcionesMenu);
                    }
                }
            }
            catch (Exception ex)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("00007", "class_Wrkf_DbOpcionesMenu");

                Wrkf_OpcionesMenuItem ObjOpcionesMenu = new Wrkf_OpcionesMenuItem()
                {
                    Codigox = mensajeerror.Codigox,
                    Mensajex = mensajeerror.Mensajex,
                    Tipox = mensajeerror.Tipox,
                    Titulox = mensajeerror.Titulox
                };

                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), pUSERID, "class_Wrkf_DbOpcionesMenu");

                lstOpcionesMenuPorRol.Add(ObjOpcionesMenu);
            }

            return lstOpcionesMenuPorRol;
        }
    }
}