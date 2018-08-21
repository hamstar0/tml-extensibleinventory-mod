using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
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
			this.ButtonLeft.OnClick += delegate ( UIMouseEvent evt, UIElement listening_elem ) {
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

			var mymod = ExtensibleInventoryMod.Instance;
			var pos = new Vector2( Main.mouseX, Main.mouseY + 16 );

			if( this.ButtonLeft.IsMouseHovering ) {
				string text = "Scroll inventory up" + (mymod.Config.CanScrollPages ? "" : " (disabled)");
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}
			if( this.ButtonRight.IsMouseHovering ) {
				string text = "Scroll inventory down" + ( mymod.Config.CanScrollPages ? "" : " (disabled)" );
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}
			if( this.ButtonAdd.IsMouseHovering ) {
				string text = "Add new inventory page" + ( mymod.Config.CanAddPages ? "" : " (disabled)" );
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}
			if( this.ButtonSub.IsMouseHovering ) {
				string text = "Remove current inventory page" + ( mymod.Config.CanDeletePages ? "" : " (disabled)" );
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}
		}
	}
}
