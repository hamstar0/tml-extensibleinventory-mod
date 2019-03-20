using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace ExtensibleInventory.UI.Elements {
	public class UIInventoryControlButton : UIImageButton {
		public bool IsHidden = false;



		////////////////

		public UIInventoryControlButton( Texture2D tex ) : base( tex ) { }

		////

		public override void MouseOver( UIMouseEvent evt ) {
			if( this.IsHidden ) { return; }

			base.MouseOver( evt );
		}

		public override void Update( GameTime gameTime ) {
			if( this.IsHidden ) { return; }

			base.Update( gameTime );
		}

		public override void Draw( SpriteBatch spriteBatch ) {
			if( this.IsHidden ) { return; }

			base.Draw( spriteBatch );
		}
	}
}
