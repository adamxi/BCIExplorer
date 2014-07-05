namespace BCIExplorer.Chart
{
	partial class ScatterPlot
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button_goTo = new System.Windows.Forms.Button();
			this.checkBox_toggleGrid = new System.Windows.Forms.CheckBox();
			this.checkBox_labels = new System.Windows.Forms.CheckBox();
			this.checkBox_filled = new System.Windows.Forms.CheckBox();
			this.numericUpDown_scale = new System.Windows.Forms.NumericUpDown();
			this.xnaPanel = new SharpDXForms.DXPanel();
			this.button_clear = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scale)).BeginInit();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(0, 507);
			this.textBox1.Margin = new System.Windows.Forms.Padding(1);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(115, 20);
			this.textBox1.TabIndex = 1;
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// button_goTo
			// 
			this.button_goTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button_goTo.Location = new System.Drawing.Point(119, 507);
			this.button_goTo.Name = "button_goTo";
			this.button_goTo.Size = new System.Drawing.Size(70, 20);
			this.button_goTo.TabIndex = 2;
			this.button_goTo.Text = "Goto Label";
			this.button_goTo.UseVisualStyleBackColor = true;
			this.button_goTo.Click += new System.EventHandler(this.button_goTo_Click);
			// 
			// checkBox_toggleGrid
			// 
			this.checkBox_toggleGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBox_toggleGrid.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBox_toggleGrid.Checked = true;
			this.checkBox_toggleGrid.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_toggleGrid.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBox_toggleGrid.Location = new System.Drawing.Point(439, 507);
			this.checkBox_toggleGrid.Name = "checkBox_toggleGrid";
			this.checkBox_toggleGrid.Size = new System.Drawing.Size(70, 20);
			this.checkBox_toggleGrid.TabIndex = 3;
			this.checkBox_toggleGrid.Text = "Show Grid";
			this.checkBox_toggleGrid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBox_toggleGrid.UseVisualStyleBackColor = true;
			this.checkBox_toggleGrid.CheckedChanged += new System.EventHandler(this.checkBox_toggleGrid_CheckedChanged);
			// 
			// checkBox_labels
			// 
			this.checkBox_labels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBox_labels.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBox_labels.Checked = true;
			this.checkBox_labels.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_labels.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBox_labels.Location = new System.Drawing.Point(363, 507);
			this.checkBox_labels.Name = "checkBox_labels";
			this.checkBox_labels.Size = new System.Drawing.Size(70, 20);
			this.checkBox_labels.TabIndex = 4;
			this.checkBox_labels.Text = "Show Labels";
			this.checkBox_labels.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBox_labels.UseVisualStyleBackColor = true;
			this.checkBox_labels.CheckedChanged += new System.EventHandler(this.checkBox_labels_CheckedChanged);
			// 
			// checkBox_filled
			// 
			this.checkBox_filled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBox_filled.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBox_filled.Checked = true;
			this.checkBox_filled.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_filled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBox_filled.Location = new System.Drawing.Point(287, 507);
			this.checkBox_filled.Name = "checkBox_filled";
			this.checkBox_filled.Size = new System.Drawing.Size(70, 20);
			this.checkBox_filled.TabIndex = 5;
			this.checkBox_filled.Text = "Filled Points";
			this.checkBox_filled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBox_filled.UseVisualStyleBackColor = true;
			this.checkBox_filled.CheckedChanged += new System.EventHandler(this.checkBox_filled_CheckedChanged);
			// 
			// numericUpDown_scale
			// 
			this.numericUpDown_scale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDown_scale.Location = new System.Drawing.Point(515, 507);
			this.numericUpDown_scale.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.numericUpDown_scale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown_scale.Name = "numericUpDown_scale";
			this.numericUpDown_scale.Size = new System.Drawing.Size(49, 20);
			this.numericUpDown_scale.TabIndex = 7;
			this.numericUpDown_scale.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numericUpDown_scale.ValueChanged += new System.EventHandler(this.numericUpDown_scale_ValueChanged);
			// 
			// xnaPanel
			// 
			this.xnaPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.xnaPanel.BackColor = System.Drawing.Color.CornflowerBlue;
			this.xnaPanel.EnableDebugDraw = false;
			this.xnaPanel.Location = new System.Drawing.Point(0, 0);
			this.xnaPanel.Name = "xnaPanel";
			this.xnaPanel.Size = new System.Drawing.Size(564, 503);
			this.xnaPanel.TabIndex = 0;
			this.xnaPanel.Text = "xnaPanel";
			this.xnaPanel.Render += new SharpDXForms.GraphicsDeviceControl.RenderHandler(this.xnaPanel_Render);
			this.xnaPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.xnaPanel_MouseDown);
			this.xnaPanel.MouseEnter += new System.EventHandler(this.xnaPanel_MouseEnter);
			this.xnaPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.xnaPanel_MouseMove);
			this.xnaPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.xnaPanel_MouseUp);
			this.xnaPanel.Resize += new System.EventHandler(this.xnaPanel_Resize);
			// 
			// button_clear
			// 
			this.button_clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button_clear.Location = new System.Drawing.Point(195, 507);
			this.button_clear.Name = "button_clear";
			this.button_clear.Size = new System.Drawing.Size(86, 20);
			this.button_clear.TabIndex = 8;
			this.button_clear.Text = "Clear Drawings";
			this.button_clear.UseVisualStyleBackColor = true;
			this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
			// 
			// ScatterPlot
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(564, 527);
			this.Controls.Add(this.button_clear);
			this.Controls.Add(this.numericUpDown_scale);
			this.Controls.Add(this.checkBox_filled);
			this.Controls.Add(this.checkBox_labels);
			this.Controls.Add(this.checkBox_toggleGrid);
			this.Controls.Add(this.button_goTo);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.xnaPanel);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "ScatterPlot";
			this.Text = "ScatterPlot";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ScatterPlot_FormClosed);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ScatterPlot_KeyPress);
			this.Resize += new System.EventHandler(this.ScatterPlot_Resize);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scale)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private SharpDXForms.DXPanel xnaPanel;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button_goTo;
		private System.Windows.Forms.CheckBox checkBox_toggleGrid;
		private System.Windows.Forms.CheckBox checkBox_labels;
		private System.Windows.Forms.CheckBox checkBox_filled;
		private System.Windows.Forms.NumericUpDown numericUpDown_scale;
		private System.Windows.Forms.Button button_clear;
	}
}