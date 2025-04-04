using UnityEngine;

namespace com.game.utilities.checkers
{
    public class SingleRayChecker : RayChecker
    {
        [SerializeField] private float m_rayLength;

        protected override int p_arraySize => 1;

        RaycastHit m_hit;

        protected override bool OnCheck(ref Collider[] rawResult)
        {
            Ray ray = BuildRay();
            bool result = CheckRay(ray, m_rayLength, out m_hit);

            rawResult[0] = m_hit.collider;

            return result;
        }

        protected override void OnParse(Collider[] rawResult, out Rigidbody rigidbody, out Vector3 contactPoint, out Transform transform)
        {
            if (!Result)
            {
                rigidbody = null;
                contactPoint = Vector3.zero;
                transform = null;
                return;
            }

            rigidbody = m_hit.rigidbody;
            contactPoint = m_hit.point;
            transform = m_hit.transform;
        }

        protected override void OnGizmos()
        {
            GizmosDrawRay(BuildRay(), m_rayLength);
        }
    }
}
