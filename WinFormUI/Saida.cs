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
    public partial class Saida : UserControl
    {
        private Font _printFont;
        public delegate void FormReturnParams();
        public FormReturnParams _returnParam;
        List<EstacionamentoModel> _listEmpresa = SqliteDataAccess.CarregaDadosEstacionamento();
        List<RegistrosModel> _listRegistro = SqliteDataAccess.CarregaUltimoCupom();
        private StringFormat _stringFormat = new StringFormat();
        public int _diarias = 0;
        public decimal _valor = 0;
        public bool _confirma = false;
        private string _placa = "";
        private string _id = "";

        public Saida()
        {
            InitializeComponent();
            txtPlaca.Size = new System.Drawing.Size(173, 100);
            txtPlaca.Focus();
            CarregaCupom();
        }

        public Saida(FormReturnParams returnFunc)
        {
            InitializeComponent();
            txtPlaca.Size = new System.Drawing.Size(173, 100);
            txtPlaca.Focus();
            CarregaCupom();
            _returnParam = returnFunc;
            txtPlaca.Text = _placa;
        }

        public Saida(FormReturnParams returnFunc, string id, string placa)
        {
            this._placa = placa;
            this._id = id;
            InitializeComponent();
            txtPlaca.Size = new System.Drawing.Size(173, 100);
            txtPlaca.Focus();
            CarregaCupom();
            _returnParam = returnFunc;
            txtPlaca.Text = placa;
        }

        private void CarregaCupom()
        {
            if(!btnImprimirSegundaVia.Enabled && !btnRegSaida.Enabled)
            {
                richTextBox1.Text="";
                return;
            }
            try
            {
                if(_listEmpresa.Count() == 0)
                    _listEmpresa = SqliteDataAccess.CarregaDadosEstacionamento();

                int idInicial = 1;
                if(_listRegistro.Count() == 0)
                    _listRegistro = SqliteDataAccess.CarregaUltimoCupom();
                else
                    idInicial = _listRegistro[0].id;
            
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

                if (_listRegistro.Count > 0)
                {

                    DataRow dr = dTable.NewRow();
                    dr["Coluna 1"] = "\t\tSAIDA";
                    dr["Coluna 2"] = "";
                    dTable.Rows.Add(dr);

                    dr = dTable.NewRow();
                    dr["Coluna 1"] = "";
                    dr["Coluna 2"] = "";
                    dTable.Rows.Add(dr);

                    dr = dTable.NewRow();
                    dr["Coluna 1"] = "Via\t\t  :";
                    dr["Coluna 2"] = (btnRegSaida.Enabled ? "1" : "2");
                    dTable.Rows.Add(dr);

                    dr = dTable.NewRow();
                    dr["Coluna 1"] = "Cupom\t  :";
                    dr["Coluna 2"] = NumeroCupom(idInicial);
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
                    dr["Coluna 2"] = _listRegistro[0].data_entrada.ToString("dd/MM/yyyy HH:mm");
                    dTable.Rows.Add(dr);

                    dr = dTable.NewRow();
                    dr["Coluna 1"] = "Saida\t\t  :";
                    dr["Coluna 2"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                    dTable.Rows.Add(dr);

                    dr = dTable.NewRow();
                    dr["Coluna 1"] = "Permanencia:";
                    dr["Coluna 2"] = lbDiarias.Text;
                    dTable.Rows.Add(dr);

                    dr = dTable.NewRow();
                    dr["Coluna 1"] = "Cabine\t  :";
                    dr["Coluna 2"] = "SAORAFAEL";
                    dTable.Rows.Add(dr);

                    dr = dTable.NewRow();
                    dr["Coluna 1"] = "Usuário\t  :";
                    dr["Coluna 2"] = "ADMIN";
                    dTable.Rows.Add(dr);

                    dr = dTable.NewRow();
                    dr["Coluna 1"] = "Local\t\t  :";
                    dr["Coluna 2"] = "PATIO";
                    dTable.Rows.Add(dr);

                    dr = dTable.NewRow();
                    dr["Coluna 1"] = "Tabela\t  :";
                    dr["Coluna 2"] = RetornaTipoValor(_listRegistro[0].tipo);
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
                    dr["Coluna 1"] = "TOTAL\t  :";
                    dr["Coluna 2"] = "R$ " + _valor.ToString() + ",00";
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
                    dr["Coluna 1"] = _listEmpresa[0].nome;
                    dr["Coluna 2"] = "";
                    dTable.Rows.Add(dr);

                    dr = dTable.NewRow();
                    dr["Coluna 1"] = "CNPJ\t  :";
                    dr["Coluna 2"] = _listEmpresa[0].cnpj;
                    dTable.Rows.Add(dr);

                    ordGrid.DataSource = dTable.DefaultView;
                    ordGrid.Refresh();
                    richTextBox1.Text = "";

                    float[] tabs = { 30, 60 };
                    _stringFormat.SetTabStops(0, tabs);

                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < dTable.Columns.Count; j++)
                        {
                            richTextBox1.Text += "\t" + dTable.Rows[i][j].ToString() + (j == 0 ? "\t" : "\n");
                        }
                    }

                    richTextBox1.SelectAll();
                    richTextBox1.SelectionTabs = new int[] { 30, 60 };
                    richTextBox1.AcceptsTab = true;
                    richTextBox1.Select(0, 0);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private object RetornaTipoValor(int tipo)
        {
            if(tipo == 1)
                return "R$ Tipo 1 - R$ 35,00";
            if(tipo == 2)
                return "R$ Tipo 2 - R$ 40,00";

            return "";
        }

        private string RetornaAlinhamento(int length)
        {
            int retorno = 20 - length;
            return retorno.ToString();
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
            List<RegistrosModel> list = new List<RegistrosModel>();
            if (String.IsNullOrEmpty(_id))
                list = SqliteDataAccess.CarregaPagamentoByPlaca(txtPlaca.Text);
            else
                list = SqliteDataAccess.CarregaPagamento(_id); 

            if (list.Count == 0 || (list.Count == 1 && list[0].data_saida != DateTime.MinValue))
            {
                _listRegistro = list;
                btnImprimirSegundaVia.Visible = true;
                btnRegSaida.Enabled = false;
                btnRegSaida.ForeColor = Color.Gray;
                MessageBox.Show("Veículo de Placa '"+txtPlaca.Text+"' não se encontra mais no pátio!");
            }

            else if(!_confirma)
            {
                btnRegSaida.Enabled = true;
                _confirma = true;
                RegistrosModel registro = _listRegistro[0];
                registro.data_saida = DateTime.Now;
                registro.total_pagar = (int)_valor;

                bool result = SqliteDataAccess.RegistraSaida(registro);

                if(!result)
                    MessageBox.Show("Não foi possível registrar a saída da placa '" + txtPlaca.Text + "'.");
                else
                {
                    PrintDocument pd = new PrintDocument();
                    _printFont = new Font("Arial", 10);
                    pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                    pd.Print();
                    _confirma = false;
                    _returnParam.Invoke();
                }
            }
        }

        protected void ReturnFunc(string tipo)
        {
            
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

            string cupom = richTextBox1.Text.Replace("Via\t\t", "Via\t");
            string cupom1 = cupom.Replace("Placa\t\t", "Placa\t");
            string cupom2 = cupom1.Replace("Saida\t\t", "Saida\t");
            string cupom3 = cupom2.Replace("Local\t\t", "Local\t");

            yPos = topMargin + (count * _printFont.GetHeight(ev.Graphics));
            yPos = 0;

            ev.Graphics.DrawString(cupom3, _printFont, Brushes.Black, leftMargin, yPos, _stringFormat);
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
            btnRegSaida.Focus();
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            //KeyDownCustom(e.KeyCode);
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //KeyPressCustom(e.KeyChar);
        }

        private void txtPlaca_KeyDown_1(object sender, KeyEventArgs e)
        {
            //KeyDownCustom(e.KeyCode);
        }

        private void txtPlaca_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //KeyPressCustom(e.KeyChar);
        }

        private void Saida_KeyDown(object sender, KeyEventArgs e)
        {
            //KeyDownCustom(e.KeyCode);
        }

        private void Saida_KeyPress(object sender, KeyPressEventArgs e)
        {
            //KeyPressCustom(e.KeyChar);
        }

        private void KeyDownCustom(Keys keyCode)
        {
            if (keyCode == Keys.Enter)
            {
                btnRegSaida.PerformClick();
            }
            else if (keyCode == Keys.F2)
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
                btnRegSaida.PerformClick();
            }
           /* else if (keyChar == (char)113)
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
            if(txtPlaca.Text.Length == 7)
            {
                CarregaPagamento();
                CarregaCupom();
            }
            else
                CarregaCupom();
        }

        private void CarregaPagamento()
        {
            try
            {
                List<RegistrosModel> list = new List<RegistrosModel>();
                //if (String.IsNullOrEmpty(_id))
                    list = SqliteDataAccess.CarregaPagamentoByPlaca(txtPlaca.Text);
                //else
                    //list = SqliteDataAccess.CarregaPagamento(_id);

                /*if(list.Count == 0)
                {
                    MessageBox.Show("Veículo de Placa '"+txtPlaca.Text+"' não se encontra mais no pátio!");
                }
                if(list.Count == 1 && list[0].data_saida != DateTime.MinValue)
                {
                    MessageBox.Show("Veículo de Placa '"+txtPlaca.Text+"' não se encontra mais no pátio!");
                }*/

                if (list.Count == 0)
                {
                    MessageBox.Show("Veículo de Placa '" + txtPlaca.Text + "' não se encontra no pátio!");
                    LimparTela();
                }
                else if (list.Count == 1 && list[0].data_saida != DateTime.MinValue)
                {
                    //MessageBox.Show("Veículo de Placa '" + txtPlaca.Text + "' já saiu do pátio!");
                    _listRegistro = list;
                    btnImprimirSegundaVia.Visible = true;
                    btnRegSaida.Enabled = false;
                    btnRegSaida.ForeColor = Color.Gray;
                    PreencheDaodsSaida();
                }
                else
                {
                    btnImprimirSegundaVia.Visible = false;
                    btnRegSaida.Enabled = true;
                    _listRegistro = list;
                    PreencheDaodsSaida();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void LimparTela()
        {
            btnImprimirSegundaVia.Enabled = false;
            btnRegSaida.Enabled = false;
            btnRegSaida.ForeColor = Color.Gray;

            lbEntrada.Text = "";
            lbSaida.Text = "";
            lbDiarias.Text = "";
            lbTotal.Text = "";
        }

        private void PreencheDaodsSaida()
        {
            lbEntrada.Text = _listRegistro[0].data_entrada.ToString("dd/MM/yyyy HH:mm");
            lbSaida.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            double dias = (DateTime.Now - _listRegistro[0].data_entrada).TotalDays;
            int diasInteger = (int)dias;
            if(diasInteger == 0)
                diasInteger = 1;
            _diarias = diasInteger;
            decimal dec = diasInteger * RetornaValor(_listRegistro[0].tipo);
            _valor = dec;

            lbDiarias.Text = diasInteger.ToString() + " diaria" + (diasInteger == 1 ? "" : "s");
            lbTotal.Text = "R$ " + dec.ToString();
        }

        private int RetornaValor(int tipo)
        {
            try
            {
                List<TiposPagamentoModel> list = SqliteDataAccess.RetornaValor(tipo);
                return (int)list[0].valor;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void cmbTipo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            CarregaCupom();
        }

        private void button1_KeyDown_1(object sender, KeyEventArgs e)
        {
            //KeyDownCustom(e.KeyCode);
        }

        private void button1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //KeyPressCustom(e.KeyChar);
        }

        private void txtPlaca_Click(object sender, EventArgs e)
        {
            //txtPlaca.Text = "";
        }

        private void Saida_Load(object sender, EventArgs e)
        {
            txtPlaca.Text = _placa;
        }

        private void btnImprimirSegundaVia_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            _printFont = new Font("Arial", 10);
            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            pd.Print();
            _confirma = false;
        }
    }
}
