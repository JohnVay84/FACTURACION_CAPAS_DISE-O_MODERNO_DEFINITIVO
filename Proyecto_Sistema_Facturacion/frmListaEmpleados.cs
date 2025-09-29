using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using Capa_LogicaDeNegocios; // Importamos la capa de Logica de Negocios

namespace Proyecto_Sistema_Facturacion
{
    public partial class frmListaEmpleados: Form
    {
        public frmListaEmpleados()

        {
            InitializeComponent();
        }

       DataTable dt = new DataTable(); // data table para consulta de datos
        Cls_Empleados empleado = new Cls_Empleados(); // Instanciamos la clase empleados

         

        private void frmListaEmpleados_Load(object sender, EventArgs e)
        {
            llenar_grid();
        }


       

        private void dgEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgEmpleados.Columns[e.ColumnIndex].Name == "btnEditar")
            {
                int posActual = dgEmpleados.CurrentRow.Index;
                // Abrimos el formulario de edición (asegúrate de que frmEditarCliente existe y está referenciado)
                frmEmpleados cliente = new frmEmpleados();

                cliente.IdEmpleadoValue = int.Parse(dgEmpleados[0, posActual].Value.ToString());
                cliente.ShowDialog();
                //frmEditarCliente cliente = new frmEditarCliente();
                //if (int.TryParse(dgClientes.Rows[posActual].Cells[0].Value?.ToString(), out int idCliente))
                //{
                //    cliente.IdCliente = idCliente;
                //}
                //else
                //{
                //    MessageBox.Show("Invalid client ID format.");
                //}
                //cliente.IdCliente = Convert.ToInt32(dgClientes.Rows[posActual].Cells[0].Value);
                //cliente.ShowDialog();
                llenar_grid(); // Actualizamos la lista después de editar
                //return;
            }
            // Verificamos si el botón presionado es el de BORRAR
            if (dgEmpleados.Columns[e.ColumnIndex].Name == "btnBorrar")
            {
                int posActual = dgEmpleados.CurrentRow.Index; // Identificamos la fila seleccionada
                if (MessageBox.Show($"¿Seguro de borrar al cliente? {dgEmpleados[1, posActual].Value.ToString()}", "CONFIRMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int IdCliente = Convert.ToInt32(dgEmpleados[0, posActual].Value.ToString());
                    string sentencia = $"Exec ELIMINAR_CLIENTE '{IdCliente}'";
                    //string Mensaje = Acceso.EjecutarComando(sentencia);
                    //MessageBox.Show(Mensaje);
                    llenar_grid(); // Actualizamos la lista después de eliminar
                }
            }




        }

        //-- consultamos la tabla de empleados para llenar el datagridview
        private void llenar_grid()
        {
            dgEmpleados.Rows.Clear(); // limpiamos los datos previos del grid
            dt = empleado.Consulta_Empleado(); // llenamos el data table (dt) con los datos retornados desde el metodo Consulta_Empleado()

            if (dt.Rows.Count > 0) // Verifico si retorno valores
            {
                foreach (DataRow row in dt.Rows) // esta instruccion permite recorrer todas las filas retornadas
                {
                    dgEmpleados.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[4].ToString()); // llenamos el grid
                }
            }
            else
            {
                MessageBox.Show("No se encontraron registros");
            }
        }

             
        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            frmEmpleados Empleado = new frmEmpleados(); // Creamos el objeto del formulario para actualizar los datos empleados
            Empleado.IdEmpleadoValue = 0; // Usamos la propiedad pública para establecer el id del empleado
            Empleado.ShowDialog(); // mostramos el formulario
            llenar_grid(); // una vez actualizados los datos cargamos de nuevo el grid para ver los cambios realizados
        }
       
        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            if (txtBuscar.Text != "") // verifico si ingresaron texto a buscar
            {
                dgEmpleados.Rows.Clear(); // limpiamos el datagridview
                dt = empleado.Filtrar_Empleados(txtBuscar.Text); // invocamos filtrar empleado con el texto a buscar como parametro

                if (dt.Rows.Count > 0) // si retorno valores los recorremos para llenar el gridview
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        dgEmpleados.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[4].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros por la busqueda solicitada");
                    llenar_grid();
                }
            }
            else
            {
                llenar_grid(); // sino ingresaron valor a buscar llenamos el grid con todos los valores
            }
            txtBuscar.Text = "";
        }
    }

}

