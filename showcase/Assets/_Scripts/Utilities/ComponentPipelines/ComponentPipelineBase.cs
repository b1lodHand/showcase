using com.absence.attributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.game.utilities.componentpipelines
{
    public abstract class ComponentPipelineBase<T> : MonoBehaviour, IComponentPipeline<T>
    {
        [SerializeField, Readonly] protected List<ComponentPipelineComponentBase<T>> m_pipeline;

        public List<ComponentPipelineComponentBase<T>> Pipeline => m_pipeline;

        [ContextMenu("Refresh Pipeline")]
        public void Refresh()
        {
            m_pipeline = GetComponents<ComponentPipelineComponentBase<T>>()
                .OrderBy(pipe => pipe.Order).ToList();

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
