using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityToolbarExtender;

namespace Plugins.UnityToolbar
{
    [InitializeOnLoad]
    public static class EditorSceneManager
    {
        static EditorSceneManager()
        {
            EditorApplication.playModeStateChanged += EditorApplicationOnplayModeStateChanged;
            AddToolBar();
        }

        private static ToolbarCustomization _settings;

        private static ToolbarCustomization Settings
        {
            get
            {
                _settings = AssetQuerying.FindAssetsByType<ToolbarCustomization>().First();
                if (_settings == null)
                {
                    _settings = ScriptableObject.CreateInstance<ToolbarCustomization>();
                    AssetDatabase.CreateAsset(_settings, "Assets/Plugins/UnityToolbar/Settings/ToolbarSettings.asset");
                }

                return _settings;
            }
        }

        private static void AddToolBar()
        {
            ToolbarExtender.RightToolbarGUI.Add(RightButtons);
            ToolbarExtender.LeftToolbarGUI.Add(LeftButtons);
        }

        private static void RightButtons()
        {
            if (GUILayout.Button(new GUIContent(Settings.SettingsWheelColored, "Open Settings"), Settings.GuiStyle))
            {
                Selection.objects = new Object[] {Settings};
            }
            if (!Settings.Enabled)
                return;
            GUILayout.FlexibleSpace();
        }

        private static void LeftButtons()
        {
            if (!Settings.Enabled)
                return;
            GUILayout.FlexibleSpace();

            if (GUILayout.Button(new GUIContent(Settings.PlayButtonColored, "Starts From PreLoadScene"), Settings.GuiStyle))
            {
                PlayFromPreLaunchScene();
            }
        }

        private static string LastPlayedPath
        {
            get => PlayerPrefs.GetString("LastPlayedScene", "");
            set => PlayerPrefs.SetString("LastPlayedScene", value);
        }

        private static void PlayFromPreLaunchScene()
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
                return;
            }

            LastPlayedPath = SceneManager.GetActiveScene().path;
            UnityEditor.SceneManagement.EditorSceneManager.SaveModifiedScenesIfUserWantsTo(new [] {SceneManager.GetActiveScene()});
            var scene = AssetQuerying.GetScenePathWithAsset(Settings.SceneAsset);
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene(scene);
            EditorApplication.isPlaying = true;
        }

        private static void EditorApplicationOnplayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredEditMode && !string.IsNullOrEmpty(LastPlayedPath))
            {
                EditorApplication.playModeStateChanged -= EditorApplicationOnplayModeStateChanged;
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene(LastPlayedPath);
                LastPlayedPath = null;
            }
        }
    }
}