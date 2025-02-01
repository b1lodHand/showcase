using System.Runtime.Serialization;

namespace com.game.saving
{
    /// <summary>
    /// The singleton class responsible for holding all of the saveable data for the game.
    /// </summary>
    [System.Serializable]
    [DataContract(Name = "GameData", Namespace = "com.game.saving")]
    public class SaveData : IExtensibleDataObject
    {
        #region Singleton
        private static SaveData m_instance;
        public static SaveData Current
        {
            get
            {
                if (m_instance == null) Reset();

                return m_instance;
            }

            set
            {
                m_instance = value;
            }
        }

        public static void Reset()
        {
            m_instance = new SaveData();
        }
        #endregion

        #region DataContract Needs
        private ExtensionDataObject m_extensionDataObject;
        public ExtensionDataObject ExtensionData
        {
            get
            {
                return m_extensionDataObject;
            }

            set
            {
                m_extensionDataObject = value;
            }
        }
        #endregion

        /* DATA */
        [DataMember]
        public int Example { get; set; } = 0;
    }
}