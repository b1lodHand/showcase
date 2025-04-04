using com.absence.attributes;
using UnityEngine;

namespace com.game.utilities.extensiblecomponents
{
    public abstract class ComponentExtensionBase<T1> : MonoBehaviour
    {
        [SerializeField] private int m_order;

        public int Order => m_order;

        public abstract void Apply(T1 context);
    }

    public abstract class ComponentExtensionBase<T1, T2> : ComponentExtensionBase<T2> where T1 : MonoBehaviour
    {
        [SerializeField, Readonly] protected T1 m_target;

        public override void Apply(T2 context)
        {
            Apply(m_target, context);
        }
        public abstract void Apply(T1 target, T2 context);


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
