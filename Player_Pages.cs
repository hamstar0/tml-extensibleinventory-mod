using Terraria;
using Terraria.ModLoader;


namespace ExtensibleInventory {
	partial class ExtensibleInventoryPlayer : ModPlayer {
		public const int BasePageCapacity = 40;



		////////////////

		private Item[] CreateBlankPage() {
			var page = new Item[ExtensibleInventoryPlayer.BasePageCapacity];

			for( int i = 0; i < ExtensibleInventoryPlayer.BasePageCapacity; i++ ) {
				page[i] = new Item();
			}

			return page;
		}

		private void ResetPages() {
			this.Pages.Clear();
			this.Pages.Add( this.CreateBlankPage() );
			this.Pages.Add( this.CreateBlankPage() );

			this.MaxPage = this.Pages.Count;
		}


		////////////////

		public void ScrollPageUp() {
			if( this.CurrentPage <= 0 ) { return; }

			this.DumpInventoryToPage( this.CurrentPage-- );
			this.DumpPageToInventory( this.CurrentPage );
		}

		public void ScrollPageDown() {
			if( this.CurrentPage >= (this.Pages.Count - 1) ) { return; }

			this.DumpInventoryToPage( this.CurrentPage++ );
			this.DumpPageToInventory( this.CurrentPage );
		}


		////////////////

		private void DumpInventoryToPage( int page_num ) {
			for( int i=10; i<50; i++ ) {
				this.Pages[page_num][i - 10] = this.player.inventory[i];
				this.player.inventory[i] = new Item();
			}
		}

		private void DumpPageToInventory( int page_num ) {
			for( int i=0; i<ExtensibleInventoryPlayer.BasePageCapacity; i++ ) {
				this.player.inventory[i + 10] = this.Pages[page_num][i];
				this.Pages[page_num][i] = new Item();
			}
		}


		////////////////

		public string RenderPagePosition() {
			return (this.CurrentPage+1) + " / " + this.Pages.Count;
		}
	}
}
