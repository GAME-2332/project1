using System.Collections.Generic;

public class ItemRegistry {
    private static Dictionary<string, SOItemInfo> items = Util.LoadResources<SOItemInfo>("ItemSOs");

    public static SOItemInfo GetItem(string name) => items[name];
    public static void Wake() { }
}