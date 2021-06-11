using System.Collections.Generic;


namespace ExtensibleInventory.Inventory {
	partial class InventoryLibrary {
		public const string DefaultBookName = "Default";



		////////////////
		
		private IDictionary<string, InventoryBook> Books = new Dictionary<string, InventoryBook>();
		private string CurrBookName = InventoryLibrary.DefaultBookName;
		

		////////////////
		
		public InventoryBook CurrentBook => this.Books[ this.CurrBookName ];



		////////////////

		internal InventoryLibrary() {
			this.Books[ this.CurrBookName ] = new InventoryBook(
				ExtensibleInventoryMod.Instance.Config.DefaultBookEnabled,
				this.CurrBookName
			);
		}
	}
}
