using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace BCIExplorer.TypeDescriptors
{
	public class DropdownCheckboxEditor : UITypeEditor
	{
		private Form_DrowdownValueEditor form;

		public DropdownCheckboxEditor()
		{
			form = new Form_DrowdownValueEditor();
		}

		public override bool IsDropDownResizable
		{
			get { return true; }
		}

		public override UITypeEditorEditStyle GetEditStyle( ITypeDescriptorContext context )
		{
			return UITypeEditorEditStyle.DropDown;
		}

		public override bool GetPaintValueSupported( ITypeDescriptorContext context )
		{
			return false;
		}

		public override object EditValue( ITypeDescriptorContext context, IServiceProvider provider, object value )
		{
			if( provider == null )
			{
				return value;
			}

			IWindowsFormsEditorService editorService = provider.GetService( typeof( IWindowsFormsEditorService ) ) as IWindowsFormsEditorService;
			if( editorService == null )
			{
				return value;
			}

			int count = Project.LoadedFile.Header.Signals.Count;
			object[] data = new object[ count ];

			for( int i = 0; i < count; i++ )
			{
				data[ i ] = Project.LoadedFile.Header.Signals[ i ].IndexNumberWithLabel;
			}

			form.PopulateDropdown( context, editorService, data, value );
			editorService.DropDownControl( form );
			return string.Join( ", ", form.GetSelected() );
		}
	}
}