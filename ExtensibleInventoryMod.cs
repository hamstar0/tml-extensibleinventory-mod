using HamstarHelpers.Components.Config;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;


namespace ExtensibleInventory {
	partial class ExtensibleInventoryMod : Mod {
		public static ExtensibleInventoryMod Instance { get; private set; }



		////////////////

		public JsonConfig<ExtensibleInventoryConfigData> ConfigJson { get; private set; }
		public ExtensibleInventoryConfigData Config { get { return this.ConfigJson.Data; } }



		////////////////

		public ExtensibleInventoryMod() {
			this.Properties = new ModProperties() {
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
			
			this.ConfigJson = new JsonConfig<ExtensibleInventoryConfigData>( ExtensibleInventoryConfigData.ConfigFileName,
				ConfigurationDataBase.RelativePath,
				new ExtensibleInventoryConfigData() );
		}

		public override void Load() {
			ExtensibleInventoryMod.Instance = this;

			if( !Main.dedServ ) {
				this.InitializeUI();
			}

			this.LoadConfig();
		}

		private void LoadConfig() {
			if( !this.ConfigJson.LoadFile() ) {
				this.ConfigJson.SaveFile();
			}

			if( this.Config.UpdateToLatestVersion() ) {
				LogHelpers.Log( "Extensible Inventory updated to " + ExtensibleInventoryConfigData.ConfigVersion.ToString() );
				this.ConfigJson.SaveFile();
			}
		}


		public override void Unload() {
			ExtensibleInventoryMod.Instance = null;

			ExtensibleInventoryMod.ButtonRight = null;
			ExtensibleInventoryMod.ButtonLeft = null;
		}


		////////////////

		public override object Call( params object[] args ) {
			if( args.Length == 0 ) { throw new Exception( "Undefined call type." ); }

			string call_type = args[0] as string;
			if( args == null ) { throw new Exception( "Invalid call type." ); }

			var new_args = new object[args.Length - 1];
			Array.Copy( args, 1, new_args, 0, args.Length - 1 );

			return ExtensibleInventoryAPI.Call( call_type, new_args );
		}
	}
}
