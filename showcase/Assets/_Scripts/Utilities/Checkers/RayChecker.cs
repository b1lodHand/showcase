using UnityEngine;

namespace com.game.utilities.checkers
{
    public abstract class RayChecker : CheckerBase
    {
        public static readonly Color GizmoRayDefaultColor = Color.red;
        public static readonly Color GizmoRayHitColor = Color.yellow;

        [SerializeField] protected LayerMask m_layerMask;
        [SerializeField] protected QueryTriggerInteraction m_queryTriggerInteraction;

        public Ray BuildRay(Vector3 localOrigin)
        {
            return new Ray(transform.position + localOrigin, GetDirection());
        }

        public bool CheckRay(Ray ray, float length, out RaycastHit hit)
        {
            bool result = Physics.Raycast(ray, out hit, length, m_layerMask, m_queryTriggerInteraction);
            return result;
        }

        public Ray BuildRay()
        {
            return BuildRay(Vector3.zero);
        }

        public void GizmosDrawRay(Vector3 localOrigin, float length, bool hit)
        {
            GizmosDrawRay(BuildRay(localOrigin), length, hit);
        }

        public void GizmosDrawRay(Ray ray, float length)
        {
            bool hit = CheckRay(ray, length, out _);
            GizmosDrawRay(ray, length, hit);
        }

        public void GizmosDrawRay(Ray ray, float length, bool hit)
        {
            Color color = hit ? GizmoRayHitColor : GizmoRayDefaultColor;

            Color previousColor = Gizmos.color;

            Gizmos.color = color;
            Gizmos.DrawLine(ray.origin, ray.origin + (length * ray.direction));

            Gizmos.color = previousColor;
        }
    }
}
