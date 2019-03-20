using ExtensibleInventory.UI.Elements;
using HamstarHelpers.Helpers.TmlHelpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;


namespace ExtensibleInventory.UI {
	partial class InventoryPageScrollerUI : UIState {
		public override void OnInitialize() {
			var mymod = ExtensibleInventoryMod.Instance;

			this.PageDisplay = new UIText( "0 / 0" );

			this.ButtonPageLeft = new UIInventoryControlButton( this.ButtonPageLeftTex );
			this.ButtonPageLeft.OnClick += ( evt, elem ) => {
				var mymod2 = ExtensibleInventoryMod.Instance;
				var myplayer2 = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod2, "ExtensibleInventoryPlayer" );
				myplayer2.Library.CurrentBook.ScrollPageUp( Main.LocalPlayer );
			};

			this.ButtonPageRight = new UIInventoryControlButton( this.ButtonPageRightTex );
			this.ButtonPageRight.OnClick += ( evt, elem ) => {
				var mymod2 = ExtensibleInventoryMod.Instance;
				var myplayer2 = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod2, "ExtensibleInventoryPlayer" );
				myplayer2.Library.CurrentBook.ScrollPageDown( Main.LocalPlayer );
			};

			this.ButtonPageAdd = new UIInventoryControlButton( this.ButtonPageAddTex );
			this.ButtonPageAdd.OnClick += ( evt, elem ) => {
				var mymod2 = ExtensibleInventoryMod.Instance;
				var myplayer2 = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod2, "ExtensibleInventoryPlayer" );

				if( myplayer2.Library.CurrentBook.InsertAtCurrentPagePosition( Main.LocalPlayer ) ) {
					Main.NewText( "Inventory page " + myplayer2.Library.CurrentBook.CurrentPageIdx + " added.", Color.LimeGreen );
				}
			};

			this.ButtonPageSub = new UIInventoryControlButton( this.ButtonPageSubTex );
			this.ButtonPageSub.OnClick += ( evt, elem ) => {
				var mymod2 = ExtensibleInventoryMod.Instance;
				var myplayer2 = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod2, "ExtensibleInventoryPlayer" );

				if( myplayer2.Library.CurrentBook.DeleteCurrentPage( Main.LocalPlayer ) ) {
					Main.NewText( "Inventory page " + myplayer2.Library.CurrentBook.CurrentPageIdx + " removed.", Color.LimeGreen );
				}
			};

			this.TogglePageOffload = new UIInventoryControlToggle( this.TogglePageOffloadOnTex, this.TogglePageOffloadOffTex );
			this.TogglePageOffload.OnToggleOff += ( evt, elem ) => {
				var mymod2 = ExtensibleInventoryMod.Instance;
				var myplayer2 = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod2, "ExtensibleInventoryPlayer" );

				if( myplayer2.Library.CurrentBook.SetCurrentPageOffloadable( false ) ) {
					Main.NewText( "Inventory page " + myplayer2.Library.CurrentBook.CurrentPageIdx + " rejects offloading.", Color.LimeGreen );
				}
			};
			this.TogglePageOffload.OnToggleOff += ( evt, elem ) => {
				var mymod2 = ExtensibleInventoryMod.Instance;
				var myplayer2 = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod2, "ExtensibleInventoryPlayer" );

				if( myplayer2.Library.CurrentBook.SetCurrentPageOffloadable( true ) ) {
					Main.NewText( "Inventory page " + myplayer2.Library.CurrentBook.CurrentPageIdx + " accepts offloading.", Color.LimeGreen );
				}
			};

			this.SetElementPositions( false );
			this.SetPageTabsPosition( false );

			base.Append( this.PageDisplay );
			base.Append( this.ButtonPageLeft );
			base.Append( this.ButtonPageRight );
			base.Append( this.ButtonPageAdd );
			base.Append( this.ButtonPageSub );
			base.Append( this.TogglePageOffload );
		}


		private void InitializeLibraryBooks() {
			var mymod = ExtensibleInventoryMod.Instance;
			var myplayer = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod, "ExtensibleInventoryPlayer" );
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
					var mymod2 = ExtensibleInventoryMod.Instance;
					var myplayer2 = (ExtensibleInventoryPlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, mymod2, "ExtensibleInventoryPlayer" );

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
