using HamstarHelpers.Components.Errors;
using Microsoft.Xna.Framework;
using Terraria;


namespace ExtensibleInventory {
	public static partial class ExtensibleInventoryAPI {
		public static ExtensibleInventoryConfigData GetModSettings() {
			return ExtensibleInventoryMod.Instance.Config;
		}

		public static void SaveModSettingsChanges() {
			if( Main.netMode != 0 ) {
				throw new HamstarException( "Mod settings may only be saved in single player." );
			}

			ExtensibleInventoryMod.Instance.ConfigJson.SaveFile();
		}


		////////////////

		public static void AddBook( string bookName, bool userCanAddPages ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();

			myplayer.Library.AddBook( bookName, userCanAddPages );
		}
		
		public static bool RemoveBook( string bookName ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();

			return myplayer.Library.RemoveBook( bookName );
		}


		////////////////

		public static void EnableBook( string bookName ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();

			myplayer.Library.EnableBook( bookName );
		}
		
		public static void DisableBook( string bookName ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();

			myplayer.Library.DisableBook( bookName );
		}
		

		////////////////

		public static int CountBookPages( string bookName ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();

			return myplayer.Library.CountBookPages( bookName );
		}

		public static void AddBookPage( string bookName ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
			string err;

			if( !myplayer.Library.AddBookPage( Main.LocalPlayer, bookName, out err ) ) {
				Main.NewText( err, Color.Red );
			}
		}

		public static Item[] GetLatestBookPageItems( string bookName ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();

			return myplayer.Library.GetLatestBookPageItems( bookName );
		}
		
		public static void RemoveLatestBookPage( string bookName ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
			string err;

			if( !myplayer.Library.RemoveLatestBookPage( Main.LocalPlayer, bookName, out err ) ) {
				Main.NewText( err, Color.Red );
			}
		}
	}
}
