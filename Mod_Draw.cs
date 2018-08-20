using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;


namespace ExtensibleInventory {
	partial class ExtensibleInventoryMod : Mod {
		private UserInterface InvUI;
		private InventoryPageScrollerUI InvPageScroller;

		

		////////////////

		private void InitializeUI() {
			this.InvUI = new UserInterface();
			this.InvPageScroller = new InventoryPageScrollerUI( this );
			this.InvUI.SetState( this.InvPageScroller );
		}


		////////////////

		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			int layer_idx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Inventory" ) );
			if( layer_idx == -1 ) { return; }

			GameInterfaceDrawMethod control_ui = delegate {
				if( !Main.playerInventory || Main.myPlayer < 0 || Main.LocalPlayer == null || !Main.LocalPlayer.active ) {
					return true;
				}

				var mymod = ExtensibleInventoryMod.Instance;
				var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
				string text = myplayer.RenderPagePosition();

				float text_wid = Main.fontMouseText.MeasureString( text ).X;
				var text_pos = new Vector2( mymod.Config.OffsetX + 20f + (48f - text_wid/2f), mymod.Config.OffsetY );

				mymod.InvUI.Update( Main._drawInterfaceGameTime );
				mymod.InvPageScroller.Draw( Main.spriteBatch );

				Main.spriteBatch.DrawString( Main.fontMouseText, text, text_pos, Color.White );

				return true;
			};

			var inv_over_layer = new LegacyGameInterfaceLayer( "Unlimited Inventory: Page Controls", control_ui, InterfaceScaleType.UI );

			layers.Insert( layer_idx + 1, inv_over_layer );
		}
	}
}
