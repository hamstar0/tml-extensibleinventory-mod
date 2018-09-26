using ExtensibleInventory.Inventory;
using HamstarHelpers.Helpers.DebugHelpers;
using Terraria;
using Terraria.GameInput;
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


		////////////////

		public override void SyncPlayer( int to_who, int from_who, bool new_player ) {
			if( Main.netMode == 2 ) {
				if( to_who == -1 && from_who == this.player.whoAmI ) {
					this.OnConnectServer();
				}
			}
		}

		public override void OnEnterWorld( Player player ) {
			if( player.whoAmI != Main.myPlayer ) { return; }
			if( this.player.whoAmI != Main.myPlayer ) { return; }

			if( Main.netMode == 0 ) {
				this.OnConnectSingle();
			} else if( Main.netMode == 1 ) {
				this.OnConnectClient();
			}
		}


		////////////////

		public override bool PreItemCheck() {
			if( Main.dedServ ) { return base.PreItemCheck(); }

			var mymod = (ExtensibleInventoryMod)this.mod;
			return !mymod.InvPageScroller.IsHoveringAnyControl();
		}


		////////////////

		public override void SetControls() {
			if( Main.mouseItem == null || Main.mouseItem.IsAir ) {
				return;
			}

			int scrolled = PlayerInput.ScrollWheelDelta;
			PlayerInput.ScrollWheelDelta = 0;

			if( scrolled >= 120 ) {
				this.Library.CurrentBook.ScrollPageUp( this.player );
			} else if( scrolled <= -120 ) {
				this.Library.CurrentBook.ScrollPageDown( this.player );
			}
		}
	}
}
