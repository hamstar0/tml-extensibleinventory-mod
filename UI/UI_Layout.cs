using Terraria;
using Terraria.UI;


namespace ExtensibleInventory.UI {
	partial class InventoryPageScrollerUI : UIState {
		private void UpdateLayout() {
			var mymod = ExtensibleInventoryMod.Instance;
			bool is_chest = true;
			bool is_player_valid = Main.LocalPlayer != null && Main.LocalPlayer.active;

			if( is_player_valid ) {
				is_chest = Main.LocalPlayer.chest != -1 || Main.npcShop > 0;
			}

			if( this.IsChest != is_chest ) {
				this.IsChest = is_chest;

				this.SetLayoutPositions( is_chest );
			}

			if( is_player_valid ) {
				this.SetPageDisplayPosition( is_chest );
			}

			this.ButtonPageAdd.IsHidden = !mymod.Config.CanAddPages;
			this.ButtonPageSub.IsHidden = !mymod.Config.CanDeletePages;
			this.ButtonPageLeft.IsHidden = !mymod.Config.CanScrollPages;
			this.ButtonPageRight.IsHidden = !mymod.Config.CanScrollPages;
			if( this.ButtonBooks != null ) {
				foreach( UIInventoryControlButton book in this.ButtonBooks.Values ) {
					book.IsHidden = !mymod.Config.CanSwitchBooks;
				}
			}
			 
			this.Recalculate();
		}


		private void SetPageDisplayPosition( bool is_chest ) {
			var mymod = ExtensibleInventoryMod.Instance;
			ExtensibleInventoryPlayer myplayer = null;

			if( Main.LocalPlayer != null && Main.LocalPlayer.active ) {
				myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
			}

			float off_x = is_chest ? mymod.Config.ChestOnOffsetX : 0;
			float off_y = is_chest ? mymod.Config.ChestOnOffsetY : 0;
			float x = mymod.Config.PagePositionX + off_x;
			float y = mymod.Config.PagePositionY + off_y;
			string page_text = this.PageDisplay.Text;

			if( myplayer != null ) {
				page_text = myplayer.Library.CurrentBook.RenderPagePosition();

				this.PageDisplay.SetText( page_text );
			}

			float text_wid = Main.fontMouseText.MeasureString( page_text ).X;

			this.PageDisplay.Top.Set( y, 0f );
			this.PageDisplay.Left.Set( x + 20f + ( 48f - text_wid / 2f ), 0f );
		}


		private void SetLayoutPositions( bool is_chest ) {
			var mymod = ExtensibleInventoryMod.Instance;

			float off_x = is_chest ? mymod.Config.ChestOnOffsetX : 0;
			float off_y = is_chest ? mymod.Config.ChestOnOffsetY : 0;
			float x = mymod.Config.PagePositionX + off_x;
			float y = mymod.Config.PagePositionY + off_y;

			this.ButtonPageLeft.Top.Set( y, 0f );
			this.ButtonPageLeft.Left.Set( x, 0f );
			this.ButtonPageRight.Top.Set( y, 0f );
			this.ButtonPageRight.Left.Set( x + 112f, 0f );
			this.ButtonPageAdd.Top.Set( y, 0f );
			this.ButtonPageAdd.Left.Set( x + 144f, 0f );
			this.ButtonPageSub.Top.Set( y, 0f );
			this.ButtonPageSub.Left.Set( x + 172f, 0f );

			if( this.ButtonBooks != null ) {
				float book_x = mymod.Config.BookPositionX + off_x;
				float book_y = mymod.Config.BookPositionY + off_y;

				int i = 0;
				foreach( var button in this.ButtonBooks.Values ) {
					button.Top.Set( book_y, 0f );
					button.Left.Set( book_x + ( i * 24 ), 0f );
					i++;
				}
			}
		}
	}
}
