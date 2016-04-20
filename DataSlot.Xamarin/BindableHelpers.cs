using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

static class BindableHelpers<T>
    where T : class
{
    public static readonly TypeInfo Info = typeof(T).GetTypeInfo();

    public static bool IsBindableObject { get; }

    static BindableHelpers()
    {
        var svc = Type.GetType("BindableService, DataSlot.XamarinForms");
        if (svc != null)
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(svc.TypeHandle);

        IsBindableObject = (BindableService.Instance?.IsBindable(Info)).GetValueOrDefault();
    }

    public static bool IsBindable(object slot)
    {
        if (slot == null)
            return false;

        if (!IsBindableObject)
            return false;

        return BindableService.Instance.IsProperty(slot.GetType().GetTypeInfo());
    }

    public static void SetSlotData(T target, object slot, object data)
        => BindableService.Instance.SetSlotData(target, slot, data);
    public static object GetSlotData(T target, object slot)
        => BindableService.Instance.GetSlotData(target, slot);

    public static object GetOrAddSlotData(T target, object slot, object data)
        => BindableService.Instance.GetOrAddSlotData(target, slot, data);
    public static bool TryAddSlotData(T target, object slot, object data)
        => BindableService.Instance.TryAddSlotData(target, slot, data);
    public static bool TryGetSlotData(T target, object slot, out object data)
        => BindableService.Instance.TryGetSlotData(target, slot, out data);
}
