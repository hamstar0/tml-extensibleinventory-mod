using HamstarHelpers.Components.Config;
using HamstarHelpers.Components.Errors;
using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.DotNetHelpers;
using HamstarHelpers.Helpers.TmlHelpers;
using HamstarHelpers.Helpers.TmlHelpers.ModHelpers;
using System;
using Terraria;
using Terraria.ModLoader;


namespace ExtensibleInventory {
	partial class ExtensibleInventoryMod : Mod {
		public static ExtensibleInventoryMod Instance { get; private set; }



		////////////////

		public JsonConfig<ExtensibleInventoryConfigData> ConfigJson { get; private set; }
		public ExtensibleInventoryConfigData Config => this.ConfigJson.Data;



		////////////////

		public ExtensibleInventoryMod() {
			this.ConfigJson = new JsonConfig<ExtensibleInventoryConfigData>(
				ExtensibleInventoryConfigData.ConfigFileName,
				ConfigurationDataBase.RelativePath,
				new ExtensibleInventoryConfigData()
			);
		}

		public override void Load() {
			string depErr = TmlHelpers.ReportBadDependencyMods( this );
			if( depErr != null ) { throw new HamstarException( depErr ); }

			ExtensibleInventoryMod.Instance = this;

			this.LoadConfig();

			if( !Main.dedServ ) {
				this.InitializeUI();
			}
		}

		private void LoadConfig() {
			if( !this.ConfigJson.LoadFile() ) {
				this.ConfigJson.SaveFile();
			}

			if( this.Config.CanUpdateVersion() ) {
				this.Config.UpdateToLatestVersion();

				LogHelpers.Log( "Extensible Inventory updated to " + this.Version.ToString() );
				this.ConfigJson.SaveFile();
			}
		}


		public override void Unload() {
			ExtensibleInventoryMod.Instance = null;
		}


		////////////////

		public override object Call( params object[] args ) {
			return ModBoilerplateHelpers.HandleModCall( typeof( ExtensibleInventoryAPI ), args );
		}
	}
}