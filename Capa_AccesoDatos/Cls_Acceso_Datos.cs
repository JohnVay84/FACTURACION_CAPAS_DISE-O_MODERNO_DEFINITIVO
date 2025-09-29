using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;

namespace Capa_AccesoDatos
{
    public class Cls_parametros
    {
        //Definir los parametros a los procedimintos almacenados
        public string Nombre { get; set; } // Nombre del parametro
        public object Valor { get; set; } // Valor del parametro
        public SqlDbType TipoDato { get; set; } // Tipo de dato del parametro
        public Int32 Tamaño { get; set; } // Tamaño del parametro
        public ParameterDirection Direccionparametro { get; set; } // Direccion del parametro, entrada, salida o ambos

        //Constructor parametros entrada
        public Cls_parametros(string Objnombre, object Objvalor)
        {
            Nombre = Objnombre;
            Valor = Objvalor;
            Direccionparametro = ParameterDirection.Input; // Por defecto es de entrada
        }
        //Constructor parametros salida
        public Cls_parametros(string Objnombre, SqlDbType ObjtipoDato, Int32 ObjTamaño)
        {
            Nombre = Objnombre;
            TipoDato = ObjtipoDato;
            Tamaño = ObjTamaño;
            Direccionparametro = ParameterDirection.Output; // Por defecto es de salida
        }
    }
    public class Cls_Acceso_Datos
    {
        SqlConnection conexion;    //Se declara la variable conexion de tipo SqlConnection
        SqlCommand cmd;            //Se declara la variable cmd de tipo SqlCommand
        SqlDataReader LectorDatos = null; //Utiliza para leer dato suele estar acompañada de un objeto command que contiene la consulta 
        SqlDataAdapter da;         //Se utiliza para llenar un DataSet y actualizar una fuente de datos SQL Server
        DataTable dt;              //Representa una tabla de datos en memoria

        public string AbrirBd() //Metodo para abrir la base de datos
        {
            string resultado = "";
            try                 //permite capturar errores en tiempo de ejecucion
            {
                //crear objeto de tipo conexion a la base de datos y se pasara como parametro la cadena de conexion
                conexion = new SqlConnection("Data Source=JOHNDASH24\\SQLEXPRESS;Initial Catalog=[DBFACTURAS];Integrated Security=True");
                conexion.Open(); // invocar metodo para abrir la conexion a la base de datos
            }
            catch (Exception ex) //captura el error y lo almacena en la variable ex
            {
                resultado = " ERROR: No se establecio la conexion con la base de datos" + ex; //muestra el mensaje del error
            }
            return resultado; //retorna el resultado
        }
        public string CerrarBd() //Metodo para cerrar la base de datos
        {
            string resultado = "";
            try                 //permite capturar errores en tiempo de ejecucion
            {
                conexion.Close(); // invocar metodo para cerrar la conexion a la base de datos
            }
            catch (Exception ex) //captura el error y lo almacena en la variable ex
            {
                resultado = " ERROR: No se cerro la conexion con la base de datos" + ex; //muestra el mensaje del error
            }
            return resultado; //retorna el resultado
        }
        //Permite ejecutar procedimientos almacenados 
        public string Ejecutar_procedimientos(string procedimiento, List<Cls_parametros> lst)
        {
            string salida = "";
            try
            {
                int retornado;

                AbrirBd(); //Abrir la base de datos
                // Crear el comando con el nombre del procedimiento almacenado y la conexion
                SqlCommand comando = new SqlCommand(procedimiento, conexion);
                comando.CommandType = CommandType.StoredProcedure; //Especifica que el comando es un procedimiento almacenado

                if (lst != null) //Verifica si la lista de parametros no es nula
                {
                    for (int i = 0; i < lst.Count; i++) //Recorre la lista de parametros
                    {
                        //Analiza si el parametro es de entrada
                        if (lst[i].Direccionparametro == ParameterDirection.Input)
                        {
                            comando.Parameters.AddWithValue(lst[i].Nombre, lst[i].Valor); //Agrega el parametro al comando
                        }

                        //Analiza si el parametro es el que recibe el valor de filas afectadas
                        if (lst[i].Direccionparametro == ParameterDirection.Output)
                        {
                            comando.Parameters.Add(lst[i].Nombre, lst[i].TipoDato, lst[i].Tamaño).Direction = ParameterDirection.Output;
                        }

                    }
                }

                retornado = comando.ExecuteNonQuery(); //Ejecuta el pocedimiento con sus parametros
                CerrarBd();

                if (retornado > 0)
                {
                    salida = "PROCESO EJECUTADO CON EXITO";
                }
                else
                {
                    salida = "ERROR: PROCESO NO EJECUTADO";
                }
            }
            catch (Exception ex)
            {
                salida = "ERROR: " + ex;
            }
            return salida;
        }

        //recibe una sentencia sql o un procedimiento para update, delete, insert
        public string EjecutarComando(string sentencia)
        {
            string salida = "";
            try
            {
                int retornado;
                AbrirBd(); //Abrir la base de datos
                // Crear el comando con la sentencia sql y la conexion
                cmd = new SqlCommand(sentencia, conexion);
                retornado = cmd.ExecuteNonQuery(); //Ejecuta la sentencia sql
                CerrarBd();
                if (retornado > 0)
                {
                    salida = "PROCESO EJECUTADO CON EXITO";
                }
                else
                {
                    salida = "ERROR: PROCESO NO EJECUTADO";
                }
            }
            catch (Exception ex)
            {
                salida = "ERROR: " + ex;
            }
            return salida;
        }

        //Metodo que permite realizar una consulta en una o varias tablas
        public DataTable EjecutarConsulta(string cmd)
        {
            try
            {
                AbrirBd(); //Abrir la base de datos
                // Crear el comando con la sentencia sql y la conexion
                da = new SqlDataAdapter(cmd, conexion); //Crear el adaptador de datos
                dt = new DataTable(); //Crear la tabla de datos
                da.Fill(dt); //Llenar la tabla de datos con el adaptador
                CerrarBd();
                return dt;
            }
            catch (Exception ex)
            {
                return null; //En caso de error retorna nulo
            }

        }
    }
}