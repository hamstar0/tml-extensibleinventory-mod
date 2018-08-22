using HamstarHelpers.Helpers.DebugHelpers;
using Terraria;
using Terraria.ModLoader.IO;


namespace ExtensibleInventory {
	partial class InventoryBook {
		internal string Name;



		////////////////
		
		public InventoryBook( string book_name ) {
			this.Name = book_name;

			this.ResetPages();
		}


		////////////////

		public void Load( string prefix, TagCompound tags ) {
			if( !tags.ContainsKey( prefix+"_page_count" ) || !tags.ContainsKey( prefix+"_curr_page" ) ) {
				return;
			}

			this.Pages.Clear();

			int pages = tags.GetInt( "page_count" );
			int curr_page = tags.GetInt( "curr_page" );

			for( int i = 0; i < pages; i++ ) {
				Item[] page = this.CreateBlankPage();
				this.Pages.Add( page );

				if( i == curr_page ) { continue; }

				for( int j = 0; j < InventoryBook.BasePageCapacity; j++ ) {
					string idx = "page_" + i + "_" + j;

					if( tags.ContainsKey( idx ) ) {
						page[j] = ItemIO.Load( tags.GetCompound( idx ) );
					} else {
						page[j] = new Item();
					}
				}
			}

			this.CurrPageIdx = curr_page;
			this.MaxPageIdx = this.Pages.Count;
		}

		public TagCompound Save( string prefix, TagCompound tags ) {
			tags[ prefix + "_page_count" ] = this.Pages.Count;
			tags[ prefix + "_curr_page" ] = this.CurrPageIdx;

			for( int i = 0; i < this.Pages.Count; i++ ) {
				if( i == this.CurrPageIdx ) { continue; }

				for( int j = 0; j < InventoryBook.BasePageCapacity; j++ ) {
					string idx = prefix+"_page_" + i + "_" + j;

					tags[idx] = ItemIO.Save( this.Pages[i][j] );
				}
			}

			return tags;
		}
	}
}
