using HamstarHelpers.Helpers.HUD;
using HamstarHelpers.Helpers.TModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;


namespace ExtensibleInventory.UI {
	partial class InventoryUI : UIState {
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
				Rectangle[] rects = this.DrawPageTicks( sb );
				this.HandlePageTickClicks( rects );
			}

			base.Draw( sb );

			this.DrawDisabledElementOverlays( sb );

			this.DrawMouseHoverTexts( sb );
		}


		private Rectangle[] DrawPageTicks( SpriteBatch sb ) {
			Player plr = Main.LocalPlayer;
			if( plr == null || !plr.active ) {
				return new Rectangle[0];    //?
			}

			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( plr );

			var pos = new Vector2( mymod.Config.PageTicksPositionXCoord, mymod.Config.PageTicksPositionYCoord );
			int pages = myplayer.Library.CurrentBook.CountPages();
			int maxPages = pages > 29 ? 28 : pages;
			var rects = new Rectangle[pages];

			var unsharedColor = new Color( 192, 192, 192 );
			var sharedColor = new Color( 128, 255, 32 );

			for( int i = 0; i < maxPages; i++ ) {
				bool isCurrPage = i == myplayer.Library.CurrentBook.CurrentPageIdx;
				bool isShared = myplayer.Library.CurrentBook.IsPageSharing( i );

				var rect = new Rectangle( (int)( pos.X + ( i * 16 ) ), (int)pos.Y, 13, 4 );
				bool isHovering = rect.Contains( Main.mouseX, Main.mouseY );

				Color fillColor = new Color( 128, 128, 256 ) * myplayer.Library.CurrentBook.GaugePageFullness( i );
				Color bordColor = (isShared ? sharedColor : unsharedColor) * (isHovering ? 1f : 0.65f);
				int thickness = isCurrPage ? 2 : 1;

				HUDHelpers.DrawBorderedRect( sb, fillColor, bordColor, rect, thickness );

				rects[i] = rect;
			}

			if( pages != maxPages ) {
				sb.DrawString( Main.fontMouseText, "...", new Vector2( pos.X + ( 28 * 16 ), pos.Y - 12f ), Color.White );
			}

			return rects;
		}


		private void DrawDisabledElementOverlays( SpriteBatch sb ) {
			var mymod = ExtensibleInventoryMod.Instance;
			Player plr = Main.LocalPlayer;
			if( plr == null || !plr.active ) { return; }    //?
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( plr );

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
					InventoryUI.DrawX( sb, this.ButtonPageLeft );
					InventoryUI.DrawX( sb, this.ButtonPageRight );
				}
				if( mymod.Config.CanAddPages ) {
					InventoryUI.DrawX( sb, this.ButtonPageAdd );
				}
				if( mymod.Config.CanDeletePages ) {
					InventoryUI.DrawX( sb, this.ButtonPageSub );
				}
			}

			if( this.ButtonBooks != null && mymod.Config.CanSwitchBooks ) {
				foreach( var kv in this.ButtonBooks ) {
					string _;
					string bookName = kv.Key;
					UIImageButton bookButton = kv.Value;

					if( !myplayer.Library.CanSwitchBooks( out _ ) || !myplayer.Library.IsBookEnabled( bookName ) ) {
						InventoryUI.DrawX( sb, bookButton );
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


		////////////////

		private void HandlePageTickClicks( Rectangle[] rects ) {
			Player plr = Main.LocalPlayer;
			if( plr == null || !plr.active ) { return; }    //?

			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( plr );
			
			if( Main.mouseLeft && Main.mouseLeftRelease ) {
				for( int i = 0; i < rects.Length; i++ ) {
					Rectangle rect = rects[i];

					if( rect.Contains( Main.mouseX, Main.mouseY ) ) {
						myplayer.Library.CurrentBook.JumpToPage( Main.LocalPlayer, i );
						break;
					}
				}
			}
		}
	}
}
