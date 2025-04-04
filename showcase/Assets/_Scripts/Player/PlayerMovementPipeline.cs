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

        public float EnpipeMoveSpeed(float defaultMoveSpeed)
        {
            PipelineContext context = GetMoveSpeedPipelineContext(defaultMoveSpeed);
            return Enpipe(context).MoveSpeed;
        }

        PipelineContext GetMoveSpeedPipelineContext(float defaultMoveSpeed)
        {
            return new PipelineContext()
            {
                Type = PipelineContext.ContextType.MoveSpeed,
                MoveSpeed = defaultMoveSpeed,
            };
        }
    }
}
