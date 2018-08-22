using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;


namespace ExtensibleInventory {
	partial class InventoryBook {
		public static int BasePageCapacity { get { return 40; } }



		////////////////

		private readonly IList<Item[]> Pages = new List<Item[]>();
		private int CurrPageIdx;
		private int MaxPageIdx;



		////////////////
		
		public InventoryBook() {
			this.Pages.Add( this.CreateBlankPage() );
			this.Pages.Add( this.CreateBlankPage() );
			this.MaxPageIdx = this.Pages.Count;
		}

		public bool IsInventoryEmpty( Player player ) {
			for( int i = 10; i < 50; i++ ) {
				if( !player.inventory[i].IsAir ) {
					return false;
				}
			}
			return true;
		}

		public bool IsPageEmpty( int page_num ) {
			var page = this.Pages[ page_num ];

			for( int i=0; i< InventoryBook.BasePageCapacity; i++ ) {
				if( !page[i].IsAir ) {
					return false;
				}
			}
			return true;
		}

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

			this.MaxPageIdx = this.Pages.Count;
		}


		////////////////

		public bool CanScrollPages() {
			var mymod = ExtensibleInventoryMod.Instance;

			return mymod.Config.CanScrollPages;
		}

		public void ScrollPageUp( Player player ) {
			var mymod = ExtensibleInventoryMod.Instance;

			if( !mymod.Config.CanScrollPages ) {
				Main.NewText( "Inventory scrolling disabled.", Color.Red );
				return;
			}
			if( !this.CanScrollPages() ) {
				return;
			}
			if( this.CurrPageIdx <= 0 ) {
				return;
			}

			this.DumpInventoryToPage( player, this.CurrPageIdx-- );
			this.DumpPageToInventory( player, this.CurrPageIdx );
		}

		public void ScrollPageDown( Player player ) {
			var mymod = ExtensibleInventoryMod.Instance;

			if( !mymod.Config.CanScrollPages ) {
				Main.NewText( "Inventory scrolling disabled.", Color.Red );
				return;
			}
			if( !this.CanScrollPages() ) {
				return;
			}
			if( this.CurrPageIdx >= (this.Pages.Count - 1) ) {
				return;
			}

			this.DumpInventoryToPage( player, this.CurrPageIdx++ );
			this.DumpPageToInventory( player, this.CurrPageIdx );
		}


		////////////////

		public void DumpInventoryToCurrentPage( Player player ) {
			this.DumpInventoryToPage( player, this.CurrPageIdx );
		}
		public void DumpCurrentPageToInventory( Player player ) {
			this.DumpPageToInventory( player, this.CurrPageIdx );
		}

		internal void DumpInventoryToPage( Player player, int page_num ) {
			for( int i=10; i<50; i++ ) {
				this.Pages[ page_num ][ i - 10 ] = player.inventory[ i ];
				player.inventory[ i ] = new Item();
			}
		}

		internal void DumpPageToInventory( Player player, int page_num ) {
			for( int i=0; i< InventoryBook.BasePageCapacity; i++ ) {
				player.inventory[i + 10] = this.Pages[page_num][i];
				this.Pages[page_num][i] = new Item();
			}
		}


		////////////////

		public bool CanAddPage( Player player, int page_num ) {
			var mymod = ExtensibleInventoryMod.Instance;

			if( !mymod.Config.CanAddPages ) {
				return false;
			}

			return this.Pages.Count < mymod.Config.MaxPages;
		}

		public bool CanDeletePage( Player player, int page_num ) {
			var mymod = ExtensibleInventoryMod.Instance;

			if( !mymod.Config.CanDeletePages ) {
				return false;
			}

			return this.Pages.Count > 1 && this.IsInventoryEmpty( player );
		}

		////////////////

		public bool InsertAtCurrentPage( Player player ) {
			var mymod = ExtensibleInventoryMod.Instance;

			if( !mymod.Config.CanAddPages ) {
				Main.NewText( "Page adding disabled.", Color.Red );
				return false;
			}
			if( !this.CanAddPage( player, this.CurrPageIdx ) ) {
				Main.NewText( "Max pages reached.", Color.Red );
				return false;
			}

			this.DumpInventoryToPage( player, this.CurrPageIdx );
			this.Pages.Insert( this.CurrPageIdx, this.CreateBlankPage() );

			return true;
		}

		public bool DeleteCurrentPage( Player player ) {
			var mymod = ExtensibleInventoryMod.Instance;

			if( !mymod.Config.CanDeletePages ) {
				Main.NewText( "Page deletion disabled.", Color.Red );
				return false;
			}
			if( !this.CanDeletePage( player, this.CurrPageIdx ) ) {
				Main.NewText( "Cannot delete non-empty inventory pages.", Color.Red );
				return false;
			}

			this.Pages.RemoveAt( this.CurrPageIdx );

			if( this.CurrPageIdx >= this.Pages.Count ) {
				this.CurrPageIdx = this.Pages.Count - 1;
			}
			this.DumpPageToInventory( player, this.CurrPageIdx );

			return true;
		}


		////////////////

		public string RenderPagePosition() {
			return (this.CurrPageIdx+1) + " / " + this.Pages.Count;
		}
	}
}
