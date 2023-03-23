#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Crosstales.RTVoice.MaryTTS
{
   /// <summary>Editor component for for adding the prefabs from 'MaryTTS' in the "Hierarchy"-menu.</summary>
   public static class VoiceProviderMaryTTSGameObject
   {
      [MenuItem("GameObject/" + Util.Constants.ASSET_NAME + "/3rd party/MaryTTS/VoiceProviderMaryTTS", false, EditorUtil.EditorHelper.GO_ID + 50)]
      private static void AddVoiceProvider()
      {
         PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets" + EditorUtil.EditorConfig.ASSET_PATH + "3rd party/MaryTTS/Prefabs/MaryTTS.prefab", typeof(GameObject)));
         EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
      }

      [MenuItem("GameObject/" + Util.Constants.ASSET_NAME + "/3rd party/MaryTTS/VoiceProviderMaryTTS", true)]
      private static bool AddVoiceProviderValidator()
      {
         return !VoiceProviderMaryTTSEditor.isPrefabInScene;
      }
   }
}
#endif
// © 2020-2021 crosstales LLC (https://www.crosstales.com)