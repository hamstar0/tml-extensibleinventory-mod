using HamstarHelpers.Components.Errors;
using HamstarHelpers.Helpers.DotNetHelpers;
using System.Collections.Generic;
using System.Linq;
using Terraria;


namespace ExtensibleInventory.Inventory {
	partial class InventoryLibrary {
		public void ChangeCurrentBook( [Nullable]Player player, string bookName ) {
			if( !this.Books.ContainsKey( bookName ) ) {
				throw new HamstarException( "No such book by name " + bookName );
			}

			this.CurrentBook.PullFromInventoryToCurrentPage( player );

			this.CurrBookName = bookName;

			this.CurrentBook.PushCurrentPageToInventory( player );
		}


		////////////////

		public void AddBook( string bookName, bool isEnabled ) {
			if( this.Books.ContainsKey( bookName ) ) {
				throw new HamstarException( "Book by name " + bookName + " already added." );
			}

			this.Books[ bookName ] = new InventoryBook( isEnabled, bookName );
		}

		public bool RemoveBook( string bookName ) {
			if( !this.Books.ContainsKey( bookName ) ) {
				throw new HamstarException( "No such book by name " + bookName );
			}
			if( bookName == "Default" ) {
				throw new HamstarException( "Cannot delete Default book; try disabling instead." );
			}

			return this.Books.Remove( bookName );
		}


		////////////////

		public void EnableBook( string bookName ) {
			if( !this.Books.ContainsKey( bookName ) ) {
				throw new HamstarException( "No such book by name " + bookName );
			}

			InventoryBook book = this.Books[bookName];

			book.IsEnabled = true;
		}

		public void DisableBook( string bookName ) {
			if( !this.Books.ContainsKey( bookName ) ) {
				throw new HamstarException( "No such book by name " + bookName );
			}

			InventoryBook book = this.Books[bookName];

			book.IsEnabled = false;
		}


		////////////////
		
		public bool AddBookPage( Player player, string bookName, out string err ) {
			if( !this.Books.ContainsKey( bookName ) ) {
				throw new HamstarException( "No such book by name " + bookName );
			}

			InventoryBook book = this.Books[bookName];

			return book.InsertNewPage( player, book.CountPages() - 1, out err );
		}

		public bool RemoveLatestBookPage( Player player, string bookName, out string err ) {
			if( !this.Books.ContainsKey( bookName ) ) {
				throw new HamstarException( "No such book by name " + bookName );
			}

			InventoryBook book = this.Books[ bookName ];

			return book.DeleteEmptyPage( player, book.CountPages() - 1, out err );
		}
	}
}
