using System;


namespace ExtensibleInventory {
	public static partial class ExtensibleInventoryAPI {
		internal static object Call( string call_type, params object[] args ) {
			switch( call_type ) {
			case "GetModSettings":
				return ExtensibleInventoryAPI.GetModSettings();
			default:
				throw new Exception( "No such api call " + call_type );
			}
		}
	}
}
