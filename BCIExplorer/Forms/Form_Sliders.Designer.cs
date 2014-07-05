namespace BCIExplorer.Forms
{
	partial class Form_Sliders
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
			this.trackBar_distance = new System.Windows.Forms.TrackBar();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label_currentClusterCount = new System.Windows.Forms.Label();
			this.label_distanceLevel = new System.Windows.Forms.Label();
			this.label_maxClusterCount = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label_maxDistance = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label_sigmaValue = new System.Windows.Forms.Label();
			this.trackBar_sigma = new System.Windows.Forms.TrackBar();
			this.button_runClustering = new System.Windows.Forms.Button();
			this.button_runFiltering = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.trackBar_distance)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar_sigma)).BeginInit();
			this.SuspendLayout();
			// 
			// trackBar_distance
			// 
			this.trackBar_distance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBar_distance.LargeChange = 1;
			this.trackBar_distance.Location = new System.Drawing.Point(6, 45);
			this.trackBar_distance.Maximum = 0;
			this.trackBar_distance.Name = "trackBar_distance";
			this.trackBar_distance.Size = new System.Drawing.Size(624, 42);
			this.trackBar_distance.TabIndex = 1;
			this.trackBar_distance.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.trackBar_distance.ValueChanged += new System.EventHandler(this.trackBar_distance_ValueChanged);
			this.trackBar_distance.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackBar_clusters_MouseDown);
			this.trackBar_distance.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar_clusters_MouseUp);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.label_currentClusterCount);
			this.groupBox1.Controls.Add(this.label_distanceLevel);
			this.groupBox1.Controls.Add(this.label_maxClusterCount);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label_maxDistance);
			this.groupBox1.Controls.Add(this.trackBar_distance);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(0, -1);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(636, 89);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Clusters";
			// 
			// label_currentClusterCount
			// 
			this.label_currentClusterCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_currentClusterCount.Location = new System.Drawing.Point(269, 30);
			this.label_currentClusterCount.Name = "label_currentClusterCount";
			this.label_currentClusterCount.Size = new System.Drawing.Size(68, 13);
			this.label_currentClusterCount.TabIndex = 10;
			this.label_currentClusterCount.Text = "0";
			this.label_currentClusterCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label_distanceLevel
			// 
			this.label_distanceLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_distanceLevel.Location = new System.Drawing.Point(103, 30);
			this.label_distanceLevel.Name = "label_distanceLevel";
			this.label_distanceLevel.Size = new System.Drawing.Size(68, 13);
			this.label_distanceLevel.TabIndex = 9;
			this.label_distanceLevel.Text = "0";
			this.label_distanceLevel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label_maxClusterCount
			// 
			this.label_maxClusterCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_maxClusterCount.Location = new System.Drawing.Point(269, 17);
			this.label_maxClusterCount.Name = "label_maxClusterCount";
			this.label_maxClusterCount.Size = new System.Drawing.Size(68, 13);
			this.label_maxClusterCount.TabIndex = 8;
			this.label_maxClusterCount.Text = "0";
			this.label_maxClusterCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(179, 30);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(84, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Current Clusters:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(8, 30);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(89, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Current Distance:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(8, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(75, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Max Distance:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(179, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Max Clusters:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_maxDistance
			// 
			this.label_maxDistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_maxDistance.Location = new System.Drawing.Point(103, 17);
			this.label_maxDistance.Name = "label_maxDistance";
			this.label_maxDistance.Size = new System.Drawing.Size(68, 13);
			this.label_maxDistance.TabIndex = 3;
			this.label_maxDistance.Text = "0";
			this.label_maxDistance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.label_sigmaValue);
			this.groupBox2.Controls.Add(this.trackBar_sigma);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(0, 94);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(636, 65);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Kernel Variance";
			// 
			// label_sigmaValue
			// 
			this.label_sigmaValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label_sigmaValue.BackColor = System.Drawing.Color.Transparent;
			this.label_sigmaValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_sigmaValue.Location = new System.Drawing.Point(0, 7);
			this.label_sigmaValue.Name = "label_sigmaValue";
			this.label_sigmaValue.Size = new System.Drawing.Size(636, 18);
			this.label_sigmaValue.TabIndex = 2;
			this.label_sigmaValue.Text = "label1";
			this.label_sigmaValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// trackBar_sigma
			// 
			this.trackBar_sigma.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBar_sigma.LargeChange = 1;
			this.trackBar_sigma.Location = new System.Drawing.Point(6, 20);
			this.trackBar_sigma.Maximum = 100;
			this.trackBar_sigma.Name = "trackBar_sigma";
			this.trackBar_sigma.Size = new System.Drawing.Size(624, 42);
			this.trackBar_sigma.TabIndex = 1;
			this.trackBar_sigma.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.trackBar_sigma.Value = 1;
			this.trackBar_sigma.Scroll += new System.EventHandler(this.trackBar_sigma_Scroll);
			this.trackBar_sigma.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackBar_sigma_MouseDown);
			this.trackBar_sigma.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar_sigma_MouseUp);
			// 
			// button_runClustering
			// 
			this.button_runClustering.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button_runClustering.Location = new System.Drawing.Point(538, 162);
			this.button_runClustering.Name = "button_runClustering";
			this.button_runClustering.Size = new System.Drawing.Size(98, 23);
			this.button_runClustering.TabIndex = 4;
			this.button_runClustering.Text = "Run Clustering";
			this.button_runClustering.UseVisualStyleBackColor = true;
			this.button_runClustering.Click += new System.EventHandler(this.button_run_Click);
			// 
			// button_runFiltering
			// 
			this.button_runFiltering.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button_runFiltering.Location = new System.Drawing.Point(434, 162);
			this.button_runFiltering.Name = "button_runFiltering";
			this.button_runFiltering.Size = new System.Drawing.Size(98, 23);
			this.button_runFiltering.TabIndex = 5;
			this.button_runFiltering.Text = "Run Filtering";
			this.button_runFiltering.UseVisualStyleBackColor = true;
			this.button_runFiltering.Click += new System.EventHandler(this.button_runFiltering_Click);
			// 
			// Form_Sliders
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(637, 187);
			this.Controls.Add(this.button_runFiltering);
			this.Controls.Add(this.button_runClustering);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "Form_Sliders";
			this.Text = "Cluster Refinement";
			((System.ComponentModel.ISupportInitialize)(this.trackBar_distance)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar_sigma)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label_maxDistance;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label_sigmaValue;
		public System.Windows.Forms.TrackBar trackBar_distance;
		public System.Windows.Forms.TrackBar trackBar_sigma;
		private System.Windows.Forms.Button button_runClustering;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label_currentClusterCount;
		private System.Windows.Forms.Label label_distanceLevel;
		private System.Windows.Forms.Label label_maxClusterCount;
		private System.Windows.Forms.Button button_runFiltering;

	}
}