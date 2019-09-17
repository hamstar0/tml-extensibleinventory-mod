using HamstarHelpers.Services.ModCompatibilities.ExtensibleInventoryCompat;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace ExtensibleInventory {
	public class ExtensibleInventoryConfigData : ModConfig {
		public override ConfigScope Mode => ConfigScope.ServerSide;


		////

		public bool DebugModeInfo { get; set; } = false;

		public bool DebugModeUI { get; set; } = false;

		public bool DebugModeReset { get; set; } = false;

		////

		[Header( "\n " )]
		[DefaultValue( true )]
		public bool DefaultBookEnabled { get; set; } = true;

		public bool EnableSharedInventoryRecipesViaRecipeHack { get; set; } = false;


		//public bool HideBookUI { get; set; } = false;

		//public bool HidePageUI { get; set; } = false;

		public bool HidePageTicksUI { get; set; } = false;


		[DefaultValue( true )]
		public bool CanSwitchBooks { get; set; } = true;

		[DefaultValue( true )]
		public bool CanScrollPages { get; set; } = true;

		[DefaultValue( true )]
		public bool CanAddPages { get; set; } = true;

		[DefaultValue( true )]
		public bool CanDeletePages { get; set; } = true;


		[DefaultValue( 99 )]
		public int MaxPages { get; set; } = 99;


		[DefaultValue( true )]
		public bool ScrollModeEnabled { get; set; } = true;

		public bool HideScrollModeIcon { get; set; } = false;


		////

		[Header("\n ")]
		[JsonIgnore]
		[Label( "[i:149] Book position X screen coordinate" )]
		[Range( -2048, 2048 )]
		[Increment( 8 )]
		[DefaultValue( 64 )]
		public int BookPositionX {
			get => (int)this._BookPositionX;
			set => this._BookPositionX = (float)value;
		}
		private float _BookPositionX { get; set; } = 64;

		[JsonIgnore]
		[Label( "[i:149] Book position Y screen coordinate" )]
		[Range( -1080, 1080 )]
		[Increment( 8 )]
		[DefaultValue( 260 )]
		public int BookPositionY {
			get => (int)this._BookPositionY;
			set => this._BookPositionY = (float)value;
		}
		private float _BookPositionY { get; set; } = 260;

		[JsonIgnore]
		[Label( "[i:903] Page number position X screen coordinate" )]
		[Range( -2048, 2048 )]
		[Increment( 8 )]
		[DefaultValue( 192 )]
		public int PagePositionX {
			get => (int)this._PagePositionX;
			set => this._PagePositionX = (float)value;
		}
		private float _PagePositionX { get; set; } = 192;

		[JsonIgnore]
		[Label( "[i:903] Page number position Y screen coordinate" )]
		[Range( -1080, 1080 )]
		[Increment( 8 )]
		[DefaultValue( 260 )]
		public int PagePositionY {
			get => (int)this._PagePositionY;
			set => this._PagePositionY = (float)value;
		}
		private float _PagePositionY { get; set; } = 260;


		[JsonIgnore]
		[Label( "[i:967] Page ticks bar position X screen coordinate" )]
		[Range( -2048, 2048 )]
		[Increment( 8 )]
		[DefaultValue( 32 )]
		public int PageTicksPositionX {
			get => (int)this._PageTicksPositionX;
			set => this._PageTicksPositionX = (float)value;
		}
		private float _PageTicksPositionX { get; set; } = 32;

		[JsonIgnore]
		[Label( "[i:967] Page ticks bar position Y screen coordinate" )]
		[Range( -1080, 1080 )]
		[Increment( 8 )]
		[DefaultValue( 254 )]
		public int PageTicksPositionY {
			get => (int)this._PageTicksPositionY;
			set => this._PageTicksPositionY = (float)value;
		}
		private float _PageTicksPositionY { get; set; } = 254;


		[JsonIgnore]
		[Label( "[i:48] Shop or chest UI X screen coordinate offset" )]
		[Range( -2048, 2048 )]
		[DefaultValue( 0 )]
		public int ChestOnOffsetX {
			get => (int)this._ChestOnOffsetX;
			set => this._ChestOnOffsetX = (float)value;
		}
		private float _ChestOnOffsetX { get; set; } = 0;

		[JsonIgnore]
		[Label( "[i:48] Shop or chest UI Y screen coordinate offset" )]
		[Range( -1080, 1080 )]
		[DefaultValue( 168 )]
		public int ChestOnOffsetY {
			get => (int)this._ChestOnOffsetY;
			set => this._ChestOnOffsetY = (float)value;
		}
		private float _ChestOnOffsetY { get; set; } = 168;



		////////////////

		public override void OnChanged() {
			var mymod = ExtensibleInventoryMod.Instance;
			mymod.InvUI?.UpdateElementPositions( mymod.InvUI.IsChest );
		}

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
