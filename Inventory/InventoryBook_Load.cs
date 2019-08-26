using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using System;
using Terraria;
using Terraria.ModLoader.IO;


namespace ExtensibleInventory.Inventory {
	partial class InventoryBook {
		public void Load( string lowercaseBookName, TagCompound tags ) {
			string prefix;

			if( lowercaseBookName == "default" ) {
				prefix = "";
			} else {
				prefix = lowercaseBookName + "_";
			}

			if( ExtensibleInventoryMod.Instance.Config.DebugModeReset ) {
				return;
			}
			if( !tags.ContainsKey( prefix + "page_count" ) || !tags.ContainsKey( prefix + "curr_page" ) ) {
				return;
			}

			this.Pages.Clear();

			//bool foundSharing = false;
			int pages = tags.GetInt( prefix + "page_count" );
			int currPage = tags.GetInt( prefix + "curr_page" );

			for( int i = 0; i < pages; i++ ) {
				InventoryPage page = new InventoryPage();
				this.Pages.Add( page );

				string pageNumKey = prefix + "page_" + i;

				//foundSharing = tags.ContainsKey( pageNumKey + "_s" );
				//
				//if( foundSharing ) {
				//	page.IsSharing = tags.GetBool( pageNumKey + "_s" );
				//}

				if( i == currPage ) { continue; }

				for( int j = 0; j < InventoryPage.BasePageCapacity; j++ ) {
					string pageNumItemNumKey = pageNumKey + "_" + j;

					if( tags.ContainsKey( pageNumItemNumKey ) ) {
						try {
							page.Items[j] = ItemIO.Load( tags.GetCompound( pageNumItemNumKey ) );
						} catch( Exception e ) {
							throw new ModHelpersException( "Could not load item for book "+lowercaseBookName+" on page "+i+" at position "+j, e );
						}
					} else {
						page.Items[j] = new Item();
					}

					//if( !foundSharing && tags.ContainsKey( pageNumItemNumKey+"_s" ) ) { // Oops!
					//	foundSharing = true;
					//	page.IsSharing = tags.GetBool( pageNumItemNumKey + "_s" );
					//}
				}
			}
			
			this.CurrentPageIdx = currPage;
		}

		public TagCompound Save( string lowercaseBookName, TagCompound tags ) {
			string prefix;

			if( lowercaseBookName == "default" ) {
				prefix = "";
			} else {
				prefix = lowercaseBookName + "_";
			}

			tags[ prefix + "page_count" ] = this.Pages.Count;
			tags[ prefix + "curr_page" ] = this.CurrentPageIdx;

			for( int i = 0; i < this.Pages.Count; i++ ) {
				string pageNumKey = prefix + "page_" + i;
				tags[ pageNumKey+"_s" ] = (bool)this.Pages[i].IsSharing;

				if( i == this.CurrentPageIdx ) { continue; }

				for( int j = 0; j < InventoryPage.BasePageCapacity; j++ ) {
					string pageNumItemNumKey = pageNumKey + "_" + j;

					try {
						tags[ pageNumItemNumKey ] = ItemIO.Save( this.Pages[i].Items[j] );
					} catch {
						LogHelpers.Warn( "Could not save item for book "+lowercaseBookName+" on page "+i+" at position "+j );
						continue;
					}
				}
			}

			return tags;
		}
	}
}
