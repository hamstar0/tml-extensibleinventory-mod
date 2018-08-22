using HamstarHelpers.Components.Config;
using System;


namespace ExtensibleInventory {
	public class ExtensibleInventoryConfigData : ConfigurationDataBase {
		public readonly static Version ConfigVersion = new Version( 1, 1, 0 );
		public readonly static string ConfigFileName = "Extensible Inventory Config.json";


		////////////////

		public string VersionSinceUpdate = "";

		public bool DebugModeInfo = false;
		public bool DebugModeReset = false;

		public bool DefaultBookEnabled = true;

		public float BookPositionX = 64f;
		public float BookPositionY = 256f;
		public float PagePositionX = 192f;
		public float PagePositionY = 256f;
		public float ChestOnOffsetX = 0f;
		public float ChestOnOffsetY = 168f;

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
