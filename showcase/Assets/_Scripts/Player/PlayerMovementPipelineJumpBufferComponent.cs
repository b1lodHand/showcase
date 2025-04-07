using com.game.utilities.componentpipelines;
using UnityEngine;
using static com.game.player.PlayerMovementPipeline;

namespace com.game.player
{
    public class PlayerMovementPipelineJumpBufferComponent : ComponentPipelineComponentBase<PlayerMovement, PipelineContext>
    {
        public override PipelineContext Enpipe(PlayerMovement target, PipelineContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
