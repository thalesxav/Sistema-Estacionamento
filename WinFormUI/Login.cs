using DemoLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormUI
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            txtSenha.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                bool login = SqliteDataAccess.Login(txtUsuario.Text, txtSenha.Text);

                if(login)
                {
                    this.Hide();
                    PeopleForm form = new PeopleForm();
                    this.Invoke((MethodInvoker)delegate() {
                        form.Text = "Controle de Estacionamento - São Rafael Carretas";
                        form.Show();
                    });
                }
                else
                    MessageBox.Show("Usuário ou senha incorretos!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message + ex.StackTrace);
            }
        }

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1.PerformClick();
            }
        }

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
            else if (e.KeyCode == Keys.F10)
            {
                Consulta c = new Consulta();
                c.ShowDialog();
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1.PerformClick();
            }
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
