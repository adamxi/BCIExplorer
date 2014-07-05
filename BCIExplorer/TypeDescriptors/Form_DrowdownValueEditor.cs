using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace BCIExplorer.TypeDescriptors
{
	internal partial class Form_DrowdownValueEditor : UserControl
	{
		private IWindowsFormsEditorService editorService;
		private object currentValue;
		private bool disableCheckEvent;
		private ListViewItem allItem;
		private ListViewItem noneItem;

		public Form_DrowdownValueEditor()
		{
			InitializeComponent();
		}

		private void Form_DrowdownValueEditor_Resize( object sender, EventArgs e )
		{
			UpdateColumnWidth();
			Invalidate( true );
		}

		private void listview_ItemChecked( object sender, ItemCheckedEventArgs e )
		{
			if( disableCheckEvent )
			{
				return;
			}

			disableCheckEvent = true;
			if( e.Item != null )
			{
				if( e.Item == allItem )
				{
					SetAllItemChecked( allItem.Checked );
				}
				else if( e.Item == noneItem )
				{
					e.Item.Checked = false;
					SetAllItemChecked( false );
				}
				else
				{
					SelectItem( e.Item );
					bool allChecked = true;
					foreach( ListViewItem item in listview.Items )
					{
						if( item == allItem || item == noneItem )
						{
							continue;
						}

						if( !item.Checked )
						{
							allChecked = false;
							break;
						}
					}
					allItem.Checked = allChecked;
					SelectItem( allItem );
				}
			}
			disableCheckEvent = false;
		}

		private void SetAllItemChecked( bool isChecked )
		{
			foreach( ListViewItem item in listview.Items )
			{
				if( item == noneItem )
				{
					continue;
				}

				item.Checked = isChecked;
				SelectItem( item );
			}
		}

		private void UpdateColumnWidth()
		{
			int offsetX = ( FormEx.GetVisibleScrollbars( listview ) & ScrollBars.Vertical ) == ScrollBars.Vertical ? 20 : 0;
			listview.Columns[ 0 ].Width = Math.Max( 200, listview.Width - offsetX );
		}

		private void SelectItem( ListViewItem item )
		{
			if( item.Checked )
			{
				item.BackColor = Color.FromKnownColor( KnownColor.Highlight );
				item.ForeColor = Color.FromKnownColor( KnownColor.HighlightText );
			}
			else
			{
				item.BackColor = Color.FromKnownColor( KnownColor.Window );
				item.ForeColor = Color.FromKnownColor( KnownColor.WindowText );
			}
		}

		public void PopulateDropdown( ITypeDescriptorContext context, IWindowsFormsEditorService editorService, object[] values, object currentValue, bool hideDescription = true )
		{
			disableCheckEvent = true;

			this.editorService = editorService;
			this.currentValue = currentValue;
			splitContainer.Panel2Collapsed = hideDescription;

			listview.Items.Clear();
			listview.CheckBoxes = true;

			if( listview.CheckBoxes )
			{
				allItem = new ListViewItem( "[ALL]" );
				noneItem = new ListViewItem( "[NONE]" );
				listview.Items.Add( allItem );
				listview.Items.Add( noneItem );
			}

			string[] currentItems = currentValue.ToString().ToLower().Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
			for( int i = 0; i < currentItems.Length; i++ )
			{
				currentItems[ i ] = currentItems[ i ].Trim();
			}

			bool allChecked = true;
			foreach( object obj in values )
			{
				ListViewItem item = new ListViewItem();
				string text = obj.ToString();
				item.Text = text;
				item.Tag = obj;

				if( currentItems.Contains( text.ToLower() ) )
				{
					item.Checked = true;
					SelectItem( item );
				}
				else
				{
					allChecked = false;
				}

				listview.Items.Add( item );
			}

			if( listview.CheckBoxes && allChecked )
			{
				allItem.Checked = true;
				SelectItem( allItem );
			}

			UpdateColumnWidth();
			disableCheckEvent = false;
		}

		public object[] GetSelected()
		{
			List<object> selected = new List<object>();
			foreach( ListViewItem item in listview.Items )
			{
				if( item == allItem || item == noneItem )
				{
					continue;
				}

				if( item.Checked )
				{
					selected.Add( item.Tag );
				}
			}
			return selected.ToArray();
		}
	}
}