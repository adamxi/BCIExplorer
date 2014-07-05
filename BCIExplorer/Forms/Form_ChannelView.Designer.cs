namespace BCIExplorer.Forms
{
	partial class Form_ChannelView
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
			this.dataLoader = new System.ComponentModel.BackgroundWorker();
			this.SuspendLayout();
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
			this.xnaPanel.Size = new System.Drawing.Size(292, 273);
			this.xnaPanel.TabIndex = 0;
			this.xnaPanel.Text = "xnaPanel";
			this.xnaPanel.Render += new SharpDXForms.GraphicsDeviceControl.RenderHandler(this.xnaPanel_Render);
			this.xnaPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.xnaPanel_MouseDown);
			this.xnaPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.xnaPanel_MouseMove);
			this.xnaPanel.Resize += new System.EventHandler(this.xnaPanel_Resize);
			// 
			// dataLoader
			// 
			this.dataLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.dataLoader_DoWork);
			// 
			// Form_ChannelView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.xnaPanel);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "Form_ChannelView";
			this.Text = "Channel View";
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form_ChannelView_KeyPress);
			this.Resize += new System.EventHandler(this.Form_ChannelView_Resize);
			this.ResumeLayout(false);

		}

		#endregion

		private SharpDXForms.DXPanel xnaPanel;
		private System.ComponentModel.BackgroundWorker dataLoader;
	}
}