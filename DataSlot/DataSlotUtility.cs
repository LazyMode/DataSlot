using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

public static class DataSlotUtility
{
    private static readonly ConditionalWeakTable<object, ConcurrentDictionary<object, object>> MapOfDataSlotTables
        = new ConditionalWeakTable<object, ConcurrentDictionary<object, object>>();

    public static object GetSlotData<T>(this T target, object slot)
        where T : class
    {
        ConcurrentDictionary<object, object> table;
        if (!MapOfDataSlotTables.TryGetValue(target, out table))
            return null;
        return table[slot];
    }
    public static void SetSlotData<T>(this T target, object slot, object data)
        where T : class
        => MapOfDataSlotTables.GetOrCreateValue(target)[slot] = data;
    public static bool TryGetSlotData<T>(this T target, object slot, out object data)
        where T : class
    {
        ConcurrentDictionary<object, object> table;
        if (MapOfDataSlotTables.TryGetValue(target, out table))
            return table.TryGetValue(slot, out data);

        data = null;
        return false;
    }
}

