using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_AccesoDatos;

namespace Capa_LogicaDeNegocios
{
    public class Validar_usuario
    {
        //Definimos atriutos de la clase

        public string C_StrUsuario { get; set; }
        public string C_StrClave { get; set; }
        public Int32 C_IdEmpleado { get; set; }

        public void ValidarUsuario() // metodo para validar usuario consultando en la base de datos
        {
            try
            {
                string sentencia = $"SELECT IdEmpleado FROM TBLSEGURIDAD WHERE StrUsuario = '{C_StrUsuario}' AND StrClave = '{C_StrClave}'";
                DataTable dt = new DataTable();
                Cls_Acceso_Datos Acceso = new Cls_Acceso_Datos(); // creacion de un objeto 
                dt = Acceso.EjecutarConsulta(sentencia); // se ejecuta el metodo para ejecutar la consulta - metodo de capa de acceso a datos

                foreach (DataRow row in dt.Rows) // recorrer las filas del datatable
                {
                    C_IdEmpleado = int.Parse(row[0].ToString()); // se asigna el valor del IdEmpleado a la variable de la clase
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar usuario: " + ex);
            }
        }
    }
}