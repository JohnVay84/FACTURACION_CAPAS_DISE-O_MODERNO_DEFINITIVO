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


namespace Proyecto_Sistema_Facturacion
{
    public partial class FrmEditarCliente : MaterialForm
    {

        public FrmEditarCliente()
        {
            InitializeComponent();
        }
        public int IdCliente { get; set; }
        private void FrmEditarCliente_Load(object sender, EventArgs e)
        {
            if (IdCliente == 0)
            {
                // Caso: Nuevo cliente
                lblTitulo.Text = "INGRESO NUEVO CLIENTE";
            }
            else
            {
                // Caso: Editar cliente (más adelante cargarías datos)
                lblTitulo.Text = "MODIFICAR CLIENTE";
                //txtIdCliente.Text = IdCliente.ToString();
                txtNombre.Text = "Nombre 1 apellido";
                txtDocumento.Text = "3456813450";
                txtDireccion.Text = "Calle donde se ubica el cliente";
                txtTelefono.Text = "3761548";

            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Datos Actualizados");
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {

        }
    }

}
