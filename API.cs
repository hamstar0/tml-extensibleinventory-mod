using Terraria;


namespace ExtensibleInventory {
	public static partial class ExtensibleInventoryAPI {
		public static ExtensibleInventoryConfigData GetModSettings() {
			return ExtensibleInventoryMod.Instance.Config;
		}

		public static void SaveModSettingsChanges() {
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

		
		public static int CountBookPages( string book_name ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();

			return myplayer.Library.CountBookPages( book_name );
		}

		////////////////

		public static void AddBookPage( string book_name ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();

			myplayer.Library.AddBookPage( Main.LocalPlayer, book_name );
		}

		public static Item[] GetLatestBookPageItems( string book_name ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();

			return myplayer.Library.GetLatestBookPageItems( book_name );
		}
		
		public static void RemoveLatestBookPage( string book_name ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();

			myplayer.Library.RemoveLatestBookPage( book_name );
		}
	}
}
