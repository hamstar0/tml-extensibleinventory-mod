namespace ExtensibleInventory {
	public static partial class ExtensibleInventoryAPI {
		public static ExtensibleInventoryConfigData GetModSettings() {
			return ExtensibleInventoryMod.Instance.Config;
		}
	}
}
