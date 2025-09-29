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
using Capa_LogicaDeNegocios;
using System.Security.Cryptography;


namespace Proyecto_Sistema_Facturacion
{
    public partial class frmAdminSeguridad: Form
    {
        public frmAdminSeguridad()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        Cls_Seguridad SeguridadEmpleado = new Cls_Seguridad();
       

        private void frmAdminSeguridad_Load(object sender, EventArgs e)
        {
            llenar_combo_Empleados();
        }

        private void llenar_combo_Empleados()
        {
           cboEmpleado.DataSource = SeguridadEmpleado.ConsultarEmpleados();//   llenar el combo box con los empleados
           cboEmpleado.DisplayMember = "StrNombre"; // mostrar el nombre del empleado
           cboEmpleado.ValueMember = "IdEmpleado"; // valor del empleado
        }
       
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consultar();
        }
        
        public void Consultar()
        {
            int IdEmpleado = int.Parse(cboEmpleado.SelectedValue.ToString());
            dt = SeguridadEmpleado.Consulta_SeguridadEmpleado(IdEmpleado);

            if (dt.Rows.Count > 0)
            {
                txtUsuario.Text = dt.Rows[0].ToString();
                txtClave.Text = dt.Rows[0].ToString();
            }
            else
            {
                txtClave.Text = "";
                txtUsuario.Text = "";
                MessageBox.Show("El empleado no tiene usuario y clave asignados");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Guardar();
        }
        // función que permite guardar los datos de ingreso a un usuario
        public void Guardar()
        {
            string mensaje = "";

            //if (Validar()) // función que Valida la información de los campos
            //{
            //    SeguridadEmpleado.C_IdEmpleado = int.Parse(cboEmpleado.SelectedValue.ToString());
            //    SeguridadEmpleado.C_StrUsuario = txtUsuario.Text;
            //    SeguridadEmpleado.C_StrClave = txtClave.Text;
            //    SeguridadEmpleado.C_StrUsuaarioModifico = "Javier";

            //    mensaje = SeguridadEmpleado.ActualizarSeguridadEmpleado(); // invocamos el metodo de actualizar
            //    MessageBox.Show(mensaje);
            //}

            txtClave.Text = "";
            txtUsuario.Text = "";
        }




        private void Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        // eliminamos un registro de la base de datos
        // función que permite eliminar los datos de ingreso de un usuario
        public void Eliminar()
        {
            // Preguntamos si esta seguro de borrar el registro
            if (MessageBox.Show("¿ESTA SEGURO DE BORRAR EL REGISTRO DE: \n" + cboEmpleado.Text, "CONFIRMACION",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // pasamos el parametro del IdEmpleado que vamos a modificar
                SeguridadEmpleado.C_IdEmpleado = int.Parse(cboEmpleado.SelectedValue.ToString());

                string mensaje = SeguridadEmpleado.EliminarSeguridadEmpleado(); // ejecutamos el borrado

                if (mensaje != "BORRADO") // asumimos que "BORRADO" es un mensaje de éxito, si no lo es, mostramos el mensaje de error.
                {
                    MessageBox.Show(mensaje);
                }
                else
                {
                    MessageBox.Show("BORRADO EL REGISTRO");
                }

                txtClave.Text = "";
                txtUsuario.Text = "";
            }
        }

    }
}
