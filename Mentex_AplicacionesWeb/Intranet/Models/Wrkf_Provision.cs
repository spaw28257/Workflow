using System;

namespace Intranet.Models
{
    /// <summary>
    /// El modelo permite gestionar los datos en la tabla de la base de datos
    /// </summary>
    public class Wrkf_Provision : MensajeError
    {
        //Atributos
        private int IdProvision;
        private string IdProveedor;
        private string CodigoPlantilla;
        private int IdGrupoRubro;
        private string IdRubro;
        private string Tienda;
        private string CodigoMoneda;
        private string IdChequera;
        private int IdFormaPago;
        private double Monto;
        private string FechaCreacion;
        private string FechaPago;
        private string Observaciones;
        private bool Anulada;
        private string Usuario;
        private string Fecha;

        //Atributos Extendidos
        private string Departamento;
        private string Rubro;
        private string Plantilla;
        private string Proveedor;
        private string Formapago;

        public Wrkf_Provision()
        {
            IdProvision = 0;
            IdProveedor = "";
            CodigoPlantilla = "";
            IdGrupoRubro = 0;
            IdRubro = "";
            Tienda = "";
            CodigoMoneda = "";
            IdChequera = "";
            IdFormaPago = 0;
            Monto = 0.00;
            FechaPago = "1900-01-01";
            Observaciones = "";
            Anulada = false;
            Usuario = "";
            Fecha = "1900-01-01";

            //Atributos Extendidos
            Departamento = "";
            Rubro = "";
            Plantilla = "";
            Proveedor = "";
            Formapago = "";
        }

        //Propiedades
        public int IdProvisionx { get => IdProvision; set => IdProvision = value; }
        public string IdProveedorx { get => IdProveedor; set => IdProveedor = value; }
        public string CodigoPlantillax { get => CodigoPlantilla; set => CodigoPlantilla = value; }
        public int IdGrupoRubrox { get => IdGrupoRubro; set => IdGrupoRubro = value; }
        public string IdRubrox { get => IdRubro; set => IdRubro = value; }
        public string Tiendax { get => Tienda; set => Tienda = value; }
        public string CodigoMonedax { get => CodigoMoneda; set => CodigoMoneda = value; }
        public string IdChequerax { get => IdChequera; set => IdChequera = value; }
        public int IdFormaPagox { get => IdFormaPago; set => IdFormaPago = value; }
        public double Montox { get => Monto; set => Monto = value; }
        public string FechaCreacionx { get => FechaCreacion; set => FechaCreacion = value; }
        public string FechaPagox { get => FechaPago; set => FechaPago = value; }
        public string Observacionesx { get => Observaciones; set => Observaciones = value; }
        public bool Anuladax { get => Anulada; set => Anulada = value; }
        public string Usuariox { get => Usuario; set => Usuario = value; }
        public string Fechax { get => Fecha; set => Fecha = value; }

        //Propiedades Extendidas
        public string Departamentox { get => Departamento; set => Departamento = value; }
        public string Rubrox { get => Rubro; set => Rubro = value; }
        public string Plantillax { get => Plantilla; set => Plantilla = value; }
        public string Proveedorx { get => Proveedor; set => Proveedor = value; }
        public string Formapagox { get => Formapago; set => Formapago = value; }
    }
}