namespace WinFormUI
{
    partial class Saida
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbTotal = new System.Windows.Forms.Label();
            this.lbDiarias = new System.Windows.Forms.Label();
            this.lbSaida = new System.Windows.Forms.Label();
            this.lbEntrada = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRegSaida = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.btnImprimirSegundaVia = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 194F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 556F));
            this.tableLayoutPanel1.Controls.Add(this.richTextBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(439, 528);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.richTextBox1.Location = new System.Drawing.Point(197, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(227, 522);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            this.richTextBox1.Click += new System.EventHandler(this.richTextBox1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightCoral;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnImprimirSegundaVia);
            this.panel1.Controls.Add(this.lbTotal);
            this.panel1.Controls.Add(this.lbDiarias);
            this.panel1.Controls.Add(this.lbSaida);
            this.panel1.Controls.Add(this.lbEntrada);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnRegSaida);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtPlaca);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(188, 522);
            this.panel1.TabIndex = 5;
            // 
            // lbTotal
            // 
            this.lbTotal.AutoSize = true;
            this.lbTotal.Location = new System.Drawing.Point(125, 175);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(45, 13);
            this.lbTotal.TabIndex = 18;
            this.lbTotal.Text = "R$ 0,00";
            this.lbTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbDiarias
            // 
            this.lbDiarias.AutoSize = true;
            this.lbDiarias.Location = new System.Drawing.Point(125, 153);
            this.lbDiarias.Name = "lbDiarias";
            this.lbDiarias.Size = new System.Drawing.Size(46, 13);
            this.lbDiarias.TabIndex = 17;
            this.lbDiarias.Text = "0 diárias";
            this.lbDiarias.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbSaida
            // 
            this.lbSaida.AutoSize = true;
            this.lbSaida.Location = new System.Drawing.Point(105, 140);
            this.lbSaida.Name = "lbSaida";
            this.lbSaida.Size = new System.Drawing.Size(65, 13);
            this.lbSaida.TabIndex = 16;
            this.lbSaida.Text = "00/00/0000";
            this.lbSaida.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbEntrada
            // 
            this.lbEntrada.AutoSize = true;
            this.lbEntrada.Location = new System.Drawing.Point(105, 127);
            this.lbEntrada.Name = "lbEntrada";
            this.lbEntrada.Size = new System.Drawing.Size(65, 13);
            this.lbEntrada.TabIndex = 15;
            this.lbEntrada.Text = "00/00/0000";
            this.lbEntrada.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Permanência:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Saída:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Entrada:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Total a pagar:";
            // 
            // btnRegSaida
            // 
            this.btnRegSaida.Location = new System.Drawing.Point(36, 205);
            this.btnRegSaida.Name = "btnRegSaida";
            this.btnRegSaida.Size = new System.Drawing.Size(99, 23);
            this.btnRegSaida.TabIndex = 10;
            this.btnRegSaida.Text = "Registrar Saída";
            this.btnRegSaida.UseVisualStyleBackColor = true;
            this.btnRegSaida.Click += new System.EventHandler(this.button1_Click);
            this.btnRegSaida.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button1_KeyDown_1);
            this.btnRegSaida.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.button1_KeyPress_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "SAÍDA";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Placa";
            // 
            // txtPlaca
            // 
            this.txtPlaca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlaca.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtPlaca.Location = new System.Drawing.Point(7, 65);
            this.txtPlaca.MaxLength = 7;
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(164, 45);
            this.txtPlaca.TabIndex = 4;
            this.txtPlaca.Click += new System.EventHandler(this.txtPlaca_Click);
            this.txtPlaca.TextChanged += new System.EventHandler(this.txtPlaca_TextChanged);
            this.txtPlaca.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPlaca_KeyDown_1);
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress_1);
            // 
            // btnImprimirSegundaVia
            // 
            this.btnImprimirSegundaVia.Location = new System.Drawing.Point(36, 240);
            this.btnImprimirSegundaVia.Name = "btnImprimirSegundaVia";
            this.btnImprimirSegundaVia.Size = new System.Drawing.Size(99, 23);
            this.btnImprimirSegundaVia.TabIndex = 19;
            this.btnImprimirSegundaVia.Text = "Imprimir 2ª Via";
            this.btnImprimirSegundaVia.UseVisualStyleBackColor = true;
            this.btnImprimirSegundaVia.Visible = false;
            this.btnImprimirSegundaVia.Click += new System.EventHandler(this.btnImprimirSegundaVia_Click);
            // 
            // Saida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.OldLace;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Saida";
            this.Size = new System.Drawing.Size(442, 532);
            this.Load += new System.EventHandler(this.Saida_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Saida_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Saida_KeyPress);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRegSaida;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtPlaca;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbTotal;
        private System.Windows.Forms.Label lbDiarias;
        private System.Windows.Forms.Label lbSaida;
        private System.Windows.Forms.Label lbEntrada;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnImprimirSegundaVia;
    }
}
