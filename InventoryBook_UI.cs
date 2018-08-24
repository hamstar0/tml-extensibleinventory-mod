using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using Terraria;


namespace ExtensibleInventory {
	partial class InventoryBook {
		public bool ScrollPageUp( Player player ) {
			var mymod = ExtensibleInventoryMod.Instance;
			string err;
			
			if( !this.CanScrollPages( out err ) ) {
				Main.NewText( err, Color.Red );
				return false;
			}
			if( this.CurrentPageIdx <= 0 ) {
				return false;
			}

			this.DumpInventoryToPage( player, this.CurrentPageIdx-- );
			this.DumpPageToInventory( player, this.CurrentPageIdx );

			return true;
		}

		public bool ScrollPageDown( Player player ) {
			var mymod = ExtensibleInventoryMod.Instance;
			string err;

			if( !this.CanScrollPages( out err ) ) {
				Main.NewText( err, Color.Red );
				return false;
			}
			if( this.CurrentPageIdx >= (this.Pages.Count - 1) ) {
				return false;
			}

			this.DumpInventoryToPage( player, this.CurrentPageIdx++ );
			this.DumpPageToInventory( player, this.CurrentPageIdx );

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