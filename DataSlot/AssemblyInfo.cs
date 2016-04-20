using System.Reflection;
using System.Runtime.CompilerServices;

#if XAMARIN
[assembly: InternalsVisibleTo("DataSlot.XamarinForms")]
#elif XAMARIN_FORMS
[assembly: InternalsVisibleTo("DataSlot")]
#endif

[assembly: AssemblyVersion("0.2")]
