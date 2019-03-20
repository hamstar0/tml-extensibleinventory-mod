using HamstarHelpers.Helpers.DebugHelpers;
using System.Collections.Generic;
using Terraria;


namespace ExtensibleInventory.Inventory {
	partial class InventoryBook {
		public static bool IsPlayerInventoryEmpty( Player player ) {
			for( int i = 10; i < 50; i++ ) {
				if( !player.inventory[i].IsAir ) {
					return false;
				}
			}
			return true;
		}



		////////////////

		public static int BasePageCapacity => 40;



		////////////////

		public bool IsEnabled { get; internal set; }
		public string Name { get; private set; }

		private readonly IList<Item[]> Pages = new List<Item[]>();

		public int CurrentPageIdx { get; private set; }



		////////////////
		
		internal InventoryBook( bool isEnabled, string bookName ) {
			this.IsEnabled = isEnabled;

			this.Pages.Add( this.CreateBlankPage() );
			this.Pages.Add( this.CreateBlankPage() );

			this.Name = bookName;

			this.ResetPages();
		}

		////////////////

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

		public void DumpInventoryToCurrentPage( Player player ) {
			this.DumpInventoryToPage( player, this.CurrentPageIdx );
		}
		public void DumpCurrentPageToInventory( Player player ) {
			this.DumpPageToInventory( player, this.CurrentPageIdx );
		}


		////////////////

		private void DumpInventoryToPage( Player player, int pageNum ) {
			for( int i=10; i<50; i++ ) {
				Item invItem = ( !player.inventory[i]?.IsAir ?? false ) ?
					player.inventory[i].DeepClone() :
					new Item();

				this.Pages[ pageNum ][ i - 10 ] = invItem;
				player.inventory[ i ] = new Item();
			}
		}

		private void DumpPageToInventory( Player player, int pageNum ) {
			for( int i=0; i< InventoryBook.BasePageCapacity; i++ ) {
				Item pageItem = ( !this.Pages[pageNum][i]?.IsAir ?? false ) ?
					this.Pages[pageNum][i].DeepClone() :
					new Item();

				player.inventory[ i + 10 ] = pageItem;
				this.Pages[ pageNum ][ i ] = new Item();
			}
		}


		////////////////

		public string PagePositionToString() {
			return ( this.CurrentPageIdx + 1 ) + " / " + this.Pages.Count;
		}
	}
}
