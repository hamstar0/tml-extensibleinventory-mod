using HamstarHelpers.Components.Errors;
using System.Collections.Generic;
using System.Linq;
using Terraria;


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
				throw new HamstarException( "ExtensibleInventory.InventoryLibrary.AddBook - Book by name " + book_name + " already added." );
			}

			this.Books[ book_name ] = new InventoryBook( is_enabled, book_name );
		}

		public bool RemoveBook( string book_name ) {
			if( !this.Books.ContainsKey( book_name ) ) {
				throw new HamstarException( "ExtensibleInventory.InventoryLibrary.RemoveBook - No such book by name " + book_name );
			}
			if( book_name == "Default" ) {
				throw new HamstarException( "ExtensibleInventory.InventoryLibrary.RemoveBook - Cannot delete Default book; try disabling instead." );
			}

			return this.Books.Remove( book_name );
		}


		////////////////

		public void EnableBook( string book_name ) {
			if( !this.Books.ContainsKey( book_name ) ) {
				throw new HamstarException( "ExtensibleInventory.InventoryLibrary.EnableBook - No such book by name " + book_name );
			}

			InventoryBook book = this.Books[book_name];

			book.IsEnabled = true;
		}

		public void DisableBook( string book_name ) {
			if( !this.Books.ContainsKey( book_name ) ) {
				throw new HamstarException( "ExtensibleInventory.InventoryLibrary.DisableBook - No such book by name " + book_name );
			}

			InventoryBook book = this.Books[book_name];

			book.IsEnabled = false;
		}


		////////////////

		public int CountBookPages( string book_name ) {
			if( !this.Books.ContainsKey( book_name ) ) {
				throw new HamstarException( "ExtensibleInventory.InventoryLibrary.CountBookPages - No such book by name " + book_name );
			}

			return this.Books.Count;
		}

		
		public bool AddBookPage( Player player, string book_name, out string err ) {
			if( !this.Books.ContainsKey( book_name ) ) {
				throw new HamstarException( "ExtensibleInventory.InventoryLibrary.AddBookPage - No such book by name " + book_name );
			}

			InventoryBook book = this.Books[book_name];

			return book.InsertNewPage( player, book.CountPages() - 1, out err );
		}

		public bool RemoveLatestBookPage( Player player, string book_name, out string err ) {
			if( !this.Books.ContainsKey( book_name ) ) {
				throw new HamstarException( "ExtensibleInventory.InventoryLibrary.RemoveLatestBookPage - No such book by name " + book_name );
			}

			InventoryBook book = this.Books[ book_name ];

			return book.DeleteEmptyPage( player, book.CountPages() - 1, out err );
		}

		////////////////
		
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
