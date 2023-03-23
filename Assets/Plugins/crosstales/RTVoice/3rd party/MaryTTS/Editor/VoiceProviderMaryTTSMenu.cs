#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Crosstales.RTVoice.MaryTTS
{
   /// <summary>Editor component for for adding the prefabs from 'MaryTTS' in the "Tools"-menu.</summary>
   public static class VoiceProviderMaryTTSMenu
   {
      [MenuItem("Tools/" + Util.Constants.ASSET_NAME + "/Prefabs/3rd party/MaryTTS/VoiceProviderMaryTTS", false, EditorUtil.EditorHelper.MENU_ID + 300)]
      private static void AddVoiceProvider()
      {
         PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets" + EditorUtil.EditorConfig.ASSET_PATH + "3rd party/MaryTTS/Prefabs/MaryTTS.prefab", typeof(GameObject)));
         EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
      }

      [MenuItem("Tools/" + Util.Constants.ASSET_NAME + "/Prefabs/3rd party/MaryTTS/VoiceProviderMaryTTS", true)]
      private static bool AddVoiceProviderValidator()
      {
         return !VoiceProviderMaryTTSEditor.isPrefabInScene;
      }
   }
}
#endif
// © 2020-2021 crosstales LLC (https://www.crosstales.com)