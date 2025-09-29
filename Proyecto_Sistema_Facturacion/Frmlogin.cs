using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_LogicaDeNegocios;


namespace Proyecto_Sistema_Facturacion
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            if (TxtUsuario.Text != "" && TxtPassword.Text != string.Empty) //validamos que usuario y la clave no esten vacios
            {
                // Creamos el objeto a partir de la clase Validar_usuario
                Validar_usuario Obj_validar = new Validar_usuario();

                // asignamos valores a los atributos
                Obj_validar.C_StrClave = TxtPassword.Text;
                Obj_validar.C_StrUsuario = TxtUsuario.Text;

                Obj_validar.ValidarUsuario(); // ejecutamos el metodo de validacion

                if (Obj_validar.C_IdEmpleado != 0)
                {
                    MessageBox.Show("Datos de verificacion Validos "); // mostramos mensaje
                    FrmPrincipal frmpal = new FrmPrincipal(); //Creamos el objeto del formulario FrmPrincipal
                    this.Hide(); // Ocultamos el formulario login
                    frmpal.Show(); // Mostramos el formulario principal
                }
                else
                {
                    MessageBox.Show("USUARIOS Y CLAVE NO ENCONTRADOS");
                    TxtUsuario.Text = "";
                    TxtUsuario.Focus();
                    TxtPassword.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Debes ingresar un usuario y una clave");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }

}
