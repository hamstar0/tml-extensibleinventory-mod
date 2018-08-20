using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;


namespace ExtensibleInventory {
	internal class InventoryPageScrollerUI : UIState {
		internal Texture2D ButtonRight;
		internal Texture2D ButtonLeft;
		internal Texture2D ButtonAdd;


		////////////////

		public InventoryPageScrollerUI( ExtensibleInventoryMod mymod ) : base() {
			this.ButtonRight = mymod.GetTexture( "ButtonRight" );
			this.ButtonLeft = mymod.GetTexture( "ButtonLeft" );
			this.ButtonLeft = mymod.GetTexture( "ButtonAdd" );
		}

		public override void OnInitialize() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();

			UIImageButton button_left = new UIImageButton( this.ButtonLeft );
			button_left.Top.Set( mymod.Config.OffsetY, 0f );
			button_left.Left.Set( mymod.Config.OffsetX, 0f );
			button_left.OnClick += delegate( UIMouseEvent evt, UIElement listening_elem ) {
				var myplayer2 = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
				myplayer2.ScrollPageUp();
			};
			UIImageButton button_right = new UIImageButton( this.ButtonRight );
			button_right.Top.Set( mymod.Config.OffsetY, 0f );
			button_right.Left.Set( mymod.Config.OffsetX + 112f, 0f );
			button_right.OnClick += delegate ( UIMouseEvent evt, UIElement listening_elem ) {
				var myplayer2 = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
				myplayer2.ScrollPageDown();
			};
			UIImageButton button_add = new UIImageButton( this.ButtonRight );
			button_add.Top.Set( mymod.Config.OffsetY, 0f );
			button_add.Left.Set( mymod.Config.OffsetX + 144f, 0f );
			button_add.OnClick += delegate ( UIMouseEvent evt, UIElement listening_elem ) {
				var myplayer2 = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
				myplayer2.InsertAtCurrentPage();
			};

			base.Append( button_left );
			base.Append( button_right );
			if( myplayer.CanAddPages() ) {
				base.Append( button_add );
			}
		}
	}




	partial class ExtensibleInventoryMod : Mod {
		private UserInterface InvUI;
		private InventoryPageScrollerUI InvPageScroller;

		
		////////////////

		private void InitializeUI() {
			this.InvUI = new UserInterface();
			this.InvPageScroller = new InventoryPageScrollerUI( this );
			this.InvUI.SetState( this.InvPageScroller );
		}

		
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
