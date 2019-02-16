using HamstarHelpers.Helpers.DebugHelpers;
using System.Collections.Generic;
using Terraria;


namespace ExtensibleInventory.Inventory {
	partial class InventoryBook {
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

		private void DumpInventoryToPage( Player player, int pageNum ) {
			for( int i=10; i<50; i++ ) {
				this.Pages[ pageNum ][ i - 10 ] = player.inventory[ i ];
				player.inventory[ i ] = new Item();
			}
		}

		private void DumpPageToInventory( Player player, int pageNum ) {
			for( int i=0; i< InventoryBook.BasePageCapacity; i++ ) {
				player.inventory[i + 10] = this.Pages[pageNum][i];
				this.Pages[pageNum][i] = new Item();
			}
		}
	}
}
