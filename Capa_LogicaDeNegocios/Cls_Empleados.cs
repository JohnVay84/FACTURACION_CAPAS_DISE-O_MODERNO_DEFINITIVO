using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_AccesoDatos;
using System.Data;

namespace Capa_LogicaDeNegocios
{
    public class Cls_Empleados
    {
        // definimos los atributos de la clase todos estan definidos en la base de datos
        public int C_IdEmpleado { get; set; }
        public string C_StrNombre { get; set; }
        public double C_NumDocumento { get; set; }
        public string C_StrDireccion { get; set; }
        public string C_StrTelefono { get; set; }
        public string C_StrEmail { get; set; }
        public int C_IdRolEmpleado { get; set; }
        public string C_DtmIngreso { get; set; }
        public string C_DtmRetiro { get; set; }
        public string C_StrDatosAdicionales { get; set; }
        public string C_StrUsuarioModifico { get; set; }

        Cls_Acceso_Datos AccesoDatos = new Cls_Acceso_Datos(); // creacion del objeto de la clase de acceso a datos

        // metodo para consultar los empleados en la base de datos
        public DataTable Consulta_Empleado()
        {
            string sentencia;
            try
            {
                sentencia = "SELECT * FROM TBLEMPLEADO"; // sentencia sql para consultar los empleados
                DataTable dt = new DataTable();
                dt = AccesoDatos.EjecutarConsulta(sentencia); // se ejecuta el metodo para ejecutar la consulta
                return dt; // retorna el datatable con los empleados
            }
            catch (Exception)
            {
                return null; // en caso de error retorna null
            }
        }

        public DataTable Consulta_Empleado(int IdEmpleado)
        {
            string sentencia = "";
            try
            {
                sentencia = $"SELECT * FROM TBLEMPLEADO WHERE IdEmpleado = {IdEmpleado}"; // sentencia sql para consultar los empleados
                DataTable dt = new DataTable();
                dt = AccesoDatos.EjecutarConsulta(sentencia); // se ejecuta el metodo para ejecutar la consulta
                return dt; // retorna el datatable con los empleados
            }
            catch (Exception)
            {
                return null; // en caso de error retorna null
            }
        }

        public DataTable Filtrar_Empleados(string filtro)
        {
            string sentencia;
            try
            {
                sentencia = $"SELECT * FROM TBLEMPLEADO WHERE StrNombre LIKE '%{filtro}%' ";  // sentencia sql para consultar los empleados
                DataTable dt = new DataTable();
                dt = AccesoDatos.EjecutarConsulta(sentencia); // se ejecuta el metodo para ejecutar la consulta
                return dt; // retorna el datatable con los empleados
            }
            catch (Exception)
            {
                return null; // en caso de error retorna null
            }


        }

        public DataTable ConsultarRol()
        {
            string sentencia;
            try
            {
                sentencia = "SELECT * FROM TBLROLES"; // sentencia sql para consultar los roles de empleados
                DataTable dt = new DataTable();
                dt = AccesoDatos.EjecutarConsulta(sentencia); // se ejecuta el metodo para ejecutar la consulta
                return dt; // retorna el datatable con los roles de empleados
            }
            catch (Exception)
            {
                return null; // en caso de error retorna null
            }
        }

        public String EliminaEmpleado()
        {
            string mensaje = "";
            try
            {
                List<Cls_parametros> lst = new List<Cls_parametros>(); // creacion de la lista de parametros

                //Agregar parametro que permite indicar el Id del empleado a eliminar   
                // contiene el nombre del parametro, el valor y el tipo de dato
                lst.Add(new Cls_parametros("@IdEmpleado", C_IdEmpleado));

                //se ejecuta el procedimiento almacenado para eliminar el empleado
                mensaje = AccesoDatos.Ejecutar_procedimientos("Eliminar_Empleado", lst);
            }
            catch (Exception ex)
            {
                mensaje = "Error al eliminar el empleado: " + ex.Message;
            }
            return mensaje; // retorna el mensaje
        }

        // metodo para insertar y actualizar un empleado en la base de datos
        public string ActualizarEmpleado()
        {
            string mensaje = "";
            try
            {
                List<Cls_parametros> lst = new List<Cls_parametros>(); // creacion de la lista de parametros
                //Agregar los parametros que permiten enviar los datos del empleado a insertar o actualizar
                // contiene el nombre del parametro, el valor y el tipo de dato
                lst.Add(new Cls_parametros("@IdEmpleado", C_IdEmpleado));
                lst.Add(new Cls_parametros("@StrNombre", C_StrNombre));
                lst.Add(new Cls_parametros("@NumDocumento", C_NumDocumento));
                lst.Add(new Cls_parametros("@StrDireccion", C_StrDireccion));
                lst.Add(new Cls_parametros("@StrTelefono", C_StrTelefono));
                lst.Add(new Cls_parametros("@StrEmail", C_StrEmail));
                lst.Add(new Cls_parametros("@IdRolEmpleado", C_IdRolEmpleado));
                lst.Add(new Cls_parametros("@DtmIngreso", C_DtmIngreso));
                lst.Add(new Cls_parametros("@DtmRetiro", C_DtmRetiro));
                lst.Add(new Cls_parametros("@StrDatosAdicionales", C_StrDatosAdicionales));
                lst.Add(new Cls_parametros("@DtmFechaModifica", DateTime.Now));
                lst.Add(new Cls_parametros("@StrUsuarioModifico", C_StrUsuarioModifico));
                //se ejecuta el procedimiento almacenado para insertar o actualizar el empleado
                mensaje = AccesoDatos.Ejecutar_procedimientos("Actualizar_Empleado", lst);
            }
            catch (Exception ex)
            {
                mensaje = "Error al actualizar el empleado: " + ex.Message;
            }
            return mensaje; // retorna el mensaje
        }
    }
}