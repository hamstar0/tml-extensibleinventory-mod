using HamstarHelpers.Services.Promises;
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
		internal Texture2D ButtonBookTex;
		internal Texture2D ButtonBookLitTex;
		internal Texture2D ButtonPageRightTex;
		internal Texture2D ButtonPageLeftTex;
		internal Texture2D ButtonPageAddTex;
		internal Texture2D ButtonPageSubTex;

		private IDictionary<string, UIImageButton> ButtonBooks = null;
		private UIImageButton ButtonPageLeft;
		private UIImageButton ButtonPageRight;
		private UIImageButton ButtonPageAdd;
		private UIImageButton ButtonPageSub;


		////////////////

		public InventoryPageScrollerUI( ExtensibleInventoryMod mymod ) : base() {
			this.ButtonBookTex = ModLoader.GetTexture( "Terraria/Item_531" );   // Spell Tome
			this.ButtonBookTex = ModLoader.GetTexture( "Terraria/Item_1336" );   // Golden Shower
			this.ButtonPageRightTex = mymod.GetTexture( "ButtonRight" );
			this.ButtonPageLeftTex = mymod.GetTexture( "ButtonLeft" );
			this.ButtonPageAddTex = mymod.GetTexture( "ButtonAdd" );
			this.ButtonPageSubTex = mymod.GetTexture( "ButtonSub" );

			Promises.AddWorldUnloadEachPromise( () => {
				this.ButtonBooks = null;
			} );
		}


		public override void OnInitialize() {
			var mymod = ExtensibleInventoryMod.Instance;

			this.ButtonPageLeft = new UIImageButton( this.ButtonPageLeftTex );
			this.ButtonPageLeft.Top.Set( mymod.Config.PageOffsetY, 0f );
			this.ButtonPageLeft.Left.Set( mymod.Config.PageOffsetX, 0f );
			this.ButtonPageLeft.OnClick += delegate ( UIMouseEvent evt, UIElement listening_elem ) {
				var myplayer2 = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
				myplayer2.Library.CurrentBook.ScrollPageUp( Main.LocalPlayer );
			};
			this.ButtonPageRight = new UIImageButton( this.ButtonPageRightTex );
			this.ButtonPageRight.Top.Set( mymod.Config.PageOffsetY, 0f );
			this.ButtonPageRight.Left.Set( mymod.Config.PageOffsetX + 112f, 0f );
			this.ButtonPageRight.OnClick += delegate ( UIMouseEvent evt, UIElement listening_elem ) {
				var myplayer2 = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
				myplayer2.Library.CurrentBook.ScrollPageDown( Main.LocalPlayer );
			};
			this.ButtonPageAdd = new UIImageButton( this.ButtonPageAddTex );
			this.ButtonPageAdd.Top.Set( mymod.Config.PageOffsetY, 0f );
			this.ButtonPageAdd.Left.Set( mymod.Config.PageOffsetX + 144f, 0f );
			this.ButtonPageAdd.OnClick += delegate ( UIMouseEvent evt, UIElement listening_elem ) {
				var myplayer2 = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
				myplayer2.Library.CurrentBook.InsertAtCurrentPage( Main.LocalPlayer );
			};
			this.ButtonPageSub = new UIImageButton( this.ButtonPageSubTex );
			this.ButtonPageSub.Top.Set( mymod.Config.PageOffsetY, 0f );
			this.ButtonPageSub.Left.Set( mymod.Config.PageOffsetX + 172f, 0f );
			this.ButtonPageSub.OnClick += delegate ( UIMouseEvent evt, UIElement listening_elem ) {
				var myplayer2 = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
				myplayer2.Library.CurrentBook.DeleteCurrentPage( Main.LocalPlayer );
			};
			
			base.Append( this.ButtonPageLeft );
			base.Append( this.ButtonPageRight );
			base.Append( this.ButtonPageAdd );
			base.Append( this.ButtonPageSub );
		}


		private void InitializeLibrary() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
			int i = 0;

			if( myplayer.Library.Books.Count == 1 ) {
				return;
			}

			this.ButtonBooks = new Dictionary<string, UIImageButton>();

			foreach( string book_name in myplayer.Library.Books.Keys ) {
				Texture2D tex = book_name == myplayer.Library.CurrentBook.Name ?
					this.ButtonBookTex : this.ButtonBookLitTex;

				var button = new UIImageButton( tex );
				button.Top.Set( mymod.Config.BookOffsetY, 0f );
				button.Left.Set( mymod.Config.BookOffsetX + ( i * 24 ), 0f );
				button.OnClick += delegate ( UIMouseEvent evt, UIElement listening_elem ) {
					var myplayer2 = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
					myplayer2.Library.SetBook( book_name );
				};

				this.ButtonBooks[ book_name ] = button;
				base.Append( button );
				i++;
			}
		}


		////////////////

		public override void Draw( SpriteBatch sb ) {
			if( this.ButtonBooks == null && Main.LocalPlayer != null && Main.LocalPlayer.active ) {
				this.InitializeLibrary();
			}

			base.Draw( sb );

			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
			var pos = new Vector2( Main.mouseX, Main.mouseY + 16 );

			string page_text = myplayer.Library.CurrentBook.RenderPagePosition();
			float text_wid = Main.fontMouseText.MeasureString( page_text ).X;
			var text_pos = new Vector2( mymod.Config.PageOffsetX + 20f + ( 48f - text_wid / 2f ), mymod.Config.PageOffsetY );

			sb.DrawString( Main.fontMouseText, page_text, text_pos, Color.White );

			if( this.ButtonPageLeft.IsMouseHovering ) {
				string text = "Scroll inventory up" + (mymod.Config.CanScrollPages ? "" : " (disabled)");
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}
			if( this.ButtonPageRight.IsMouseHovering ) {
				string text = "Scroll inventory down" + ( mymod.Config.CanScrollPages ? "" : " (disabled)" );
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}
			if( this.ButtonPageAdd.IsMouseHovering ) {
				string text = "Add new inventory page" + ( mymod.Config.CanAddPages ? "" : " (disabled)" );
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}
			if( this.ButtonPageSub.IsMouseHovering ) {
				string text = "Remove current inventory page" + ( mymod.Config.CanDeletePages ? "" : " (disabled)" );
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}

			foreach( var kv in this.ButtonBooks ) {
				if( kv.Value.IsMouseHovering ) {
					sb.DrawString( Main.fontMouseText, kv.Key, pos, Color.White );
				}
			}
		}
	}
}
