using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_AccesoDatos;
using System.Data;

namespace Capa_LogicaDeNegocios
{
    public class Cls_Seguridad
    {
        // Definir atributos de la clase

        public int C_IdEmpleado { get; set; }
        public string C_StrUsuario { get; set; }
        public string C_StrClave { get; set; }
        public string C_StrUsuaarioModifico { get; set; }

        Cls_Acceso_Datos AccesoDatos = new Cls_Acceso_Datos(); //Crear un objeto de la clase Cls_Acceso_Datos

        // Consultar empleados para mostrar en el combobox
        public DataTable ConsultarEmpleados()
        {
            string sentencia;
            try
            {
                sentencia = "select IdEmpleado, strNombre from TBLEMPLEADO";
                DataTable dt = new DataTable();
                dt = AccesoDatos.EjecutarConsulta(sentencia);
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }
        // Consultar la informacion de usuario y clave de un empleado
        public DataTable Consulta_SeguridadEmpleado(int IdEmpleado)
        {
            string sentencia;
            try
            {
                sentencia = $"select StrUsuario, StrClave from TBLSEGURIDAD where IdEmpleado = + {IdEmpleado}";
                DataTable dt = new DataTable();
                dt = AccesoDatos.EjecutarConsulta(sentencia);
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //Creacion de metedo para eliminar informacion de seguridad de un empleado
        public string EliminarSeguridadEmpleado()
        {
            string mensaje = "";
            try
            {   //Crear la lista de parametros
                List<Cls_parametros> lst = new List<Cls_parametros>();

                //Adicionar parametor que permite indicar el id del empleado que se desea eliminar
                //Contiene el nombre del parametro, el valor y el tipo de dato
                lst.Add(new Cls_parametros("@IdEmpleado", C_IdEmpleado));
                //Ejecuta el procedimiento almacenado para eliminar la informacion de seguridad del empleado
                mensaje = AccesoDatos.Ejecutar_procedimientos("Eliminar_Seguridad", lst);
                return mensaje;
            }
            catch (Exception ex)
            {
                mensaje = "Error al eliminar la informacion de seguridad del empleado " + ex;
            }
            return mensaje;
        }

        public string ActualizarSeguridadEmpleado()
        {
            string mensaje = "";
            try
            {
                //Crear la lista de parametros
                List<Cls_parametros> lst = new List<Cls_parametros>();
                //Adicionar los parametros que se van a enviar al procedimiento almacenado
                //Contiene el nombre del parametro, el valor y el tipo de dato
                lst.Add(new Cls_parametros("@IdEmpleado", C_IdEmpleado));
                lst.Add(new Cls_parametros("@StrUsuario", C_StrUsuario));
                lst.Add(new Cls_parametros("@StrClave", C_StrClave));
                lst.Add(new Cls_parametros("@StrUsuarioModifico", C_StrUsuaarioModifico));
                lst.Add(new Cls_parametros("@DtmFechaModifica", DateTime.Now));
                //Ejecuta el procedimiento almacenado para actualizar la informacion de seguridad del empleado
                mensaje = AccesoDatos.Ejecutar_procedimientos("Actualizar_Seguridad", lst);
                return mensaje;
            }
            catch (Exception ex)
            {
                mensaje = "Error al actualizar la informacion de seguridad del empleado " + ex.Message;
            }
            return mensaje;
        }

    }
}