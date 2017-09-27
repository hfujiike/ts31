namespace T31
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label_pv = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_in = new System.Windows.Forms.Button();
            this.TB_indir = new System.Windows.Forms.TextBox();
            this.button2_out = new System.Windows.Forms.Button();
            this.TB_buncol = new System.Windows.Forms.TextBox();
            this.TB_bunta = new System.Windows.Forms.TextBox();
            this.TB_hogocol = new System.Windows.Forms.TextBox();
            this.TB_hogopw = new System.Windows.Forms.TextBox();
            this.TB_mes = new System.Windows.Forms.TextBox();
            this.TB_outdir = new System.Windows.Forms.TextBox();
            this.TB_fname = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button_run = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(16, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "エクセルデータ分割処理";
            // 
            // label_pv
            // 
            this.label_pv.AutoSize = true;
            this.label_pv.Font = new System.Drawing.Font("ＭＳ ゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_pv.Location = new System.Drawing.Point(17, 33);
            this.label_pv.Name = "label_pv";
            this.label_pv.Size = new System.Drawing.Size(53, 11);
            this.label_pv.TabIndex = 1;
            this.label_pv.Text = "ｖｖｖｖ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "インプットホルダ/ファイル";
            // 
            // button_in
            // 
            this.button_in.Location = new System.Drawing.Point(143, 63);
            this.button_in.Name = "button_in";
            this.button_in.Size = new System.Drawing.Size(50, 21);
            this.button_in.TabIndex = 3;
            this.button_in.Text = "参照";
            this.button_in.UseVisualStyleBackColor = true;
            this.button_in.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_in_MouseClick);
            // 
            // TB_indir
            // 
            this.TB_indir.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TB_indir.Location = new System.Drawing.Point(199, 65);
            this.TB_indir.Name = "TB_indir";
            this.TB_indir.ReadOnly = true;
            this.TB_indir.Size = new System.Drawing.Size(352, 20);
            this.TB_indir.TabIndex = 4;
            // 
            // button2_out
            // 
            this.button2_out.Location = new System.Drawing.Point(145, 133);
            this.button2_out.Name = "button2_out";
            this.button2_out.Size = new System.Drawing.Size(50, 20);
            this.button2_out.TabIndex = 5;
            this.button2_out.Text = "参照";
            this.button2_out.UseVisualStyleBackColor = true;
            this.button2_out.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_out_MouseClick);
            // 
            // TB_buncol
            // 
            this.TB_buncol.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TB_buncol.Location = new System.Drawing.Point(214, 98);
            this.TB_buncol.Name = "TB_buncol";
            this.TB_buncol.Size = new System.Drawing.Size(59, 20);
            this.TB_buncol.TabIndex = 6;
            // 
            // TB_bunta
            // 
            this.TB_bunta.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TB_bunta.Location = new System.Drawing.Point(372, 98);
            this.TB_bunta.Name = "TB_bunta";
            this.TB_bunta.Size = new System.Drawing.Size(90, 20);
            this.TB_bunta.TabIndex = 7;
            // 
            // TB_hogocol
            // 
            this.TB_hogocol.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TB_hogocol.Location = new System.Drawing.Point(261, 165);
            this.TB_hogocol.Name = "TB_hogocol";
            this.TB_hogocol.Size = new System.Drawing.Size(63, 20);
            this.TB_hogocol.TabIndex = 8;
            // 
            // TB_hogopw
            // 
            this.TB_hogopw.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TB_hogopw.Location = new System.Drawing.Point(431, 165);
            this.TB_hogopw.Name = "TB_hogopw";
            this.TB_hogopw.Size = new System.Drawing.Size(120, 20);
            this.TB_hogopw.TabIndex = 9;
            // 
            // TB_mes
            // 
            this.TB_mes.Location = new System.Drawing.Point(23, 201);
            this.TB_mes.Multiline = true;
            this.TB_mes.Name = "TB_mes";
            this.TB_mes.ReadOnly = true;
            this.TB_mes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TB_mes.Size = new System.Drawing.Size(771, 152);
            this.TB_mes.TabIndex = 10;
            // 
            // TB_outdir
            // 
            this.TB_outdir.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TB_outdir.Location = new System.Drawing.Point(199, 134);
            this.TB_outdir.Name = "TB_outdir";
            this.TB_outdir.ReadOnly = true;
            this.TB_outdir.Size = new System.Drawing.Size(352, 20);
            this.TB_outdir.TabIndex = 11;
            // 
            // TB_fname
            // 
            this.TB_fname.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TB_fname.Location = new System.Drawing.Point(557, 65);
            this.TB_fname.Name = "TB_fname";
            this.TB_fname.ReadOnly = true;
            this.TB_fname.Size = new System.Drawing.Size(190, 20);
            this.TB_fname.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "アウトプットホルダ";
            // 
            // button_run
            // 
            this.button_run.Location = new System.Drawing.Point(744, 28);
            this.button_run.Name = "button_run";
            this.button_run.Size = new System.Drawing.Size(50, 20);
            this.button_run.TabIndex = 14;
            this.button_run.Text = "実行";
            this.button_run.UseVisualStyleBackColor = true;
            this.button_run.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_run_MouseClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(143, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "分割列位置";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(290, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "分割列タイトル";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(143, 171);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "編集可開始列位置";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(347, 171);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "シート保護ＰＷ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 186);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "実行結果";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 365);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button_run);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TB_fname);
            this.Controls.Add(this.TB_outdir);
            this.Controls.Add(this.TB_mes);
            this.Controls.Add(this.TB_hogopw);
            this.Controls.Add(this.TB_hogocol);
            this.Controls.Add(this.TB_bunta);
            this.Controls.Add(this.TB_buncol);
            this.Controls.Add(this.button2_out);
            this.Controls.Add(this.TB_indir);
            this.Controls.Add(this.button_in);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_pv);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Excel分割";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_pv;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_in;
        private System.Windows.Forms.TextBox TB_indir;
        private System.Windows.Forms.Button button2_out;
        private System.Windows.Forms.TextBox TB_buncol;
        private System.Windows.Forms.TextBox TB_bunta;
        private System.Windows.Forms.TextBox TB_hogocol;
        private System.Windows.Forms.TextBox TB_hogopw;
        private System.Windows.Forms.TextBox TB_mes;
        private System.Windows.Forms.TextBox TB_outdir;
        private System.Windows.Forms.TextBox TB_fname;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_run;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}

