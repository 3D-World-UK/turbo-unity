using com.turbo.core;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace com.turbo.editor
{
    public class CoreEditorAPI
    {
        public static void ConfigureProject()
        {
            Debug.Log("Setting Default Script Defines for Standalone build target");
            PlayerSettings.SetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.Standalone, "TURBO");

            if (!AddressableAssetSettingsDefaultObject.SettingsExists)
            {
                Debug.Log("Addressable settings do not yet exist. Creating new Default settings");
                var settings = AddressableAssetSettingsDefaultObject.GetSettings(true);
            }
            Debug.Log("Looking for AppContext");
            var op = Addressables.LoadAssetAsync<AppContext>("AppContext");
            op.Completed += HandleAppContextResult;
        }

        public static void HandleAppContextResult(AsyncOperationHandle<AppContext> op)
        {
            if (op.Status == AsyncOperationStatus.Failed)
            {
                Debug.Log("AppContext not found");
                CreateAppContext();
            }
            else if (op.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("AppContext already exists");
            }
            Addressables.Release(op);
        }

        public static void CreateAppContext()
        {

            string path = "Assets/AppContext.asset";

            Debug.Log("Creating App Context");
            var settings = AddressableAssetSettingsDefaultObject.Settings;
            var group = settings.DefaultGroup;

            AppContext so = ScriptableObject.CreateInstance<AppContext>();

            AssetDatabase.CreateAsset(so, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            var guid = AssetDatabase.AssetPathToGUID(path);

            var entry = settings.CreateOrMoveEntry(guid, group, readOnly: false, postEvent: false);
            entry.address = "AppContext";
            settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryAdded, entry, true);

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = so;

        }
    }
}
