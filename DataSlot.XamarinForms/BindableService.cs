extern alias ds;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

class BindableService : ds::BindableService
{
    static readonly TypeInfo BindableObjectType = typeof(BindableObject).GetTypeInfo();
    static readonly TypeInfo BindablePropertyType = typeof(BindableProperty).GetTypeInfo();
    static readonly TypeInfo BindablePropertyKeyType = typeof(BindablePropertyKey).GetTypeInfo();

    static BindableService()
    {
        Instance = new BindableService();
    }

    public override object GetOrAddSlotData(object target, object slot, object data)
        => GetSlotData(target, slot);

    public override object GetSlotData(object target, object slot)
        => ((BindableObject)target).GetValue((BindableProperty)slot);

    public override bool IsBindable(TypeInfo info)
        => BindableObjectType.IsAssignableFrom(info);

    public override bool IsProperty(TypeInfo info)
        => BindablePropertyType.IsAssignableFrom(info)
        || BindablePropertyKeyType.IsAssignableFrom(info);

    public override void SetSlotData(object target, object slot, object data)
        => ((BindableObject)target).SetValue((BindableProperty)slot, data);

    public override bool TryAddSlotData(object target, object slot, object data)
        => false;

    public override bool TryGetSlotData(object target, object slot, out object data)
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
