using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_AccesoDatos;
using System.Data;

namespace Capa_LogicaDeNegocios
{
    public class Cls_Roles
    {
        // Atributos que corresponden a las columnas de la tabla TBLROLES
        public int C_IdRolEmpleado { get; set; }
        public string C_StrDescripcion { get; set; }

        // Objeto para acceder a la capa de datos
        Cls_Acceso_Datos AccesoDatos = new Cls_Acceso_Datos();

        // Método para consultar todos los roles
        public DataTable ConsultarRoles()
        {
            string sentencia;
            try
            {
                sentencia = "SELECT * FROM TBLROLES"; // consulta todos los roles
                DataTable dt = new DataTable();
                dt = AccesoDatos.EjecutarConsulta(sentencia);
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Método para consultar un rol específico por Id
        public DataTable ConsultarRol(int IdRolEmpleado)
        {
            string sentencia;
            try
            {
                sentencia = $"SELECT * FROM TBLROLES WHERE IdRolEmpleado = {IdRolEmpleado}";
                DataTable dt = new DataTable();
                dt = AccesoDatos.EjecutarConsulta(sentencia);
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable Filtrar_Roles(string filtro)
        {
            string sentencia;
            try
            {
                sentencia = $"SELECT * FROM TBLROLES WHERE StrDescripcion LIKE '%{filtro}%'";
                DataTable dt = new DataTable();
                dt = AccesoDatos.EjecutarConsulta(sentencia);
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Método para insertar o actualizar un rol
        public string ActualizarRol()
        {
            string mensaje = "";
            try
            {
                List<Cls_parametros> lst = new List<Cls_parametros>();

                lst.Add(new Cls_parametros("@IdRolEmpleado", C_IdRolEmpleado));
                lst.Add(new Cls_parametros("@StrDescripcion", C_StrDescripcion));

                // Se asume que tienes un procedimiento almacenado llamado "Actualizar_Rol"
                mensaje = AccesoDatos.Ejecutar_procedimientos("Actualizar_Rol", lst);
            }
            catch (Exception ex)
            {
                mensaje = "Error al actualizar el rol: " + ex.Message;
            }
            return mensaje;
        }

        // Método para eliminar un rol
        public string EliminarRol()
        {
            string mensaje = "";
            try
            {
                List<Cls_parametros> lst = new List<Cls_parametros>();

                lst.Add(new Cls_parametros("@IdRolEmpleado", C_IdRolEmpleado));

                // Se asume que tienes un procedimiento almacenado llamado "Eliminar_Rol"
                mensaje = AccesoDatos.Ejecutar_procedimientos("Eliminar_Rol", lst);
            }
            catch (Exception ex)
            {
                mensaje = "Error al eliminar el rol: " + ex.Message;
            }
            return mensaje;
        }
    }
}
