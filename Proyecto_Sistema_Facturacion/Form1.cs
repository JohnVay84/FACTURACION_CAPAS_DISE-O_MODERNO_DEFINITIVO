using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Sistema_Facturacion
{
    public partial class FrmEditarRolEmpleados : Form
    {
        public FrmEditarRolEmpleados()
        {
            InitializeComponent();
        }

       
        private void FrmEditarRolEmpleados_Load(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Datos Actualizados");
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
