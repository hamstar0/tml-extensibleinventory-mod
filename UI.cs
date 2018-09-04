﻿using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Services.Promises;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;


namespace ExtensibleInventory {
	partial class InventoryPageScrollerUI : UIState {
		internal Texture2D ButtonBookTex;
		internal Texture2D ButtonBookDimTex;
		internal Texture2D ButtonBookLitTex;
		internal Texture2D ButtonPageRightTex;
		internal Texture2D ButtonPageLeftTex;
		internal Texture2D ButtonPageAddTex;
		internal Texture2D ButtonPageSubTex;
		
		private IDictionary<string, UIImageButton> ButtonBooks = null;
		private UIText PageDisplay;
		private UIImageButton ButtonPageLeft;
		private UIImageButton ButtonPageRight;
		private UIImageButton ButtonPageAdd;
		private UIImageButton ButtonPageSub;

		private bool IsChest = true;



		////////////////

		public InventoryPageScrollerUI( ExtensibleInventoryMod mymod ) : base() {
			this.ButtonBookTex = ModLoader.GetTexture( "Terraria/Item_531" );   // Spell Tome
			this.ButtonBookDimTex = ModLoader.GetTexture( "Terraria/Item_1313" );   // Book of Skulls
			this.ButtonBookLitTex = ModLoader.GetTexture( "Terraria/Item_1336" );   // Golden Shower
			this.ButtonPageRightTex = mymod.GetTexture( "ButtonRight" );
			this.ButtonPageLeftTex = mymod.GetTexture( "ButtonLeft" );
			this.ButtonPageAddTex = mymod.GetTexture( "ButtonAdd" );
			this.ButtonPageSubTex = mymod.GetTexture( "ButtonSub" );

			Promises.AddWorldUnloadEachPromise( () => {
				this.ButtonBooks = null;
			} );
		}


		////////////////

		public bool IsHoveringAnyControl() {
			if( this.ButtonPageLeft.IsMouseHovering ) {
				return true;
			}
			if( this.ButtonPageRight.IsMouseHovering ) {
				return true;
			}
			if( this.ButtonPageAdd.IsMouseHovering ) {
				return true;
			}
			if( this.ButtonPageSub.IsMouseHovering ) {
				return true;
			}
			if( this.ButtonBooks != null ) {
				foreach( var button in this.ButtonBooks.Values ) {
					if( button.IsMouseHovering ) {
						return true;
					}
				}
			}
			return false;
		}


		////////////////

		public override void Update( GameTime game_time ) {
			if( this.ButtonBooks == null && Main.LocalPlayer != null && Main.LocalPlayer.active ) {
				this.InitializeLibraryBooks();
			}

			this.UpdateLayout();

			base.Update( game_time );
		}
	}
}
