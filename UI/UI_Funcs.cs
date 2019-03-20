using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.TmlHelpers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace ExtensibleInventory.UI {
	partial class InventoryUI : UIState {
		public void ScrollPageUp() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod, "ExtensibleInventoryPlayer" );

			if( myplayer.Library.CurrentBook.ScrollPageUp( Main.LocalPlayer ) ) {
				bool isOffloadable = myplayer.Library.CurrentBook.IsCurrentPageOffloadable();
				this.TogglePageOffload.SetOn( !isOffloadable, false );
			}
		}

		public void ScrollPageDown() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod, "ExtensibleInventoryPlayer" );

			if( myplayer.Library.CurrentBook.ScrollPageDown( Main.LocalPlayer ) ) {
				bool isOffloadable = myplayer.Library.CurrentBook.IsCurrentPageOffloadable();
				this.TogglePageOffload.SetOn( !isOffloadable, false );
			}
		}

		////

		public void AddPage() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod, "ExtensibleInventoryPlayer" );

			if( myplayer.Library.CurrentBook.InsertAtCurrentPagePosition( Main.LocalPlayer ) ) {
				Main.NewText( "Inventory page " + myplayer.Library.CurrentBook.CurrentPageIdx + " added.", Color.LimeGreen );
			}
		}

		public void DelPage() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod, "ExtensibleInventoryPlayer" );

			if( myplayer.Library.CurrentBook.DeleteCurrentPage( Main.LocalPlayer ) ) {
				Main.NewText( "Inventory page " + myplayer.Library.CurrentBook.CurrentPageIdx + " removed.", Color.LimeGreen );
			}
		}

		////

		public void ToggleOffloadablePageOn() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod, "ExtensibleInventoryPlayer" );

			if( myplayer.Library.CurrentBook.SetCurrentPageOffloadable( true ) ) {
				Main.NewText( "Inventory page " + myplayer.Library.CurrentBook.CurrentPageIdx + " accepts auto-offloading.", Color.LimeGreen );
			}
		}

		public void ToggleOffloadablePageOff() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod, "ExtensibleInventoryPlayer" );

			if( myplayer.Library.CurrentBook.SetCurrentPageOffloadable( false ) ) {
				Main.NewText( "Inventory page " + myplayer.Library.CurrentBook.CurrentPageIdx + " rejects auto-offloading.", Color.LimeGreen );
			}
		}
	}
}
