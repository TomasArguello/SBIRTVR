#if UNITY_EDITOR && (CT_RTV_AWS_20 || CT_RTV_AWS_45 || CT_DEVELOP)
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Crosstales.RTVoice.AWSPolly
{
   /// <summary>Editor component for for adding the prefabs from 'AWS Polly' in the "Hierarchy"-menu.</summary>
   public static class VoiceProviderAWSGameObject
   {
      [MenuItem("GameObject/" + Util.Constants.ASSET_NAME + "/3rd party/AWS Polly/VoiceProviderAWS", false, EditorUtil.EditorHelper.GO_ID + 10)]
      private static void AddVoiceProvider()
      {
         PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets" + EditorUtil.EditorConfig.ASSET_PATH + "3rd party/AWS Polly/Prefabs/AWS Polly.prefab", typeof(GameObject)));
         EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
      }

      [MenuItem("GameObject/" + Util.Constants.ASSET_NAME + "/3rd party/AWS Polly/VoiceProviderAWS", true)]
      private static bool AddVoiceProviderValidator()
      {
         return !VoiceProviderAWSEditor.isPrefabInScene;
      }
   }
}
#endif
// © 2018-2021 crosstales LLC (https://www.crosstales.com)