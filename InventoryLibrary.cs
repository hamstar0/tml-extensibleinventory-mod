using System.Collections.Generic;
using Terraria.ModLoader.IO;


namespace ExtensibleInventory {
	partial class InventoryLibrary {
		public const string DefaultBookName = "Default";


		////////////////
		
		private IDictionary<string, InventoryBook> Books = new Dictionary<string, InventoryBook>();

		private string CurrBookName = InventoryLibrary.DefaultBookName;

		////////////////

		public InventoryBook CurrentBook {
			get { return this.Books[ this.CurrBookName ]; }
		}



		////////////////

		internal InventoryLibrary() {
			this.Books[ this.CurrBookName ] = new InventoryBook(
				ExtensibleInventoryMod.Instance.Config.DefaultBookEnabled,
				this.CurrBookName
			);
		}

		////////////////

		internal void Load( TagCompound tags ) {
			if( !tags.ContainsKey("book_count") || !tags.ContainsKey("curr_book") ) {
				var book = new InventoryBook( ExtensibleInventoryMod.Instance.Config.DefaultBookEnabled, InventoryLibrary.DefaultBookName );

				book.Load( InventoryLibrary.DefaultBookName.ToLower(), tags );
				this.Books[ InventoryLibrary.DefaultBookName ] = book;

				return;
			}

			this.Books.Clear();

			int book_count = tags.GetInt( "book_count" );
			string curr_book_name = tags.GetString( "curr_book" );

			for( int i=0; i< book_count; i++ ) {
				string book_name = tags.GetString( "book_name_" + i );
				string book_name_io = book_name.ToLower();

				bool is_enabled = false;
				if( book_name == InventoryLibrary.DefaultBookName ) {
					is_enabled = ExtensibleInventoryMod.Instance.Config.DefaultBookEnabled;
				}

				var book = new InventoryBook( is_enabled, book_name );

				book.Load( book_name_io, tags );

				this.Books[ book_name ] = book;
			}

			this.CurrBookName = curr_book_name;
		}

		internal TagCompound Save( TagCompound tags ) {
			tags["book_count"] = this.Books.Count;
			tags["curr_book"] = this.CurrBookName;

			int i = 0;
			foreach( var kv in this.Books ) {
				string book_name = kv.Key;
				string book_name_io = book_name.ToLower();

				InventoryBook book = kv.Value;

				tags["book_name_" + i] = book_name;
				book.Save( book_name_io, tags );

				i++;
			}

			return tags;
		}
	}
}
