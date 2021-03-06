using System.Collections.Generic;
using Terraria;


namespace ExtensibleInventory.Inventory {
	partial class InventoryBook {
		public int CountPages() {
			return this.Pages.Count;
		}


		////////////////

		public bool CanScrollPages( out string err ) {
			if( !this.IsEnabled ) {
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

		public bool CanAddPage( int pageNum, out string err ) {
			if( !this.IsEnabled ) {
				err = this.Name + " inventory extension disabled.";
				return false;
			}

			var mymod = ExtensibleInventoryMod.Instance;

			if( !mymod.Config.CanAddPages ) {
				err = "Page adding disabled.";
				return false;
			}

			bool isMaxed = this.Pages.Count >= mymod.Config.MaxPages;
			
			if( isMaxed ) {
				err = "Max pages reached.";
				return false;
			}

			err = "";
			return true;
		}

		public bool CanDeletePage( Player player, int pageNum, out string err ) {
			if( !this.IsEnabled ) {
				err = this.Name+" inventory extension disabled.";
				return false;
			}

			var mymod = ExtensibleInventoryMod.Instance;

			if( !mymod.Config.CanDeletePages ) {
				err = "Page deletion disabled.";
				return false;
			}

			bool minPages = this.Pages.Count > 1;

			if( !minPages ) {
				err = "Too few pages to delete any more.";
				return false;
			}

			bool isEmpty = ExtensibleInventoryPlayer.IsPlayerInventoryEmpty( player );

			if( !isEmpty ) {
				err = "Cannot delete non-empty inventory pages.";
				return false;
			}

			err = "";
			return true;
		}


		////////////////

		public Item[] GetPageItems( Player player, int pageNum ) {
			if( pageNum == this.CurrentPageIdx ) {
				return player.inventory;
			} else {
				return this.Pages[ pageNum ].Items;
			}
		}

		public bool IsPageEmpty( int pageNum ) {
			if( pageNum == this.CurrentPageIdx ) {
				return InventoryPage.IsItemSetEmpty( Main.LocalPlayer.inventory );
			} else {
				return this.Pages[pageNum].IsEmpty();
			}
		}

		public float GaugePageFullness( int pageNum ) {
			if( pageNum == this.CurrentPageIdx ) {
				return InventoryPage.GaugeItemSetFullness( Main.LocalPlayer.inventory );
			} else {
				return this.Pages[ pageNum ].GaugeFullness();
			}
		}


		////////////////

		public bool IsCurrentPageSharing() {
			return this.Pages[ this.CurrentPageIdx ].IsSharing;
		}

		public bool IsPageSharing( int pageNum ) {
			return this.Pages[ pageNum ].IsSharing;
		}

		public IList<InventoryPage> GetSharingPages( out bool includesCurrent ) {
			var pages = new List<InventoryPage>( this.Pages.Count );
			includesCurrent = false;

			for( int i=0; i<this.Pages.Count; i++ ) {
				InventoryPage page = this.Pages[i];

				if( page.IsSharing ) {
					if( i == this.CurrentPageIdx ) {
						includesCurrent = true;
					}
					pages.Add( page );
				}
			}

			return pages;
		}
		
		
		////////////////

		public string PagePositionToString() {
			return ( this.CurrentPageIdx + 1 ) + " / " + this.Pages.Count;
		}
	}
}
