using HamstarHelpers.Components.Network;
using HamstarHelpers.Components.Network.Data;
using Terraria;


namespace ExtensibleInventory.NetProtocol {
	class ModSettingsProtocol : PacketProtocol {
		public ExtensibleInventoryConfigData Settings;


		////////////////

		private ModSettingsProtocol( PacketProtocolDataConstructorLock ctor_lock ) { }

		protected override void SetServerDefaults( int to_who ) {
			this.Settings = ExtensibleInventoryMod.Instance.Config;
		}

		////////////////

		protected override void ReceiveWithClient() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();

			mymod.ConfigJson.SetData( this.Settings );

			//myplayer.FinishModSettingsSync();
		}
	}
}
