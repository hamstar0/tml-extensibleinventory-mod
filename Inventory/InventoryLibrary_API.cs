using HamstarHelpers.Components.Errors;
using HamstarHelpers.Helpers.DotNetHelpers;
using System.Collections.Generic;
using System.Linq;
using Terraria;


namespace ExtensibleInventory.Inventory {
	partial class InventoryLibrary {
		public bool CanSwitchBooks( out string err ) {
			var mymod = ExtensibleInventoryMod.Instance;

			if( !mymod.Config.CanSwitchBooks ) {
				err = "Book switching disabled.";
				return false;
			}

			err = "";
			return true;
		}



		////////////////

		public IList<string> GetBookNames() {
			return this.Books.Keys.ToList();
		}

		////////////////

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

		public bool IsBookEnabled( string bookName ) {
			if( !this.Books.ContainsKey( bookName ) ) {
				throw new HamstarException( "No such book by name " + bookName );
			}

			InventoryBook book = this.Books[ bookName ];

			return book.IsEnabled;
		}

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

		public int CountBookPages( string bookName ) {
			if( !this.Books.ContainsKey( bookName ) ) {
				throw new HamstarException( "No such book by name " + bookName );
			}

			return this.Books.Count;
		}

		
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

		////////////////
		
		public Item[] GetLatestBookPageItems( string bookName ) {
			if( this.Books.ContainsKey( bookName ) ) {
				throw new HamstarException( "No such book by name " + bookName );
			}

			InventoryBook book = this.Books[bookName];
			int count = book.CountPages();
			if( count == 0 ) {
				return null;
			}

			return book.GetPageItems( count - 1 );
		}
	}
}
