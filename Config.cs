using HamstarHelpers.Components.Config;
using System;


namespace ExtensibleInventory {
	public class ExtensibleInventoryConfigData : ConfigurationDataBase {
		public readonly static Version ConfigVersion = new Version( 1, 0, 0 );
		public readonly static string ConfigFileName = "Extensible Inventory Config.json";


		////////////////

		public string VersionSinceUpdate = "";

		public bool DebugInfoMode = false;

		public float OffsetX = 192f;
		public float OffsetY = 256f;

		public bool CanScrollPages = true;
		public bool CanAddPages = true;
		public bool CanDeletePages = true;

		public int MaxPages = 99;



		////////////////

		public bool UpdateToLatestVersion() {
			var new_config = new ExtensibleInventoryConfigData();
			var vers_since = this.VersionSinceUpdate != "" ?
				new Version( this.VersionSinceUpdate ) :
				new Version();

			if( vers_since >= ExtensibleInventoryConfigData.ConfigVersion ) {
				return false;
			}

			this.VersionSinceUpdate = ExtensibleInventoryConfigData.ConfigVersion.ToString();

			return true;
		}
	}
}
