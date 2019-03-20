using Terraria;


namespace ExtensibleInventory.Inventory {
	partial class InventoryBook {
		public int CountPages() {
			return this.Pages.Count;
		}


		////////////////

		public bool CanScrollPages( out string err ) {
			if( !this.IsEnabled ) {
				err = this.Name + " inventory extension disabled.";
				return false;
			}

			var mymod = ExtensibleInventoryMod.Instance;

			if( !mymod.Config.CanScrollPages ) {
				err = "Page scrolling disabled.";
				return false;
			}

			err = "";
			return true;
		}

		public bool CanAddPage( Player player, int pageNum, out string err ) {
			if( !this.IsEnabled ) {
				err = this.Name + " inventory extension disabled.";
				return false;
			}

			var mymod = ExtensibleInventoryMod.Instance;

			if( !mymod.Config.CanAddPages ) {
				err = "Page adding disabled.";
				return false;
			}

			bool isMaxed = this.Pages.Count >= mymod.Config.MaxPages;
			
			if( isMaxed ) {
				err = "Max pages reached.";
				return false;
			}

			err = "";
			return true;
		}

		public bool CanDeletePage( Player player, int pageNum, out string err ) {
			if( !this.IsEnabled ) {
				err = this.Name+" inventory extension disabled.";
				return false;
			}

			var mymod = ExtensibleInventoryMod.Instance;

			if( !mymod.Config.CanDeletePages ) {
				err = "Page deletion disabled.";
				return false;
			}

			bool minPages = this.Pages.Count > 1;

			if( !minPages ) {
				err = "Too few pages to delete any more.";
				return false;
			}

			bool isEmpty = InventoryBook.IsPlayerInventoryEmpty( player );

			if( !isEmpty ) {
				err = "Cannot delete non-empty inventory pages.";
				return false;
			}

			err = "";
			return true;
		}


		////////////////

		public Item[] GetPageItems( int pageNum ) {
			return this.Pages[ pageNum ];
		}

		public bool IsPageEmpty( Player player, int pageNum ) {
			var page = this.Pages[pageNum];
			
			if( pageNum == this.CurrentPageIdx ) {
				page = player.inventory;
			}

			for( int i = 0; i < InventoryBook.BasePageCapacity; i++ ) {
				if( !page[i].IsAir ) {
					return false;
				}
			}
			return true;
		}

		public float GaugePageFullness( Player player, int pageNum ) {
			var page = this.Pages[pageNum];
			int slots = 0;

			if( pageNum == this.CurrentPageIdx ) {
				page = player.inventory;
			}

			for( int i = 0; i < InventoryBook.BasePageCapacity; i++ ) {
				if( !page[i].IsAir ) {
					slots++;
				}
			}

			return slots / (float)InventoryBook.BasePageCapacity;
		}


		////////////////

		public string PagePositionToString() {
			return ( this.CurrentPageIdx + 1 ) + " / " + this.Pages.Count;
		}
	}
}
