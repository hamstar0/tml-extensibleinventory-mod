using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;


namespace ExtensibleInventory.Inventory {
	partial class InventoryBook {
		public bool IsEnabled { get; internal set; }
		public string Name { get; private set; }

		public int CurrentPageIdx { get; private set; }

		////////////////

		private readonly IList<InventoryPage> Pages = new List<InventoryPage>();



		////////////////

		internal InventoryBook( bool isEnabled, string bookName ) {
			this.IsEnabled = isEnabled;

			this.Pages.Add( new InventoryPage() );
			this.Pages.Add( new InventoryPage() );

			this.Name = bookName;

			this.ResetPages();
		}


		////////////////

		public void PullFromInventoryToCurrentPage( Player player ) {
			this.Pages[ this.CurrentPageIdx ].PullFromInventory( player );
		}
		public void PushCurrentPageToInventory( Player player ) {
			this.Pages[ this.CurrentPageIdx ].PushToInventory( player );
		}
		public void PullNonLockedInventoryToLastPage( Player player ) {
			this.Pages[ this.CurrentPageIdx - 1 ].PullNonLockedItemsFromInventory( player );
		}


		////////////////

		private void PullFromInventoryToPage( Player player, int pageNum ) {
			this.Pages[ pageNum ].PullFromInventory( player );
		}

		private void PushPageToInventory( Player player, int pageNum ) {
			this.Pages[ pageNum ].PushToInventory( player );
		}


		////////////////

		public int Share( Player player, Item item ) {
			// Do not share singular items
			if( item.maxStack == 1 ) {
				return 0;
			}

			int totalShared = 0;

			for( int i = 0; i < this.Pages.Count; i++ ) {
				InventoryPage page = this.Pages[i];
				if( !page.IsSharing ) {
					continue;
				}

				Item[] items = this.GetPageItems( player, i );
				for( int j = 0; j < items.Length; j++ ) {
					Item dest = items[j];
					if( dest.IsAir || dest.type != item.type ) {
						continue;
					}

					int availableStackSpace = dest.maxStack - dest.stack;
					if( availableStackSpace <= 0 ) {
						continue;
					}

					int transferAmt = Math.Min( availableStackSpace, item.stack );
					item.stack -= transferAmt;
					dest.stack += transferAmt;
					totalShared += transferAmt;

					if( item.stack <= 0 ) {
						item.active = false;
						break;
					}
				}

				if( !item.active ) {
					break;
				}
			}

			return totalShared;
		}


		////////////////

		public IEnumerable<Item> GetSharedItems( Player player, bool excludeCurrentPage ) {
			var pages = new List<Item[]>();

			for( int i=0; i<this.Pages.Count; i++ ) {
				if( excludeCurrentPage && i == this.CurrentPageIdx ) {
					continue;
				}

				if( this.Pages[i].IsSharing || i == this.CurrentPageIdx ) {
					pages.Add( this.GetPageItems(player, i) );
				}
			}

			IEnumerable<Item> items = pages
				.SelectMany( p => p )
				.Where( item => item != null && !item.IsAir );

			return items;
		}
	}
}
