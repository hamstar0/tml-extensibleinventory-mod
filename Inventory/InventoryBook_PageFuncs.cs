using HamstarHelpers.Helpers.DotNetHelpers;
using Terraria;


namespace ExtensibleInventory.Inventory {
	partial class InventoryBook {
		private void ResetPages() {
			this.Pages.Clear();
			this.Pages.Add( new InventoryPage() );
			this.Pages.Add( new InventoryPage() );
		}


		////////////////

		public bool InsertNewPage( [Nullable]Player player, int pageNum, out string err ) {
			if( !this.CanAddPage( pageNum, out err ) ) {
				return false;
			}

			var mymod = ExtensibleInventoryMod.Instance;
			
			this.PullFromInventoryToPage( player, pageNum );
			this.Pages.Insert( pageNum, new InventoryPage() );
			
			return true;
		}

		public bool DeleteEmptyPage( [Nullable]Player player, int pageNum, out string err ) {
			var mymod = ExtensibleInventoryMod.Instance;
			
			if( !this.CanDeletePage( player, pageNum, out err ) ) {
				return false;
			}

			this.Pages.RemoveAt( pageNum );

			if( this.CurrentPageIdx >= this.Pages.Count ) {
				this.CurrentPageIdx = this.Pages.Count - 1;

				this.PushPageToInventory( player, this.CurrentPageIdx );
			} else {
				this.PushPageToInventory( player, pageNum );
			}
			
			return true;
		}


		////////////////

		public bool SetCurrentPageOffloadable( bool on ) {
			this.Pages[ this.CurrentPageIdx ].IsOffloadable = on;
			return true;
		}
	}
}
