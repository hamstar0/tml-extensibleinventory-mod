using Terraria;


namespace ExtensibleInventory.Inventory {
	partial class InventoryBook {
		private Item[] CreateBlankPage() {
			var page = new Item[InventoryBook.BasePageCapacity];

			for( int i = 0; i < InventoryBook.BasePageCapacity; i++ ) {
				page[i] = new Item();
			}

			return page;
		}

		private void ResetPages() {
			this.Pages.Clear();
			this.Pages.Add( this.CreateBlankPage() );
			this.Pages.Add( this.CreateBlankPage() );
		}


		////////////////

		public bool InsertNewPage( Player player, int pageNum, out string err ) {
			if( !this.CanAddPage( player, pageNum, out err ) ) {
				return false;
			}

			var mymod = ExtensibleInventoryMod.Instance;
			
			this.DumpInventoryToPage( player, pageNum );
			this.Pages.Insert( pageNum, this.CreateBlankPage() );
			
			return true;
		}

		public bool DeleteEmptyPage( Player player, int pageNum, out string err ) {
			var mymod = ExtensibleInventoryMod.Instance;
			
			if( !this.CanDeletePage( player, this.CurrentPageIdx, out err ) ) {
				return false;
			}

			this.Pages.RemoveAt( pageNum );

			if( this.CurrentPageIdx >= this.Pages.Count ) {
				this.CurrentPageIdx = this.Pages.Count - 1;

				this.DumpPageToInventory( player, this.CurrentPageIdx );
			} else {
				this.DumpPageToInventory( player, pageNum );
			}
			
			return true;
		}


		////////////////

		SetOffloadCurrentPage
	}
}
