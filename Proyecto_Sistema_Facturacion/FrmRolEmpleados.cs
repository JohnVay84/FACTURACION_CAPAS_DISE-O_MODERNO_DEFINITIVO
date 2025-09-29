using Capa_LogicaDeNegocios;
using Proyecto_Sistema_Facturacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pantalla_Sistema_facturación
{
    public partial class frmRolEmpleados : Form
    {
        public frmRolEmpleados()
        {
            InitializeComponent();
        }

        private void llenar_grid()
        {
            dgvRoles.Rows.Clear(); // limpia el data grid view

            Cls_Roles rol = new Cls_Roles(); // objeto de la clase roles
            DataTable dt = rol.ConsultarRoles(); // consulta la tabla de roles

            if (dt != null && dt.Rows.Count > 0) // si la consulta fue exitosa
            {
                foreach (DataRow row in dt.Rows)
                {
                    dgvRoles.Rows.Add(row["IdRolEmpleado"].ToString(),
                                      row["StrDescripcion"].ToString());
                }
            }
            else
            {
                MessageBox.Show("No se encontraron registros"); // muestra mensaje si está vacío
            }
        }

        private void FrmRolEmpleados_Load(object sender, EventArgs e)
        {
            llenar_grid(); // llena el data grid view con los roles
        }

                  

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            FrmEditarRolEmpleados editarRolEmpleados = new FrmEditarRolEmpleados();
            editarRolEmpleados.ShowDialog();
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            Cls_Roles rol = new Cls_Roles();
            DataTable dt = rol.Filtrar_Roles(TxtRol.Text);

            if (dt != null)
            {
                dgvRoles.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dgvRoles.Rows.Add(row["IdRolEmpleado"].ToString(), row["StrDescripcion"].ToString());
                }
            }
            else
            {
                MessageBox.Show("No se encontraron roles con ese filtro.");
            }
        }
    }
}