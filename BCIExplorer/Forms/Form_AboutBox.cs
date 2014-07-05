﻿using System;
using System.Reflection;
using System.Windows.Forms;

namespace BCIExplorer
{
	partial class Form_AboutBox : Form
	{
		public Form_AboutBox()
		{
			InitializeComponent();
			this.Text = String.Format( "About {0}", AssemblyTitle );
			this.labelProductName.Text = AssemblyProduct;
			this.labelVersion.Text = String.Format( "Version {0}", AssemblyVersion );
			this.labelCopyright.Text = AssemblyCopyright;
			this.labelCompanyName.Text = AssemblyCompany;
			this.textBoxDescription.Text = AssemblyDescription;
		}

		#region Assembly Attribute Accessors

		public static string AssemblyTitle
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes( typeof( AssemblyTitleAttribute ), false );
				if( attributes.Length > 0 )
				{
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[ 0 ];
					if( titleAttribute.Title != "" )
					{
						return titleAttribute.Title;
					}
				}
				return System.IO.Path.GetFileNameWithoutExtension( Assembly.GetExecutingAssembly().CodeBase );
			}
		}

		public static string AssemblyVersion
		{
			get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
		}

		public string AssemblyDescription
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes( typeof( AssemblyDescriptionAttribute ), false );
				if( attributes.Length == 0 )
				{
					return "";
				}
				return ( (AssemblyDescriptionAttribute)attributes[ 0 ] ).Description;
			}
		}

		public string AssemblyProduct
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes( typeof( AssemblyProductAttribute ), false );
				if( attributes.Length == 0 )
				{
					return "";
				}
				return ( (AssemblyProductAttribute)attributes[ 0 ] ).Product;
			}
		}

		public string AssemblyCopyright
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes( typeof( AssemblyCopyrightAttribute ), false );
				if( attributes.Length == 0 )
				{
					return "";
				}
				return ( (AssemblyCopyrightAttribute)attributes[ 0 ] ).Copyright;
			}
		}

		public string AssemblyCompany
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes( typeof( AssemblyCompanyAttribute ), false );
				if( attributes.Length == 0 )
				{
					return "";
				}
				return ( (AssemblyCompanyAttribute)attributes[ 0 ] ).Company;
			}
		}
		#endregion
	}
}
