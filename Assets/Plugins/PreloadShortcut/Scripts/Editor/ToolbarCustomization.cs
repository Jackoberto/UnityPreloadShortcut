using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Plugins.PreloadShortcut
{
    public class ToolbarCustomization : ScriptableObject
    {
        [SerializeField] private bool enabled = true;
        [SerializeField] private SceneAsset preloadScene;
        [SerializeField, Tooltip("Loads the first Scene that's set up in the Build Settings")]
        bool loadMainScene = true;
        
        public Texture SettingsWheelColored
        {
            get
            {
                var guids = AssetDatabase.FindAssets("SettingsWheelColored");
                var path = AssetDatabase.GUIDToAssetPath(guids.First());
                var settingsWheelColored = AssetDatabase.LoadAssetAtPath<Texture>(path);
                return settingsWheelColored;
            }
        }

        public Texture PlayButtonColored
        {
            get
            {
                var guids = AssetDatabase.FindAssets("PlayButtonColored");
                var path = AssetDatabase.GUIDToAssetPath(guids.First());
                var playButtonColored = AssetDatabase.LoadAssetAtPath<Texture>(path);
                return playButtonColored;
            }
        }

        public GUIStyle GuiStyle
        {
            get
            {
                var guiStyle = new GUIStyle(ToolbarStyles.commandButtonStyle)
                {
                    normal =
                    {
                        background = TextureCreator.GetTextureOfColor(new Vector2Int(1, 1), new Color(0f, 0.6f, 0)),
                        textColor = Color.white
                    },
                    hover =
                    {
                        background = TextureCreator.GetTextureOfColor(new Vector2Int(1, 1), new Color(0f, 0.8f, 0)),
                        textColor = Color.white
                    },
                    imagePosition = ImagePosition.ImageLeft
                };
                return guiStyle;
            }
        }

        public SceneAsset SceneAsset => loadMainScene 
            ? AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[0].path) 
            : preloadScene;

        public bool Enabled => enabled;
    }
}