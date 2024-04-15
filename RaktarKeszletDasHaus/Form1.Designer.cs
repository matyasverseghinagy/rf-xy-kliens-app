namespace RaktarKeszletDasHaus
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            dataGridView1 = new DataGridView();
            panel1 = new Panel();
            panel2 = new Panel();
            comboBox1 = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            textBox2 = new TextBox();
            label5 = new Label();
            textBox3 = new TextBox();
            label6 = new Label();
            textBox4 = new TextBox();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            termekNevL = new Label();
            kategoriaNevL = new Label();
            skuNevL = new Label();
            arNevL = new Label();
            bvinNevL = new Label();
            panel3 = new Panel();
            panel4 = new Panel();
            label17 = new Label();
            toolTip1 = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(269, 127);
            dataGridView1.Margin = new Padding(0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(703, 560);
            dataGridView1.TabIndex = 4;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(252, 163, 17);
            panel1.Controls.Add(panel2);
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(269, 127);
            panel1.TabIndex = 6;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.BackgroundImage = (Image)resources.GetObject("panel2.BackgroundImage");
            panel2.BackgroundImageLayout = ImageLayout.Zoom;
            panel2.Location = new Point(12, 9);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(243, 106);
            panel2.TabIndex = 0;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(22, 179);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(233, 23);
            comboBox1.TabIndex = 7;
            comboBox1.SelectionChangeCommitted += comboBox1_SelectionChangeCommitted;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(252, 163, 17);
            label1.Location = new Point(12, 144);
            label1.Name = "label1";
            label1.Size = new Size(115, 32);
            label1.TabIndex = 8;
            label1.Text = "Kategória";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.FromArgb(252, 163, 17);
            label2.Location = new Point(12, 223);
            label2.Name = "label2";
            label2.Size = new Size(131, 32);
            label2.TabIndex = 9;
            label2.Text = "Terméknév";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(22, 258);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(233, 23);
            textBox1.TabIndex = 10;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.FromArgb(252, 163, 17);
            label3.Location = new Point(12, 297);
            label3.Name = "label3";
            label3.Size = new Size(57, 32);
            label3.TabIndex = 11;
            label3.Text = "SKU";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(22, 332);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(233, 23);
            textBox2.TabIndex = 12;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = Color.FromArgb(252, 163, 17);
            label5.Location = new Point(12, 421);
            label5.Name = "label5";
            label5.Size = new Size(142, 32);
            label5.TabIndex = 13;
            label5.Text = "Bolti készlet";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(22, 456);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(233, 23);
            textBox3.TabIndex = 14;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label6.ForeColor = Color.FromArgb(252, 163, 17);
            label6.Location = new Point(12, 542);
            label6.Name = "label6";
            label6.Size = new Size(165, 32);
            label6.TabIndex = 15;
            label6.Text = "Online készlet";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(22, 577);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(233, 23);
            textBox4.TabIndex = 16;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(22, 485);
            button2.Name = "button2";
            button2.Size = new Size(112, 33);
            button2.TabIndex = 17;
            button2.Text = "-";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button3.Location = new Point(143, 485);
            button3.Name = "button3";
            button3.Size = new Size(112, 33);
            button3.TabIndex = 18;
            button3.Text = "+";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button4.Location = new Point(143, 606);
            button4.Name = "button4";
            button4.Size = new Size(112, 33);
            button4.TabIndex = 20;
            button4.Text = "+";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button5.Location = new Point(22, 606);
            button5.Name = "button5";
            button5.Size = new Size(112, 33);
            button5.TabIndex = 19;
            button5.Text = "-";
            button5.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label7.ForeColor = Color.FromArgb(252, 163, 17);
            label7.Location = new Point(282, 9);
            label7.Name = "label7";
            label7.Size = new Size(83, 20);
            label7.TabIndex = 21;
            label7.Text = "Terméknév:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label8.ForeColor = Color.FromArgb(252, 163, 17);
            label8.Location = new Point(282, 29);
            label8.Name = "label8";
            label8.Size = new Size(77, 20);
            label8.TabIndex = 22;
            label8.Text = "Kategória:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label9.ForeColor = Color.FromArgb(252, 163, 17);
            label9.Location = new Point(282, 49);
            label9.Name = "label9";
            label9.Size = new Size(39, 20);
            label9.TabIndex = 23;
            label9.Text = "SKU:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.Transparent;
            label10.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label10.ForeColor = Color.FromArgb(252, 163, 17);
            label10.Location = new Point(282, 69);
            label10.Name = "label10";
            label10.Size = new Size(27, 20);
            label10.TabIndex = 24;
            label10.Text = "Ár:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = Color.Transparent;
            label11.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label11.ForeColor = Color.FromArgb(252, 163, 17);
            label11.Location = new Point(282, 95);
            label11.Name = "label11";
            label11.Size = new Size(40, 20);
            label11.TabIndex = 25;
            label11.Text = "Bvin:";
            // 
            // termekNevL
            // 
            termekNevL.BackColor = Color.Transparent;
            termekNevL.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            termekNevL.ForeColor = Color.White;
            termekNevL.Location = new Point(371, 9);
            termekNevL.Name = "termekNevL";
            termekNevL.Size = new Size(589, 20);
            termekNevL.TabIndex = 26;
            termekNevL.Text = "nincs termék kiválasztva";
            termekNevL.TextAlign = ContentAlignment.TopRight;
            // 
            // kategoriaNevL
            // 
            kategoriaNevL.BackColor = Color.Transparent;
            kategoriaNevL.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            kategoriaNevL.ForeColor = Color.White;
            kategoriaNevL.Location = new Point(371, 29);
            kategoriaNevL.Name = "kategoriaNevL";
            kategoriaNevL.Size = new Size(589, 20);
            kategoriaNevL.TabIndex = 27;
            kategoriaNevL.Text = "nincs termék kiválasztva";
            kategoriaNevL.TextAlign = ContentAlignment.TopRight;
            // 
            // skuNevL
            // 
            skuNevL.BackColor = Color.Transparent;
            skuNevL.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            skuNevL.ForeColor = Color.White;
            skuNevL.Location = new Point(371, 49);
            skuNevL.Name = "skuNevL";
            skuNevL.Size = new Size(589, 20);
            skuNevL.TabIndex = 28;
            skuNevL.Text = "nincs termék kiválasztva";
            skuNevL.TextAlign = ContentAlignment.TopRight;
            // 
            // arNevL
            // 
            arNevL.BackColor = Color.Transparent;
            arNevL.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            arNevL.ForeColor = Color.White;
            arNevL.Location = new Point(371, 69);
            arNevL.Name = "arNevL";
            arNevL.Size = new Size(589, 20);
            arNevL.TabIndex = 29;
            arNevL.Text = "nincs termék kiválasztva";
            arNevL.TextAlign = ContentAlignment.TopRight;
            // 
            // bvinNevL
            // 
            bvinNevL.BackColor = Color.Transparent;
            bvinNevL.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            bvinNevL.ForeColor = Color.White;
            bvinNevL.Location = new Point(371, 95);
            bvinNevL.Name = "bvinNevL";
            bvinNevL.Size = new Size(589, 20);
            bvinNevL.TabIndex = 30;
            bvinNevL.Text = "nincs termék kiválasztva";
            bvinNevL.TextAlign = ContentAlignment.TopRight;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(252, 163, 17);
            panel3.Controls.Add(panel4);
            panel3.Controls.Add(label17);
            panel3.Location = new Point(0, 687);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new Size(972, 23);
            panel3.TabIndex = 7;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Transparent;
            panel4.BackgroundImage = Properties.Resources.sync;
            panel4.BackgroundImageLayout = ImageLayout.Zoom;
            panel4.Location = new Point(949, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(20, 19);
            panel4.TabIndex = 2;
            panel4.MouseClick += panel4_MouseClick;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.BackColor = Color.Transparent;
            label17.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label17.ForeColor = Color.FromArgb(20, 33, 61);
            label17.Location = new Point(0, 0);
            label17.Name = "label17";
            label17.Size = new Size(111, 17);
            label17.TabIndex = 1;
            label17.Text = "v0.2 - DasHaus ©";
            label17.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(20, 33, 61);
            ClientSize = new Size(972, 710);
            Controls.Add(panel3);
            Controls.Add(bvinNevL);
            Controls.Add(arNevL);
            Controls.Add(skuNevL);
            Controls.Add(kategoriaNevL);
            Controls.Add(termekNevL);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(button4);
            Controls.Add(button5);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(textBox4);
            Controls.Add(label6);
            Controls.Add(textBox3);
            Controls.Add(label5);
            Controls.Add(textBox2);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(panel1);
            Controls.Add(dataGridView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Raktárkészlet nyilvántartás - DasHaus ©";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dataGridView1;
        private Panel panel1;
        private Panel panel2;
        private ComboBox comboBox1;
        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private Label label3;
        private TextBox textBox2;
        private Label label5;
        private TextBox textBox3;
        private Label label6;
        private TextBox textBox4;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label termekNevL;
        private Label kategoriaNevL;
        private Label skuNevL;
        private Label arNevL;
        private Label bvinNevL;
        private Panel panel3;
        private Label label17;
        private Panel panel4;
        private ToolTip toolTip1;
    }
}
