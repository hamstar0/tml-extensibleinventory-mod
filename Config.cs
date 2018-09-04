using HamstarHelpers.Components.Config;
using System;


namespace ExtensibleInventory {
	public class ExtensibleInventoryConfigData : ConfigurationDataBase {
		public readonly static string ConfigFileName = "Extensible Inventory Config.json";


		////////////////

		public string VersionSinceUpdate = "";

		public bool DebugModeInfo = false;
		public bool DebugModeReset = false;

		public bool DefaultBookEnabled = true;

		//public bool HideBookUI = false;
		//public bool HidePageUI = false;
		public bool HidePageTicksUI = false;

		public float BookPositionX = 64f;
		public float BookPositionY = 260f;
		public float PagePositionX = 192f;
		public float PagePositionY = 260f;
		public float PageTicksPositionX = 32f;
		public float PageTicksPositionY = 254f;

		public float ChestOnOffsetX = 0f;
		public float ChestOnOffsetY = 168f;

		public bool CanSwitchBooks = true;
		public bool CanScrollPages = true;
		public bool CanAddPages = true;
		public bool CanDeletePages = true;

		public int MaxPages = 99;



		////////////////

		private static float _1_1_BookPositionY = 256f;
		private static float _1_1_PagePositionY = 256f;


		////////////////

		public bool UpdateToLatestVersion() {
			var new_config = new ExtensibleInventoryConfigData();
			var vers_since = this.VersionSinceUpdate != "" ?
				new Version( this.VersionSinceUpdate ) :
				new Version();

			if( vers_since >= ExtensibleInventoryMod.Instance.Version ) {
				return false;
			}

			if( vers_since < new Version( 1, 2, 0 ) ) {
				if( this.BookPositionY == ExtensibleInventoryConfigData._1_1_BookPositionY ) {
					this.BookPositionY = new_config.BookPositionY;
				}
				if( this.PagePositionY == ExtensibleInventoryConfigData._1_1_PagePositionY ) {
					this.PagePositionY = new_config.PagePositionY;
				}
			}

			this.VersionSinceUpdate = ExtensibleInventoryMod.Instance.Version.ToString();

			return true;
		}
	}
}
