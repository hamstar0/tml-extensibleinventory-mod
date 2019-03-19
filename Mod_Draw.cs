using ExtensibleInventory.UI;
using HamstarHelpers.Components.Errors;
using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.TmlHelpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;


namespace ExtensibleInventory {
	partial class ExtensibleInventoryMod : Mod {
		private UserInterface InvUI;
		internal InventoryPageScrollerUI InvPageScroller;

		private Texture2D ScrollIcon;

		

		////////////////

		private void InitializeUI() {
			this.InvUI = new UserInterface();
			this.InvPageScroller = new InventoryPageScrollerUI();
			this.InvUI.SetState( this.InvPageScroller );
			this.ScrollIcon = ModLoader.GetTexture( "Terraria/UI/VK_Shift" );
		}


		////////////////

		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			int layerIdx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Inventory" ) );
			if( layerIdx == -1 ) { return; }
			
			GameInterfaceDrawMethod controlUi = delegate {
				if( !Main.playerInventory || Main.myPlayer < 0 || Main.LocalPlayer == null || !Main.LocalPlayer.active ) {
					if( Main.playerInventory ) {
						LogHelpers.LogOnce( ""+(Main.myPlayer<0)+","+(Main.LocalPlayer == null)+","+(!Main.LocalPlayer.active) );
					}
					return true;
				}

				var mymod = ExtensibleInventoryMod.Instance;

				try {
					mymod.InvUI?.Update( Main._drawInterfaceGameTime );
					mymod.InvPageScroller?.Draw( Main.spriteBatch );

					var myplayer = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, this, "ExtensibleInventoryPlayer" );
					if( myplayer.ScrollModeOn & this.ScrollIcon != null ) {
						var pos = new Vector2( Main.mouseX - 24, Main.mouseY - 4 );

						Main.spriteBatch.Draw( this.ScrollIcon, pos, Color.White );
					}
				} catch( Exception e ) {
					throw new HamstarException( "", e );
				}

				return true;
			};

			var invOverLayer = new LegacyGameInterfaceLayer( "Extensible Inventory: Page Controls", controlUi, InterfaceScaleType.UI );

			layers.Insert( layerIdx + 1, invOverLayer );
		}
	}
}
