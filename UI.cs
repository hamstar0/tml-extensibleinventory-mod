using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.HudHelpers;
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
	partial class InventoryPageScrollerUI : UIState {
		public static void DrawX( SpriteBatch sb, UIElement elem ) {
			sb.DrawString( Main.fontMouseText, "X", elem.GetOuterDimensions().Position() + new Vector2( 4, -2 ), Color.Red, 0f, default( Vector2 ), 1.25f, SpriteEffects.None, 1f );
		}



		////////////////

		internal Texture2D ButtonBookTex;
		internal Texture2D ButtonBookDimTex;
		internal Texture2D ButtonBookLitTex;
		internal Texture2D ButtonPageRightTex;
		internal Texture2D ButtonPageLeftTex;
		internal Texture2D ButtonPageAddTex;
		internal Texture2D ButtonPageSubTex;
		
		private IDictionary<string, UIImageButton> ButtonBooks = null;
		private UIText PageDisplay;
		private UIImageButton ButtonPageLeft;
		private UIImageButton ButtonPageRight;
		private UIImageButton ButtonPageAdd;
		private UIImageButton ButtonPageSub;

		private bool IsChest = true;



		////////////////

		public InventoryPageScrollerUI( ExtensibleInventoryMod mymod ) : base() {
			this.ButtonBookTex = ModLoader.GetTexture( "Terraria/Item_531" );   // Spell Tome
			this.ButtonBookDimTex = ModLoader.GetTexture( "Terraria/Item_1313" );   // Book of Skulls
			this.ButtonBookLitTex = ModLoader.GetTexture( "Terraria/Item_1336" );   // Golden Shower
			this.ButtonPageRightTex = mymod.GetTexture( "ButtonRight" );
			this.ButtonPageLeftTex = mymod.GetTexture( "ButtonLeft" );
			this.ButtonPageAddTex = mymod.GetTexture( "ButtonAdd" );
			this.ButtonPageSubTex = mymod.GetTexture( "ButtonSub" );

			Promises.AddWorldUnloadEachPromise( () => {
				this.ButtonBooks = null;
			} );
		}


		////////////////

		public bool IsHoveringAnyControl() {
			if( this.ButtonPageLeft.IsMouseHovering ) {
				return true;
			}
			if( this.ButtonPageRight.IsMouseHovering ) {
				return true;
			}
			if( this.ButtonPageAdd.IsMouseHovering ) {
				return true;
			}
			if( this.ButtonPageSub.IsMouseHovering ) {
				return true;
			}
			if( this.ButtonBooks != null ) {
				foreach( var button in this.ButtonBooks.Values ) {
					if( button.IsMouseHovering ) {
						return true;
					}
				}
			}
			return false;
		}


		////////////////

		public override void Update( GameTime game_time ) {
			if( this.ButtonBooks == null && Main.LocalPlayer != null && Main.LocalPlayer.active ) {
				this.InitializeLibraryBooks();
			}

			this.UpdateLayout();

			base.Update( game_time );
		}


		////////////////

		public override void Draw( SpriteBatch sb ) {
			if( !ExtensibleInventoryMod.Instance.Config.HidePageTicksUI ) {
				this.DrawPageTicks( sb );
			}

			base.Draw( sb );

			this.DrawDisabledElementOverlays( sb );

			this.DrawMouseHoverTexts( sb );
		}


		private void DrawPageTicks( SpriteBatch sb ) {
			Player plr = Main.LocalPlayer;
			if( plr == null || !plr.active ) { return; }    //?
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
			var mymod = ExtensibleInventoryMod.Instance;

			var pos = new Vector2( mymod.Config.PageTicksPositionX, mymod.Config.PageTicksPositionY );
			int pages = myplayer.Library.CurrentBook.CountPages();
			int max_pages = pages > 30 ? 29 : pages;

			for( int i=0; i< max_pages; i++ ) {
				var rect = new Rectangle( (int)(pos.X + (i * 16)), (int)(pos.Y), 12, 3 );
				var fill_color = i == myplayer.Library.CurrentBook.CurrentPageIdx ?
					new Color(64, 64, 128, 48) :
					new Color(80, 80, 160, 48);
				var bord_color = new Color( 224, 224, 224, 48 );

				HudHelpers.DrawBorderedRect( sb, fill_color, bord_color, rect, 1 );
			}

			if( pages != max_pages ) {
				sb.DrawString( Main.fontMouseText, "...", new Vector2( pos.X * ( 30 * 16 ), pos.Y ), Color.White );
			}
		}


		private void DrawDisabledElementOverlays( SpriteBatch sb ) {
			Player plr = Main.LocalPlayer;
			if( plr == null || !plr.active ) { return; }    //?
			var myplayer = plr.GetModPlayer<ExtensibleInventoryPlayer>();
			var mymod = ExtensibleInventoryMod.Instance;

			if( !mymod.Config.CanScrollPages ) {
				InventoryPageScrollerUI.DrawX( sb, this.ButtonPageLeft );
				InventoryPageScrollerUI.DrawX( sb, this.ButtonPageRight );
			}
			if( !mymod.Config.CanAddPages ) {
				InventoryPageScrollerUI.DrawX( sb, this.ButtonPageAdd );
			}
			if( !mymod.Config.CanDeletePages ) {
				InventoryPageScrollerUI.DrawX( sb, this.ButtonPageSub );
			}
			
			if( !myplayer.Library.CurrentBook.IsEnabled ) {
				InventoryPageScrollerUI.DrawX( sb, this.ButtonPageLeft );
				InventoryPageScrollerUI.DrawX( sb, this.ButtonPageRight );
				InventoryPageScrollerUI.DrawX( sb, this.ButtonPageAdd );
				InventoryPageScrollerUI.DrawX( sb, this.ButtonPageSub );
			}

			if( this.ButtonBooks != null ) {
				foreach( var kv in this.ButtonBooks ) {
					string _;
					string book_name = kv.Key;
					UIImageButton book_button = kv.Value;

					if( !myplayer.Library.CanSwitchBooks(out _) || !myplayer.Library.IsBookEnabled( book_name ) ) {
						InventoryPageScrollerUI.DrawX( sb, book_button );
					}
				}
			}
		}


		private void DrawMouseHoverTexts( SpriteBatch sb ) {
			var pos = new Vector2( Main.mouseX, Main.mouseY + 16 );

			if( this.ButtonPageLeft.IsMouseHovering ) {
				string text = "Scroll inventory up";
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}
			if( this.ButtonPageRight.IsMouseHovering ) {
				string text = "Scroll inventory down";
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}
			if( this.ButtonPageAdd.IsMouseHovering ) {
				string text = "Add new inventory page";
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}
			if( this.ButtonPageSub.IsMouseHovering ) {
				string text = "Remove current inventory page";
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}

			if( this.ButtonBooks != null ) {
				foreach( var kv in this.ButtonBooks ) {
					string book_name = kv.Key;
					UIImageButton book_button = kv.Value;

					if( book_button.IsMouseHovering ) {
						sb.DrawString( Main.fontMouseText, book_name, pos, Color.White );
					}
				}
			}
		}
	}
}
