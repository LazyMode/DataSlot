#if !PCL
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

#if XAMARIN_FORMS
using BindableObject = Xamarin.Forms.BindableObject;
using BindableProperty = Xamarin.Forms.BindableProperty;
#elif NETFX_CORE
using FE = Windows.UI.Xaml.FrameworkElement;
using BindableObject = Windows.UI.Xaml.DependencyObject;
using BindableProperty = Windows.UI.Xaml.DependencyProperty;
#else
using FE = System.Windows.FrameworkElement;
using BindableObject = System.Windows.DependencyObject;
using BindableProperty = System.Windows.DependencyProperty;
#endif

using static Types;

static class BindableHelpers<T>
    where T : class
{
#if NO_TYPEINFO
    public static readonly Type Info = typeof(T);
#else
    public static readonly TypeInfo Info = typeof(T).GetTypeInfo();
#endif
    public static readonly bool IsBindableObject;

    static BindableHelpers()
    {
        IsBindableObject = BindableObjectType.IsAssignableFrom(Info);
    }

    public static bool IsBindable(object slot)
    {
        if (slot == null)
            return false;

        if (!IsBindableObject)
            return false;

#if NO_TYPEINFO
        var info = slot.GetType();
#else
        var info = slot.GetType().GetTypeInfo();
#endif
        return IsBindableObject && BindablePropertyType.IsAssignableFrom(info);
    }

    public static void SetSlotData(T target, object slot, object data)
        => (target as BindableObject).SetValue((BindableProperty)slot, data);
    public static object GetSlotData(T target, object slot)
        => (target as BindableObject).GetValue((BindableProperty)slot);

    public static object GetOrAddSlotData(T target, object slot, object data)
        => (target as BindableObject).GetValue((BindableProperty)slot);
    public static bool TryAddSlotData(T target, object slot, object data)
        => false;
    public static bool TryGetSlotData(T target, object slot, out object data)
    {
        try
        {
            data = (target as BindableObject).GetValue((BindableProperty)slot);
            return true;
        }
        catch
        {
            data = null;
            return false;
        }
    }
}
#endif
