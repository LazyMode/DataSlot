#if !PCL
using System;
using System.Reflection;

#if XAMARIN_FORMS
using BindableObject = Xamarin.Forms.BindableObject;
using BindableProperty = Xamarin.Forms.BindableProperty;
#elif NETFX_CORE
using BindableObject = Windows.UI.Xaml.DependencyObject;
using BindableProperty = Windows.UI.Xaml.DependencyProperty;
#else
using BindableObject = System.Windows.DependencyObject;
using BindableProperty = System.Windows.DependencyProperty;
#endif

static class Types
{
#if NO_TYPEINFO
    public static readonly Type BindableObjectType = typeof(BindableObject);
    public static readonly Type BindablePropertyType = typeof(BindableProperty);
#else             
    public static readonly TypeInfo BindableObjectType = typeof(BindableObject).GetTypeInfo();
    public static readonly TypeInfo BindablePropertyType = typeof(BindableProperty).GetTypeInfo();
#endif
}
#endif
