using UnityEditor;
using UnityEngine;
using Settings;

namespace InEditor
{
    [CustomEditor(typeof(InventorySettings))]
    public sealed class EditorInventorySettings : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Fill settings"))
            {
                FillSettings();
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            base.OnInspectorGUI();
        }

        private void FillSettings()
        {
            var inventorySettings = (InventorySettings)target;
            var path = "Assets/Settings/Inventory/Items";
            var guids = AssetDatabase.FindAssets("t:ItemSettings", new string[] { path });

            foreach (var guid in guids)
            {
                var settingsPath = AssetDatabase.GUIDToAssetPath(guid);
                var settings = AssetDatabase.LoadAssetAtPath<ItemSettings>(settingsPath);

                inventorySettings.Add(settings);
            }

            EditorUtility.SetDirty(inventorySettings);
            AssetDatabase.SaveAssets();
        }
    }
}