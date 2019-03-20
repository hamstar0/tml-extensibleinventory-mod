using ExtensibleInventory.UI.Elements;
using HamstarHelpers.Helpers.TmlHelpers;
using Terraria;
using Terraria.UI;


namespace ExtensibleInventory.UI {
	partial class InventoryPageScrollerUI : UIState {
		private void UpdateLayout() {
			var mymod = ExtensibleInventoryMod.Instance;
			bool isChest = true;
			bool isPlayerValid = Main.LocalPlayer != null && Main.LocalPlayer.active;

			if( isPlayerValid ) {
				isChest = Main.LocalPlayer.chest != -1 || Main.npcShop > 0;
			}

			if( this.IsChest != isChest ) {
				this.IsChest = isChest;

				this.SetLayoutPositions( isChest );
			}

			if( isPlayerValid ) {
				this.SetPageDisplayPosition( isChest );
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


		private void SetPageDisplayPosition( bool isChest ) {
			var mymod = ExtensibleInventoryMod.Instance;
			ExtensibleInventoryPlayer myplayer = null;

			if( Main.LocalPlayer != null && Main.LocalPlayer.active ) {
				myplayer = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod, "ExtensibleInventoryPlayer" );
			}

			float offX = isChest ? mymod.Config.ChestOnOffsetX : 0;
			float offY = isChest ? mymod.Config.ChestOnOffsetY : 0;
			float x = mymod.Config.PagePositionX + offX;
			float y = mymod.Config.PagePositionY + offY;
			string pageText = this.PageDisplay.Text;

			if( myplayer != null ) {
				pageText = myplayer.Library.CurrentBook.RenderPagePosition();

				this.PageDisplay.SetText( pageText );
			}

			float textWid = Main.fontMouseText.MeasureString( pageText ).X;

			this.PageDisplay.Top.Set( y, 0f );
			this.PageDisplay.Left.Set( x + 20f + ( 48f - textWid / 2f ), 0f );
		}


		private void SetLayoutPositions( bool isChest ) {
			var mymod = ExtensibleInventoryMod.Instance;

			float offX = isChest ? mymod.Config.ChestOnOffsetX : 0;
			float offY = isChest ? mymod.Config.ChestOnOffsetY : 0;
			float x = mymod.Config.PagePositionX + offX;
			float y = mymod.Config.PagePositionY + offY;

			this.ButtonPageLeft.Top.Set( y, 0f );
			this.ButtonPageLeft.Left.Set( x, 0f );
			this.ButtonPageRight.Top.Set( y, 0f );
			this.ButtonPageRight.Left.Set( x + 112f, 0f );
			this.ButtonPageAdd.Top.Set( y, 0f );
			this.ButtonPageAdd.Left.Set( x + 144f, 0f );
			this.ButtonPageSub.Top.Set( y, 0f );
			this.ButtonPageSub.Left.Set( x + 172f, 0f );

			if( this.ButtonBooks != null ) {
				float bookX = mymod.Config.BookPositionX + offX;
				float bookY = mymod.Config.BookPositionY + offY;

				int i = 0;
				foreach( var button in this.ButtonBooks.Values ) {
					button.Top.Set( bookY, 0f );
					button.Left.Set( bookX + ( i * 24 ), 0f );
					i++;
				}
			}
		}
	}
}
