using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace com.game.utilities.cinemachine
{
    public class CinemachineHelpers : MonoBehaviour
    {
        public static readonly OutputChannels NonUniquePlayerChannels = OutputChannels.Default;

        static Dictionary<int, OutputChannels> s_uniqueChannelPairs = new()
        {
            //{ -1, OutputChannels.Channel01 },
            { 0, OutputChannels.Channel01 },
            { 1, OutputChannels.Channel02 },
            { 2, OutputChannels.Channel03 },
            { 3, OutputChannels.Channel04 },
        };

        public static OutputChannels GetOutputChannelForPlayer(int playerIndex, bool includeNonUniqueChannels)
        {
            OutputChannels uniqueChannel;

            if (Game.LobbyType != GameLobbyType.SplitScreen)
                uniqueChannel = OutputChannels.Channel01;

            else if (!s_uniqueChannelPairs.TryGetValue(playerIndex, out uniqueChannel))
            {
                Debug.LogError("Intended player index exceeds the max split-screen player index or is below zero. Returning default channel.");
                return OutputChannels.Default;
            }

            if (includeNonUniqueChannels) 
                return NonUniquePlayerChannels | uniqueChannel;

            return uniqueChannel;
        }
    }
}
