using HamstarHelpers.Components.Errors;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader.IO;


namespace ExtensibleInventory {
	partial class InventoryLibrary {
		public IDictionary<string, InventoryBook> EnabledBooks {
			get {
				return this.Books.Where( kv => kv.Value.IsEnabled )
					.ToDictionary( kv => kv.Key, kv => kv.Value );
			}
		}

		public InventoryBook CurrentBook {
			get { return this.Books[this.CurrBookName]; }
		}


		////////////////

		public void ChangeBook( string book_name ) {
			if( this.Books.ContainsKey( book_name ) ) {
				throw new HamstarException( "ExtensibleInventory.InventoryLibrary.ChangeBook - No such book by name " + book_name );
			}

			this.CurrentBook.DumpInventoryToCurrentPage( Main.LocalPlayer );

			this.CurrBookName = book_name;

			this.CurrentBook.DumpCurrentPageToInventory( Main.LocalPlayer );
		}


		////////////////

		public void AddBook( string book_name, bool is_enabled ) {
			if( this.Books.ContainsKey( book_name ) ) {
				throw new HamstarException( "ExtensibleInventory.InventoryLibrary.AddBook - No such book by name " + book_name );
			}

			this.Books[ book_name ] = new InventoryBook( is_enabled, book_name );
		}

		public bool RemoveBook( string book_name ) {
			if( this.Books.ContainsKey( book_name ) ) {
				throw new HamstarException( "ExtensibleInventory.InventoryLibrary.RemoveBook - No such book by name " + book_name );
			}

			return this.Books.Remove( book_name );
		}

		public int CountBookPages( string book_name ) {
			if( this.Books.ContainsKey( book_name ) ) {
				throw new HamstarException( "ExtensibleInventory.InventoryLibrary.CountBookPages - No such book by name " + book_name );
			}

			return this.Books.Count;
		}

		////////////////
		
		public void AddBookPage( Player player, string book_name ) {
			if( this.Books.ContainsKey( book_name ) ) {
				throw new HamstarException( "ExtensibleInventory.InventoryLibrary.AddBookPage - No such book by name " + book_name );
			}

			InventoryBook book = this.Books[book_name];

			book.InsertNewPage( player, book.CountPages() - 1 );
		}

		public void RemoveLatestBookPage( string book_name ) {
			if( this.Books.ContainsKey( book_name ) ) {
				throw new HamstarException( "ExtensibleInventory.InventoryLibrary.RemoveLatestBookPage - No such book by name " + book_name );
			}
		}

		public Item[] GetLatestBookPageItems( string book_name ) {
			if( this.Books.ContainsKey( book_name ) ) {
				throw new HamstarException( "ExtensibleInventory.InventoryLibrary.GetLatestBookPageItems - No such book by name " + book_name );
			}

			InventoryBook book = this.Books[book_name];
			int count = book.CountPages();
			if( count == 0 ) {
				return null;
			}

			return book.GetPageItems( count - 1 );
		}
	}
}
