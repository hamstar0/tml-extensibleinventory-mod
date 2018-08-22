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

		public static void AddBook( string book_name, bool user_can_add_pages ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();

			myplayer.Library.AddBook( book_name, user_can_add_pages );
		}
		
		public static bool RemoveBook( string book_name ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();

			return myplayer.Library.RemoveBook( book_name );
		}


		////////////////

		public static void EnableBook( string book_name ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();

			myplayer.Library.EnableBook( book_name );
		}
		
		public static void DisableBook( string book_name ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();

			myplayer.Library.DisableBook( book_name );
		}
		

		////////////////

		public static int CountBookPages( string book_name ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();

			return myplayer.Library.CountBookPages( book_name );
		}

		public static void AddBookPage( string book_name ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
			string err;

			if( !myplayer.Library.AddBookPage( Main.LocalPlayer, book_name, out err ) ) {
				Main.NewText( err, Color.Red );
			}
		}

		public static Item[] GetLatestBookPageItems( string book_name ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();

			return myplayer.Library.GetLatestBookPageItems( book_name );
		}
		
		public static void RemoveLatestBookPage( string book_name ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
			string err;

			if( !myplayer.Library.RemoveLatestBookPage( Main.LocalPlayer, book_name, out err ) ) {
				Main.NewText( err, Color.Red );
			}
		}
	}
}
