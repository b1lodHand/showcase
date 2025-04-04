using System;
using System.Collections.Generic;
using System.Linq;

namespace com.game.generics
{
    public interface IExtensibleComponent<T1> where T1 : Enum
    {
        List<ComponentExtensionBase<T1>> Extensions { get; }
        void RunExtensionsWithContext(T1 context)
        {
            foreach (var extension in Extensions.OrderBy(ext => ext.Order))
            {
                extension.ApplyLogic(context);
            }
        }

        T2 RunExtensionsWithContext<T2>(T2 value, T1 context)
        {
            T2 result = value;
            foreach (var extension in Extensions.OrderBy(ext => ext.Order))
            {
                result = extension.ApplyLogic(result, context);
            }

            return result;
        }
    }
}
