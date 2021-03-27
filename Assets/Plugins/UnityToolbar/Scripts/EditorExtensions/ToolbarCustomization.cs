using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Plugins.UnityToolbar
{
    public class ToolbarCustomization : ScriptableObject
    {
        [SerializeField] private bool enabled = true;
        [SerializeField] private SceneAsset preloadScene;
        
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

        public SceneAsset SceneAsset => preloadScene;

        public bool Enabled => enabled;
    }
}