namespace BCIExplorer.TypeDescriptors
{
  partial class Form_DrowdownValueEditor
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "item"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Transparent, null);
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.listview = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label_description = new System.Windows.Forms.Label();
			this.label_caption = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer.Location = new System.Drawing.Point(0, 0);
			this.splitContainer.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainer.Name = "splitContainer";
			this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.listview);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.label_description);
			this.splitContainer.Panel2.Controls.Add(this.label_caption);
			this.splitContainer.Size = new System.Drawing.Size(218, 277);
			this.splitContainer.SplitterDistance = 148;
			this.splitContainer.SplitterWidth = 6;
			this.splitContainer.TabIndex = 1;
			// 
			// listview
			// 
			this.listview.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.listview.Alignment = System.Windows.Forms.ListViewAlignment.Left;
			this.listview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listview.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listview.CheckBoxes = true;
			this.listview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.listview.FullRowSelect = true;
			this.listview.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			listViewItem1.StateImageIndex = 0;
			this.listview.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
			this.listview.Location = new System.Drawing.Point(0, 0);
			this.listview.Margin = new System.Windows.Forms.Padding(0);
			this.listview.MultiSelect = false;
			this.listview.Name = "listview";
			this.listview.RightToLeftLayout = true;
			this.listview.ShowGroups = false;
			this.listview.Size = new System.Drawing.Size(218, 148);
			this.listview.TabIndex = 0;
			this.listview.UseCompatibleStateImageBehavior = false;
			this.listview.View = System.Windows.Forms.View.Details;
			this.listview.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listview_ItemChecked);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 200;
			// 
			// label_description
			// 
			this.label_description.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label_description.Location = new System.Drawing.Point(-1, 18);
			this.label_description.Name = "label_description";
			this.label_description.Size = new System.Drawing.Size(219, 105);
			this.label_description.TabIndex = 1;
			// 
			// label_caption
			// 
			this.label_caption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label_caption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_caption.Location = new System.Drawing.Point(-1, 0);
			this.label_caption.Name = "label_caption";
			this.label_caption.Size = new System.Drawing.Size(219, 18);
			this.label_caption.TabIndex = 0;
			// 
			// Form_DrowdownValueEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer);
			this.DoubleBuffered = true;
			this.Name = "Form_DrowdownValueEditor";
			this.Padding = new System.Windows.Forms.Padding(3);
			this.Size = new System.Drawing.Size(218, 277);
			this.Resize += new System.EventHandler(this.Form_DrowdownValueEditor_Resize);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer;
    private System.Windows.Forms.Label label_caption;
    private System.Windows.Forms.Label label_description;
    private System.Windows.Forms.ListView listview;
    private System.Windows.Forms.ColumnHeader columnHeader1;

  }
}
