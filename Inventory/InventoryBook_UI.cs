using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Reflection;
using HamstarHelpers.Helpers.TModLoader;
using HamstarHelpers.Services.RecipeHack;
using HamstarHelpers.Services.Timers;
using Microsoft.Xna.Framework;
using Terraria;


namespace ExtensibleInventory.Inventory {
	partial class InventoryBook {
		public const string PageScrollTimerName = "ExtensibleInventoryPageScroll";



		////////////////

		public bool JumpToPage( Player player, int pageNum ) {
			if( !LoadHelpers.IsWorldSafelyBeingPlayed() ) {
				LogHelpers.Warn( "World not in play" );
				return false;
			}
			if( Timers.GetTimerTickDuration( InventoryBook.PageScrollTimerName ) > 0 ) {
				return false;
			}

			string err;

			if( !this.CanScrollPages( out err ) ) {
				Main.NewText( err, Color.Red );
				return false;
			}

			this.PullFromInventoryToPage( player, this.CurrentPageIdx );
			this.PushPageToInventory( player, pageNum );

			this.CurrentPageIdx = pageNum;
			//RecipeHack.ForceRecipeRefresh();
			Recipe.FindRecipes();

			Timers.SetTimer( InventoryBook.PageScrollTimerName, 10, true, () => {
				return false;
			} );

			return true;
		}


		public bool ScrollPageUp( Player player ) {
			if( !LoadHelpers.IsWorldSafelyBeingPlayed() ) {
				LogHelpers.Warn( "World not in play" );
				//Main.NewText( "Could not scroll pages. Please report this as an issue.", Color.Red );
				return false;
			}
			if( Timers.GetTimerTickDuration( InventoryBook.PageScrollTimerName ) > 0 ) {
				return false;
			}

			string err;

			if( !this.CanScrollPages( out err ) ) {
				Main.NewText( err, Color.Red );
				return false;
			}
			if( this.CurrentPageIdx <= 0 ) {
				return false;
			}
			
			this.PullFromInventoryToPage( player, this.CurrentPageIdx );
			this.PushPageToInventory( player, --this.CurrentPageIdx );
			//RecipeHack.ForceRecipeRefresh();
			Recipe.FindRecipes();

			Timers.SetTimer( InventoryBook.PageScrollTimerName, 10, true, () => {
				RecipeHack.ForceRecipeRefresh();
				return false;
			} );

			return true;
		}

		public bool ScrollPageDown( Player player ) {
			if( !LoadHelpers.IsWorldSafelyBeingPlayed() ) {
				LogHelpers.Warn( "World not in play" );
				//Main.NewText( "Could not scroll pages. Please report this as an issue.", Color.Red );
				return false;
			}
			if( Timers.GetTimerTickDuration( InventoryBook.PageScrollTimerName ) > 0 ) {
				return false;
			}

			string err;

			if( !this.CanScrollPages( out err ) ) {
				Main.NewText( err, Color.Red );
				return false;
			}
			if( this.CurrentPageIdx >= ( this.Pages.Count - 1 ) ) {
				return false;
			}

			this.PullFromInventoryToPage( player, this.CurrentPageIdx );
			this.PushPageToInventory( player, ++this.CurrentPageIdx );
			//RecipeHack.ForceRecipeRefresh();
			Recipe.FindRecipes();

			Timers.SetTimer( InventoryBook.PageScrollTimerName, 10, true, () => {
				return false;
			} );

			return true;
		}


		////////////////

		public bool InsertAtCurrentPagePosition( [Nullable]Player player ) {
			string err;
			bool success = this.InsertNewPage( player, this.CurrentPageIdx, out err );

			if( !success ) {
				Main.NewText( err, Color.Red );
			}

			return success;
		}

		public bool DeleteCurrentPage( [Nullable]Player player ) {
			string err;
			bool success = this.DeleteEmptyPage( player, this.CurrentPageIdx, out err );

			if( !success ) {
				Main.NewText( err, Color.Red );
			}

			return success;
		}
	}
}
