using HamstarHelpers.Components.Config;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;


namespace ExtensibleInventory {
	partial class ExtensibleInventoryMod : Mod {
		public static string GithubUserName { get { return "hamstar0"; } }
		public static string GithubProjectName { get { return "tml-extensibleinventory-mod"; } }

		public static string ConfigFileRelativePath {
			get { return ConfigurationDataBase.RelativePath + Path.DirectorySeparatorChar + ExtensibleInventoryConfigData.ConfigFileName; }
		}
		public static void ReloadConfigFromFile() {
			if( Main.netMode != 0 ) {
				throw new Exception( "Cannot reload configs outside of single player." );
			}
			if( ExtensibleInventoryMod.Instance != null ) {
				if( !ExtensibleInventoryMod.Instance.ConfigJson.LoadFile() ) {
					ExtensibleInventoryMod.Instance.ConfigJson.SaveFile();
				}
			}
		}

		public static void ResetConfigFromDefaults() {
			if( Main.netMode != 0 ) {
				throw new Exception( "Cannot reset to default configs outside of single player." );
			}

			var new_config = new ExtensibleInventoryConfigData();
			//new_config.SetDefaults();

			ExtensibleInventoryMod.Instance.ConfigJson.SetData( new_config );
			ExtensibleInventoryMod.Instance.ConfigJson.SaveFile();

			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
			myplayer.Library.CurrentBook.IsEnabled = new_config.DefaultBookEnabled;
		}
	}
}
