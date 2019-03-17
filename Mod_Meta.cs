using HamstarHelpers.Components.Config;
using HamstarHelpers.Helpers.TmlHelpers;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;


namespace ExtensibleInventory {
	partial class ExtensibleInventoryMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-extensibleinventory-mod";

		public static string ConfigFileRelativePath =>
			ConfigurationDataBase.RelativePath + Path.DirectorySeparatorChar + ExtensibleInventoryConfigData.ConfigFileName;

		public static void ReloadConfigFromFile() {
			if( Main.netMode != 0 ) {
				throw new Exception( "Cannot reload configs outside of single player." );
			}

			var mymod = ExtensibleInventoryMod.Instance;

			if( mymod != null ) {
				if( !mymod.ConfigJson.LoadFile() ) {
					mymod.ConfigJson.SaveFile();
				}
			}

			var myplayer = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod, "ExtensibleInventoryPlayer" );
			myplayer.Library.CurrentBook.IsEnabled = mymod.Config.DefaultBookEnabled;
		}

		public static void ResetConfigFromDefaults() {
			if( Main.netMode != 0 ) {
				throw new Exception( "Cannot reset to default configs outside of single player." );
			}

			var mymod = ExtensibleInventoryMod.Instance;
			var newConfig = new ExtensibleInventoryConfigData();
			//new_config.SetDefaults();

			mymod.ConfigJson.SetData( newConfig );
			mymod.ConfigJson.SaveFile();

			var myplayer = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod, "ExtensibleInventoryPlayer" );
			myplayer.Library.CurrentBook.IsEnabled = newConfig.DefaultBookEnabled;
		}
	}
}
