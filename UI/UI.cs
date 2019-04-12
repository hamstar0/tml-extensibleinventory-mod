﻿using ExtensibleInventory.UI.Elements;
using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.TmlHelpers;
using HamstarHelpers.Services.Promises;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;


namespace ExtensibleInventory.UI {
	partial class InventoryUI : UIState {
		internal Texture2D ButtonBookTex;
		internal Texture2D ButtonBookDimTex;
		internal Texture2D ButtonBookLitTex;
		internal Texture2D ButtonPageRightTex;
		internal Texture2D ButtonPageLeftTex;
		internal Texture2D ButtonPageAddTex;
		internal Texture2D ButtonPageSubTex;
		internal Texture2D TogglePageSharingOnTex;
		internal Texture2D TogglePageSharingOffTex;

		private IDictionary<string, UIInventoryControlButton> ButtonBooks = null;
		private UIText PageDisplay;
		private UIInventoryControlButton ButtonPageLeft;
		private UIInventoryControlButton ButtonPageRight;
		private UIInventoryControlButton ButtonPageAdd;
		private UIInventoryControlButton ButtonPageSub;

		private bool IsChest = true;



		////////////////

		public InventoryUI() : base() {
			var mymod = ExtensibleInventoryMod.Instance;

			this.ButtonBookTex = ModLoader.GetTexture( "Terraria/Item_531" );   // Spell Tome
			this.ButtonBookDimTex = ModLoader.GetTexture( "Terraria/Item_1313" );   // Book of Skulls
			this.ButtonBookLitTex = ModLoader.GetTexture( "Terraria/Item_1336" );   // Golden Shower
			this.ButtonPageRightTex = mymod.GetTexture( "UI/ButtonRight" );
			this.ButtonPageLeftTex = mymod.GetTexture( "UI/ButtonLeft" );
			this.ButtonPageAddTex = mymod.GetTexture( "UI/ButtonAdd" );
			this.ButtonPageSubTex = mymod.GetTexture( "UI/ButtonSub" );
			this.TogglePageSharingOnTex = mymod.GetTexture( "UI/ToggleOnSharing" );
			this.TogglePageSharingOffTex = mymod.GetTexture( "UI/ToggleOffSharing" );

			Promises.AddWorldUnloadEachPromise( () => {
				this.ButtonBooks = null;
			} );
		}


		////////////////

		public bool IsHoveringAnyControl() {
			var mymod = ExtensibleInventoryMod.Instance;

			if( !Main.playerInventory ) {
				return false;
			}

			if( this.ButtonPageLeft.IsMouseHovering && mymod.Config.CanScrollPages ) {
				return true;
			}
			if( this.ButtonPageRight.IsMouseHovering && mymod.Config.CanScrollPages ) {
				return true;
			}
			if( this.ButtonPageAdd.IsMouseHovering && mymod.Config.CanAddPages ) {
				return true;
			}
			if( this.ButtonPageSub.IsMouseHovering && mymod.Config.CanDeletePages ) {
				return true;
			}
			if( this.ButtonBooks != null && mymod.Config.CanSwitchBooks ) {
				foreach( var button in this.ButtonBooks.Values ) {
					if( button.IsMouseHovering ) {
						return true;
					}
				}
			}
			return false;
		}


		////////////////

		public override void Update( GameTime gameTime ) {
			if( this.ButtonBooks == null && Main.LocalPlayer != null && Main.LocalPlayer.active ) {
				this.InitializeLibraryBooks();
			}

			this.UpdateLayout();

			base.Update( gameTime );
		}
	}
}
