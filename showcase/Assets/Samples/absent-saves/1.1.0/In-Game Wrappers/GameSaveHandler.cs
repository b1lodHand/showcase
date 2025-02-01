using com.absence.savesystem;
using com.absence.savesystem.builtin;

namespace com.game.saving
{
    /// <summary>
    /// A static wrapper class for handling the save/load process of the game easily.
    /// </summary>
    public static class GameSaveHandler
    {
        /// <summary>
        /// Use to create a new game.
        /// </summary>
        /// <param name="saveName">Name of the new save.</param>
        /// <returns>False if anything goes wrong, true otherwise.</returns>
        public static bool NewGame(string saveName)
        {
            SaveData.Reset();
            return SaveLoadHandler.NewGame(SaveData.Current, OnHandleLoadedData, new DataContractSerializator(saveName, typeof(SaveData)));
        }

        /// <summary>
        /// Use to save the current save quickly.
        /// </summary>
        /// <returns>False if anything goes wrong, true otherwise.</returns>
        public static bool QuickSave()
        {
            return SaveLoadHandler.QuickSave(SaveData.Current, new DataContractSerializator(SaveLoadHandler.CurrentSaveName, typeof(SaveData)));
        }

        /// <summary>
        /// Use to save the game.
        /// </summary>
        /// <param name="saveName">Name of the save file to override.</param>
        /// <returns>False if anything goes wrong, true otherwise.</returns>
        public static bool Save(string saveName)
        {
            return SaveLoadHandler.Save(SaveData.Current, new DataContractSerializator(saveName, typeof(SaveData)));
        }

        /// <summary>
        /// Use to load a save.
        /// </summary>
        /// <param name="saveName">Name of the save file to read from.</param>
        /// <returns>False if anything goes wrong, true otherwise.</returns>
        public static bool Load(string saveName)
        {
            return SaveLoadHandler.Load(OnHandleLoadedData, new DataContractSerializator(saveName, typeof(SaveData)));
        }

        static void OnHandleLoadedData(object data)
        {
            SaveData.Current = (SaveData)data;
        }
    }

}