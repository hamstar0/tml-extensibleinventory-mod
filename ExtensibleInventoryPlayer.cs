using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace ExtensibleInventory {
	class ExtensibleInventoryPlayer : ModPlayer {
		public const int BasePageCapacity = 40;



		////////////////

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
	}
}
