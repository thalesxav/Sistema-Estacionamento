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
    public partial class ConfirmaTipo : Form
    {
        public delegate void FormReturnParams(string tipo);
        public FormReturnParams returnParam;
        string _tipo = "0";

        public ConfirmaTipo()
        {
            InitializeComponent();
        }

        public ConfirmaTipo(string tipo, FormReturnParams del)
        {
            InitializeComponent();
            returnParam = del;
            _tipo = tipo;

            if(tipo == "0")
                radioButton1.Checked = true;
            else if(tipo == "1")
                radioButton2.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                _tipo = "0";
            else if (radioButton2.Checked)
                _tipo = "1";
            
            returnParam.Invoke(_tipo);
            this.Close();
        }

        private void ConfirmaTipo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void ConfirmaTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1.PerformClick();
            }
        }

        private void radioButton1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void radioButton1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1.PerformClick();
            }
        }

        private void radioButton2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void radioButton2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1.PerformClick();
            }
        }
    }
}
