using com.absence.attributes;
using com.game.player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.game.generics
{
    [DefaultExecutionOrder(-100)]
    public class Correlator : MonoBehaviour
    {
        public enum CheckMode
        {
            [InspectorName("Is Local (Player)")] Player_IsLocal,
            [InspectorName("Is Singleplayer (Game)")] Game_IsSingleplayer,
            [InspectorName("Is Split Screen Multiplayer (Game)")] Game_IsSplitscreenMultiplayer,
            [InspectorName("Is Local Multiplayer (Game)")] Game_IsLocalMultiplayer,
            [InspectorName("Is Wide Multiplayer (Game)")] Game_IsWideMultiplayer,
        }

        [SerializeField] private CheckMode m_checkMode;
        [SerializeField] private bool m_invert;
        [SerializeField] private bool m_bypassOnAwake;

        [Space]

        [SerializeField, ShowIf(nameof(m_checkMode), CheckMode.Player_IsLocal)] private Player m_player;

        [Space]

        [SerializeField] private List<Component> m_scriptsToDisable;
        [SerializeField] private List<Component> m_scriptsToEnable;
        [SerializeField] private List<GameObject> m_objectsToDestroy;

        private void Awake()
        {
            if (m_bypassOnAwake)
                return;

            Correlate();
        }

        public void Correlate()
        {
            bool result = false;
            switch (m_checkMode)
            {
                case CheckMode.Player_IsLocal:
                    result = (m_player.IsLocal && !m_invert) || (m_invert && !m_player.IsLocal);
                    break;
                case CheckMode.Game_IsSingleplayer:
                    result = Game.LobbyType == GameLobbyType.Singleplayer;
                    break;
                case CheckMode.Game_IsSplitscreenMultiplayer:
                    result = Game.LobbyType == GameLobbyType.SplitScreen;
                    break;
                case CheckMode.Game_IsLocalMultiplayer:
                    result = Game.LobbyType == GameLobbyType.LAN;
                    break;
                case CheckMode.Game_IsWideMultiplayer:
                    result = Game.LobbyType == GameLobbyType.WAN;
                    break;
                default:
                    enabled = false;
                    return;
            }

            if (m_invert)
                result = !result;

            if (result)
                Apply();
        }

        void Apply()
        {
            foreach (MonoBehaviour script in m_scriptsToDisable)
            {
                script.enabled = false;
            }

            foreach (MonoBehaviour script in m_scriptsToDisable)
            {
                script.enabled = true;
            }

            foreach (GameObject obj in m_objectsToDestroy)
            {
                DestroyImmediate(obj);
            }

            enabled = false;
        }
    }
}
