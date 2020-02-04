using System;
using Terraria;


namespace ExtensibleInventory.Inventory {
	class InventoryPage {
		public static int BasePageCapacity => 40;



		////////////////

		public static bool IsItemSetEmpty( Item[] items ) {
			for( int i = 0; i < InventoryPage.BasePageCapacity; i++ ) {
				if( !items[i].IsAir ) {
					return false;
				}
			}
			return true;
		}

		public static float GaugeItemSetFullness( Item[] items ) {
			int slots = 0;

			for( int i = 0; i < InventoryPage.BasePageCapacity; i++ ) {
				if( !items[i].IsAir ) {
					slots++;
				}
			}

			return slots / (float)InventoryPage.BasePageCapacity;
		}



		////////////////

		public Item[] Items;
		public bool IsSharing = true;



		////////////////

		public InventoryPage() {
			this.Items = new Item[ InventoryPage.BasePageCapacity ];

			for( int i = 0; i < InventoryPage.BasePageCapacity; i++ ) {
				this.Items[i] = new Item();
			}
		}


		////////////////

		public void PullFromInventory( Player player ) {
			for( int i = 10; i < 50; i++ ) {
				Item invItem = ( !player.inventory[i]?.IsAir ?? false ) ?
					player.inventory[i].DeepClone() :
					new Item();

				this.Items[i - 10] = invItem;
				player.inventory[i] = new Item();
			}
		}

		public void PushToInventory( Player player ) {
			for( int i = 0; i < InventoryPage.BasePageCapacity; i++ ) {
				Item pageItem = ( !this.Items[i]?.IsAir ?? false ) ?
					this.Items[i].DeepClone() :
					new Item();

				player.inventory[i + 10] = pageItem;
				this.Items[i] = new Item();
			}
		}

		public void PullCleanupFromInventory(Player player)
		{
			for( int i = 10; i < 50; i++ ) {
				Item invItem;
				if (player.inventory[i] == null ||
					player.inventory[i].IsAir ||
					player.inventory[i].favorited)
				{
					invItem = new Item();
				}
				else
				{
					invItem = player.inventory[i].DeepClone();
					player.inventory[i] = new Item();
				}
					
				this.Items[i - 10] = invItem;
			}
		}

		public void FinishCleanup(Player player)
		{
			for (int i = 10; i < 50; i++)
			{
				if (player.inventory[i] == null ||
					player.inventory[i].IsAir ||
					player.inventory[i].favorited)
					continue;
				else
					player.inventory[i].TurnToAir();
			}
		}


		////////////////

		public bool IsEmpty() {
			return InventoryPage.IsItemSetEmpty( this.Items );
		}

		public float GaugeFullness() {
			return InventoryPage.GaugeItemSetFullness( this.Items );
		}
	}
}
