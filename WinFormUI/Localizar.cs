﻿using System;
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
using System.Globalization;

namespace WinFormUI
{
    public partial class Localizar : UserControl
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

        public Localizar()
        {
            InitializeComponent();
            PreencheTiposPagamentos();
            txtPlaca.Size = new System.Drawing.Size(173, 100);
            txtPlaca.Focus();
            richTextBox1.ScrollBars = RichTextBoxScrollBars.Vertical;
            CarregaCupom();
        }

        private void PreencheTiposPagamentos()
        {
            try
            {
                 
                List<TiposPagamentoModel> list = SqliteDataAccess.CarregaTiposPagamentos();

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void CarregaCupom()
        {
            richTextBox1.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBox1.Refresh();
            try
            {
                //if(_list.Count() == 0)
                //    _list = SqliteDataAccess.CarregaDadosEstacionamento();

//                int idInicial = 1;
                
                
                
            
                DataGridTableStyle tableStyle = new DataGridTableStyle();
                DataGridTextBoxColumn txtColumn = new DataGridTextBoxColumn();
                txtColumn.HeaderText = "Coluna 1";
                //txtColumn.Width = 100;
                tableStyle.GridColumnStyles.Add(txtColumn);
                //DataGridTextBoxColumn txtColumn2 = new DataGridTextBoxColumn();
                //txtColumn2.HeaderText = "Coluna 2";
                //txtColumn2.Width = 50;
                //tableStyle.GridColumnStyles.Add(txtColumn2);

                DataGrid ordGrid = new DataGrid();
                ordGrid.TableStyles.Add(tableStyle);
                DataTable dTable = new DataTable();
                dTable.Columns.Add(new DataColumn("Coluna 1"));
                //dTable.Columns.Add(new DataColumn("Coluna 2"));

                DataRow dr = dTable.NewRow();
                //dr["Coluna 1"] = "\t\tENTRADA";
                dTable.Rows.Add(dr);

                foreach (RegistrosModel reg in _listCupom)
                {
                    dr = dTable.NewRow();
                    dr["Coluna 1"] =  "Data Entrada: " + reg.data_entrada + Environment.NewLine +
                                      "Data saida: " + reg.data_saida + Environment.NewLine +
                                      "Impresso: " + (reg.impresso == 1 ? "Sim" : "Nao") + Environment.NewLine + 
                                      "Tipo: " + reg.tipo + Environment.NewLine + 
                                      "Valor: R$ " + reg.total_pagar + ",00" + Environment.NewLine + Environment.NewLine;
                    dTable.Rows.Add(dr);
                }

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
                    richTextBox1.Text += "" + Environment.NewLine;
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
        

        private void button1_Click(object sender, EventArgs e)
        {
            if(!_confirma)
            {
                _confirma = true;
                
            }
        }

        protected void ReturnFunc(string tipo)
        {
            _confirma = false;

            //Insere Cupom
            RegistrosModel registro = new RegistrosModel();
            registro.placa = txtPlaca.Text;
            //registro.entrada_saida = "1";
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
            _listCupom = SqliteDataAccess.LocalizarPlaca(txtPlaca.Text);
            CarregaCupom();
        }

        private void cmbTipo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            _listCupom = SqliteDataAccess.LocalizarPlaca(txtPlaca.Text);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DateTime valor;
            var convertido = DateTime
                .TryParseExact(textBox1.Text,
                                "dd/MM/yyyy",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out valor);

            if(convertido)
            {
                int ano = Convert.ToInt32(textBox1.Text.Substring(6, 4));
                int mes = Convert.ToInt32(textBox1.Text.Substring(3, 2));
                int dia = Convert.ToInt32(textBox1.Text.Substring(0, 2));
                DateTime dt = new DateTime(ano, mes, dia);
                DateTime dt2 = dt.AddDays(1);
                string dataFormatada = dt.ToString("yyyy-MM-dd");
                string dataFormatada2 = dt2.ToString("yyyy-MM-dd");
                _listCupom = SqliteDataAccess.RelatorioPorData(dataFormatada, dataFormatada2);
                CarregaCupom();
            }
        }
    }
}
