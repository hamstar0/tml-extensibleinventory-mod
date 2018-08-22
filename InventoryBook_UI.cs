using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using Terraria;


namespace ExtensibleInventory {
	partial class InventoryBook {
		public bool CanScrollPages() {
			var mymod = ExtensibleInventoryMod.Instance;

			return mymod.Config.CanScrollPages;
		}

		public void ScrollPageUp( Player player ) {
			var mymod = ExtensibleInventoryMod.Instance;

			if( !mymod.Config.CanScrollPages ) {
				Main.NewText( "Inventory scrolling disabled.", Color.Red );
				return;
			}
			if( !this.CanScrollPages() ) {
				return;
			}
			if( this.CurrentPageIdx <= 0 ) {
				return;
			}

			this.DumpInventoryToPage( player, this.CurrentPageIdx-- );
			this.DumpPageToInventory( player, this.CurrentPageIdx );
		}

		public void ScrollPageDown( Player player ) {
			var mymod = ExtensibleInventoryMod.Instance;

			if( !mymod.Config.CanScrollPages ) {
				Main.NewText( "Inventory scrolling disabled.", Color.Red );
				return;
			}
			if( !this.CanScrollPages() ) {
				return;
			}
			if( this.CurrentPageIdx >= (this.Pages.Count - 1) ) {
				return;
			}

			this.DumpInventoryToPage( player, this.CurrentPageIdx++ );
			this.DumpPageToInventory( player, this.CurrentPageIdx );
		}


		////////////////

		public bool CanAddPage( Player player, int page_num ) {
			var mymod = ExtensibleInventoryMod.Instance;

			if( !mymod.Config.CanAddPages ) {
				return false;
			}

			return this.Pages.Count < mymod.Config.MaxPages;
		}

		public bool CanDeletePage( Player player, int page_num ) {
			var mymod = ExtensibleInventoryMod.Instance;

			if( !mymod.Config.CanDeletePages ) {
				return false;
			}

			return this.Pages.Count > 1 && this.IsPlayerInventoryEmpty( player );
		}


		////////////////

		public bool InsertAtCurrentPagePosition( Player player ) {
			return this.InsertNewPage( player, this.CurrentPageIdx );
		}

		public bool DeleteCurrentPage( Player player ) {
			return this.DeletePage( player, this.CurrentPageIdx );
		}
	}
}
