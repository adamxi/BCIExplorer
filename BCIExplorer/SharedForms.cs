using System.Collections.Generic;
using BCIExplorer.Forms;

namespace BCIExplorer
{
	public class SharedForms
	{
		public static Form1 main;
		public static Form_ChannelView channelView;
		public static Form_ClusterView clusterView;
		public static Form_Control control;
		public static Form_Sliders sliders;
		public static Form_Log log;
		public static List<object> dockingForms = new List<object>();

		public SharedForms()
		{
			channelView = new Form_ChannelView();
			clusterView = new Form_ClusterView();
			control = new Form_Control();
			sliders = new Form_Sliders();
			log = new Form_Log();
			dockingForms.Add( log );
			dockingForms.Add( sliders );
			dockingForms.Add( channelView );
			dockingForms.Add( clusterView );
			dockingForms.Add( control );
		}
	}
}