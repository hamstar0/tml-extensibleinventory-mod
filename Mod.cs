using HamstarHelpers.Components.Config;
using HamstarHelpers.Components.Errors;
using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.DotNetHelpers;
using HamstarHelpers.Helpers.TmlHelpers;
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
			if( args == null || args.Length == 0 ) { throw new HamstarException( "Undefined call type." ); }

			string callType = args[0] as string;
			if( callType == null ) { throw new HamstarException( "Invalid call type." ); }

			var methodInfo = typeof( ExtensibleInventoryAPI ).GetMethod( callType );
			if( methodInfo == null ) { throw new HamstarException( "Invalid call type " + callType ); }

			var newArgs = new object[args.Length - 1];
			Array.Copy( args, 1, newArgs, 0, args.Length - 1 );

			try {
				return ReflectionHelpers.SafeCall( methodInfo, null, newArgs );
			} catch( Exception e ) {
				throw new HamstarException( "Bad API call.", e );
			}
		}
	}
}
