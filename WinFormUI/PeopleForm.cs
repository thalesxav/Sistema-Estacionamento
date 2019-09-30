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
    public partial class PeopleForm : Form
    {
        public PeopleForm()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            label1.Text = datetime.ToString();
        }

        private void PeopleForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
            CarregaUltimasPlacas();
            btnEntrada.Focus();
        }

        private void CarregaUltimasPlacas()
        {
            try
            {
                List<RegistrosModel> list = SqliteDataAccess.CarregaUltimasPlacas();

                var columns = from t in list  
                orderby t.data_entrada descending  
                select new  
                {  
                    Placa = t.placa,  
                    Entrada = t.data_entrada,  
                    Saída = t.data_saida/*,
                    Diárias = (t.data_saida == DateTime.MinValue ? 0 : (t.data_saida - t.data_entrada).TotalDays),
                    Valor = RetornaValor(t.data_entrada, t.data_saida, t.tipo).ToString()*/
                }; 

                //dataGridView1.BeginEdit();
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9, FontStyle.Bold);
                dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 9);
                dataGridView1.DataSource = columns.ToList();

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.CurrentRow.Selected = true;


                dataGridView1.Refresh();
                dataGridView1.EndEdit();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private object RetornaValor(DateTime data_entrada, DateTime data_saida, int tipo)
        {
            if(data_saida == DateTime.MinValue)
                return "";

            double dias = (data_saida - data_entrada).TotalDays;
            int diasInteger = (int)dias;
            decimal dec = diasInteger * RetornaValor(tipo);

            return "R$ " + dec;
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

        private void btnEntrada_Click_1(object sender, EventArgs e)
        {
            panel2.Controls.Clear();

            Entrada entrada = new Entrada(ReturnFuncEntrada);
            this.Invoke((MethodInvoker)delegate() {
                panel2.Controls.Add(entrada);
                entrada.Focus();
            });
        }        

        private void btnSaida_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();

            Saida saida = new Saida(ReturnFuncSaida);
            this.Invoke((MethodInvoker)delegate() {
                panel2.Controls.Add(saida);
                saida.Focus();
            });
        }

        protected void ReturnFuncEntrada()
        {
            CarregaUltimasPlacas();
            btnSaida.PerformClick();
        }

        protected void ReturnFuncSaida()
        {
            CarregaUltimasPlacas();
            btnEntrada.PerformClick();
        }

        private void btnSaida_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownCustom(e.KeyCode);
        }

        private void PeopleForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Confirma ?");
            }
            else
                KeyDownCustom(e.KeyCode);
        }

        private void PeopleForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                MessageBox.Show("Confirma ?");
            }
            else
                KeyPressCustom(e.KeyChar);
        }

        private void btnEntrada_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownCustom(e.KeyCode);
        }

        private void btnEntrada_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressCustom(e.KeyChar);
        }

        private void KeyDownCustom(Keys keyCode)
        {
            if (keyCode == Keys.F2)
            {
                btnEntrada.PerformClick();
            }
            else if (keyCode == Keys.F3)
            {
                btnSaida.PerformClick();
            }
        }

        private void KeyPressCustom(char keyChar)
        {
            if (keyChar == (char)113)
            {
                btnEntrada.PerformClick();
            }
            else if (keyChar == (char)114)
            {
                btnSaida.PerformClick();
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownCustom(e.KeyCode);
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressCustom(e.KeyChar);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var placa = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            dataGridView1.CurrentRow.Selected = true;
            //MessageBox.Show(placa);

            panel2.Controls.Clear();

            Saida saida = new Saida(ReturnFuncSaida, placa);
            this.Invoke((MethodInvoker)delegate() {
                panel2.Controls.Add(saida);
                saida.Focus();
            });
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
        }

        private void dataGridView1_KeyDown_1(object sender, KeyEventArgs e)
        {
            KeyDownCustom(e.KeyCode);
        }

        private void dataGridView1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            KeyPressCustom(e.KeyChar);
        }
    }
}
