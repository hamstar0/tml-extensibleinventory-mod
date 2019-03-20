using HamstarHelpers.Components.Config;
using System;


namespace ExtensibleInventory {
	public class ExtensibleInventoryConfigData : ConfigurationDataBase {
		public static string ConfigFileName => "Extensible Inventory Config.json";


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
		public bool CanTogglePageOffloads = true;

		public int MaxPages = 99;

		public bool ScrollModeEnabled = true;



		////////////////

		private static float _1_1_BookPositionY = 256f;
		private static float _1_1_PagePositionY = 256f;


		////////////////

		private void SetDefaults() { }

		////

		public bool CanUpdateVersion() {
			if( this.VersionSinceUpdate == "" ) { return true; }
			var versSince = new Version( this.VersionSinceUpdate );
			return versSince < ExtensibleInventoryMod.Instance.Version;
		}

		public void UpdateToLatestVersion() {
			var mymod = ExtensibleInventoryMod.Instance;
			var newConfig = new ExtensibleInventoryConfigData();
			var versSince = this.VersionSinceUpdate != "" ?
				new Version( this.VersionSinceUpdate ) :
				new Version();

			if( this.VersionSinceUpdate == "" ) {
				this.SetDefaults();
			}

			if( versSince < new Version( 1, 2, 0 ) ) {
				if( this.BookPositionY == ExtensibleInventoryConfigData._1_1_BookPositionY ) {
					this.BookPositionY = newConfig.BookPositionY;
				}
				if( this.PagePositionY == ExtensibleInventoryConfigData._1_1_PagePositionY ) {
					this.PagePositionY = newConfig.PagePositionY;
				}
			}

			this.VersionSinceUpdate = mymod.Version.ToString();
		}
	}
}
