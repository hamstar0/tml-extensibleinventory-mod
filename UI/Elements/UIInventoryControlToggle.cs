using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;


namespace ExtensibleInventory.UI.Elements {
	public class UIInventoryControlToggle : UIImageButton {
		private Texture2D OnTex;
		private Texture2D OffTex;
		
		public bool IsHidden = false;


		////////////////

		public event MouseEvent OnToggleOn;
		public event MouseEvent OnToggleOff;

		public bool On { get; private set; } = false;



		////////////////

		public UIInventoryControlToggle( Texture2D onTex, Texture2D offTex ) : base( offTex ) { }
		
		public override void OnInitialize() {
			base.OnInitialize();

			this.OnClick += ( evt, listeningElement ) => {
				if( this.IsHidden ) { return; }

				if( this.On ) {
					this.OnToggleOff?.Invoke( evt, listeningElement );
				} else {
					this.OnToggleOn?.Invoke( evt, listeningElement );
				}
				this.On = !this.On;
			};
		}

		////////////////

		public override void MouseOver( UIMouseEvent evt ) {
			if( this.IsHidden ) { return; }

			base.MouseOver( evt );
		}

		////////////////

		public override void Update( GameTime gameTime ) {
			if( this.IsHidden ) { return; }

			base.Update( gameTime );
		}

		////////////////

		public override void Draw( SpriteBatch spriteBatch ) {
			if( this.IsHidden ) { return; }

			base.Draw( spriteBatch );
		}
	}
}
