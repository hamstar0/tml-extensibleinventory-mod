﻿using ExtensibleInventory.UI.Elements;
using HamstarHelpers.Helpers.TmlHelpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;


namespace ExtensibleInventory.UI {
	partial class InventoryUI : UIState {
		public override void OnInitialize() {
			var mymod = ExtensibleInventoryMod.Instance;

			this.PageDisplay = new UIText( "0 / 0" );

			this.ButtonPageLeft = new UIInventoryControlButton( this.ButtonPageLeftTex );
			this.ButtonPageLeft.OnClick += ( evt, elem ) => {
				this.ScrollPageUp();
			};

			this.ButtonPageRight = new UIInventoryControlButton( this.ButtonPageRightTex );
			this.ButtonPageRight.OnClick += ( evt, elem ) => {
				this.ScrollPageDown();
			};

			this.ButtonPageAdd = new UIInventoryControlButton( this.ButtonPageAddTex );
			this.ButtonPageAdd.OnClick += ( evt, elem ) => {
				this.AddPage();
			};

			this.ButtonPageSub = new UIInventoryControlButton( this.ButtonPageSubTex );
			this.ButtonPageSub.OnClick += ( evt, elem ) => {
				this.DelPage();
			};

			this.TogglePageSharing = new UIInventoryControlToggle( this.TogglePageSharingOnTex, this.TogglePageSharingOffTex );
			this.TogglePageSharing.OnToggleOn += ( evt, elem ) => {
				this.TogglePageSharingOn();
			};
			this.TogglePageSharing.OnToggleOff += ( evt, elem ) => {
				this.TogglePageSharingOff();
			};

			this.SetElementPositions( false );
			this.SetPageTabsPosition( false );

			base.Append( this.PageDisplay );
			base.Append( this.ButtonPageLeft );
			base.Append( this.ButtonPageRight );
			base.Append( this.ButtonPageAdd );
			base.Append( this.ButtonPageSub );
			base.Append( this.TogglePageSharing );
		}


		private void InitializeLibraryBooks() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( Main.LocalPlayer );
			IList<string> allBookNames = myplayer.Library.GetBookNames();

			if( allBookNames.Count == 1 ) {
				return;
			}

			this.ButtonBooks = new Dictionary<string, UIInventoryControlButton>();

			int i = 0;

			foreach( string bookName in allBookNames ) {
				string currBookName = bookName;
				bool isCurrentBook = bookName == myplayer.Library.CurrentBook.Name;

				Texture2D tex = isCurrentBook ?
					this.ButtonBookLitTex :
					myplayer.Library.IsBookEnabled( bookName ) ?
						this.ButtonBookDimTex :
						this.ButtonBookTex;
				
				var button = new UIInventoryControlButton( tex );
				button.OnClick += delegate ( UIMouseEvent evt, UIElement listeningElem ) {
					string err;
					var myplayer2 = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( Main.LocalPlayer );

					if( !myplayer2.Library.CanSwitchBooks(out err) ) {
						Main.NewText( err, Color.Red );
					}

					myplayer2.Library.ChangeCurrentBook( Main.LocalPlayer, currBookName );

					this.ButtonBooks[ currBookName ].SetImage( this.ButtonBookTex );
					button.SetImage( this.ButtonBookLitTex );
				};

				this.ButtonBooks[ bookName ] = button;
				base.Append( button );

				i++;
			}
		}
	}
}
