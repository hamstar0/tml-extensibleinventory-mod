using HamstarHelpers.Components.Network;
using Terraria;


namespace ExtensibleInventory.NetProtocol {
	class ModSettingsProtocol : PacketProtocolRequestToServer {
		public ExtensibleInventoryConfigData Settings;


		////////////////

		private ModSettingsProtocol() { }

		protected override void InitializeServerSendData( int to_who ) {
			this.Settings = ExtensibleInventoryMod.Instance.Config;
		}

		////////////////

		protected override void ReceiveReply() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();

			mymod.ConfigJson.SetData( this.Settings );

			//myplayer.FinishModSettingsSync();
		}
	}
}
