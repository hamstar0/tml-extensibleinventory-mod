using System;


namespace ExtensibleInventory {
	public static partial class ExtensibleInventoryAPI {
		internal static object Call( string call_type, params object[] args ) {
			string book_name;

			switch( call_type ) {
			case "GetModSettings":
				return ExtensibleInventoryAPI.GetModSettings();
			case "SaveModSettingsChanges":
				ExtensibleInventoryAPI.SaveModSettingsChanges();
				return null;

			case "AddBook":
				if( args.Length < 2 ) { throw new Exception( "Insufficient parameters for API call " + call_type ); }

				book_name = args[0] as string;
				if( book_name == null ) { throw new Exception( "Invalid parameter book_name for API call " + call_type ); }

				if( !( args[1] is bool ) ) { throw new Exception( "Invalid parameter can_user_add_pages for API call " + call_type ); }
				bool user_can_add_pages = (bool)args[1];

				ExtensibleInventoryAPI.AddBook( book_name, user_can_add_pages );
				return null;
			case "RemoveBook":
				if( args.Length < 1 ) { throw new Exception( "Insufficient parameters for API call " + call_type ); }

				book_name = args[0] as string;
				if( book_name == null ) { throw new Exception( "Invalid parameter book_name for API call " + call_type ); }

				ExtensibleInventoryAPI.RemoveBook( book_name );
				return null;
			case "EnableBook":
				if( args.Length < 1 ) { throw new Exception( "Insufficient parameters for API call " + call_type ); }

				book_name = args[0] as string;
				if( book_name == null ) { throw new Exception( "Invalid parameter book_name for API call " + call_type ); }

				ExtensibleInventoryAPI.EnableBook( book_name );
				return null;
			case "DisableBook":
				if( args.Length < 1 ) { throw new Exception( "Insufficient parameters for API call " + call_type ); }

				book_name = args[0] as string;
				if( book_name == null ) { throw new Exception( "Invalid parameter book_name for API call " + call_type ); }

				ExtensibleInventoryAPI.DisableBook( book_name );
				return null;
			case "CountBookPages":
				if( args.Length < 1 ) { throw new Exception( "Insufficient parameters for API call " + call_type ); }

				book_name = args[0] as string;
				if( book_name == null ) { throw new Exception( "Invalid parameter book_name for API call " + call_type ); }

				return ExtensibleInventoryAPI.CountBookPages( book_name );
			case "AddBookPage":
				if( args.Length < 1 ) { throw new Exception( "Insufficient parameters for API call " + call_type ); }

				book_name = args[0] as string;
				if( book_name == null ) { throw new Exception( "Invalid parameter book_name for API call " + call_type ); }

				ExtensibleInventoryAPI.AddBookPage( book_name );
				return null;
			case "GetLatestBookPageItems":
				if( args.Length < 1 ) { throw new Exception( "Insufficient parameters for API call " + call_type ); }

				book_name = args[0] as string;
				if( book_name == null ) { throw new Exception( "Invalid parameter book_name for API call " + call_type ); }

				return ExtensibleInventoryAPI.GetLatestBookPageItems( book_name );
			case "RemoveLatestBookPage":
				if( args.Length < 1 ) { throw new Exception( "Insufficient parameters for API call " + call_type ); }

				book_name = args[0] as string;
				if( book_name == null ) { throw new Exception( "Invalid parameter book_name for API call " + call_type ); }

				ExtensibleInventoryAPI.RemoveLatestBookPage( book_name );
				return null;
			default:
				throw new Exception( "No such api call " + call_type );
			}
		}
	}
}
