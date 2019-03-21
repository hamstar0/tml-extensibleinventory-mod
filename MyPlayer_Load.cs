using ExtensibleInventory.NetProtocol;
using HamstarHelpers.Components.Network;
using HamstarHelpers.Helpers.DebugHelpers;
using Terraria.ModLoader;


namespace ExtensibleInventory {
	partial class ExtensibleInventoryPlayer : ModPlayer {
		private void OnConnectSingle() {
			var mymod = (ExtensibleInventoryMod)this.mod;

			if( !mymod.ConfigJson.LoadFile() ) {
				mymod.ConfigJson.SaveFile();
				LogHelpers.Log( "Extensible Inventory config " + mymod.Version.ToString() + " created." );
			}
		}

		private void OnConnectClient() {
			PacketProtocolRequestToServer.QuickRequest<ModSettingsProtocol>( -1 );
		}

		private void OnConnectServer() { }
	}
}
