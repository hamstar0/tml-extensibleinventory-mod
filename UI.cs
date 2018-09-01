using HamstarHelpers.Helpers.DebugHelpers;
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
		public static void DrawX( SpriteBatch sb, UIElement elem ) {
			sb.DrawString( Main.fontMouseText, "X", elem.GetOuterDimensions().Position() + new Vector2( 4, -2 ), Color.Red, 0f, default( Vector2 ), 1.25f, SpriteEffects.None, 1f );
		}



		////////////////

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


		public override void Draw( SpriteBatch sb ) {
			base.Draw( sb );

			Player plr = Main.LocalPlayer;
			var mymod = ExtensibleInventoryMod.Instance;
			var pos = new Vector2( Main.mouseX, Main.mouseY + 16 );

			if( this.ButtonPageLeft.IsMouseHovering ) {
				string text = "Scroll inventory up";
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}
			if( this.ButtonPageRight.IsMouseHovering ) {
				string text = "Scroll inventory down";
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}
			if( this.ButtonPageAdd.IsMouseHovering ) {
				string text = "Add new inventory page";
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}
			if( this.ButtonPageSub.IsMouseHovering ) {
				string text = "Remove current inventory page";
				sb.DrawString( Main.fontMouseText, text, pos, Color.White );
			}

			if( !mymod.Config.CanScrollPages ) {
				InventoryPageScrollerUI.DrawX( sb, this.ButtonPageLeft );
				InventoryPageScrollerUI.DrawX( sb, this.ButtonPageRight );
			}
			if( !mymod.Config.CanAddPages ) {
				InventoryPageScrollerUI.DrawX( sb, this.ButtonPageAdd );
			}
			if( !mymod.Config.CanDeletePages ) {
				InventoryPageScrollerUI.DrawX( sb, this.ButtonPageSub );
			}

			if( plr != null && plr.active ) {
				var myplayer = plr.GetModPlayer<ExtensibleInventoryPlayer>();

				if( !myplayer.Library.CurrentBook.IsBookEnabled() ) {
					InventoryPageScrollerUI.DrawX( sb, this.ButtonPageLeft );
					InventoryPageScrollerUI.DrawX( sb, this.ButtonPageRight );
					InventoryPageScrollerUI.DrawX( sb, this.ButtonPageAdd );
					InventoryPageScrollerUI.DrawX( sb, this.ButtonPageSub );
				}

				if( this.ButtonBooks != null ) {
					foreach( var kv in this.ButtonBooks ) {
						string book_name = kv.Key;
						UIImageButton book_button = kv.Value;

						if( book_button.IsMouseHovering ) {
							sb.DrawString( Main.fontMouseText, book_name, pos, Color.White );
						}

						if( !myplayer.Library.IsBookEnabled( book_name ) ) {
							InventoryPageScrollerUI.DrawX( sb, book_button );
						}
					}
				}
			}
		}
	}
}
