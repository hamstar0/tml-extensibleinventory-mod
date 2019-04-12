﻿using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.TmlHelpers;
using System;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;


namespace ExtensibleInventory {
	class MyItem : GlobalItem {
		public override bool OnPickup( Item item, Player player ) {
			switch( item.type ) {
			case ItemID.CopperCoin:
			case ItemID.SilverCoin:
			case ItemID.GoldCoin:
			case ItemID.PlatinumCoin:
				return true;
			}

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

			if( item.stack != oldStack ) {
				Item worldItem = Main.item[item.whoAmI];

				if( worldItem != null && !worldItem.IsAir && !worldItem.IsNotTheSameAs(item) ) {
LogHelpers.Log("Sync needed?");
					if( Main.netMode != 0 ) {
						NetMessage.SendData( MessageID.SyncItem, -1, -1, null, item.whoAmI, 0f, 0f, 0f, 0, 0, 0 );
					}
				}
			}

			return true;
			//return item.stack > 0;
		}
	}
}
