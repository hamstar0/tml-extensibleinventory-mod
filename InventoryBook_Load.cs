using HamstarHelpers.Helpers.DebugHelpers;
using Terraria;
using Terraria.ModLoader.IO;


namespace ExtensibleInventory {
	partial class InventoryBook {
		public void Load( string prefix, TagCompound tags ) {
			if( prefix == "default" ) {
				prefix = "";
			} else {
				prefix += "_";
			}

			if( ExtensibleInventoryMod.Instance.Config.DebugModeReset ) {
				return;
			}
			if( !tags.ContainsKey( prefix+"page_count" ) || !tags.ContainsKey( prefix+"curr_page" ) ) {
				return;
			}

			this.Pages.Clear();

			int pages = tags.GetInt( prefix + "page_count" );
			int curr_page = tags.GetInt( prefix + "curr_page" );

			for( int i = 0; i < pages; i++ ) {
				Item[] page = this.CreateBlankPage();
				this.Pages.Add( page );

				if( i == curr_page ) { continue; }

				for( int j = 0; j < InventoryBook.BasePageCapacity; j++ ) {
					string idx = prefix+"page_" + i + "_" + j;

					if( tags.ContainsKey( idx ) ) {
						page[j] = ItemIO.Load( tags.GetCompound( idx ) );
					} else {
						page[j] = new Item();
					}
				}
			}

			this.CurrentPageIdx = curr_page;
		}

		public TagCompound Save( string prefix, TagCompound tags ) {
			if( prefix == "default" ) {
				prefix = "";
			} else {
				prefix += "_";
			}

			tags[ prefix + "page_count" ] = this.Pages.Count;
			tags[ prefix + "curr_page" ] = this.CurrentPageIdx;

			for( int i = 0; i < this.Pages.Count; i++ ) {
				if( i == this.CurrentPageIdx ) { continue; }

				for( int j = 0; j < InventoryBook.BasePageCapacity; j++ ) {
					string idx = prefix+"page_" + i + "_" + j;

					tags[idx] = ItemIO.Save( this.Pages[i][j] );
				}
			}

			return tags;
		}
	}
}
