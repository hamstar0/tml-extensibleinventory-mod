using Terraria;


namespace ExtensibleInventory {
	partial class InventoryBook {
		public bool IsBookEnabled() {
			if( this.Name == "Default" ) {
				return ExtensibleInventoryMod.Instance.Config.DefaultBookEnabled;
			} else {
				return this.IsEnabled;
			}
		}



		public int CountPages() {
			return this.Pages.Count;
		}


		////////////////

		public bool CanScrollPages( out string err ) {
			if( !this.IsBookEnabled() ) {
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

		public bool CanAddPage( Player player, int page_num, out string err ) {
			if( !this.IsBookEnabled() ) {
				err = this.Name + " inventory extension disabled.";
				return false;
			}

			var mymod = ExtensibleInventoryMod.Instance;

			if( !mymod.Config.CanAddPages ) {
				err = "Page adding disabled.";
				return false;
			}

			bool is_maxed = this.Pages.Count >= mymod.Config.MaxPages;
			
			if( is_maxed ) {
				err = "Max pages reached.";
				return false;
			}

			err = "";
			return true;
		}

		public bool CanDeletePage( Player player, int page_num, out string err ) {
			if( !this.IsBookEnabled() ) {
				err = this.Name+" inventory extension disabled.";
				return false;
			}

			var mymod = ExtensibleInventoryMod.Instance;

			if( !mymod.Config.CanDeletePages ) {
				err = "Page deletion disabled.";
				return false;
			}

			bool min_pages = this.Pages.Count > 1;

			if( !min_pages ) {
				err = "Too few pages to delete any more.";
				return false;
			}

			bool is_empty = this.IsPlayerInventoryEmpty( player );

			if( !is_empty ) {
				err = "Cannot delete non-empty inventory pages.";
				return false;
			}

			err = "";
			return true;
		}


		////////////////

		public Item[] GetPageItems( int page_num ) {
			return this.Pages[page_num];
		}

		public bool IsPageEmpty( int page_num ) {
			var page = this.Pages[page_num];

			for( int i = 0; i < InventoryBook.BasePageCapacity; i++ ) {
				if( !page[i].IsAir ) {
					return false;
				}
			}
			return true;
		}

		public bool IsPlayerInventoryEmpty( Player player ) {
			for( int i = 10; i < 50; i++ ) {
				if( !player.inventory[i].IsAir ) {
					return false;
				}
			}
			return true;
		}


		////////////////

		public bool InsertNewPage( Player player, int page_num, out string err ) {
			if( !this.CanAddPage( player, page_num, out err ) ) {
				return false;
			}

			var mymod = ExtensibleInventoryMod.Instance;
			
			this.DumpInventoryToPage( player, page_num );
			this.Pages.Insert( page_num, this.CreateBlankPage() );
			
			return true;
		}

		public bool DeleteEmptyPage( Player player, int page_num, out string err ) {
			var mymod = ExtensibleInventoryMod.Instance;
			
			if( !this.CanDeletePage( player, this.CurrentPageIdx, out err ) ) {
				return false;
			}

			this.Pages.RemoveAt( page_num );

			if( this.CurrentPageIdx >= this.Pages.Count ) {
				this.CurrentPageIdx = this.Pages.Count - 1;

				this.DumpPageToInventory( player, this.CurrentPageIdx );
			} else {
				this.DumpPageToInventory( player, page_num );
			}
			
			return true;
		}

		
		////////////////

		public void DumpInventoryToCurrentPage( Player player ) {
			this.DumpInventoryToPage( player, this.CurrentPageIdx );
		}
		public void DumpCurrentPageToInventory( Player player ) {
			this.DumpPageToInventory( player, this.CurrentPageIdx );
		}


		////////////////

		public string RenderPagePosition() {
			return ( this.CurrentPageIdx + 1 ) + " / " + this.Pages.Count;
		}
	}
}
