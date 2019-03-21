using HamstarHelpers.Helpers.TmlHelpers;
using System;
using Terraria;
using Terraria.ModLoader;


namespace ExtensibleInventory {
	class MyItem : GlobalItem {
		public override bool OnPickup( Item item, Player player ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( player );

			myplayer.Library.CurrentBook.Share( player, item );
			
			return item.stack > 0;
		}
	}
}
