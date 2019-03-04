using ExtensibleInventory.Inventory;
using HamstarHelpers.Components.Errors;
using Terraria;


namespace ExtensibleInventory {
	public static partial class ExtensibleInventoryAPI {
		public static void ResetPlayerModData( Player player ) {    // <- In accordance with Mod Helpers convention
			var myplayer = player.GetModPlayer<ExtensibleInventoryPlayer>();
			myplayer.Library = new InventoryLibrary();
		}
	}
}
