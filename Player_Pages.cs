using Terraria;
using Terraria.ModLoader;


namespace ExtensibleInventory {
	partial class ExtensibleInventoryPlayer : ModPlayer {
		public static int BasePageCapacity { get { return 40; } }



		////////////////

		public bool IsCurrentPageEmpty() {
			var page = this.Pages[this.CurrentPage];

			for( int i=0; i<ExtensibleInventoryPlayer.BasePageCapacity; i++ ) {
				if( page[i].active ) {
					return false;
				}
			}
			return true;
		}

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

		public bool CanScrollPages() {
			var mymod = (ExtensibleInventoryMod)this.mod;

			return mymod.Config.CanScrollPages;
		}

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

		public bool CanAddPages() {
			var mymod = (ExtensibleInventoryMod)this.mod;

			if( !mymod.Config.CanAddPages ) {
				return false;
			}

			return this.Pages.Count < mymod.Config.MaxPages;
		}

		public bool CanDeleteCurrentPage() {
			var mymod = (ExtensibleInventoryMod)this.mod;

			if( !mymod.Config.CanDeletePages ) {
				return false;
			}

			return this.Pages.Count > 1 && this.IsCurrentPageEmpty();
		}

		////////////////

		public bool InsertAtCurrentPage() {
			if( !this.CanAddPages() ) {
				return false;
			}

			this.DumpInventoryToPage( this.CurrentPage );
			this.Pages.Insert( this.CurrentPage, this.CreateBlankPage() );

			return true;
		}

		public bool DeleteCurrentPage() {
			if( !this.CanDeleteCurrentPage() ) {
				return false;
			}

			this.Pages.RemoveAt( this.CurrentPage );

			if( this.CurrentPage >= this.Pages.Count ) {
				this.CurrentPage = this.Pages.Count - 1;
				this.DumpPageToInventory( this.CurrentPage );
			}

			return true;
		}


		////////////////

		public string RenderPagePosition() {
			return (this.CurrentPage+1) + " / " + this.Pages.Count;
		}
	}
}
