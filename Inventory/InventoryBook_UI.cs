using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.DotNetHelpers;
using HamstarHelpers.Helpers.TmlHelpers;
using HamstarHelpers.Services.Timers;
using Microsoft.Xna.Framework;
using Terraria;


namespace ExtensibleInventory.Inventory {
	partial class InventoryBook {
		public const string PageScrollTimerName = "ExtensibleInventoryPageScroll";



		////////////////

		public bool ScrollPageUp( Player player ) {
			if( !LoadHelpers.IsWorldSafelyBeingPlayed() ) {
				LogHelpers.Warn( "World not in play" );
				//Main.NewText( "Could not scroll pages. Please report this as an issue.", Color.Red );
				return false;
			}
			if( Timers.GetTimerTickDuration( InventoryBook.PageScrollTimerName ) > 0 ) {
				return false;
			}

			string err;

			if( !this.CanScrollPages( out err ) ) {
				Main.NewText( err, Color.Red );
				return false;
			}
			if( this.CurrentPageIdx <= 0 ) {
				return false;
			}
			
			this.PullFromInventoryToPage( player, this.CurrentPageIdx );
			this.PushPageToInventory( player, --this.CurrentPageIdx );

			Timers.SetTimer( InventoryBook.PageScrollTimerName, 10, () => {
				return false;
			} );

			return true;
		}

		public bool ScrollPageDown( Player player ) {
			if( !LoadHelpers.IsWorldSafelyBeingPlayed() ) {
				LogHelpers.Warn( "World not in play" );
				//Main.NewText( "Could not scroll pages. Please report this as an issue.", Color.Red );
				return false;
			}
			if( Timers.GetTimerTickDuration( InventoryBook.PageScrollTimerName ) > 0 ) {
				return false;
			}

			string err;

			if( !this.CanScrollPages( out err ) ) {
				Main.NewText( err, Color.Red );
				return false;
			}
			if( this.CurrentPageIdx >= ( this.Pages.Count - 1 ) ) {
				return false;
			}

			this.PullFromInventoryToPage( player, this.CurrentPageIdx );
			this.PushPageToInventory( player, ++this.CurrentPageIdx );

			Timers.SetTimer( InventoryBook.PageScrollTimerName, 10, () => {
				return false;
			} );

			return true;
		}


		////////////////

		public bool InsertAtCurrentPagePosition( [Nullable]Player player ) {
			string err;
			bool success = this.InsertNewPage( player, this.CurrentPageIdx, out err );

			if( !success ) {
				Main.NewText( err, Color.Red );
			}

			return success;
		}

		public bool DeleteCurrentPage( [Nullable]Player player ) {
			string err;
			bool success = this.DeleteEmptyPage( player, this.CurrentPageIdx, out err );

			if( !success ) {
				Main.NewText( err, Color.Red );
			}

			return success;
		}
	}
}
