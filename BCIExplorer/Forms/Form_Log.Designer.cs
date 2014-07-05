namespace BCIExplorer.Forms
{
	partial class Form_Log {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.textBox_log = new System.Windows.Forms.TextBox();
			this.comboBox_levels = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button_clear = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox_log
			// 
			this.textBox_log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_log.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox_log.Location = new System.Drawing.Point(0, 27);
			this.textBox_log.Multiline = true;
			this.textBox_log.Name = "textBox_log";
			this.textBox_log.ReadOnly = true;
			this.textBox_log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox_log.Size = new System.Drawing.Size(444, 192);
			this.textBox_log.TabIndex = 0;
			this.textBox_log.TabStop = false;
			this.textBox_log.WordWrap = false;
			// 
			// comboBox_levels
			// 
			this.comboBox_levels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox_levels.FormattingEnabled = true;
			this.comboBox_levels.Location = new System.Drawing.Point(94, 3);
			this.comboBox_levels.Name = "comboBox_levels";
			this.comboBox_levels.Size = new System.Drawing.Size(275, 21);
			this.comboBox_levels.TabIndex = 1;
			this.comboBox_levels.SelectedIndexChanged += new System.EventHandler(this.comboBox_levels_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Output level";
			// 
			// button_clear
			// 
			this.button_clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button_clear.Location = new System.Drawing.Point(375, 3);
			this.button_clear.Name = "button_clear";
			this.button_clear.Size = new System.Drawing.Size(69, 21);
			this.button_clear.TabIndex = 3;
			this.button_clear.Text = "Clear";
			this.button_clear.UseVisualStyleBackColor = true;
			this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
			// 
			// Form_Log
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(444, 219);
			this.Controls.Add(this.button_clear);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBox_levels);
			this.Controls.Add(this.textBox_log);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Form_Log";
			this.Text = "Log";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox_log;
		private System.Windows.Forms.ComboBox comboBox_levels;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button_clear;
	}
}