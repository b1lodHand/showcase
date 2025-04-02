using com.absence.attributes;
using System.Collections.Generic;
using UnityEngine;

namespace com.game.generics
{
    public class DirectionReapplier : MonoBehaviour
    {
        public enum UpdateMode
        {
            [InspectorName("None (Manual)")] None,
            AwakeOnly,
            StartOnly,
            Update,
            FixedUpdate,
            LateUpdate,
        }

        [Header("Settings")]
        [SerializeField] private UpdateMode m_updateMode = UpdateMode.Update;

        [SerializeField] private bool m_x = true;
        [SerializeField] private bool m_y = true;
        [SerializeField] private bool m_z = true;

        [Space, Header("Fields")]

        [SerializeField, Required] private Transform m_source;
        [SerializeField] private Transform m_target;
        [SerializeField] private List<Transform> m_otherTargets;

        private void Awake()
        {
            if (m_updateMode == UpdateMode.AwakeOnly)
                ForceReapply();
        }

        private void Start()
        {
            if (m_updateMode == UpdateMode.StartOnly)
                ForceReapply();
        }

        private void Update()
        {
            if (m_updateMode == UpdateMode.Update)
                ForceReapply();
        }

        private void LateUpdate()
        {
            if (m_updateMode == UpdateMode.LateUpdate)
                ForceReapply();
        }

        private void FixedUpdate()
        {
            if (m_updateMode == UpdateMode.FixedUpdate)
                ForceReapply();
        }

        public void ForceReapply()
        {
            Vector3 orientationEulers = m_source.transform.forward;
            if (!m_x) orientationEulers.x = 0;
            if (!m_y) orientationEulers.y = 0;
            if (!m_z) orientationEulers.z = 0;
            orientationEulers.Normalize();

            if (m_target != null) 
                m_target.forward = orientationEulers;

            if (m_otherTargets != null)
            {
                foreach (Transform t in m_otherTargets)
                {
                    t.forward = orientationEulers;
                }
            }
        }
    }
}
