#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.AWSPolly
{
   /// <summary>Installs the 'AWSSDK'-package.</summary>
   [InitializeOnLoad]
   public static class ZInstaller
   {
      private const string aws_20 = "CT_RTV_AWS_20";
      private const string aws_45 = "CT_RTV_AWS_45";

      #region Constructor

      static ZInstaller()
      {
#if !CT_DEVELOP
         string pathInstaller = Application.dataPath + EditorUtil.EditorConfig.ASSET_PATH + "3rd party/AWS Polly/Installer/";

         try
         {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            BuildTargetGroup group = BuildPipeline.GetBuildTargetGroup(target);
            //ScriptingImplementation scriptingBackend = PlayerSettings.GetScriptingBackend(group);

            //Debug.Log("API level: " + PlayerSettings.GetApiCompatibilityLevel(group));
            //Debug.Log("Scripting backend: " + PlayerSettings.GetScriptingBackend(group));

#if !CT_RTV_AWS_20
            if (PlayerSettings.GetApiCompatibilityLevel(group) == ApiCompatibilityLevel.NET_Standard_2_0)
            {
               string pathSDKStandard20 = pathInstaller + "AWSPolly_NETStandard2.0.unitypackage";

               if (System.IO.File.Exists(pathSDKStandard20))
               {
                  AssetDatabase.ImportPackage(pathSDKStandard20, false);

                  Crosstales.Common.EditorTask.BaseCompileDefines.AddSymbolsToAllTargets(aws_20);
                  Crosstales.Common.EditorTask.BaseCompileDefines.RemoveSymbolsFromAllTargets(aws_45);
               }
               else
               {
                  Debug.LogWarning("Package not found: " + pathSDKStandard20);
               }
            }
#endif
#if !CT_RTV_AWS_45
            if (PlayerSettings.GetApiCompatibilityLevel(group) != ApiCompatibilityLevel.NET_Standard_2_0)
            {
               string pathSDK35 = pathInstaller + "AWSPolly_NET4.5.unitypackage";

               if (System.IO.File.Exists(pathSDK35))
               {
                  AssetDatabase.ImportPackage(pathSDK35, false);

                  Crosstales.Common.EditorTask.BaseCompileDefines.AddSymbolsToAllTargets(aws_45);
                  Crosstales.Common.EditorTask.BaseCompileDefines.RemoveSymbolsFromAllTargets(aws_20);
               }
               else
               {
                  Debug.LogWarning("Package not found: " + pathSDK35);
               }
            }
#endif
         }
         catch (System.Exception ex)
         {
            Debug.LogError("Could not import the 'AWSSDK'-package: " + ex);
         }
#endif
      }

      #endregion
   }
}
#endif
// © 2020-2021 crosstales LLC (https://www.crosstales.com)