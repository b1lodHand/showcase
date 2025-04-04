using com.absence.attributes;
using UnityEngine;

namespace com.game.utilities.extensiblecomponents
{
    public abstract class ComponentExtensionBase<T1> : MonoBehaviour
    {
        [SerializeField] private uint m_order;

        public uint Order => m_order;

        public abstract void ApplyLogic(T1 context);

        //public abstract T2 ApplyLogic<T2>(T2 value, T1 context);
    }

    public abstract class ComponentExtensionBase<T1, T2> : ComponentExtensionBase<T2> where T1 : MonoBehaviour
    {
        [SerializeField, Readonly] private T1 m_target;

        public override void ApplyLogic(T2 context)
        {
            ApplyLogic(m_target, context);
        }
        public abstract void ApplyLogic(T1 target, T2 context);

        //public override T3 ApplyLogic<T3>(T3 value, T2 context)
        //{
        //    return ApplyLogic(m_target, value, context);
        //}
        //public virtual T3 ApplyLogic<T3>(T1 target, T3 value, T2 context)
        //{
        //    return default;
        //}

        private void Reset()
        {
            FetchTarget();
        }

        [ContextMenu("Find Target")]
        void FetchTarget()
        {
            m_target = GetComponent<T1>();
        }
    }
}
