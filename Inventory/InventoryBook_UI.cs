using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Services.Timers;
using Microsoft.Xna.Framework;
using Terraria;


namespace ExtensibleInventory.Inventory {
	partial class InventoryBook {
		public bool ScrollPageUp( Player player ) {
			if( Timers.GetTimerTickDuration("ExtensibleInventoryPageScrollFrom") > 0
				|| Timers.GetTimerTickDuration("ExtensibleInventoryPageScrollTo") > 0 ) {
				return false;
			}
			
			string err;
			int curr_page_idx = this.CurrentPageIdx;

			if( !this.CanScrollPages( out err ) ) {
				Main.NewText( err, Color.Red );
				return false;
			}
			if( curr_page_idx <= 0 ) {
				return false;
			}
			
			this.DumpInventoryToPage( player, curr_page_idx );

			Timers.SetTimer( "ExtensibleInventoryPageScrollFrom", 10, () => {
				if( !InventoryBook.IsPlayerInventoryEmpty( player ) ) {
					this.DumpInventoryToPage( player, curr_page_idx );
					return true;
				}

				this.CurrentPageIdx = curr_page_idx - 1;
				this.DumpPageToInventory( player, this.CurrentPageIdx );

				return false;
			} );

			return true;
		}

		public bool ScrollPageDown( Player player ) {
			if( Timers.GetTimerTickDuration("ExtensibleInventoryPageScrollFrom") > 0
				|| Timers.GetTimerTickDuration("ExtensibleInventoryPageScrollTo") > 0 ) {
				return false;
			}
			
			string err;
			int curr_page_idx = this.CurrentPageIdx;

			if( !this.CanScrollPages( out err ) ) {
				Main.NewText( err, Color.Red );
				return false;
			}
			if( curr_page_idx >= ( this.Pages.Count - 1 ) ) {
				return false;
			}

			this.DumpInventoryToPage( player, curr_page_idx );

			Timers.SetTimer( "ExtensibleInventoryPageScrollFrom", 10, () => {
				if( !InventoryBook.IsPlayerInventoryEmpty( player ) ) {
					this.DumpInventoryToPage( player, curr_page_idx );
					return true;
				}

				this.CurrentPageIdx = curr_page_idx + 1;
				this.DumpPageToInventory( player, this.CurrentPageIdx );

				return false;
			} );

			return true;
		}


		////////////////

		public bool InsertAtCurrentPagePosition( Player player ) {
			string err;
			bool success = this.InsertNewPage( player, this.CurrentPageIdx, out err );

			if( !success ) {
				Main.NewText( err, Color.Red );
			}

			return success;
		}

		public bool DeleteCurrentPage( Player player ) {
			string err;
			bool success = this.DeleteEmptyPage( player, this.CurrentPageIdx, out err );

			if( !success ) {
				Main.NewText( err, Color.Red );
			}

			return success;
		}
	}
}
