using ExtensibleInventory.Inventory;
using HamstarHelpers.Helpers.TModLoader;
using Terraria;


namespace ExtensibleInventory {
	public static partial class ExtensibleInventoryAPI {
		public static void ResetPlayerModData( Player player ) {    // <- In accordance with Mod Helpers convention
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( player );
			myplayer.Library = new InventoryLibrary();
		}
	}
}
