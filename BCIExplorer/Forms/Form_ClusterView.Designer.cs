namespace BCIExplorer.Forms
{
	partial class Form_ClusterView
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
			this.xnaPanel = new SharpDXForms.DXPanel();
			this.SuspendLayout();
			// 
			// xnaPanel
			// 
			this.xnaPanel.BackColor = System.Drawing.Color.CornflowerBlue;
			this.xnaPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xnaPanel.EnableDebugDraw = false;
			this.xnaPanel.Location = new System.Drawing.Point(0, 0);
			this.xnaPanel.Name = "xnaPanel";
			this.xnaPanel.Size = new System.Drawing.Size(292, 273);
			this.xnaPanel.TabIndex = 0;
			this.xnaPanel.Text = "xnaPanel";
			this.xnaPanel.Render += new SharpDXForms.GraphicsDeviceControl.RenderHandler(this.xnaPanel_Render);
			this.xnaPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.xnaPanel_MouseDown);
			this.xnaPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.xnaPanel_MouseMove);
			this.xnaPanel.Resize += new System.EventHandler(this.xnaPanel_Resize);
			// 
			// Form_ClusterView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.xnaPanel);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "Form_ClusterView";
			this.Text = "Cluster View";
			this.ResumeLayout(false);

		}

		#endregion

		private SharpDXForms.DXPanel xnaPanel;
	}
}