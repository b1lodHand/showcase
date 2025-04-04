using com.absence.attributes;
using UnityEngine;

namespace com.game.utilities.componentpipelines
{
    public abstract class ComponentPipelineComponentBase<T>  : MonoBehaviour
    {
        [SerializeField] private int m_order;

        public int Order => m_order;

        public abstract T Enpipe(T value);
    }

    public abstract class ComponentPipelineComponentBase<T1, T2> : ComponentPipelineComponentBase<T2> where T1 : MonoBehaviour
    {
        [SerializeField, Readonly] protected T1 m_target;

        public override T2 Enpipe(T2 value)
        {
            return Enpipe(m_target, value);
        }
        public abstract T2 Enpipe(T1 target, T2 context);

        private void Reset()
        {
            FetchTarget();
        }

        [Button("Find Target")]
        void FetchTarget()
        {
            m_target = GetComponent<T1>();

#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                UnityEditor.EditorUtility.SetDirty(this);
                UnityEditor.AssetDatabase.SaveAssetIfDirty(this);
            }
#endif
        }
    }
}
