using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.TmlHelpers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace ExtensibleInventory.UI {
	partial class InventoryUI : UIState {
		public void ScrollPageUp() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( Main.LocalPlayer );

			if( myplayer.Library.CurrentBook.ScrollPageUp( Main.LocalPlayer ) ) {
			}
		}

		public void ScrollPageDown() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( Main.LocalPlayer );

			if( myplayer.Library.CurrentBook.ScrollPageDown( Main.LocalPlayer ) ) {
			}
		}

		////

		public void AddPage() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( Main.LocalPlayer );

			if( myplayer.Library.CurrentBook.InsertAtCurrentPagePosition( Main.LocalPlayer ) ) {
				Main.NewText( "Inventory page " + myplayer.Library.CurrentBook.CurrentPageIdx + " added.", Color.LimeGreen );
			}
		}

		public void DelPage() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( Main.LocalPlayer );

			if( myplayer.Library.CurrentBook.DeleteCurrentPage( Main.LocalPlayer ) ) {
				Main.NewText( "Inventory page " + myplayer.Library.CurrentBook.CurrentPageIdx + " removed.", Color.LimeGreen );
			}
		}

		////

		public void TogglePageSharingOn() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( Main.LocalPlayer );

			if( myplayer.Library.CurrentBook.SetCurrentPageShared( true ) ) {
				Main.NewText( "Inventory page " + myplayer.Library.CurrentBook.CurrentPageIdx + " auto-shares items.", Color.LimeGreen );
			}
		}

		public void TogglePageSharingOff() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( Main.LocalPlayer );

			if( myplayer.Library.CurrentBook.SetCurrentPageShared( false ) ) {
				Main.NewText( "Inventory page " + myplayer.Library.CurrentBook.CurrentPageIdx + " auto-sharing disabled.", Color.Yellow );
			}
		}
	}
}
