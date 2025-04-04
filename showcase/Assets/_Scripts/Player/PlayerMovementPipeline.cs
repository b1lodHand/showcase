using com.game.utilities.componentpipelines;

namespace com.game.player
{
    public class PlayerMovementPipeline : ComponentPipelineBase<PlayerMovementPipeline.PipelineContext>
    {
        public class PipelineContext
        {
            public enum ContextType
            {
                MoveSpeed,
            }

            public ContextType Type;
            public float MoveSpeed;
        }
    }
}
