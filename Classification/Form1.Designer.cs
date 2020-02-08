namespace Classification
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.load_train = new System.Windows.Forms.Button();
            this.concept = new System.Windows.Forms.TextBox();
            this.testconcept = new System.Windows.Forms.TextBox();
            this.load_test = new System.Windows.Forms.Button();
            this.train = new System.Windows.Forms.Button();
            this.test = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.ex_rules = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.acc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.per = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.re = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nsupp = new System.Windows.Forms.TextBox();
            this.lb4 = new System.Windows.Forms.Label();
            this.lb3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.psupp = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.npconf = new System.Windows.Forms.TextBox();
            this.nconf = new System.Windows.Forms.TextBox();
            this.lb2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lb1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ppconf = new System.Windows.Forms.TextBox();
            this.pconf = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.keyword_txt = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // load_train
            // 
            this.load_train.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.load_train.Location = new System.Drawing.Point(19, 71);
            this.load_train.Name = "load_train";
            this.load_train.Size = new System.Drawing.Size(115, 37);
            this.load_train.TabIndex = 0;
            this.load_train.Text = "Load train data";
            this.load_train.UseVisualStyleBackColor = true;
            this.load_train.Click += new System.EventHandler(this.load_train_button);
            // 
            // concept
            // 
            this.concept.AllowDrop = true;
            this.concept.Location = new System.Drawing.Point(154, 80);
            this.concept.Name = "concept";
            this.concept.Size = new System.Drawing.Size(324, 20);
            this.concept.TabIndex = 1;
            this.concept.DragDrop += new System.Windows.Forms.DragEventHandler(this.sslCertField_DragDrop);
            this.concept.DragEnter += new System.Windows.Forms.DragEventHandler(this.sslCertField_DragEnter_concept);
            // 
            // testconcept
            // 
            this.testconcept.AllowDrop = true;
            this.testconcept.Location = new System.Drawing.Point(154, 123);
            this.testconcept.Name = "testconcept";
            this.testconcept.Size = new System.Drawing.Size(324, 20);
            this.testconcept.TabIndex = 3;
            this.testconcept.DragDrop += new System.Windows.Forms.DragEventHandler(this.sslCertField_DragDrop);
            this.testconcept.DragEnter += new System.Windows.Forms.DragEventHandler(this.sslCertField_DragEnter_tconcept);
            // 
            // load_test
            // 
            this.load_test.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.load_test.Location = new System.Drawing.Point(19, 114);
            this.load_test.Name = "load_test";
            this.load_test.Size = new System.Drawing.Size(115, 36);
            this.load_test.TabIndex = 2;
            this.load_test.Text = "Load test data";
            this.load_test.UseVisualStyleBackColor = true;
            this.load_test.Click += new System.EventHandler(this.load_test_button);
            // 
            // train
            // 
            this.train.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.train.Location = new System.Drawing.Point(122, 49);
            this.train.Name = "train";
            this.train.Size = new System.Drawing.Size(108, 42);
            this.train.TabIndex = 4;
            this.train.Text = "Train";
            this.train.UseVisualStyleBackColor = true;
            this.train.Click += new System.EventHandler(this.train_button);
            // 
            // test
            // 
            this.test.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.test.Location = new System.Drawing.Point(343, 49);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(108, 42);
            this.test.TabIndex = 5;
            this.test.Text = "Test";
            this.test.UseVisualStyleBackColor = true;
            this.test.Click += new System.EventHandler(this.test_button);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.keyword_txt);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.testconcept);
            this.panel1.Controls.Add(this.concept);
            this.panel1.Controls.Add(this.load_test);
            this.panel1.Controls.Add(this.load_train);
            this.panel1.Location = new System.Drawing.Point(27, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(570, 157);
            this.panel1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(484, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Text File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(484, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Text File";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.train);
            this.panel2.Controls.Add(this.test);
            this.panel2.Location = new System.Drawing.Point(27, 314);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(570, 105);
            this.panel2.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(173, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Algorithm";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(229, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // ex_rules
            // 
            this.ex_rules.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ex_rules.Location = new System.Drawing.Point(17, 30);
            this.ex_rules.Name = "ex_rules";
            this.ex_rules.Size = new System.Drawing.Size(115, 63);
            this.ex_rules.TabIndex = 8;
            this.ex_rules.Text = "Extract Ruls";
            this.ex_rules.UseVisualStyleBackColor = true;
            this.ex_rules.Click += new System.EventHandler(this.ex_rules_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.acc,
            this.TP,
            this.FP,
            this.TN,
            this.FN,
            this.per,
            this.re});
            this.dataGridView1.Location = new System.Drawing.Point(27, 425);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(570, 139);
            this.dataGridView1.TabIndex = 16;
            // 
            // acc
            // 
            this.acc.HeaderText = "Accuracy";
            this.acc.Name = "acc";
            this.acc.Width = 75;
            // 
            // TP
            // 
            this.TP.HeaderText = "True Positive";
            this.TP.Name = "TP";
            this.TP.Width = 75;
            // 
            // FP
            // 
            this.FP.HeaderText = "False Positive";
            this.FP.Name = "FP";
            this.FP.Width = 75;
            // 
            // TN
            // 
            this.TN.HeaderText = "True Negative";
            this.TN.Name = "TN";
            this.TN.Width = 75;
            // 
            // FN
            // 
            this.FN.HeaderText = "False Negative";
            this.FN.Name = "FN";
            this.FN.Width = 75;
            // 
            // per
            // 
            this.per.HeaderText = "Percision";
            this.per.Name = "per";
            this.per.Width = 75;
            // 
            // re
            // 
            this.re.HeaderText = "Recall";
            this.re.Name = "re";
            this.re.Width = 75;
            // 
            // nsupp
            // 
            this.nsupp.Location = new System.Drawing.Point(457, 30);
            this.nsupp.Name = "nsupp";
            this.nsupp.Size = new System.Drawing.Size(47, 20);
            this.nsupp.TabIndex = 38;
            this.nsupp.Text = "5";
            // 
            // lb4
            // 
            this.lb4.AutoSize = true;
            this.lb4.Location = new System.Drawing.Point(399, 85);
            this.lb4.Name = "lb4";
            this.lb4.Size = new System.Drawing.Size(52, 13);
            this.lb4.TabIndex = 37;
            this.lb4.Text = "prun conf";
            // 
            // lb3
            // 
            this.lb3.AutoSize = true;
            this.lb3.Location = new System.Drawing.Point(395, 59);
            this.lb3.Name = "lb3";
            this.lb3.Size = new System.Drawing.Size(60, 13);
            this.lb3.TabIndex = 36;
            this.lb3.Text = "confidence";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(412, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 35;
            this.label9.Text = "support";
            // 
            // psupp
            // 
            this.psupp.Location = new System.Drawing.Point(272, 30);
            this.psupp.Name = "psupp";
            this.psupp.Size = new System.Drawing.Size(47, 20);
            this.psupp.TabIndex = 25;
            this.psupp.Text = "5";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(454, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "Negative";
            // 
            // npconf
            // 
            this.npconf.Location = new System.Drawing.Point(457, 82);
            this.npconf.Name = "npconf";
            this.npconf.Size = new System.Drawing.Size(47, 20);
            this.npconf.TabIndex = 33;
            this.npconf.Text = "0.8";
            // 
            // nconf
            // 
            this.nconf.Location = new System.Drawing.Point(457, 56);
            this.nconf.Name = "nconf";
            this.nconf.Size = new System.Drawing.Size(47, 20);
            this.nconf.TabIndex = 32;
            this.nconf.Text = "0.7";
            // 
            // lb2
            // 
            this.lb2.AutoSize = true;
            this.lb2.Location = new System.Drawing.Point(214, 85);
            this.lb2.Name = "lb2";
            this.lb2.Size = new System.Drawing.Size(52, 13);
            this.lb2.TabIndex = 31;
            this.lb2.Text = "prun conf";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(210, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "confidence";
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.Location = new System.Drawing.Point(227, 33);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(42, 13);
            this.lb1.TabIndex = 29;
            this.lb1.Text = "support";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(269, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Positive";
            // 
            // ppconf
            // 
            this.ppconf.Location = new System.Drawing.Point(272, 82);
            this.ppconf.Name = "ppconf";
            this.ppconf.Size = new System.Drawing.Size(47, 20);
            this.ppconf.TabIndex = 27;
            this.ppconf.Text = "0.8";
            // 
            // pconf
            // 
            this.pconf.Location = new System.Drawing.Point(272, 56);
            this.pconf.Name = "pconf";
            this.pconf.Size = new System.Drawing.Size(47, 20);
            this.pconf.TabIndex = 26;
            this.pconf.Text = "0.7";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button1.Location = new System.Drawing.Point(254, 570);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 36);
            this.button1.TabIndex = 39;
            this.button1.Text = "clear rows";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel3.Controls.Add(this.ex_rules);
            this.panel3.Controls.Add(this.pconf);
            this.panel3.Controls.Add(this.nsupp);
            this.panel3.Controls.Add(this.ppconf);
            this.panel3.Controls.Add(this.lb4);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.lb3);
            this.panel3.Controls.Add(this.lb1);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.psupp);
            this.panel3.Controls.Add(this.lb2);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.nconf);
            this.panel3.Controls.Add(this.npconf);
            this.panel3.Location = new System.Drawing.Point(27, 186);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(570, 122);
            this.panel3.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(484, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Text File";
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.Location = new System.Drawing.Point(154, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(324, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.sslCertField_DragDrop);
            this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.sslCertField_DragEnter_keywords);
            // 
            // keyword_txt
            // 
            this.keyword_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.keyword_txt.Location = new System.Drawing.Point(19, 14);
            this.keyword_txt.Name = "keyword_txt";
            this.keyword_txt.Size = new System.Drawing.Size(115, 37);
            this.keyword_txt.TabIndex = 6;
            this.keyword_txt.Text = "Keywords";
            this.keyword_txt.UseVisualStyleBackColor = true;
            this.keyword_txt.Click += new System.EventHandler(this.keyword_txt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 618);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "ARClassification_mn156_97.4.4";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button load_train;
        private System.Windows.Forms.TextBox concept;
        private System.Windows.Forms.TextBox testconcept;
        private System.Windows.Forms.Button load_test;
        private System.Windows.Forms.Button train;
        private System.Windows.Forms.Button test;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ex_rules;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn acc;
        private System.Windows.Forms.DataGridViewTextBoxColumn TP;
        private System.Windows.Forms.DataGridViewTextBoxColumn FP;
        private System.Windows.Forms.DataGridViewTextBoxColumn TN;
        private System.Windows.Forms.DataGridViewTextBoxColumn FN;
        private System.Windows.Forms.DataGridViewTextBoxColumn per;
        private System.Windows.Forms.DataGridViewTextBoxColumn re;
        private System.Windows.Forms.TextBox nsupp;
        private System.Windows.Forms.Label lb4;
        private System.Windows.Forms.Label lb3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox psupp;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox npconf;
        private System.Windows.Forms.TextBox nconf;
        private System.Windows.Forms.Label lb2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox ppconf;
        private System.Windows.Forms.TextBox pconf;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button keyword_txt;
    }
}

