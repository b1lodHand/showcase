using com.absence.dialoguesystem;
using com.absence.dialoguesystem.internals;
using System.Collections.Generic;
using UnityEngine;

namespace com.game.extensions.dialoguesystem
{
    public class OllamaNode : Node
    {
        public static string CreationMenuName => "AI Prompt";

        [HideInInspector] public Node Next;

        public override string Title => "AI Prompt";

        public override List<string> AdditionalUSSFileLocations => new List<string>()
        {
            "Assets/Editor/StyleSheets/AIPromptNode.uss",
        };

        protected override void OnAddOutputConnection(Node nextWillBeAdded, int atPort)
        {
            Next = nextWillBeAdded;
        }

        protected override Node OnPass(DialogueFlowContext context)
        {
            // get option index and parse.
            return Next;
        }

        protected override void OnReach(DialogueFlowContext context)
        {
            // generate options and text.
        }

        protected override void OnRemoveOutputConnection(int atPort)
        {
            Next = null;
        }

        protected override void WriteOutputConnections(ref List<Node> result)
        {
            result.Add(Next);
        }
    }
}
