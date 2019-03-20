using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.TmlHelpers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace ExtensibleInventory.UI {
	partial class InventoryUI : UIState {
		private void ScrollPageUp() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod, "ExtensibleInventoryPlayer" );
			myplayer.Library.CurrentBook.ScrollPageUp( Main.LocalPlayer );
		}

		private void ScrollPageDown() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod, "ExtensibleInventoryPlayer" );
			myplayer.Library.CurrentBook.ScrollPageDown( Main.LocalPlayer );
		}


		private void AddPage() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod, "ExtensibleInventoryPlayer" );

			if( myplayer.Library.CurrentBook.InsertAtCurrentPagePosition( Main.LocalPlayer ) ) {
				Main.NewText( "Inventory page " + myplayer.Library.CurrentBook.CurrentPageIdx + " added.", Color.LimeGreen );
			}
		}

		private void DelPage() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod, "ExtensibleInventoryPlayer" );

			if( myplayer.Library.CurrentBook.DeleteCurrentPage( Main.LocalPlayer ) ) {
				Main.NewText( "Inventory page " + myplayer.Library.CurrentBook.CurrentPageIdx + " removed.", Color.LimeGreen );
			}
		}


		private void ToggleOffloadablePageOff() {
			this.TogglePageOffload.OnToggleOff += ( evt, elem ) => {
				var mymod = ExtensibleInventoryMod.Instance;
				var myplayer = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod, "ExtensibleInventoryPlayer" );

				if( myplayer.Library.CurrentBook.SetCurrentPageOffloadable( false ) ) {
					Main.NewText( "Inventory page " + myplayer.Library.CurrentBook.CurrentPageIdx + " rejects offloading.", Color.LimeGreen );
				}
			};
		}

		private void ToggleOffloadablePageOn() {
			this.TogglePageOffload.OnToggleOff += ( evt, elem ) => {
				var mymod = ExtensibleInventoryMod.Instance;
				var myplayer = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod, "ExtensibleInventoryPlayer" );

				if( myplayer.Library.CurrentBook.SetCurrentPageOffloadable( true ) ) {
					Main.NewText( "Inventory page " + myplayer.Library.CurrentBook.CurrentPageIdx + " accepts offloading.", Color.LimeGreen );
				}
			};
		}
	}
}
