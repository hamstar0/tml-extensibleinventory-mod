using HamstarHelpers.Components.Errors;
using HamstarHelpers.Helpers.DebugHelpers;
using Terraria;
using Terraria.ModLoader.IO;


namespace ExtensibleInventory.Inventory {
	partial class InventoryBook {
		public void Load( string lowercaseBookName, TagCompound tags ) {
			string prefix;

			if( lowercaseBookName == "default" ) {
				prefix = "";
			} else {
				prefix = lowercaseBookName + "_";
			}

			if( ExtensibleInventoryMod.Instance.Config.DebugModeReset ) {
				return;
			}
			if( !tags.ContainsKey( prefix + "page_count" ) || !tags.ContainsKey( prefix + "curr_page" ) ) {
				return;
			}

			this.Pages.Clear();

			int pages = tags.GetInt( prefix + "page_count" );
			int currPage = tags.GetInt( prefix + "curr_page" );

			for( int i = 0; i < pages; i++ ) {
				InventoryPage page = new InventoryPage();
				this.Pages.Add( page );

				if( i == currPage ) { continue; }

				for( int j = 0; j < InventoryPage.BasePageCapacity; j++ ) {
					string idx = prefix + "page_" + i + "_" + j;

					if( tags.ContainsKey( idx ) ) {
						try {
							page.Items[j] = ItemIO.Load( tags.GetCompound( idx ) );
						} catch {
							throw new HamstarException( "Could not load item for book "+lowercaseBookName+" on page "+i+" at position "+j );
						}
					} else {
						page.Items[j] = new Item();
					}

					if( tags.ContainsKey( idx+"_s" ) ) {
						page.IsSharing = tags.GetBool( idx + "_s" );
					}
				}
			}
			
			this.CurrentPageIdx = currPage;
		}

		public TagCompound Save( string lowercaseBookName, TagCompound tags ) {
			string prefix;

			if( lowercaseBookName == "default" ) {
				prefix = "";
			} else {
				prefix = lowercaseBookName + "_";
			}

			tags[ prefix + "page_count" ] = this.Pages.Count;
			tags[ prefix + "curr_page" ] = this.CurrentPageIdx;

			for( int i = 0; i < this.Pages.Count; i++ ) {
				if( i == this.CurrentPageIdx ) { continue; }

				for( int j = 0; j < InventoryPage.BasePageCapacity; j++ ) {
					string idx = prefix + "page_" + i + "_" + j;

					try {
						tags[idx] = ItemIO.Save( this.Pages[i].Items[j] );
					} catch {
						LogHelpers.Warn( "Could not save item for book "+lowercaseBookName+" on page "+i+" at position "+j );
						continue;
					}

					tags[idx+"_s"] = (bool)this.Pages[i].IsSharing;
				}
			}

			return tags;
		}
	}
}
