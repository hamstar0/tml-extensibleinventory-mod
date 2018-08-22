using System.Collections.Generic;
using Terraria.ModLoader.IO;


namespace ExtensibleInventory {
	partial class InventoryLibrary {
		internal IDictionary<string, InventoryBook> Books = new Dictionary<string, InventoryBook>();

		private string CurrBookName = "Default";
		
		////////////////

		public InventoryBook CurrentBook {
			get { return this.Books[ this.CurrBookName ]; }
		}



		////////////////
		
		public void Initialize() {
			this.Books[ this.CurrBookName ] = new InventoryBook( this.CurrBookName );
		}

		////////////////

		public void Load( TagCompound tags ) {
			if( !tags.ContainsKey("book_count") || !tags.ContainsKey("curr_book") ) {
				return;
			}

			this.Books.Clear();

			int book_count = tags.GetInt( "book_count" );
			string curr_book_name = tags.GetString( "curr_book" );

			for( int i=0; i< book_count; i++ ) {
				string book_name = tags.GetString( "book_name_" + i );
				var book = new InventoryBook( book_name );

				book.Load( book_name.ToLower(), tags );

				this.Books[ book_name ] = book;
			}

			this.CurrBookName = curr_book_name;
		}

		public TagCompound Save( TagCompound tags ) {
			tags["book_count"] = this.Books.Count;
			tags["curr_book"] = this.CurrBookName;

			int i = 0;
			foreach( var kv in this.Books ) {
				string book_name = kv.Key;
				InventoryBook book = kv.Value;

				tags["book_name_" + i] = book_name;
				book.Save( book_name.ToLower(), tags );

				i++;
			}

			return tags;
		}


		////////////////
		
		public void SetBook( string name ) {
			this.CurrBookName = name;
		}
	}
}
