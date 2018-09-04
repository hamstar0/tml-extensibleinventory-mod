using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.HudHelpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;


namespace ExtensibleInventory {
	partial class InventoryPageScrollerUI : UIState {
		public static void DrawX( SpriteBatch sb, UIElement elem ) {
			sb.DrawString( Main.fontMouseText, "X", elem.GetOuterDimensions().Position() + new Vector2( 4, -2 ), Color.Red, 0f, default( Vector2 ), 1.25f, SpriteEffects.None, 1f );
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
			var myplayer = plr.GetModPlayer<ExtensibleInventoryPlayer>();
			var mymod = ExtensibleInventoryMod.Instance;

			var pos = new Vector2( mymod.Config.PageTicksPositionX, mymod.Config.PageTicksPositionY );
			int pages = myplayer.Library.CurrentBook.CountPages();
			int max_pages = pages > 29 ? 28 : pages;

			for( int i=0; i<max_pages; i++ ) {
				bool is_curr_page = i == myplayer.Library.CurrentBook.CurrentPageIdx;
				var rect = new Rectangle( (int)(pos.X + (i * 16)), (int)(pos.Y), 13, 4 );
				var fill_color = new Color( 128, 128, 256 ) * myplayer.Library.CurrentBook.GaugePageFullness( plr, i );
				var bord_color = Color.White * 0.65f;
				int thickness = is_curr_page ? 2 : 1;

				HudHelpers.DrawBorderedRect( sb, fill_color, bord_color, rect, thickness );
			}

			if( pages != max_pages ) {
				sb.DrawString( Main.fontMouseText, "...", new Vector2( pos.X + ( 28 * 16 ), pos.Y - 12f ), Color.White );
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
