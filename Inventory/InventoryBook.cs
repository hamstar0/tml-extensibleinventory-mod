using HamstarHelpers.Helpers.DebugHelpers;
using System.Collections.Generic;
using Terraria;


namespace ExtensibleInventory.Inventory {
	partial class InventoryBook {
		public bool IsEnabled { get; internal set; }
		public string Name { get; private set; }

		public int CurrentPageIdx { get; private set; }

		////////////////

		private readonly IList<InventoryPage> Pages = new List<InventoryPage>();



		////////////////

		internal InventoryBook( bool isEnabled, string bookName ) {
			this.IsEnabled = isEnabled;

			this.Pages.Add( new InventoryPage() );
			this.Pages.Add( new InventoryPage() );

			this.Name = bookName;

			this.ResetPages();
		}


		////////////////

		public void PullFromInventoryToCurrentPage( Player player ) {
			this.Pages[ this.CurrentPageIdx ].PullFromInventory( player );
		}
		public void PushCurrentPageToInventory( Player player ) {
			this.Pages[ this.CurrentPageIdx ].PushToInventory( player );
		}


		////////////////

		private void PullFromInventoryToPage( Player player, int pageNum ) {
			this.Pages[ pageNum ].PullFromInventory( player );
		}

		private void PushPageToInventory( Player player, int pageNum ) {
			this.Pages[ pageNum ].PushToInventory( player );
		}
	}
}
