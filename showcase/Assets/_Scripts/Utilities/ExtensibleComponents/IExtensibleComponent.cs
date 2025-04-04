using System.Collections.Generic;

namespace com.game.utilities.extensiblecomponents
{
    public interface IExtensibleComponent<T1>
    {
        List<ComponentExtensionBase<T1>> Extensions { get; }
    }
}
