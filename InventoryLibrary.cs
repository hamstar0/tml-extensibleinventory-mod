using System.Collections.Generic;
using Terraria.ModLoader.IO;


namespace ExtensibleInventory {
	partial class InventoryLibrary {
		private IDictionary<int, InventoryBook> Books = new Dictionary<int, InventoryBook>();

		private int CurrBookIdx = 0;
		
		////////////////

		public InventoryBook CurrentBook { get { return this.Books[this.CurrBookIdx]; } }




		////////////////
		
		public void Initialize() {
			this.Books[0] = new InventoryBook();
		}

		////////////////

		public void Load( TagCompound tags ) {
			if( !tags.ContainsKey("book_count") || !tags.ContainsKey("curr_book") ) {
				return;
			}

			this.Books.Clear();

			int book_count = tags.GetInt( "book_count" );
			int curr_book = tags.GetInt( "curr_book" );

			for( int i=0; i< book_count; i++ ) {
				var book = new InventoryBook();

				book.Load( i + "", tags );
			}

			this.CurrBookIdx = curr_book;
		}

		public TagCompound Save( TagCompound tags ) {
			tags["book_count"] = this.Books.Count;
			tags["curr_book"] = this.CurrBookIdx;

			for( int i=0; i<this.Books.Count; i++ ) {
				this.Books[i].Save( i + "", tags );
			}

			return tags;
		}


		////////////////
	}
}
