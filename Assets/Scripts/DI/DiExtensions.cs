using JetBrains.Annotations;
using Zenject;

namespace DI
{
    public static class DiExtensions
    {
        
        //Used to avoid the "class is never instantiated" warning
        public static ConcreteIdArgConditionCopyNonLazyBinder BindSingle<[MeansImplicitUse]T>(this DiContainer container)
        {
            return container.BindInterfacesAndSelfTo<T>().AsSingle();
        }
    }
}