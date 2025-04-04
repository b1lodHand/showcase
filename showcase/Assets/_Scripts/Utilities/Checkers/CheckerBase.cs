using com.absence.attributes;
using UnityEngine;

namespace com.game.utilities.checkers
{
    public abstract class CheckerBase : MonoBehaviour
    {
        public const int DEFAULT_ARRAY_SIZE = 8;

        public enum RefreshMode
        {
            [InspectorName("None (Manual)")] None,
            FixedUpdate,
        }

        [SerializeField] protected RefreshMode m_refreshMode = RefreshMode.FixedUpdate;
        [SerializeField] protected int m_arraySize = DEFAULT_ARRAY_SIZE;
        [SerializeField, DisableIf(nameof(m_drawGizmosAlways))] protected bool m_drawGizmosSelected = false;
        [SerializeField] protected bool m_drawGizmosAlways = true;
        [SerializeField, Runtime, Readonly] protected Rigidbody m_resultRigidbody;
        [SerializeField, Runtime, Readonly] protected Vector3 m_globalContactPoint;
        [SerializeField, Runtime, Readonly] protected Transform m_resultTransform;
        [SerializeField, Runtime, Readonly] protected bool m_result;

        public Transform ResultTransform => m_resultTransform;
        public Rigidbody ResultRigidbody => m_resultRigidbody;
        public Vector3 ResultContactPoint => m_globalContactPoint;
        public Collider[] RawResult => m_rawResult;
        public RefreshMode Mode { get { return m_refreshMode; } set { m_refreshMode = value; } }

        protected virtual int p_arraySize => m_arraySize;

        public virtual bool Result
        {
            get
            {
                return m_result;
            }
        }

        protected Collider[] m_rawResult;

        private void OnEnable()
        {
            if (!Application.isPlaying)
                return;

            m_rawResult = new Collider[m_arraySize];
        }

        private void FixedUpdate()
        {
            if (m_refreshMode == RefreshMode.FixedUpdate)
                CheckInternal();
        }

        public void Check()
        {
            if (m_refreshMode != RefreshMode.None)
            {
                Debug.LogWarning("You cannot refresh a none-manual RayChecker manually. Change it to be manual in the inspector.");
                return;
            }

            CheckInternal();
        }

        public void Reparse()
        {
            ParseInternal();
        }

        void CheckInternal()
        {
            Clear();

            m_result = OnCheck(ref m_rawResult);

            ParseInternal();
        }

        void Clear()
        {
            for (int i = 0; i < m_arraySize; i++)
            {
                m_rawResult[i] = null;
            }
        }

        void ParseInternal()
        {
            OnParse(m_rawResult, out m_resultRigidbody, out m_globalContactPoint, out m_resultTransform);
        }

        protected virtual Vector3 GetDirection()
        {
            return transform.forward;
        }

        protected abstract bool OnCheck(ref Collider[] rawResult);
        protected abstract void OnParse(Collider[] rawResult, out Rigidbody rigidbody, out Vector3 contactPoint, out Transform transform);
        protected abstract void OnGizmos();

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            if (!m_drawGizmosAlways)
                return;

            OnGizmos();
#endif
        }

        private void OnDrawGizmosSelected()
        {
#if UNITY_EDITOR
            if (m_drawGizmosAlways)
                return;

            if (!m_drawGizmosSelected)
                return;

            OnGizmos();
#endif
        }
    }
}
