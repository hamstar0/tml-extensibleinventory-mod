using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.HudHelpers;
using HamstarHelpers.Helpers.TmlHelpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;


namespace ExtensibleInventory.UI {
	partial class InventoryPageScrollerUI : UIState {
		public static void DrawX( SpriteBatch sb, UIElement elem ) {
			sb.DrawString(
				Main.fontMouseText,
				"X",
				elem.GetOuterDimensions().Position() + new Vector2( 4, -2 ),
				Color.Red,
				0f,
				default( Vector2 ),
				1.25f,
				SpriteEffects.None,
				1f
			);
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

			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( plr, mymod, "ExtensibleInventoryPlayer" );

			var pos = new Vector2( mymod.Config.PageTicksPositionX, mymod.Config.PageTicksPositionY );
			int pages = myplayer.Library.CurrentBook.CountPages();
			int maxPages = pages > 29 ? 28 : pages;

			for( int i=0; i<maxPages; i++ ) {
				bool isCurrPage = i == myplayer.Library.CurrentBook.CurrentPageIdx;
				var rect = new Rectangle( (int)(pos.X + (i * 16)), (int)(pos.Y), 13, 4 );
				var fillColor = new Color( 128, 128, 256 ) * myplayer.Library.CurrentBook.GaugePageFullness( i );
				var bordColor = Color.White * 0.65f;
				int thickness = isCurrPage ? 2 : 1;

				HudHelpers.DrawBorderedRect( sb, fillColor, bordColor, rect, thickness );
			}

			if( pages != maxPages ) {
				sb.DrawString( Main.fontMouseText, "...", new Vector2( pos.X + (28 * 16), pos.Y - 12f ), Color.White );
			}
		}


		private void DrawDisabledElementOverlays( SpriteBatch sb ) {
			var mymod = ExtensibleInventoryMod.Instance;
			Player plr = Main.LocalPlayer;
			if( plr == null || !plr.active ) { return; }    //?
			var myplayer = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( plr, mymod, "ExtensibleInventoryPlayer" );

			/*if( !mymod.Config.CanScrollPages ) {
				InventoryPageScrollerUI.DrawX( sb, this.ButtonPageLeft );
				InventoryPageScrollerUI.DrawX( sb, this.ButtonPageRight );
			}
			if( !mymod.Config.CanAddPages ) {
				InventoryPageScrollerUI.DrawX( sb, this.ButtonPageAdd );
			}
			if( !mymod.Config.CanDeletePages ) {
				InventoryPageScrollerUI.DrawX( sb, this.ButtonPageSub );
			}*/

			if( !myplayer.Library.CurrentBook.IsEnabled ) {
				if( mymod.Config.CanScrollPages ) {
					InventoryPageScrollerUI.DrawX( sb, this.ButtonPageLeft );
					InventoryPageScrollerUI.DrawX( sb, this.ButtonPageRight );
				}
				if( mymod.Config.CanAddPages ) {
					InventoryPageScrollerUI.DrawX( sb, this.ButtonPageAdd );
				}
				if( mymod.Config.CanDeletePages ) {
					InventoryPageScrollerUI.DrawX( sb, this.ButtonPageSub );
				}
			}

			if( this.ButtonBooks != null && mymod.Config.CanSwitchBooks ) {
				foreach( var kv in this.ButtonBooks ) {
					string _;
					string bookName = kv.Key;
					UIImageButton bookButton = kv.Value;

					if( !myplayer.Library.CanSwitchBooks(out _) || !myplayer.Library.IsBookEnabled( bookName ) ) {
						InventoryPageScrollerUI.DrawX( sb, bookButton );
					}
				}
			}
		}


		private void DrawMouseHoverTexts( SpriteBatch sb ) {
			var mymod = ExtensibleInventoryMod.Instance;
			var pos = new Vector2( Main.mouseX, Main.mouseY + 16 );

			if( this.ButtonPageLeft.IsMouseHovering && mymod.Config.CanScrollPages ) {
				string text = "Scroll inventory left";
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}
			if( this.ButtonPageRight.IsMouseHovering && mymod.Config.CanScrollPages ) {
				string text = "Scroll inventory right";
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}
			if( this.ButtonPageAdd.IsMouseHovering && mymod.Config.CanAddPages ) {
				string text = "Add new inventory page";
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}
			if( this.ButtonPageSub.IsMouseHovering && mymod.Config.CanDeletePages ) {
				string text = "Remove current inventory page";
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}

			if( this.ButtonBooks != null && mymod.Config.CanSwitchBooks ) {
				foreach( var kv in this.ButtonBooks ) {
					string bookName = kv.Key;
					UIImageButton bookButton = kv.Value;

					if( bookButton.IsMouseHovering ) {
						sb.DrawString( Main.fontMouseText, bookName, pos, Color.White );
					}
				}
			}
		}
	}
}
