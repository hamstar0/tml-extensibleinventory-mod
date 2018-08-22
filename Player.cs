using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace ExtensibleInventory {
	partial class ExtensibleInventoryPlayer : ModPlayer {
		internal InventoryLibrary Library;
		


		////////////////

		public override bool CloneNewInstances { get { return false; } }

		public override void Initialize() {
			this.Library = new InventoryLibrary();
		}

		////////////////

		public override void Load( TagCompound tags ) {
			this.Library.Load( tags );
		}

		public override TagCompound Save() {
			var tags = new TagCompound();

			return this.Library.Save( tags );
		}
	}
}
