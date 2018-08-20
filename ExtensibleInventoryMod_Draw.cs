using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;


namespace ExtensibleInventory {
	internal class InventoryPageScrollerUI : UIState {
		public override void OnInitialize() {
			var mymod = ExtensibleInventoryMod.Instance;

			UIImageButton button_left = new UIImageButton( ExtensibleInventoryMod.ButtonLeft );
			button_left.Top.Set( mymod.Config.OffsetY, 0f );
			button_left.Left.Set( mymod.Config.OffsetX, 0f );
			button_left.OnClick += delegate( UIMouseEvent evt, UIElement listening_elem ) {
				var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
				myplayer.ScrollPageUp();
			};
			UIImageButton button_right = new UIImageButton( ExtensibleInventoryMod.ButtonRight );
			button_right.Top.Set( mymod.Config.OffsetY, 0f );
			button_right.Left.Set( mymod.Config.OffsetX + 96f, 0f );
			button_right.OnClick += delegate ( UIMouseEvent evt, UIElement listening_elem ) {
				var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
				myplayer.ScrollPageDown();
			};

			base.Append( button_left );
			base.Append( button_right );
		}
	}



	partial class ExtensibleInventoryMod : Mod {
		internal static Texture2D ButtonRight;
		internal static Texture2D ButtonLeft;


		////////////////

		private UserInterface InvUI;
		private InventoryPageScrollerUI InvPageScroller;



		////////////////

		private void InitializeUI() {
			ExtensibleInventoryMod.ButtonRight = this.GetTexture( "ButtonRight" );
			ExtensibleInventoryMod.ButtonLeft = this.GetTexture( "ButtonLeft" );

			this.InvUI = new UserInterface();
			this.InvPageScroller = new InventoryPageScrollerUI();
			this.InvUI.SetState( this.InvPageScroller );
		}

		
		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			int layer_idx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Inventory" ) );
			if( layer_idx == -1 ) { return; }

			GameInterfaceDrawMethod control_ui = delegate {
				if( !Main.playerInventory ) {
					return true;
				}
				
				this.InvUI.Update( Main._drawInterfaceGameTime );
				this.InvPageScroller.Draw( Main.spriteBatch );

				Main.spriteBatch.DrawString( Main.fontMouseText,  )

				return true;
			};

			var inv_over_layer = new LegacyGameInterfaceLayer( "Unlimited Inventory: Page Controls", control_ui, InterfaceScaleType.UI );

			layers.Insert( layer_idx + 1, inv_over_layer );
		}
	}
}
