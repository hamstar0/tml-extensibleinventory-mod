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
		public int BookPositionXInput {
			get => (int)this.BookPositionX;
			set => this.BookPositionX = (float)value;
		}
		[JsonIgnore]
		[Label( "[i:149] Book position Y screen coordinate" )]
		[Range( -1080f, 1080f )]
		[Increment( 8 )]
		public int BookPositionYInput {
			get => (int)this.BookPositionY;
			set => this.BookPositionY = (float)value;
		}

		[JsonIgnore]
		[Label( "[i:903] Page number position X screen coordinate" )]
		[Range( -2048, 2048 )]
		[Increment( 8 )]
		public int PagePositionXInput {
			get => (int)this.PagePositionX;
			set => this.PagePositionX = (float)value;
		}
		[JsonIgnore]
		[Label( "[i:903] Page number position Y screen coordinate" )]
		[Range( -1080f, 1080f )]
		[Increment( 8 )]
		public int PagePositionYInput {
			get => (int)this.PagePositionY;
			set => this.PagePositionY = (float)value;
		}

		[JsonIgnore]
		[Label( "[i:967] Page ticks bar position X screen coordinate" )]
		[Range( -2048, 2048 )]
		[Increment( 8 )]
		public int PageTicksPositionXInput {
			get => (int)this.PageTicksPositionX;
			set => this.PageTicksPositionX = (float)value;
		}
		[JsonIgnore]
		[Label( "[i:967] Page ticks bar position Y screen coordinate" )]
		[Range( -1080f, 1080f )]
		[Increment( 8 )]
		public int PageTicksPositionYInput {
			get => (int)this.PageTicksPositionY;
			set => this.PageTicksPositionY = (float)value;
		}

		[JsonIgnore]
		[Label( "[i:48] Shop or chest UI X screen coordinate offset" )]
		[Range( -2048, 2048 )]
		[Increment( 8 )]
		public int ChestOnOffsetXInput {
			get => (int)this.ChestOnOffsetX;
			set => this.ChestOnOffsetX = (float)value;
		}
		[JsonIgnore]
		[Label( "[i:48] Shop or chest UI Y screen coordinate offset" )]
		[Range( -1080f, 1080f )]
		[Increment( 8 )]
		public int ChestOnOffsetYInput {
			get => (int)this.ChestOnOffsetY;
			set => this.ChestOnOffsetY = (float)value;
		}

		////

		[Header("\n \n \n \n")]
		[Range( -2048f, 2048f )]
		[DefaultValue( 64f )]
		public float BookPositionX { get; set; } = 64f;

		[Range( -1080f, 1080f )]
		[DefaultValue( 260f )]
		public float BookPositionY { get; set; } = 260f;

		[Range( -2048f, 2048f )]
		[DefaultValue( 192f )]
		public float PagePositionX { get; set; } = 192f;

		[Range( -1080f, 1080f )]
		[DefaultValue( 260f )]
		public float PagePositionY { get; set; } = 260f;

		[Range( -2048f, 2048f )]
		[DefaultValue( 32f )]
		public float PageTicksPositionX { get; set; } = 32f;

		[Range( -1080f, 1080f )]
		[DefaultValue( 254f )]
		public float PageTicksPositionY { get; set; } = 254f;


		[DefaultValue( 0f )]
		public float ChestOnOffsetX { get; set; } = 0f;

		[DefaultValue( 168f )]
		public float ChestOnOffsetY { get; set; } = 168f;



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
