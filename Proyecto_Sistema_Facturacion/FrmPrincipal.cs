using MaterialSkin;
using MaterialSkin.Controls;
using Pantalla_Sistema_facturación;
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
    public partial class FrmPrincipal: MaterialForm
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        public void AbrirForm(Form formHijo)
        {
            if (this.pnlContenedor.Controls.Count > 0)
                this.pnlContenedor.Controls.RemoveAt(0);
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill;
            this.pnlContenedor.Controls.Add(formHijo);
            this.pnlContenedor.Tag = formHijo;
            formHijo.Show();
        }


                private void btnClientes_Click(object sender, EventArgs e)
        {
            frmListaClientes listaCliente = new frmListaClientes();
            AbrirForm(listaCliente);
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            frmProductos Productos = new frmProductos();
            AbrirForm(Productos);
        }
        private void btnCategorias_Click(object sender, EventArgs e)
        {
            frmCategoriaProductos Categorias = new frmCategoriaProductos();
            AbrirForm(Categorias);
        }
        private void btnfacturas_Click(object sender, EventArgs e)
        {
            frmFacturas Facturas = new frmFacturas();
            AbrirForm(Facturas);
        }
        private void btnInformes_Click(object sender, EventArgs e)
        {
            frmInformes Informes = new frmInformes();
            AbrirForm(Informes);
        }
        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            frmListaEmpleados Empleado = new frmListaEmpleados();
            AbrirForm(Empleado);
        }
        private void btnRoles_Click(object sender, EventArgs e)
        {
            frmRolEmpleados Rol = new frmRolEmpleados();
            AbrirForm(Rol);
        }
        private void btnSeguridad_Click(object sender, EventArgs e)
        {

            frmAdminSeguridad adminSeguridad = new frmAdminSeguridad();
            AbrirForm(adminSeguridad);
        }

        private void btnAcerca_Click(object sender, EventArgs e)
        {
            frmAcerca Acerca = new frmAcerca();
            AbrirForm(Acerca);
        }




        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            //frmAyuda Ayuda = new frmAyuda();
            //AbrirForm(Ayuda);
            //WebBrowser1.Navigate("https://help.sap.com/docs/");
                     
            
            
        }
    
    }
}
