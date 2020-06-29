using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using DemoLibrary;

namespace WinFormUI
{
    public partial class Entrada : UserControl
    {
        public bool _confirma = false;
        private Font _printFont;
        //private System.IO.StreamReader _streamToPrint;
        string _tipo = "1";
        public delegate void FormReturnParams();
        public FormReturnParams _returnParam;
        List<EstacionamentoModel> _list = SqliteDataAccess.CarregaDadosEstacionamento();
        List<RegistrosModel> _listCupom = SqliteDataAccess.CarregaUltimoCupom();
        private StringFormat _stringFormat = new StringFormat();

        public Entrada()
        {
            InitializeComponent();
            PreencheTiposPagamentos();
            txtPlaca.Size = new System.Drawing.Size(173, 100);
            cmbTipo.SelectedIndex = 0;
            txtPlaca.Focus();
            CarregaCupom();
        }

        public Entrada(FormReturnParams returnFunc)
        {
            InitializeComponent();
            PreencheTiposPagamentos();
            txtPlaca.Size = new System.Drawing.Size(173, 100);
            cmbTipo.SelectedIndex = 0;
            txtPlaca.Focus();
            CarregaCupom();
            _returnParam = returnFunc;
        }

        private void PreencheTiposPagamentos()
        {
            try
            {
                 
                List<TiposPagamentoModel> list = SqliteDataAccess.CarregaTiposPagamentos();

                foreach(TiposPagamentoModel tpPag in list)
                {
                    cmbTipo.Items.Add("Tipo " + tpPag.tipo.ToString() + " - R$ " + tpPag.valor.ToString());
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void CarregaCupom()
        {
            try
            {
                if(_list.Count() == 0)
                    _list = SqliteDataAccess.CarregaDadosEstacionamento();

                int idInicial = 1;
                if(_listCupom.Count() == 0)
                    _listCupom = SqliteDataAccess.CarregaUltimoCupom();
                else
                    idInicial = _listCupom[0].id;
            
                DataGridTableStyle tableStyle = new DataGridTableStyle();
                DataGridTextBoxColumn txtColumn = new DataGridTextBoxColumn();
                txtColumn.HeaderText = "Coluna 1";
                txtColumn.Width = 100;
                tableStyle.GridColumnStyles.Add(txtColumn);
                DataGridTextBoxColumn txtColumn2 = new DataGridTextBoxColumn();
                txtColumn2.HeaderText = "Coluna 2";
                txtColumn2.Width = 50;
                tableStyle.GridColumnStyles.Add(txtColumn2);

                DataGrid ordGrid = new DataGrid();
                ordGrid.TableStyles.Add(tableStyle);
                DataTable dTable = new DataTable();
                dTable.Columns.Add(new DataColumn("Coluna 1"));
                dTable.Columns.Add(new DataColumn("Coluna 2"));
            
                /*DataRow dr = dTable.NewRow();
                dr["Coluna 1"] = "";
                dr["Coluna 2"] = "";
                dTable.Rows.Add(dr);*/

                DataRow dr = dTable.NewRow();
                dr["Coluna 1"] = "\t\tENTRADA";
                dr["Coluna 2"] = "";
                dTable.Rows.Add(dr);

                dr = dTable.NewRow();
                dr["Coluna 1"] = "";
                dr["Coluna 2"] = "";
                dTable.Rows.Add(dr);

                dr = dTable.NewRow();
                dr["Coluna 1"] = "Cupom\t  :";
                dr["Coluna 2"] = NumeroCupom(idInicial + 1);
                dTable.Rows.Add(dr);

                dr = dTable.NewRow();
                dr["Coluna 1"] = "Placa\t\t  :";
                dr["Coluna 2"] = txtPlaca.Text;
                dTable.Rows.Add(dr);

                dr = dTable.NewRow();
                dr["Coluna 1"] = "";
                dr["Coluna 2"] = "";
                dTable.Rows.Add(dr);

                dr = dTable.NewRow();
                dr["Coluna 1"] = "Entrada\t  :";
                dr["Coluna 2"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                dTable.Rows.Add(dr);

                dr = dTable.NewRow();
                dr["Coluna 1"] = "Cabine\t  :";
                dr["Coluna 2"] = "SAORAFAEL";
                dTable.Rows.Add(dr);


                dr = dTable.NewRow();
                dr["Coluna 1"] = "Usuario\t  :";
                dr["Coluna 2"] = "ADMIN";
                dTable.Rows.Add(dr);

                dr = dTable.NewRow();
                dr["Coluna 1"] = "Local\t\t  :";
                dr["Coluna 2"] = "PATIO";
                dTable.Rows.Add(dr);

                dr = dTable.NewRow();
                dr["Coluna 1"] = "Tabela\t  :";
                dr["Coluna 2"] = cmbTipo.SelectedItem + ",00";
                dTable.Rows.Add(dr);

                dr = dTable.NewRow();
                dr["Coluna 1"] = "";
                dr["Coluna 2"] = "";
                dTable.Rows.Add(dr);
                dr = dTable.NewRow();
                dr["Coluna 1"] = "";
                dr["Coluna 2"] = "";
                dTable.Rows.Add(dr);

                dr = dTable.NewRow();
                dr["Coluna 1"] = "Seja bem vindo!";
                dr["Coluna 2"] = "";
                dTable.Rows.Add(dr);

                dr = dTable.NewRow();
                dr["Coluna 1"] = "";
                dr["Coluna 2"] = "";
                dTable.Rows.Add(dr);
                dr = dTable.NewRow();
                dr["Coluna 1"] = "";
                dr["Coluna 2"] = "";
                dTable.Rows.Add(dr);
                dr = dTable.NewRow();
                dr["Coluna 1"] = "";
                dr["Coluna 2"] = "";
                dTable.Rows.Add(dr);

                dr = dTable.NewRow();
                dr["Coluna 1"] = _list[0].nome;
                dr["Coluna 2"] = "";
                dTable.Rows.Add(dr);

                dr = dTable.NewRow();
                dr["Coluna 1"] = "CNPJ\t  :";
                dr["Coluna 2"] = _list[0].cnpj;
                dTable.Rows.Add(dr);

                ordGrid.DataSource = dTable.DefaultView;
                ordGrid.Refresh();   
                richTextBox1.Text = "";

                float[] tabs = { 30, 60 };
                _stringFormat.SetTabStops(0, tabs);

                for(int i = 0; i < dTable.Rows.Count; i++)
                {
                    for(int j = 0; j < dTable.Columns.Count; j++)
                    {
                        richTextBox1.Text += "\t" + dTable.Rows[i][j].ToString() + (j == 0 ? "\t" : "\n");
                    }
                }

                richTextBox1.SelectAll();
                richTextBox1.SelectionTabs = new int[] { 30, 60 };
                richTextBox1.AcceptsTab = true;
                richTextBox1.Select(0, 0);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private string NumeroCupom(int id)
        {
            string a = id.ToString();;
            string s = "";

            for(int x = 0; x < (6 - a.Length); x++)
            {
                s += "0";
            }

            a = s+a;
            return a;
        }

        private void txtUsuario_MouseClick(object sender, MouseEventArgs e)
        {
            if(txtPlaca.Text == "XXX0000")
                txtPlaca.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!_confirma)
            {
                _confirma = true;
                ConfirmaTipo form = new ConfirmaTipo(cmbTipo.SelectedIndex.ToString(), ReturnFunc);
                this.Invoke((MethodInvoker)delegate() {
                    form.ShowDialog();
                });
            }
        }

        protected void ReturnFunc(string tipo)
        {
            _confirma = false;

            //Insere Cupom
            RegistrosModel registro = new RegistrosModel();
            registro.placa = txtPlaca.Text;
            //registro.entrada_saida = "1";
            registro.tipo = cmbTipo.SelectedIndex + 1;
            registro.data_entrada = DateTime.Now;
            registro.impresso = 0;

            bool result = SqliteDataAccess.ExistePlacaEntrada(txtPlaca.Text);

            if(result)
                MessageBox.Show("A Placa '" + txtPlaca.Text + "' já se encontra no Pátio. Favor registrar Saída(F3) primeiro.");
            else
            {
                result = SqliteDataAccess.InsereCupom(registro);

                if(!result)
                    MessageBox.Show("Erro ao dar Entrada na placa " + txtPlaca.Text);
                else
                {
                     //Imprime Cupom
                    PrintDocument pd = new PrintDocument();
                    _printFont = new Font("Arial", 10);
                    pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                    pd.Print();
                    _returnParam.Invoke();
                }
            }
        }

        // The PrintPage event is raised for each page to be printed.
        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left / 2;
            leftMargin = 0;
            float topMargin = ev.MarginBounds.Top / 2;
            string line = null;

            // Calculate the number of lines per page.
            linesPerPage = ev.MarginBounds.Height / _printFont.GetHeight(ev.Graphics);

            string cupom = richTextBox1.Text.Replace("Placa\t\t", "Placa\t");
            string cupomEntrada = cupom.Replace("Local\t\t", "Local\t");

            yPos = topMargin + (count * _printFont.GetHeight(ev.Graphics));
            yPos = 0;

            ev.Graphics.DrawString(cupomEntrada, _printFont, Brushes.Black, leftMargin, yPos, _stringFormat);
            count++;

            // If more lines exist, print another page.
            if (line != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownCustom(e.KeyCode);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressCustom(e.KeyChar);
        }

        private void txtPlaca_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownCustom(e.KeyCode);
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressCustom(e.KeyChar);
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownCustom(e.KeyCode);
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressCustom(e.KeyChar);
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Focus();
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownCustom(e.KeyCode);
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressCustom(e.KeyChar);
        }

        private void txtPlaca_KeyDown_1(object sender, KeyEventArgs e)
        {
            KeyDownCustom(e.KeyCode);
        }

        private void txtPlaca_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            KeyPressCustom(e.KeyChar);
        }

        private void Entrada_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownCustom(e.KeyCode);
        }

        private void Entrada_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressCustom(e.KeyChar);
        }

        private void KeyDownCustom(Keys keyCode)
        {
            if (keyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
            else if (keyCode == Keys.F3)
            {
                _returnParam.Invoke();
                this.Hide();
            }
            else if (keyCode == Keys.Enter)
            {
                _returnParam.Invoke();
                this.Hide();
            }
        }

        private void KeyPressCustom(char keyChar)
        {
            if (keyChar == (char)13)
            {
                button1.PerformClick();
            }
            /*else if (keyChar == (char)113)
            {
                _returnParam.Invoke();
                this.Hide();
            }
            else if (keyChar == (char)114)
            {
                _returnParam.Invoke();
                this.Hide();
            }*/
        }

        private void cmbTipo_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownCustom(e.KeyCode);
        }

        private void cmbTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressCustom(e.KeyChar);
        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {
            txtPlaca.Focus();
        }

        private void txtPlaca_TextChanged(object sender, EventArgs e)
        {
            CarregaCupom();
        }

        private void cmbTipo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            CarregaCupom();
        }

        private void button1_KeyDown_1(object sender, KeyEventArgs e)
        {
            KeyDownCustom(e.KeyCode);
        }

        private void button1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            KeyPressCustom(e.KeyChar);
        }

        private void txtPlaca_Click(object sender, EventArgs e)
        {
            //txtPlaca.Text = "";
        }
    }
}
