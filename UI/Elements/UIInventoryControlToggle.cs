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

		public UIInventoryControlToggle( Texture2D onTex, Texture2D offTex ) : base( offTex ) {
			this.OnTex = onTex;
			this.OffTex = offTex;
		}

		
		public override void OnInitialize() {
			base.OnInitialize();

			this.OnClick += ( evt, listeningElement ) => {
				if( this.IsHidden ) { return; }
				
				this.SetOn( !this.On, true );
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


		////////////////

		public void SetOn( bool on, bool triggerMouseEvents ) {
			if( triggerMouseEvents ) {
				if( on == false ) {
					this.OnToggleOff?.Invoke( (UIMouseEvent)null, (UIElement)null );
				} else {
					this.OnToggleOn?.Invoke( (UIMouseEvent)null, (UIElement)null );
				}
			}

			if( this.On != on ) {
				this.On = on;

				if( on == false ) {
					this.SetImage( this.OffTex );
				} else {
					this.SetImage( this.OnTex );
				}
			}
		}
	}
}
