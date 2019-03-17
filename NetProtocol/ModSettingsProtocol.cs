using HamstarHelpers.Components.Network;
using HamstarHelpers.Helpers.TmlHelpers;
using Terraria;


namespace ExtensibleInventory.NetProtocol {
	class ModSettingsProtocol : PacketProtocolRequestToServer {
		public ExtensibleInventoryConfigData Settings;


		////////////////

		private ModSettingsProtocol() { }

		protected override void InitializeServerSendData( int toWho ) {
			this.Settings = ExtensibleInventoryMod.Instance.Config;
		}

		////////////////

		protected override void ReceiveReply() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod, "ExtensibleInventoryPlayer" );

			mymod.ConfigJson.SetData( this.Settings );

			//myplayer.FinishModSettingsSync();
		}
	}
}
