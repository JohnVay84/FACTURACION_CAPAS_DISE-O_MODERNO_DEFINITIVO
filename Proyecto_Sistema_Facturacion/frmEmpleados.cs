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
    public partial class frmEmpleados: Form
    {
        public frmEmpleados()
        {
            InitializeComponent();
        }

        // Cambiar el nombre de la propiedad o del control para evitar la ambigüedad
        private int _idEmpleadoValue;
        public int IdEmpleadoValue
        {
            get { return _idEmpleadoValue; }
            set { _idEmpleadoValue = value; }
        }

        DataTable dt = new DataTable(); // data table para consulta de datos
        Cls_Empleados empleado = new Cls_Empleados(); // Instanciamos la clase empleados
        private void materialLabel1_Click(object sender, EventArgs e)
        {

        }

        // evento click del boton guardar
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Guardar();
        }
        //-- funciones que permiten el ingreso o actualizacion de la informacion de empleados en la base de datos
        public void Guardar()
        {
            string mensaje = "";
            if (validar()) // funcion que Valida la informacion de los campos
            {
                empleado.C_IdEmpleado = IdEmpleadoValue;
                empleado.C_StrNombre = txtNombre.Text;
                empleado.C_NumDocumento = double.Parse(txtDocumento.Text);
                empleado.C_StrDireccion = txtDireccion.Text;
                empleado.C_StrTelefono = txtTelefono.Text;
                empleado.C_StrEmail = txtEmail.Text;
                empleado.C_IdRolEmpleado = int.Parse(cboRol.SelectedValue.ToString());
                empleado.C_DtmIngreso = dtmIngreso.Text;
                empleado.C_DtmRetiro = dtmRetiro.Text;
                empleado.C_StrDatosAdicionales = txtDatosAdicionales.Text;
                empleado.C_StrUsuarioModifico = "Javier";

                mensaje = empleado.ActualizarEmpleado(); // invocamos el metodo de actualizar informacion de empleados
                MessageBox.Show(mensaje);
            }
        }

        // evento load del formulario para llenar el combo y los campos si es necesario
        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            llenar_combo(); // funcion para llenar el combo con valores de los roles

            if (IdEmpleadoValue == 0)
            {
                // REGISTRO NUEVO
                lblTitulo.Text = "INGRESO NUEVO EMPLEADO"; // solo cambiamos el titulo de nuestra ventana
            }
            else
            {
                // ACTUALIZAR EL REGISTRO DEL ID QUE SE RECIBE
                lblTitulo.Text = "MODIFICAR EMPLEADO"; // CAMBIAMOS EL TITULO
                llenar_campos(); // llenamos los campos con lo que se tiene en la base de datos y sobre estos valores modificamos el registro
            }
        }
        //=== llenamos el combo cboRol el cual mostrara la lista de roles
        private void llenar_combo()
        {
            cboRol.DataSource = empleado.ConsultarRol(); // Invocamos metodo de consulta del Rol
            cboRol.DisplayMember = "StrDescripcion"; // Informacion del nombre del rol que se muestra al usuario
            cboRol.ValueMember = "IdRolEmpleado"; // codigo interno que identifica el rol, este es el valor que se guarda luego en la tabla de empleados
        }

        //==== consultamos por el Id el empleado que vamos a modificar y mostramos la informacion consultada
        private void llenar_campos()
        {
            dt = empleado.Consulta_Empleado(IdEmpleadoValue); // consultamos el empleado con un IdEmpleado especifico

            if (dt.Rows.Count > 0) // si retorna valores de consulta, asignamos los valores a los campos
            {
                foreach (DataRow row in dt.Rows)
                {
                    txtNombre.Text = row[1].ToString();
                    txtDocumento.Text = row[2].ToString();
                    txtDireccion.Text = row[3].ToString();
                    txtTelefono.Text = row[4].ToString();
                    txtEmail.Text = row[5].ToString();

                    // selecciona en la lista el rol de acuerdo al valor que se tiene en la tabla tblEmpleados
                    cboRol.SelectedValue = int.Parse(row[6].ToString());

                    dtmIngreso.Value = Convert.ToDateTime(row[7].ToString());
                    dtmRetiro.Value = Convert.ToDateTime(row[8].ToString());
                    txtDatosAdicionales.Text = row[9].ToString();
                }
            }
        }

            //FUNCIÓN QUE PERMITE VALIDAR LOS CAMPOS DEL FORMULARIO
private Boolean validar()
        {
            Boolean errorCampos = true;

            if (txtNombre.Text == string.Empty)
            {
                MensajeError.SetError(txtNombre, "debe ingresar el nombre del empleado");
                txtNombre.Focus();
                errorCampos = false;
            }
            else { MensajeError.SetError(txtNombre, ""); }

            if (txtDocumento.Text == "")
            {
                MensajeError.SetError(txtDocumento, "debe ingresar el documento");
                txtDocumento.Focus();
                errorCampos = false;
            }
            else { MensajeError.SetError(txtDocumento, ""); }

            if (!esNumerico(txtDocumento.Text))
            {
                MensajeError.SetError(txtDocumento, "El Documento debe ser numerico");
                txtDocumento.Focus();
                return false;
            }
            MensajeError.SetError(txtDocumento, "");
            return errorCampos;
        }
        
        private bool esNumerico(string num)
        {
            try
            {
                double x = Convert.ToDouble(num);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            // verificamos si desea cerrar la ventana

            DialogResult Rta;
            Rta = MessageBox.Show("Desea salir de la edición ?", "MENSAJE DE ADVERTENCIA",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (Rta == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
