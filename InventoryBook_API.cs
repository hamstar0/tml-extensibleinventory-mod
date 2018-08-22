using Microsoft.Xna.Framework;
using Terraria;


namespace ExtensibleInventory {
	partial class InventoryBook {
		public int CountPages() {
			return this.Pages.Count;
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

		public bool InsertNewPage( Player player, int page_num ) {
			var mymod = ExtensibleInventoryMod.Instance;

			if( !mymod.Config.CanAddPages ) {
				Main.NewText( "Page adding disabled.", Color.Red );
				return false;
			}
			if( !this.CanAddPage( player, page_num ) ) {
				Main.NewText( "Max pages reached.", Color.Red );
				return false;
			}

			this.DumpInventoryToPage( player, page_num );
			this.Pages.Insert( page_num, this.CreateBlankPage() );

			return true;
		}

		public bool DeletePage( Player player, int page_num ) {
			var mymod = ExtensibleInventoryMod.Instance;

			if( !mymod.Config.CanDeletePages ) {
				Main.NewText( "Page deletion disabled.", Color.Red );
				return false;
			}
			if( !this.CanDeletePage( player, this.CurrentPageIdx ) ) {
				Main.NewText( "Cannot delete non-empty inventory pages.", Color.Red );
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
