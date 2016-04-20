#if !PCL && !XAMARIN
using System;
using System.Reflection;

#if NETFX_CORE
using BindableObject = Windows.UI.Xaml.DependencyObject;
using BindableProperty = Windows.UI.Xaml.DependencyProperty;
#else
using BindableObject = System.Windows.DependencyObject;
using BindableProperty = System.Windows.DependencyProperty;
using BindablePropertyKey = System.Windows.DependencyPropertyKey;
#endif

static class Types
{
#if NO_TYPEINFO
    public static readonly Type BindableObjectType = typeof(BindableObject);
    public static readonly Type BindablePropertyType = typeof(BindableProperty);
    public static readonly Type BindablePropertyKeyType = typeof(BindablePropertyKey);
#else             
    public static readonly TypeInfo BindableObjectType = typeof(BindableObject).GetTypeInfo();
    public static readonly TypeInfo BindablePropertyType = typeof(BindableProperty).GetTypeInfo();
#if !NETFX_CORE
    public static readonly TypeInfo BindablePropertyKeyType = typeof(BindablePropertyKey).GetTypeInfo();
#endif
#endif
}
#endif
