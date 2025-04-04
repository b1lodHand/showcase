using com.absence.attributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.game.utilities.extensiblecomponents
{
    public abstract class ExtensibleComponentBase<T> : MonoBehaviour, IExtensibleComponent<T>
    {
        [SerializeField, Readonly] protected List<ComponentExtensionBase<T>> m_extensionList;

        public List<ComponentExtensionBase<T>> Extensions => m_extensionList;

        [ContextMenu("Refresh Extension List")]
        public void Refresh()
        {
            m_extensionList = GetComponents<ComponentExtensionBase<T>>()
                .OrderBy(ext => ext.Order).ToList();

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
