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
    public partial class frmListaClientes: Form
    {
        public frmListaClientes()
        {
            InitializeComponent();
        }

        private void llenar_grid() //Método para llenar el GridView con los datos
        {
            for (int i = 1; i < 10; i++)
            {
                dgClientes.Rows.Add(i, $"Nombre {i} Apellido1 Apellido 2 ", $"{i * 12345}", $"{i * 12345}");
            }
        }


        private void frmListaClientes_Load(object sender, EventArgs e)
        {
            llenar_grid();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmEditarCliente Cliente = new frmEditarCliente();
            Cliente.ShowDialog();

        }

        private void dgClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int posActual; 

            if (dgClientes.Columns[e.ColumnIndex].Name == "btnBorrar")
            {
                posActual = dgClientes.CurrentRow.Index;
                if (MessageBox.Show("Seguro de borrar", "CONFIRMACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MessageBox.Show($"BORRANDO índice {e.RowIndex} ID {dgClientes[0, posActual].Value}");
                    dgClientes.Rows.RemoveAt(posActual);
                }
            }

            if (dgClientes.Columns[e.ColumnIndex].Name == "btnEditar")
            {
                posActual = dgClientes.CurrentRow.Index;
                frmEditarCliente Cliente = new frmEditarCliente();
                Cliente.IdCliente = int.Parse(dgClientes[0, posActual].Value.ToString());
                Cliente.ShowDialog();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }
    }

    internal class frmEditarCliente
    {
        public int IdCliente { get; internal set; }

        internal void ShowDialog()
        {
            throw new NotImplementedException();
        }
    }
}

