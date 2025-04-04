using System.Collections.Generic;
using System.Linq;

namespace com.game.utilities.componentpipelines
{
    public interface IComponentPipeline<T>
    {
        List<ComponentPipelineComponentBase<T>> Pipeline { get; }

        T Enpipe(T value)
        {
            T result = value;
            foreach (var pipe in Pipeline.OrderBy(pipe => pipe.Order))
            {
                result = pipe.Enpipe(result);
            }

            return result;
        }
    }
}
