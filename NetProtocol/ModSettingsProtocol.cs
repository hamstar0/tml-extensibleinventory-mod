using HamstarHelpers.Components.Network;
using HamstarHelpers.Components.Network.Data;
using Terraria;


namespace ExtensibleInventory.NetProtocol {
	class ModSettingsProtocol : PacketProtocolRequestToServer {
		public ExtensibleInventoryConfigData Settings;


		////////////////

		protected ModSettingsProtocol( PacketProtocolDataConstructorLock ctor_lock ) : base( ctor_lock ) { }

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
