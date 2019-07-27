using Terraria.ModLoader;


namespace ExtensibleInventory {
	partial class ExtensibleInventoryPlayer : ModPlayer {
		private void OnConnectSingle() {
			var mymod = (ExtensibleInventoryMod)this.mod;

			mymod.Config.ReadyingForLocalPlayerUse();
		}

		private void OnConnectClient() {
		}

		private void OnConnectServer() {
		}
	}
}
