using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace ExtensibleInventory {
	partial class ExtensibleInventoryPlayer : ModPlayer {
		public static int BasePageCapacity { get { return 40; } }



		////////////////

		public bool IsInventoryEmpty() {
			for( int i = 10; i < 50; i++ ) {
				if( !this.player.inventory[i].IsAir ) {
					return false;
				}
			}
			return true;
		}

		public bool IsPageEmpty( int page_num ) {
			var page = this.Pages[page_num];

			for( int i=0; i<ExtensibleInventoryPlayer.BasePageCapacity; i++ ) {
				if( !page[i].IsAir ) {
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
			var mymod = (ExtensibleInventoryMod)this.mod;

			if( !mymod.Config.CanScrollPages ) {
				Main.NewText( "Inventory scrolling disabled.", Color.Red );
				return;
			}
			if( !this.CanScrollPages() ) {
				return;
			}
			if( this.CurrentPage <= 0 ) {
				return;
			}

			this.DumpInventoryToPage( this.CurrentPage-- );
			this.DumpPageToInventory( this.CurrentPage );
		}

		public void ScrollPageDown() {
			var mymod = (ExtensibleInventoryMod)this.mod;

			if( !mymod.Config.CanScrollPages ) {
				Main.NewText( "Inventory scrolling disabled.", Color.Red );
				return;
			}
			if( !this.CanScrollPages() ) {
				return;
			}
			if( this.CurrentPage >= (this.Pages.Count - 1) ) {
				return;
			}

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

		public bool CanAddPage( int page_num ) {
			var mymod = (ExtensibleInventoryMod)this.mod;
			
			if( !mymod.Config.CanAddPages ) {
				return false;
			}

			return this.Pages.Count < mymod.Config.MaxPages;
		}

		public bool CanDeletePage( int page_num ) {
			var mymod = (ExtensibleInventoryMod)this.mod;

			if( !mymod.Config.CanDeletePages ) {
				return false;
			}

			return this.Pages.Count > 1 && this.IsInventoryEmpty();
		}

		////////////////

		public bool InsertAtCurrentPage() {
			var mymod = (ExtensibleInventoryMod)this.mod;

			if( !mymod.Config.CanAddPages ) {
				Main.NewText( "Page adding disabled.", Color.Red );
				return false;
			}
			if( !this.CanAddPage( this.CurrentPage ) ) {
				Main.NewText( "Max pages reached.", Color.Red );
				return false;
			}

			this.DumpInventoryToPage( this.CurrentPage );
			this.Pages.Insert( this.CurrentPage, this.CreateBlankPage() );

			return true;
		}

		public bool DeleteCurrentPage() {
			var mymod = (ExtensibleInventoryMod)this.mod;

			if( !mymod.Config.CanDeletePages ) {
				Main.NewText( "Page deletion disabled.", Color.Red );
				return false;
			}
			if( !this.CanDeletePage( this.CurrentPage ) ) {
				Main.NewText( "Cannot delete non-empty inventory pages.", Color.Red );
				return false;
			}

			this.Pages.RemoveAt( this.CurrentPage );

			if( this.CurrentPage >= this.Pages.Count ) {
				this.CurrentPage = this.Pages.Count - 1;
			}
			this.DumpPageToInventory( this.CurrentPage );

			return true;
		}


		////////////////

		public string RenderPagePosition() {
			return (this.CurrentPage+1) + " / " + this.Pages.Count;
		}
	}
}
