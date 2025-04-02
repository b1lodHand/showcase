using Unity.Cinemachine;
using com.absence.utilities;
using UnityEngine;

namespace com.game
{
    [DefaultExecutionOrder(-10000)]
    public class GameDefaultCamera : PersistentSingleton<GameDefaultCamera>
    {
        [SerializeField] private Camera m_camera;
        [SerializeField] private CinemachineBrain m_brain;

        public Camera Camera => m_camera;
        public CinemachineBrain Brain => m_brain;
    }
}