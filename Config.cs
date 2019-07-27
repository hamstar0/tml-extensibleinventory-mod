using HamstarHelpers.Services.ModCompatibilities.ExtensibleInventoryCompat;
using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace ExtensibleInventory {
	public class ExtensibleInventoryConfigData : ModConfig {
		public override ConfigScope Mode => ConfigScope.ServerSide;


		////

		public bool DebugModeInfo = false;

		public bool DegugModeUI = false;

		public bool DebugModeReset = false;


		[DefaultValue( true )]
		public bool DefaultBookEnabled = true;

		public bool EnableSharedInventoryRecipesViaRecipeHack = false;


		//public bool HideBookUI = false;

		//public bool HidePageUI = false;

		public bool HidePageTicksUI = false;


		[Range( -4096, 4096 )]
		[DefaultValue( 64f )]
		public float BookPositionX = 64f;

		[Range( -2160, 2160 )]
		[DefaultValue( 260f )]
		public float BookPositionY = 260f;

		[Range( -4096, 4096 )]
		[DefaultValue( 192f )]
		public float PagePositionX = 192f;

		[Range( -2160, 2160 )]
		[DefaultValue( 260f )]
		public float PagePositionY = 260f;

		[Range( -4096, 4096 )]
		[DefaultValue( 32f )]
		public float PageTicksPositionX = 32f;

		[Range( -2160, 2160 )]
		[DefaultValue( 254f )]
		public float PageTicksPositionY = 254f;


		[DefaultValue( 0f )]
		public float ChestOnOffsetX = 0f;

		[DefaultValue( 168f )]
		public float ChestOnOffsetY = 168f;


		[DefaultValue( true )]
		public bool CanSwitchBooks = true;

		[DefaultValue( true )]
		public bool CanScrollPages = true;

		[DefaultValue( true )]
		public bool CanAddPages = true;

		[DefaultValue( true )]
		public bool CanDeletePages = true;


		[DefaultValue( 99 )]
		public int MaxPages = 99;


		[DefaultValue( true )]
		public bool ScrollModeEnabled = true;

		public bool HideScrollModeIcon = false;



		////////////////

		public void ReadyingForLocalPlayerUse() {
			ExtensibleInventoryCompatibilities.ApplyCompats();

			/*IDictionary<string, ISet<Mod>> modsByAuthor = ModListHelpers.GetModsByAuthor();
			modsByAuthor.Remove( "hamstar" );
			ISet<Mod> mods = new HashSet<Mod>( modsByAuthor.Values.SelectMany( m=>m ) );

			foreach( Mod mod in mods ) {
				try {
					mod.Call( "ExtensibleInventoryReadyingSettingsForLocalPlayerUse", this );
				} catch { }
			}*/	// TODO: Implement events as method of hooking. Not Mod.Call()
		}
	}
}
