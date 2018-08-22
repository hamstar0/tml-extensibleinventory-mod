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
	internal class InventoryPageScrollerUI : UIState {
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


		public override void OnInitialize() {
			var mymod = ExtensibleInventoryMod.Instance;

			this.PageDisplay = new UIText( "0 / 0" );
			this.ButtonPageLeft = new UIImageButton( this.ButtonPageLeftTex );
			this.ButtonPageLeft.OnClick += delegate ( UIMouseEvent evt, UIElement listening_elem ) {
				var myplayer2 = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
				myplayer2.Library.CurrentBook.ScrollPageUp( Main.LocalPlayer );
			};
			this.ButtonPageRight = new UIImageButton( this.ButtonPageRightTex );
			this.ButtonPageRight.OnClick += delegate ( UIMouseEvent evt, UIElement listening_elem ) {
				var myplayer2 = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
				myplayer2.Library.CurrentBook.ScrollPageDown( Main.LocalPlayer );
			};
			this.ButtonPageAdd = new UIImageButton( this.ButtonPageAddTex );
			this.ButtonPageAdd.OnClick += delegate ( UIMouseEvent evt, UIElement listening_elem ) {
				var myplayer2 = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
				myplayer2.Library.CurrentBook.InsertAtCurrentPagePosition( Main.LocalPlayer );
			};
			this.ButtonPageSub = new UIImageButton( this.ButtonPageSubTex );
			this.ButtonPageSub.OnClick += delegate ( UIMouseEvent evt, UIElement listening_elem ) {
				var myplayer2 = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
				myplayer2.Library.CurrentBook.DeleteCurrentPage( Main.LocalPlayer );
			};

			this.ApplyLayout( false );

			base.Append( this.PageDisplay );
			base.Append( this.ButtonPageLeft );
			base.Append( this.ButtonPageRight );
			base.Append( this.ButtonPageAdd );
			base.Append( this.ButtonPageSub );
		}


		private void InitializeLibraryBooks() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
			IDictionary<string, InventoryBook> enabled_books = myplayer.Library.EnabledBooks;
			int i = 0;

			if( enabled_books.Count == 1 ) {
				return;
			}

			this.ButtonBooks = new Dictionary<string, UIImageButton>();

			foreach( string book_name in enabled_books.Keys ) {
				Texture2D tex = book_name == myplayer.Library.CurrentBook.Name ?
					this.ButtonBookLitTex : this.ButtonBookTex;
				
				var button = new UIImageButton( tex );
				button.OnClick += delegate ( UIMouseEvent evt, UIElement listening_elem ) {
					var myplayer2 = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
					myplayer2.Library.ChangeBook( book_name );

					this.ButtonBooks[ book_name ].SetImage( this.ButtonBookTex );
					button.SetImage( this.ButtonBookLitTex );
				};

				this.ButtonBooks[ book_name ] = button;
				base.Append( button );
				i++;
			}
		}


		////////////////
		
		private void RefreshLayout() {
			bool is_chest = true;
			if( Main.LocalPlayer != null && Main.LocalPlayer.active ) {
				is_chest = Main.LocalPlayer.chest != -1;
			}

			if( this.IsChest != is_chest ) {
				this.IsChest = is_chest;
				
				this.ApplyLayout( is_chest );

				this.Recalculate();
			}
		}

		private void ApplyLayout( bool is_chest ) {
			var mymod = ExtensibleInventoryMod.Instance;

			float off_x = is_chest ? mymod.Config.ChestOnOffsetX : 0;
			float off_y = is_chest ? mymod.Config.ChestOnOffsetY : 0;
			float x = mymod.Config.PagePositionX + off_x;
			float y = mymod.Config.PagePositionY + off_y;

			this.ButtonPageLeft.Top.Set( y, 0f );
			this.ButtonPageLeft.Left.Set( x, 0f );
			this.ButtonPageRight.Top.Set( y, 0f );
			this.ButtonPageRight.Left.Set( x + 112f, 0f );
			this.ButtonPageAdd.Top.Set( y, 0f );
			this.ButtonPageAdd.Left.Set( x + 144f, 0f );
			this.ButtonPageSub.Top.Set( y, 0f );
			this.ButtonPageSub.Left.Set( x + 172f, 0f );

			if( this.ButtonBooks != null ) {
				float book_x = mymod.Config.BookPositionX + off_x;
				float book_y = mymod.Config.BookPositionY + off_y;

				int i = 0;
				foreach( var button in this.ButtonBooks.Values ) {
					button.Top.Set( book_y, 0f );
					button.Left.Set( book_x + (i * 24), 0f );
					i++;
				}
			}

			if( Main.LocalPlayer != null && Main.LocalPlayer.active ) {
				var myplayer = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();

				string page_text = myplayer.Library.CurrentBook.RenderPagePosition();
				float text_wid = Main.fontMouseText.MeasureString( page_text ).X;
				
				this.PageDisplay.SetText( page_text );
				this.PageDisplay.Top.Set( y, 0f );
				this.PageDisplay.Left.Set( x + 20f + ( 48f - text_wid / 2f ), 0f );
			}
		}


		////////////////

		public override void Update( GameTime gameTime ) {
			if( this.ButtonBooks == null && Main.LocalPlayer != null && Main.LocalPlayer.active ) {
				this.InitializeLibraryBooks();
			}

			this.RefreshLayout();

			base.Update( gameTime );
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
