using HamstarHelpers.Services.Promises;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;


namespace ExtensibleInventory {
	partial class InventoryPageScrollerUI : UIState {
		internal Texture2D ButtonBookTex;
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
		
		public override void Update( GameTime game_time ) {
			if( this.ButtonBooks == null && Main.LocalPlayer != null && Main.LocalPlayer.active ) {
				this.InitializeLibraryBooks();
			}

			this.UpdateLayout();

			base.Update( game_time );
		}


		public override void Draw( SpriteBatch sb ) {
			base.Draw( sb );

			var mymod = ExtensibleInventoryMod.Instance;
			var pos = new Vector2( Main.mouseX, Main.mouseY + 16 );

			if( this.ButtonPageLeft.IsMouseHovering ) {
				string text = "Scroll inventory up" + (mymod.Config.CanScrollPages ? "" : " (disabled)");
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}
			if( this.ButtonPageRight.IsMouseHovering ) {
				string text = "Scroll inventory down" + ( mymod.Config.CanScrollPages ? "" : " (disabled)" );
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}
			if( this.ButtonPageAdd.IsMouseHovering ) {
				string text = "Add new inventory page" + ( mymod.Config.CanAddPages ? "" : " (disabled)" );
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}
			if( this.ButtonPageSub.IsMouseHovering ) {
				string text = "Remove current inventory page" + ( mymod.Config.CanDeletePages ? "" : " (disabled)" );
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}

			if( this.ButtonBooks != null ) {
				foreach( var kv in this.ButtonBooks ) {
					if( kv.Value.IsMouseHovering ) {
						sb.DrawString( Main.fontMouseText, kv.Key, pos, Color.White );
					}
				}
			}
		}
	}
}
