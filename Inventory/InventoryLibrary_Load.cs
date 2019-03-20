using HamstarHelpers.Helpers.DotNetHelpers;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader.IO;


namespace ExtensibleInventory.Inventory {
	partial class InventoryLibrary {
		internal void Load( TagCompound tags ) {
			var mymod = ExtensibleInventoryMod.Instance;

			if( !tags.ContainsKey("book_count") || !tags.ContainsKey("curr_book") ) {
				var book = new InventoryBook( mymod.Config.DefaultBookEnabled, InventoryLibrary.DefaultBookName );

				book.Load( InventoryLibrary.DefaultBookName.ToLower(), tags );
				this.Books[ InventoryLibrary.DefaultBookName ] = book;

				return;
			}

			this.Books.Clear();

			int bookCount = tags.GetInt( "book_count" );
			string currBookName = tags.GetString( "curr_book" );

			for( int i=0; i< bookCount; i++ ) {
				string bookName = tags.GetString( "book_name_" + i );
				string bookNameIo = bookName.ToLower();

				bool isEnabled = false;
				if( bookName == InventoryLibrary.DefaultBookName ) {
					isEnabled = mymod.Config.DefaultBookEnabled;
				}

				var book = new InventoryBook( isEnabled, bookName );

				book.Load( bookNameIo, tags );

				this.Books[ bookName ] = book;
			}

			this.CurrBookName = currBookName;
		}

		internal TagCompound Save( TagCompound tags ) {
			tags["book_count"] = this.Books.Count;
			tags["curr_book"] = this.CurrBookName;

			int i = 0;
			foreach( var kv in this.Books ) {
				string bookName = kv.Key;
				string bookNameIo = bookName.ToLower();

				InventoryBook book = kv.Value;

				tags["book_name_" + i] = bookName;
				book.Save( bookNameIo, tags );

				i++;
			}

			return tags;
		}
	}
}
