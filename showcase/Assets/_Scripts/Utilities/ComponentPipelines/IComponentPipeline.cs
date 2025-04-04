using System.Collections.Generic;

namespace com.game.utilities.componentpipelines
{
    public interface IComponentPipeline<T>
    {
        List<ComponentPipelineComponentBase<T>> Pipeline { get; }
    }
}
