using ExtensibleInventory.UI;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.TModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;


namespace ExtensibleInventory {
	partial class ExtensibleInventoryMod : Mod {
		private UserInterface InvUIMngr;
		internal InventoryUI InvUI;

		private Texture2D ScrollIcon;

		

		////////////////

		private void InitializeUI() {
			this.InvUIMngr = new UserInterface();
			this.InvUI = new InventoryUI();
			this.InvUIMngr.SetState( this.InvUI );
			this.ScrollIcon = this.GetTexture( "UI/IconScroll" );
		}


		////////////////

		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			int layerIdx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Inventory" ) );
			if( layerIdx == -1 ) { return; }
			
			GameInterfaceDrawMethod controlUi = delegate {
				if( !Main.playerInventory || Main.myPlayer < 0 || Main.LocalPlayer == null || !Main.LocalPlayer.active ) {
					if( Main.LocalPlayer != null && !Main.LocalPlayer.active ) {
						TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( Main.LocalPlayer );
						if( !Main.LocalPlayer.active ) {
							return true;
						}
					}
					//if( Main.playerInventory ) {
					//	LogHelpers.LogOnce( ""+(Main.myPlayer<0)+","+(Main.LocalPlayer == null)+","+(!Main.LocalPlayer.active) );
					//}
					return true;
				}

				var mymod = ExtensibleInventoryMod.Instance;

				try {
					mymod.InvUIMngr?.Update( Main._drawInterfaceGameTime );
					mymod.InvUI?.Draw( Main.spriteBatch );

					if( !this.Config.HideScrollModeIcon ) {
						var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( Main.LocalPlayer );
						if( myplayer.ScrollModeOn & this.ScrollIcon != null ) {
							var pos = new Vector2( Main.mouseX - 24, Main.mouseY );
							float colorScale = 0.35f * ( (float)myplayer.ScrollModeDuration / (float)ExtensibleInventoryPlayer.ScrollModeMaxDuration );

							Main.spriteBatch.Draw( this.ScrollIcon, pos, Color.White * colorScale );
						}
					}
				} catch( Exception e ) {
					throw new ModHelpersException( "", e );
				}

				return true;
			};

			var invOverLayer = new LegacyGameInterfaceLayer( "Extensible Inventory: Page Controls", controlUi, InterfaceScaleType.UI );

			layers.Insert( layerIdx + 1, invOverLayer );
		}
	}
}
