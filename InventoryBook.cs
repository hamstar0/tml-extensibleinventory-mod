using HamstarHelpers.Helpers.DebugHelpers;
using System.Collections.Generic;
using Terraria;


namespace ExtensibleInventory {
	partial class InventoryBook {
		public static int BasePageCapacity { get { return 40; } }



		////////////////

		public bool IsEnabled { get; internal set; }
		public string Name { get; private set; }

		private readonly IList<Item[]> Pages = new List<Item[]>();

		public int CurrentPageIdx { get; private set; }



		////////////////
		
		internal InventoryBook( bool is_enabled, string book_name ) {
			this.IsEnabled = is_enabled;

			this.Pages.Add( this.CreateBlankPage() );
			this.Pages.Add( this.CreateBlankPage() );

			this.Name = book_name;

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

		private void DumpInventoryToPage( Player player, int page_num ) {
			for( int i=10; i<50; i++ ) {
				this.Pages[ page_num ][ i - 10 ] = player.inventory[ i ];
				player.inventory[ i ] = new Item();
			}
		}

		private void DumpPageToInventory( Player player, int page_num ) {
			for( int i=0; i< InventoryBook.BasePageCapacity; i++ ) {
				player.inventory[i + 10] = this.Pages[page_num][i];
				this.Pages[page_num][i] = new Item();
			}
		}
	}
}
