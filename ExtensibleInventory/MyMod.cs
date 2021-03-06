using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.TModLoader;
using HamstarHelpers.Helpers.TModLoader.Mods;
using HamstarHelpers.Services.RecipeHack;
using System;
using Terraria;
using Terraria.ModLoader;


namespace ExtensibleInventory {
	partial class ExtensibleInventoryMod : Mod {
		public static ExtensibleInventoryMod Instance { get; private set; }



		////////////////

		public ExtensibleInventoryConfig Config => ModContent.GetInstance<ExtensibleInventoryConfig>();

		internal ModHotKey NonLockedInventoryToNewPageHotKey { get; private set; }



		////////////////

		public ExtensibleInventoryMod() {
			ExtensibleInventoryMod.Instance = this;
		}

		public override void Load() {
			if( !Main.dedServ ) {
				this.InitializeUI();
				this.NonLockedInventoryToNewPageHotKey = this.RegisterHotKey( "Create Page After (brings locked)", "F2" );
			}
		}

		public override void PostAddRecipes() {
			if( this.Config.EnableSharedInventoryRecipesViaRecipeHack ) {
				RecipeHack.RegisterIngredientSource( "ExtensibleInventoryShared", ( plr ) => {
					if( plr.whoAmI != Main.myPlayer ) { return new Item[0]; }

					var myplayer = TmlHelpers.SafelyGetModPlayer<ExtensibleInventoryPlayer>( plr );
					return myplayer.Library.CurrentBook.GetSharedItems( plr, false );
				} );
			}
		}

		////

		public override void Unload() {
			this.NonLockedInventoryToNewPageHotKey = null;
			ExtensibleInventoryMod.Instance = null;
		}


		////////////////

		public override object Call( params object[] args ) {
			return ModBoilerplateHelpers.HandleModCall( typeof( ExtensibleInventoryAPI ), args );
		}
	}
}
