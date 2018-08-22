﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;


namespace ExtensibleInventory {
	partial class InventoryPageScrollerUI : UIState {
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
				Main.NewText( "Inventory page "+myplayer2.Library.CurrentBook.CurrentPageIdx+" added.", Color.LimeGreen );
			};

			this.ButtonPageSub = new UIImageButton( this.ButtonPageSubTex );
			this.ButtonPageSub.OnClick += delegate ( UIMouseEvent evt, UIElement listening_elem ) {
				var myplayer2 = Main.LocalPlayer.GetModPlayer<ExtensibleInventoryPlayer>();
				myplayer2.Library.CurrentBook.DeleteCurrentPage( Main.LocalPlayer );
				Main.NewText( "Inventory page "+myplayer2.Library.CurrentBook.CurrentPageIdx+" removed.", Color.LimeGreen );
			};

			this.SetLayoutPositions( false );
			this.SetPageDisplayPosition( false );

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
	}
}
