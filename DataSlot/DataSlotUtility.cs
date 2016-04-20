using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

public static class DataSlotUtility
{
    private static readonly ConditionalWeakTable<object, ConcurrentDictionary<object, object>> MapOfDataSlotTables
        = new ConditionalWeakTable<object, ConcurrentDictionary<object, object>>();

    public static object GetSlotData<T>(this T target, object slot)
        where T : class
    {
#if !PCL
        if (BindableHelpers<T>.IsBindable(slot))
            return BindableHelpers<T>.GetSlotData(target, slot);
#endif
        ConcurrentDictionary<object, object> table;
        if (MapOfDataSlotTables.TryGetValue(target, out table))
            return table[slot];

        return null;
    }
    public static object GetOrAddSlotData<T>(this T target, object slot, object data)
        where T : class
    {
#if !PCL
        if (BindableHelpers<T>.IsBindable(slot))
            return BindableHelpers<T>.GetOrAddSlotData(target, slot, data);
#endif
        return MapOfDataSlotTables.GetOrCreateValue(target).GetOrAdd(slot, data);
    }
    public static void SetSlotData<T>(this T target, object slot, object data)
        where T : class
    {
#if !PCL
        if (BindableHelpers<T>.IsBindable(slot))
        {
            BindableHelpers<T>.SetSlotData(target, slot, data);
            return;
        }
#endif
        MapOfDataSlotTables.GetOrCreateValue(target)[slot] = data;
    }
    public static bool TryAddSlotData<T>(this T target, object slot, object data)
        where T : class
    {
#if !PCL
        if (BindableHelpers<T>.IsBindable(slot))
            return BindableHelpers<T>.TryAddSlotData(target, slot, data);
#endif
        return MapOfDataSlotTables.GetOrCreateValue(target).TryAdd(slot, data);
    }
    public static bool TryGetSlotData<T>(this T target, object slot, out object data)
        where T : class
    {
#if !PCL
        if (BindableHelpers<T>.IsBindable(slot))
            return BindableHelpers<T>.TryGetSlotData(target, slot, out data);
#endif
        ConcurrentDictionary<object, object> table;
        if (MapOfDataSlotTables.TryGetValue(target, out table))
            return table.TryGetValue(slot, out data);

        data = null;
        return false;
    }
}

