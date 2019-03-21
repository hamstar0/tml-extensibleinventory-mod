using HamstarHelpers.Components.Errors;
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

		public bool IsBookEnabled( string bookName ) {
			if( !this.Books.ContainsKey( bookName ) ) {
				throw new HamstarException( "No such book by name " + bookName );
			}

			InventoryBook book = this.Books[ bookName ];

			return book.IsEnabled;
		}


		////////////////

		public int CountBookPages( string bookName ) {
			if( !this.Books.ContainsKey( bookName ) ) {
				throw new HamstarException( "No such book by name " + bookName );
			}

			return this.Books.Count;
		}

		////////////////
		
		public Item[] GetLatestBookPageItems( Player player, string bookName ) {
			if( this.Books.ContainsKey( bookName ) ) {
				throw new HamstarException( "No such book by name " + bookName );
			}

			InventoryBook book = this.Books[bookName];
			int count = book.CountPages();
			if( count == 0 ) {
				return null;
			}

			return book.GetPageItems( player, count - 1 );
		}
	}
}
