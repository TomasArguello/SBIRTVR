#if UNITY_EDITOR && (CT_RTV_AWS_20 || CT_RTV_AWS_45 || CT_DEVELOP)
using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.AWSPolly
{
   /// <summary>Custom editor for the 'VoiceProviderAWS'-class.</summary>
   [CustomEditor(typeof(VoiceProviderAWS))]
   public class VoiceProviderAWSEditor : Editor
   {
      #region Variables

      private VoiceProviderAWS script;

      private string cognitoCredentials = string.Empty;
      private Endpoint endpoint = Endpoint.EUCentral1; //USEast1
      private bool autoBreath = true;
      private SampleRate sampleRate = SampleRate._22050Hz;
      private bool useNeural = false;

      #endregion


      #region Properties

      public static bool isPrefabInScene => GameObject.Find("AWS Polly") != null;

      #endregion


      #region Editor methods

      public void OnEnable()
      {
         script = (VoiceProviderAWS)target;
      }

      public override void OnInspectorGUI()
      {
         EditorUtil.EditorHelper.BannerOC();

         if (GUILayout.Button(new GUIContent(" Learn more", EditorUtil.EditorHelper.Icon_Manual, "Learn more about AWS Polly.")))
         {
            Util.Helper.OpenURL("https://aws.amazon.com/polly/");
         }

         GUILayout.Label("AWS Connection", EditorStyles.boldLabel);

         cognitoCredentials = EditorGUILayout.PasswordField(new GUIContent("Cognito Credentials", "Cognito credentials for AWS Polly."), script.CognitoCredentials);
         if (!cognitoCredentials.Equals(script.CognitoCredentials))
         {
            serializedObject.FindProperty("cognitoCredentials").stringValue = cognitoCredentials;
            serializedObject.ApplyModifiedProperties();
         }

         endpoint = (Endpoint)EditorGUILayout.EnumPopup(new GUIContent("Endpoint", "Active endpoint for AWS Polly."), script.Endpoint);
         if (endpoint != script.Endpoint)
         {
            serializedObject.FindProperty("endpoint").enumValueIndex = (int)endpoint;
            serializedObject.ApplyModifiedProperties();
         }

         GUILayout.Space(8);
         GUILayout.Label("Voice Settings", EditorStyles.boldLabel);

         autoBreath = EditorGUILayout.Toggle(new GUIContent("Auto Breath", "Enables or disables the simulation of natural breathing while speaking (default: true)."), script.AutoBreath);
         if (autoBreath != script.AutoBreath)
         {
            serializedObject.FindProperty("autoBreath").boolValue = autoBreath;
            serializedObject.ApplyModifiedProperties();
         }

         sampleRate = (SampleRate)EditorGUILayout.EnumPopup(new GUIContent("Sample Rate", "Desired sample rate in Hz (default: 22050)."), script.SampleRate);
         if (sampleRate != script.SampleRate)
         {
            serializedObject.FindProperty("sampleRate").enumValueIndex = (int)sampleRate;
            serializedObject.ApplyModifiedProperties();
         }

         useNeural = EditorGUILayout.Toggle(new GUIContent("Use Neural Voices", "Enable or disable neural voices (default: false)."), script.UseNeuralVoices);
         if (useNeural != script.UseNeuralVoices)
         {
            serializedObject.FindProperty("useNeuralVoices").boolValue = useNeural;
            serializedObject.ApplyModifiedProperties();
         }

         //DrawDefaultInspector();

         //EditorHelper.SeparatorUI();

         if (script.isActiveAndEnabled)
         {
            /*
            if (Util.Helper.isIL2CPP && Util.Helper.isAndroidPlatform)
            {
                EditorUtil.EditorHelper.SeparatorUI();
                EditorGUILayout.HelpBox("IL2CPP under Android is not supported by AWS Polly. Please use Mono, MaryTTS or a custom provider (e.g. Klattersynth).", MessageType.Error);
            }
            else 
            */
            if (!script.isPlatformSupported)
            {
               EditorUtil.EditorHelper.SeparatorUI();
               EditorGUILayout.HelpBox("The current platform is not supported by AWS Polly. Please use MaryTTS or a custom provider (e.g. Klattersynth).", MessageType.Error);
            }
            else
            {
               if (script.isValidCognitoCredentials)
               {
                  if (script.UseNeuralVoices && !script.hasNeuralVoices)
                  {
                     EditorUtil.EditorHelper.SeparatorUI();
                     EditorGUILayout.HelpBox("The current AWS endpoint does not support neural voices.", MessageType.Warning);
                  }

                  //add stuff if needed
               }
               else
               {
                  EditorUtil.EditorHelper.SeparatorUI();
                  EditorGUILayout.HelpBox("Please add valid 'Cognito Credentials' to access AWS Polly!", MessageType.Warning);
               }
            }
         }
         else
         {
            EditorUtil.EditorHelper.SeparatorUI();
            EditorGUILayout.HelpBox("Script is disabled!", MessageType.Info);
         }
      }

      #endregion
   }
}
#endif
// © 2018-2021 crosstales LLC (https://www.crosstales.com)