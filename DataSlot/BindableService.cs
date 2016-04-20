#if XAMARIN
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;

abstract class BindableService
{
    private static BindableService _Instance;
    public static BindableService Instance
    {
        get { return _Instance; }
        set
        {
            if (Interlocked.CompareExchange(ref _Instance, value, null) != null)
                throw new InvalidOperationException();
        }
    }

    public abstract bool IsBindable(TypeInfo info);

    public abstract bool IsProperty(TypeInfo info);
    public abstract void SetSlotData(object target, object slot, object data);
    public abstract object GetSlotData(object target, object slot);
    public abstract object GetOrAddSlotData(object target, object slot, object data);
    public abstract bool TryAddSlotData(object target, object slot, object data);
    public abstract bool TryGetSlotData(object target, object slot, out object data);
}
#endif
