using ExtensibleInventory.Inventory;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Messages.Inbox;
using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace ExtensibleInventory {
	partial class ExtensibleInventoryPlayer : ModPlayer {
		public const int ScrollModeMaxDuration = 2 * 60;



		////////////////

		public static bool IsPlayerInventoryEmpty( Player player ) {
			for( int i = 10; i < 50; i++ ) {
				if( !player.inventory[i].IsAir ) {
					return false;
				}
			}
			return true;
		}



		////////////////

		internal InventoryLibrary Library;


		////////////////

		public int ScrollModeDuration { get; private set; } = 0;

		public bool ScrollModeOn => this.ScrollModeDuration > 0;

		public override bool CloneNewInstances => false;



		////////////////
		
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

		public override void SyncPlayer( int toWho, int fromWho, bool newPlayer ) {
			if( Main.netMode == 2 ) {
				if( toWho == -1 && fromWho == this.player.whoAmI ) {
					this.OnConnectServer();
				}
			}
		}

		public override void OnEnterWorld( Player player ) {
			if( player.whoAmI != Main.myPlayer ) { return; }
			if( this.player.whoAmI != Main.myPlayer ) { return; }

			var mymod = (ExtensibleInventoryMod)this.mod;

			if( !mymod.Config.EnableSharedInventoryRecipesViaRecipeHack ) {
				InboxMessages.SetMessage( "ExtensibleInventoryRecipeHackAlert",
					"As of v1.4.1.3, Extensible Inventory disables recipes between shared pages, due to compatibility issues. If you want to risk using it, simply enable the EnableSharedInventoryRecipesViaRecipeHack setting.",
					false
				);
			}

			if( Main.netMode == 0 ) {
				this.OnConnectSingle();
			} else if( Main.netMode == 1 ) {
				this.OnConnectClient();
			}

			string msg = "You can now quickly scroll inventory pages by selecting an item and using the mouse wheel while viewing your inventory.";
			InboxMessages.SetMessage( "ExtensibleInventoryScroll", msg, false );
		}


		////////////////

		public override void PreUpdate() {
			var mymod = (ExtensibleInventoryMod)this.mod;

			if( mymod.Config.ScrollModeEnabled ) {
				if( this.ScrollModeDuration > 0 ) {
					this.ScrollModeDuration--;
				}

				if( Main.playerInventory ) {
					if( Main.mouseItem != null && !Main.mouseItem.IsAir ) {
						this.ScrollModeDuration = ( this.ScrollModeDuration < 60 ) ? 60 : this.ScrollModeDuration;
					}
				} else {
					this.ScrollModeDuration = 0;
				}

				float minX = 20;
				float minY = 20;
				float maxX = minX + (14f * 56f) * Main.inventoryScale * Main.UIScale;
				float maxY = minY + (8.5f * 56f) * Main.inventoryScale * Main.UIScale;

				if( Main.mouseX < minX || Main.mouseX >= maxX || Main.mouseY < minY || Main.mouseY >= maxY ) {
					this.ScrollModeDuration = 0;
				}
			}
		}


		////////////////

		public override void ModifyDrawInfo( ref PlayerDrawInfo drawInfo ) {
			if( !Main.dedServ ) {
				try {
					var mymod = (ExtensibleInventoryMod)this.mod;
					if( mymod.InvUI?.IsHoveringAnyControl() ?? false ) {
						this.player.mouseInterface = true;
					}
				} catch( Exception e ) {
					LogHelpers.Warn( "What's this doing here? - " + e.ToString() );
				}
			}
			base.ModifyDrawInfo( ref drawInfo );
		}


		////////////////

		public override void SetControls() {
			var mymod = (ExtensibleInventoryMod)this.mod;

			if( mymod.Config.ScrollModeEnabled && this.ScrollModeOn && Main.playerInventory ) {
				this.CaptureMouseScrollForScrollMode();
			}
		}

		private void CaptureMouseScrollForScrollMode() {
			var mymod = (ExtensibleInventoryMod)this.mod;
			int scrolled = PlayerInput.ScrollWheelDelta;

			PlayerInput.ScrollWheelDelta = 0;

			if( Main.playerInventory ) {
				if( scrolled >= 120 ) {
					this.ScrollModeDuration = ExtensibleInventoryPlayer.ScrollModeMaxDuration;
					mymod.InvUI?.ScrollPageUp();
				} else if( scrolled <= -120 ) {
					this.ScrollModeDuration = ExtensibleInventoryPlayer.ScrollModeMaxDuration;
					mymod.InvUI?.ScrollPageDown();
				}
			}
		}
	}
}
