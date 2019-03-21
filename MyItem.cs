using HamstarHelpers.Helpers.TmlHelpers;
using System;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ModLoader;


namespace ExtensibleInventory {
	class MyItem : GlobalItem {
		public override bool OnPickup( Item item, Player player ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( player );

			int oldStack = item.stack;

			if( myplayer.Library.CurrentBook.Share( player, item ) > 0 ) {
				if( item.stack == 0 ) {
					item.stack = oldStack;
					item.active = true;

					ItemText.NewText( item, oldStack, false, false );
					Main.PlaySound( 7, (int)player.position.X, (int)player.position.Y, 1, 1f, 0f );
					if( player.whoAmI == Main.myPlayer ) {
						Recipe.FindRecipes();
					}
					AchievementsHelper.NotifyItemPickup( player, item );

					item.stack = 0;
					item.active = false;
				}
			}

			return item.stack > 0;
		}
	}
}
