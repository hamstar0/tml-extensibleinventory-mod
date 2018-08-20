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
		internal Texture2D ButtonRightTex;
		internal Texture2D ButtonLeftTex;
		internal Texture2D ButtonAddTex;
		internal Texture2D ButtonSubTex;

		private UIImageButton ButtonLeft;
		private UIImageButton ButtonRight;
		private UIImageButton ButtonAdd;
		private UIImageButton ButtonSub;


		////////////////

		public InventoryPageScrollerUI( ExtensibleInventoryMod mymod ) : base() {
			this.ButtonRightTex = mymod.GetTexture( "ButtonRight" );
			this.ButtonLeftTex = mymod.GetTexture( "ButtonLeft" );
			this.ButtonAddTex = mymod.GetTexture( "ButtonAdd" );
			this.ButtonSubTex = mymod.GetTexture( "ButtonSub" );
		}


		public override void OnInitialize() {
			var mymod = ExtensibleInventoryMod.Instance;

			this.ButtonLeft = new UIImageButton( this.ButtonLeftTex );
			this.ButtonLeft.Top.Set( mymod.Config.OffsetY, 0f );
			this.ButtonLeft.Left.Set( mymod.Config.OffsetX, 0f );
			this.ButtonLeft.OnClick += delegate( UIMouseEvent evt, UIElement listening_elem ) {
				var myplayer2 = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
				myplayer2.ScrollPageUp();
			};
			this.ButtonRight = new UIImageButton( this.ButtonRightTex );
			this.ButtonRight.Top.Set( mymod.Config.OffsetY, 0f );
			this.ButtonRight.Left.Set( mymod.Config.OffsetX + 112f, 0f );
			this.ButtonRight.OnClick += delegate ( UIMouseEvent evt, UIElement listening_elem ) {
				var myplayer2 = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
				myplayer2.ScrollPageDown();
			};
			this.ButtonAdd = new UIImageButton( this.ButtonAddTex );
			this.ButtonAdd.Top.Set( mymod.Config.OffsetY, 0f );
			this.ButtonAdd.Left.Set( mymod.Config.OffsetX + 144f, 0f );
			this.ButtonAdd.OnClick += delegate ( UIMouseEvent evt, UIElement listening_elem ) {
				var myplayer2 = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
				myplayer2.InsertAtCurrentPage();
			};
			this.ButtonSub = new UIImageButton( this.ButtonSubTex );
			this.ButtonSub.Top.Set( mymod.Config.OffsetY, 0f );
			this.ButtonSub.Left.Set( mymod.Config.OffsetX + 172f, 0f );
			this.ButtonSub.OnClick += delegate ( UIMouseEvent evt, UIElement listening_elem ) {
				var myplayer2 = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
				myplayer2.DeleteCurrentPage();
			};

			base.Append( this.ButtonLeft );
			base.Append( this.ButtonRight );
			base.Append( this.ButtonAdd );
			base.Append( this.ButtonSub );
		}


		////////////////

		public override void Draw( SpriteBatch sb ) {
			base.Draw( sb );
			
			var pos = new Vector2( Main.mouseX, Main.mouseY + 16 );
			
			if( this.ButtonLeft.IsMouseHovering ) {
				sb.DrawString( Main.fontMouseText, "Scroll inventory up", pos, Color.White );
			}
			if( this.ButtonRight.IsMouseHovering ) {
				sb.DrawString( Main.fontMouseText, "Scroll inventory down", pos, Color.White );
			}
			if( this.ButtonAdd.IsMouseHovering ) {
				sb.DrawString( Main.fontMouseText, "Add new inventory page", pos, Color.White );
			}
			if( this.ButtonSub.IsMouseHovering ) {
				sb.DrawString( Main.fontMouseText, "Remove current inventory page", pos, Color.White );
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
