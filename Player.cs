using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace ExtensibleInventory {
	partial class ExtensibleInventoryPlayer : ModPlayer {
		private readonly IList<Item[]> Pages = new List<Item[]>();
		private int CurrentPage = 0;
		private int MaxPage = 2;



		////////////////

		public override bool CloneNewInstances { get { return false; } }

		public override void Initialize() {
			this.ResetPages();
		}

		////////////////

		public override void Load( TagCompound tags ) {
			if( !tags.ContainsKey("page_count") || !tags.ContainsKey("curr_page") ) {
				return;
			}

			this.Pages.Clear();

			int pages = tags.GetInt( "page_count" );
			int curr_page = tags.GetInt( "curr_page" );

			for( int i=0; i<pages; i++ ) {
				Item[] page = new Item[ ExtensibleInventoryPlayer.BasePageCapacity ];
				this.Pages.Add( page );

				if( i == curr_page ) { continue; }
				
				for( int j=0; j<ExtensibleInventoryPlayer.BasePageCapacity; j++ ) {
					string idx = "page_" + i + "_" + j;

					if( tags.ContainsKey(idx) ) {
						page[j] = ItemIO.Load( tags.GetCompound( idx ) );
					} else {
						page[j] = new Item();
					}
				}
			}

			this.MaxPage = this.Pages.Count;
		}

		public override TagCompound Save() {
			var tags = new TagCompound {
				{ "page_count", this.Pages.Count },
				{ "curr_page", this.CurrentPage }
			};

			for( int i=0; i<this.Pages.Count; i++ ) {
				if( i == this.CurrentPage ) { continue; }

				for( int j=0; j<ExtensibleInventoryPlayer.BasePageCapacity; j++ ) {
					string idx = "page_" + i + "_" + j;

					tags[ idx ] = ItemIO.Save( this.Pages[i][j] );
				}
			}

			return tags;
		}
	}
}
