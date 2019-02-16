﻿using ExtensibleInventory.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;


namespace ExtensibleInventory {
	partial class ExtensibleInventoryMod : Mod {
		private UserInterface InvUI;
		internal InventoryPageScrollerUI InvPageScroller;

		

		////////////////

		private void InitializeUI() {
			this.InvUI = new UserInterface();
			this.InvPageScroller = new InventoryPageScrollerUI();
			this.InvUI.SetState( this.InvPageScroller );
		}


		////////////////

		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			int layerIdx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Inventory" ) );
			if( layerIdx == -1 ) { return; }

			GameInterfaceDrawMethod controlUi = delegate {
				if( !Main.playerInventory || Main.myPlayer < 0 || Main.LocalPlayer == null || !Main.LocalPlayer.active ) {
					return true;
				}

				var mymod = ExtensibleInventoryMod.Instance;

				mymod.InvUI.Update( Main._drawInterfaceGameTime );
				mymod.InvPageScroller.Draw( Main.spriteBatch );

				return true;
			};

			var invOverLayer = new LegacyGameInterfaceLayer( "Extensible Inventory: Page Controls", controlUi, InterfaceScaleType.UI );

			layers.Insert( layerIdx + 1, invOverLayer );
		}
	}
}
