using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.TModLoader;


namespace ExtensibleInventory {
	public static partial class ExtensibleInventoryAPI {
		public static void AddBook( bool userCanAddPages, string bookName = "Default" ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( Main.LocalPlayer );

			myplayer.Library.AddBook( bookName, userCanAddPages );
		}
		
		public static bool RemoveBook( string bookName = "Default" ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( Main.LocalPlayer );

			return myplayer.Library.RemoveBook( bookName );
		}


		////////////////

		public static void EnableBook( string bookName = "Default" ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( Main.LocalPlayer );

			myplayer.Library.EnableBook( bookName );
		}
		
		public static void DisableBook( string bookName = "Default" ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( Main.LocalPlayer );

			myplayer.Library.DisableBook( bookName );
		}
		

		////////////////

		public static int CountBookPages( string bookName = "Default" ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( Main.LocalPlayer );

			return myplayer.Library.CountBookPages( bookName );
		}

		public static void AddBookPage( string bookName = "Default" ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( Main.LocalPlayer );
			string err;

			if( !myplayer.Library.AddBookPage( Main.LocalPlayer, bookName, out err ) ) {
				Main.NewText( err, Color.Red );
			}
		}

		////////////////

		public static Item[] GetBookPageItems( Player player, string bookName = "Default", int pageIdx = 0 ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( player );

			return myplayer.Library.GetBookPageItems( player, bookName, pageIdx );
		}

		public static Item[] GetLatestBookPageItems( Player player, string bookName = "Default" ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( player );

			return myplayer.Library.GetLatestBookPageItems( player, bookName );
		}
		
		public static void RemoveLatestBookPage( Player player, string bookName = "Default" ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( player );
			string err;

			if( !myplayer.Library.RemoveLatestBookPage( player, bookName, out err ) ) {
				Main.NewText( err, Color.Red );
			}
		}
	}
}
