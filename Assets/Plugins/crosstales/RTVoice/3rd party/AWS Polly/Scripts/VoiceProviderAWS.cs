#if CT_RTV_AWS_20 || CT_RTV_AWS_45 || CT_DEVELOP
using UnityEngine;
using System.Collections;
using System.Linq;

namespace Crosstales.RTVoice.AWSPolly
{
   /// <summary>AWS Polly voice provider.</summary>
   [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_a_w_s_polly_1_1_voice_provider_a_w_s.html")]
   public class VoiceProviderAWS : Provider.BaseCustomVoiceProvider
   {
      #region Variables

      [UnityEngine.Serialization.FormerlySerializedAsAttribute("CognitoCredentials")] [Header("AWS Connection"), Tooltip("Cognito credentials to access AWS Polly."), SerializeField]
      private string cognitoCredentials = string.Empty;

      [UnityEngine.Serialization.FormerlySerializedAsAttribute("Endpoint")] [Tooltip("AWS endpoint for the connection."), SerializeField]
      private Endpoint endpoint = Endpoint.APNortheast1;


      [UnityEngine.Serialization.FormerlySerializedAsAttribute("AutoBreath")] [Header("Voice Settings"), Tooltip("Enables or disables the simulation of natural breathing while speaking (default: true)."), SerializeField]
      private bool autoBreath = true;

      [UnityEngine.Serialization.FormerlySerializedAsAttribute("SampleRate")] [Tooltip("Desired sample rate in Hz (default: 22050)."), SerializeField]
      private SampleRate sampleRate = SampleRate._22050Hz;


      [UnityEngine.Serialization.FormerlySerializedAsAttribute("UseNeuralVoices")] [Tooltip("Enable or disable neural voices (default: false)."), SerializeField]
      private bool useNeuralVoices;

      private System.Collections.Generic.List<Model.Voice> cachedNeuralVoices = new System.Collections.Generic.List<Model.Voice>();

//#if (!UNITY_WSA && !UNITY_WEBGL) || UNITY_EDITOR
      private Amazon.Polly.AmazonPollyClient client;
      private static System.Collections.Generic.Dictionary<Endpoint, Amazon.RegionEndpoint> endpoints;

      private string lastCredentials = string.Empty;
//#endif

      private static readonly System.Text.RegularExpressions.Regex apiRegex = new System.Text.RegularExpressions.Regex(@"[\w-]+:[0-9a-f-]{36}$");

      #endregion


      #region Properties

      /// <summary>Cognito credentials to access AWS Polly.</summary>
#if CT_DEVELOP
      public string CognitoCredentials
      {
         get => string.IsNullOrEmpty(cognitoCredentials) ? APIKeys.AWS : cognitoCredentials;
         set => cognitoCredentials = value;
      }
#else
      public string CognitoCredentials
      {
         get => cognitoCredentials;
         set => cognitoCredentials = value;
      }
#endif

      /// <summary>AWS endpoint for the connection.</summary>
      public Endpoint Endpoint
      {
         get => endpoint;
         set => endpoint = value;
      }

      /// <summary>Enables or disables the simulation of natural breathing while speaking.</summary>
      public bool AutoBreath
      {
         get => autoBreath;
         set => autoBreath = value;
      }

      /// <summary>Desired sample rate in Hz.</summary>
      public SampleRate SampleRate
      {
         get => sampleRate;
         set => sampleRate = value;
      }

      /// <summary>Enable or disable neural voices.</summary>
      public bool UseNeuralVoices
      {
         get => useNeuralVoices;
         set => useNeuralVoices = value;
      }

      public override string AudioFileExtension => ".ogg";

      public override AudioType AudioFileType => AudioType.OGGVORBIS;

      public override string DefaultVoiceName => "Matthew";

      public override bool isWorkingInEditor => false;

      public override bool isWorkingInPlaymode => true;

      public override bool isPlatformSupported => !Util.Helper.isWebPlatform; // && ! Util.Helper.isWSABasedPlatform; }

      public override int MaxTextLength => 256000;

      public override bool isSpeakNativeSupported => false;

      public override bool isSpeakSupported => true;

      public override bool isSSMLSupported => true;

      public override bool isOnlineService => true;

      public override bool hasCoRoutines => true;

      public override bool isIL2CPPSupported => true;

      public override bool hasVoicesInEditor => true;

      /// <summary>Indicates if the Cognito Credentials are valid.</summary>
      /// <returns>True if the Cognito Credentials are valid.</returns>
      public bool isValidCognitoCredentials => !string.IsNullOrEmpty(CognitoCredentials) && apiRegex.IsMatch(CognitoCredentials);

      public override System.Collections.Generic.List<Model.Voice> Voices => useNeuralVoices && hasNeuralVoices ? cachedNeuralVoices : cachedVoices;

      /// <summary>Checks if neural voices are supported on the current AWS endpoint.</summary>
      /// <returns>True if neural voices are supported on the current AWS endpoint.</returns>
      public bool hasNeuralVoices =>
         endpoint == Endpoint.USEast1 ||
         endpoint == Endpoint.USWest2 ||
         endpoint == Endpoint.EUWest1 ||
         endpoint == Endpoint.EUWest2 ||
         endpoint == Endpoint.EUCentral1 ||
         endpoint == Endpoint.APNortheast1 ||
         endpoint == Endpoint.APSoutheast1 ||
         endpoint == Endpoint.APSoutheast2;


      //#if (!UNITY_WSA && !UNITY_WEBGL) || UNITY_EDITOR
      private Amazon.RegionEndpoint getAWSEndpoint
      {
         get
         {
            if (endpoints == null)
            {
               endpoints = new System.Collections.Generic.Dictionary<Endpoint, Amazon.RegionEndpoint>
               {
                  {Endpoint.APNortheast1, Amazon.RegionEndpoint.APNortheast1},
                  {Endpoint.APNortheast2, Amazon.RegionEndpoint.APNortheast2},
                  {Endpoint.APSouth1, Amazon.RegionEndpoint.APSouth1},
                  {Endpoint.APSoutheast1, Amazon.RegionEndpoint.APSoutheast1},
                  {Endpoint.APSoutheast2, Amazon.RegionEndpoint.APSoutheast2},
                  {Endpoint.CACentral1, Amazon.RegionEndpoint.CACentral1},
                  //{Endpoint.CNNorth1, Amazon.RegionEndpoint.CNNorth1},
                  {Endpoint.EUCentral1, Amazon.RegionEndpoint.EUCentral1},
                  {Endpoint.EUWest1, Amazon.RegionEndpoint.EUWest1},
                  {Endpoint.EUWest2, Amazon.RegionEndpoint.EUWest2},
                  {Endpoint.SAEast1, Amazon.RegionEndpoint.SAEast1},
                  {Endpoint.USEast1, Amazon.RegionEndpoint.USEast1},
                  {Endpoint.USEast2, Amazon.RegionEndpoint.USEast2},
                  //{Endpoint.USGovCloudWest1, Amazon.RegionEndpoint.USGovCloudWest1},
                  {Endpoint.USWest1, Amazon.RegionEndpoint.USWest1},
                  {Endpoint.USWest2, Amazon.RegionEndpoint.USWest2}
               };
            }

            return endpoints[endpoint];
         }
      }
//#endif

      #endregion


      #region MonoBehaviour methods

#if CT_DEVELOP
      private void Awake()
      {
         Endpoint = Endpoint.EUCentral1;
      }
#endif

      private void Start()
      {
         connect(null);
      }

      #endregion


      #region Implemented methods

      public override void Load(bool forceReload = false)
      {
         if (cachedVoices?.Count == 0 || forceReload)
         {
            System.Collections.Generic.List<Model.Voice> neuralVoices = new System.Collections.Generic.List<Model.Voice>
            {
               new Model.Voice("Amy", "English, British (en-GB)", Model.Enum.Gender.FEMALE, "Adult", "en-GB"),
               new Model.Voice("Emma", "English, British (en-GB)", Model.Enum.Gender.FEMALE, "Adult", "en-GB"),
               new Model.Voice("Brian", "English, British (en-GB)", Model.Enum.Gender.MALE, "Adult", "en-GB"),
               new Model.Voice("Ivy", "English, US (en-US)", Model.Enum.Gender.FEMALE, "Child", "en-US"),
               new Model.Voice("Joanna", "English, US (en-US)", Model.Enum.Gender.FEMALE, "Adult", "en-US"),
               new Model.Voice("Kendra", "English, US (en-US)", Model.Enum.Gender.FEMALE, "Adult", "en-US"),
               new Model.Voice("Kimberly", "English, US (en-US)", Model.Enum.Gender.FEMALE, "Adult", "en-US"),
               new Model.Voice("Salli", "English, US (en-US)", Model.Enum.Gender.FEMALE, "Adult", "en-US"),
               new Model.Voice("Joey", "English, US (en-US)", Model.Enum.Gender.MALE, "Adult", "en-US"),
               new Model.Voice("Justin", "English, US (en-US)", Model.Enum.Gender.MALE, "Child", "en-US"),
               new Model.Voice("Kevin", "English, US (en-US)", Model.Enum.Gender.MALE, "Child", "en-US"), //neural only?
               new Model.Voice("Matthew", "English, US (en-US)", Model.Enum.Gender.MALE, "Adult", "en-US"),
               new Model.Voice("Camila", "Portuguese, Brazilian (pt-BR)", Model.Enum.Gender.FEMALE, "Adult", "pt-BR"),
               new Model.Voice("Lupe", "Spanish, US (es-US)", Model.Enum.Gender.FEMALE, "Adult", "es-US")
            };

            cachedNeuralVoices = neuralVoices.OrderBy(s => s.Name).ToList();

            System.Collections.Generic.List<Model.Voice> voices = new System.Collections.Generic.List<Model.Voice>
            {
               new Model.Voice("Zeina", "Arabic (arb)", Model.Enum.Gender.FEMALE, "Adult", "ar"),
               new Model.Voice("Zhiyu", "Chinese, Mandarin (cmn-CN)", Model.Enum.Gender.FEMALE, "Adult", "zh"),
               new Model.Voice("Mads", "Danish (da-DK)", Model.Enum.Gender.MALE, "Adult", "da-DK"),
               new Model.Voice("Naja", "Danish (da-DK)", Model.Enum.Gender.FEMALE, "Adult", "da-DK"),
               new Model.Voice("Ruben", "Dutch (nl-NL)", Model.Enum.Gender.MALE, "Adult", "nl-NL"),
               new Model.Voice("Lotte", "Dutch (nl-NL)", Model.Enum.Gender.FEMALE, "Adult", "nl-NL"),
               new Model.Voice("Russell", "English, Australian (en-AU)", Model.Enum.Gender.MALE, "Adult", "en-AU"),
               new Model.Voice("Nicole", "English, Australian (en-AU)", Model.Enum.Gender.FEMALE, "Adult", "en-AU"),
               new Model.Voice("Aditi", "English, Indian (en-IN)", Model.Enum.Gender.FEMALE, "Adult", "en-IN"),
               new Model.Voice("Raveena", "English, Indian (en-IN)", Model.Enum.Gender.FEMALE, "Adult", "en-IN"),
               new Model.Voice("Geraint", "English, Welsh (en-GB-WLS)", Model.Enum.Gender.MALE, "Adult", "en-GB-WLS"),
               new Model.Voice("Mathieu", "French (fr-FR)", Model.Enum.Gender.MALE, "Adult", "fr-FR"),
               new Model.Voice("Celine", "French (fr-FR)", Model.Enum.Gender.FEMALE, "Adult", "fr-FR"),
               new Model.Voice("Lea", "French (fr-FR)", Model.Enum.Gender.FEMALE, "Adult", "fr-FR"),
               new Model.Voice("Chantal", "French (fr-CA)", Model.Enum.Gender.FEMALE, "Adult", "fr-CA"),
               new Model.Voice("Hans", "German (de-DE)", Model.Enum.Gender.MALE, "Adult", "de-DE"),
               new Model.Voice("Marlene", "German (de-DE)", Model.Enum.Gender.FEMALE, "Adult", "de-DE"),
               new Model.Voice("Vicki", "German (de-DE)", Model.Enum.Gender.FEMALE, "Adult", "de-DE"),
               new Model.Voice("Karl", "Icelandic (is-IS)", Model.Enum.Gender.MALE, "Adult", "is-IS"),
               new Model.Voice("Dora", "Icelandic (is-IS)", Model.Enum.Gender.FEMALE, "Adult", "is-IS"),
               new Model.Voice("Giorgio", "Italian (it-IT)", Model.Enum.Gender.MALE, "Adult", "it-IT"),
               new Model.Voice("Carla", "Italian (it-IT)", Model.Enum.Gender.FEMALE, "Adult", "it-IT"),
               new Model.Voice("Bianca", "Italian (it-IT)", Model.Enum.Gender.FEMALE, "Adult", "it-IT"),
               new Model.Voice("Takumi", "Japanese (ja-JP)", Model.Enum.Gender.MALE, "Adult", "ja-JP"),
               new Model.Voice("Mizuki", "Japanese (ja-JP)", Model.Enum.Gender.FEMALE, "Adult", "ja-JP"),
               new Model.Voice("Seoyeon", "Korean (ko-KR)", Model.Enum.Gender.FEMALE, "Adult", "ko-KR"),
               new Model.Voice("Liv", "Norwegian (no-NO)", Model.Enum.Gender.FEMALE, "Adult", "no-NO"),
               new Model.Voice("Jacek", "Polish (pl-PL)", Model.Enum.Gender.MALE, "Adult", "pl-PL"),
               new Model.Voice("Jan", "Polish (pl-PL)", Model.Enum.Gender.MALE, "Adult", "pl-PL"),
               new Model.Voice("Ewa", "Polish (pl-PL)", Model.Enum.Gender.FEMALE, "Adult", "pl-PL"),
               new Model.Voice("Maja", "Polish (pl-PL)", Model.Enum.Gender.FEMALE, "Adult", "pl-PL"),
               new Model.Voice("Ricardo", "Portuguese, Brazilian (pt-BR)", Model.Enum.Gender.MALE, "Adult", "pt-BR"),
               new Model.Voice("Vitoria", "Portuguese, Brazilian (pt-BR)", Model.Enum.Gender.FEMALE, "Adult", "pt-BR"),
               new Model.Voice("Cristiano", "Portuguese, European (pt-PT)", Model.Enum.Gender.MALE, "Adult", "pt-PT"),
               new Model.Voice("Ines", "Portuguese, European (pt-PT)", Model.Enum.Gender.FEMALE, "Adult", "pt-PT"),
               new Model.Voice("Carmen", "Romanian (ro-RO)", Model.Enum.Gender.FEMALE, "Adult", "ro-RO"),
               new Model.Voice("Maxim", "Russian (ru-RU)", Model.Enum.Gender.MALE, "Adult", "ru-RU"),
               new Model.Voice("Tatyana", "Russian (ru-RU)", Model.Enum.Gender.FEMALE, "Adult", "ru-RU"),
               new Model.Voice("Enrique", "Spanish, European (es-ES)", Model.Enum.Gender.MALE, "Adult", "es-ES"),
               new Model.Voice("Conchita", "Spanish, European (es-ES)", Model.Enum.Gender.FEMALE, "Adult", "es-ES"),
               new Model.Voice("Lucia", "Spanish, European (es-ES)", Model.Enum.Gender.FEMALE, "Adult", "es-ES"),
               new Model.Voice("Mia", "Spanish, (Mexican) (es-MX)", Model.Enum.Gender.FEMALE, "Adult", "es-MX"),
               new Model.Voice("Miguel", "Spanish, US (es-US)", Model.Enum.Gender.MALE, "Adult", "es-US"),
               new Model.Voice("Penelope", "Spanish, US (es-US)", Model.Enum.Gender.FEMALE, "Adult", "es-US"),
               new Model.Voice("Astrid", "Swedish (sv-SE)", Model.Enum.Gender.FEMALE, "Adult", "sv-SE"),
               new Model.Voice("Filiz", "Turkish (tr-TR)", Model.Enum.Gender.FEMALE, "Adult", "tr-TR"),
               new Model.Voice("Gwyneth", "Welsh (cy-GB)", Model.Enum.Gender.FEMALE, "Adult", "cy-GB")
            };

            voices.AddRange(neuralVoices);
            cachedVoices = voices.OrderBy(s => s.Name).ToList();
         }

         onVoicesReady();
      }

      public override IEnumerator Generate(Model.Wrapper wrapper)
      {
#if !UNITY_WEBGL // && !UNITY_WSA  //TODO check if it works with WSA!
         //Debug.Log("Generate: " + wrapper);
         connect(wrapper);

         if (client == null)
         {
            Debug.LogWarning("'client' is null! Did you enter the correct Cognito credentials?", this);
         }
         else
         {
            if (wrapper == null)
            {
               Debug.LogWarning("'wrapper' is null!", this);
            }
            else
            {
               if (string.IsNullOrEmpty(wrapper.Text))
               {
                  Debug.LogWarning("'wrapper.Text' is null or empty!", this);
               }
               else
               {
                  if (!Util.Helper.isInternetAvailable)
                  {
                     const string errorMessage = "Internet is not available - can't use AWS Polly right now!";
                     Debug.LogError(errorMessage, this);
                     onErrorInfo(wrapper, errorMessage);
                  }
                  else
                  {
                     yield return null; //return to the main process (uid)

                     string voiceCulture = getVoiceCulture(wrapper);
                     string voiceName = getVoiceName(wrapper);

                     Amazon.Polly.Model.SynthesizeSpeechRequest synthesizeSpeechPresignRequest = new Amazon.Polly.Model.SynthesizeSpeechRequest
                     {
                        Text = prepareText(wrapper, voiceCulture),
                        TextType = wrapper.ForceSSML ? Amazon.Polly.TextType.Ssml : Amazon.Polly.TextType.Text,
                        VoiceId = voiceName,
                        Engine = useNeuralVoices && hasNeuralVoices ? Amazon.Polly.Engine.Neural : Amazon.Polly.Engine.Standard,
                        OutputFormat = Amazon.Polly.OutputFormat.Ogg_vorbis
                     };

                     /*
                     if (Util.Helper.isWebPlatform)
                     {
                         synthesizeSpeechPresignRequest.OutputFormat = Amazon.Polly.OutputFormat.Pcm;
                     }
                     else
                     {
                         synthesizeSpeechPresignRequest.OutputFormat = Amazon.Polly.OutputFormat.Ogg_vorbis;
                     }
                     */
                     string _sampleRate = sampleRate.ToString();
                     synthesizeSpeechPresignRequest.SampleRate = _sampleRate.Substring(1, _sampleRate.Length - 3);

                     silence = false;

                     onSpeakAudioGenerationStart(wrapper);

                     string outputFile = getOutputFile(wrapper.Uid, Util.Helper.isWebPlatform);
                     bool success = false;
#if CT_RTV_AWS_20
                     System.Threading.Tasks.Task<Amazon.Polly.Model.SynthesizeSpeechResponse> responseObject = null;

                     System.Threading.Thread worker = new System.Threading.Thread(() => callAmazon(synthesizeSpeechPresignRequest, out responseObject));
                     worker.Start();

                     do
                     {
                        yield return null;
                     } while (worker.IsAlive);

                     if (responseObject.Exception == null)
                     {
                        try
                        {
                           System.IO.File.WriteAllBytes(outputFile, responseObject.Result.AudioStream.CTReadFully());
                           success = true;
                        }
                        catch (System.Exception ex)
                        {
                           string errorMessage = "Could not create output file: " + outputFile + System.Environment.NewLine + "Error: " + ex;
                           Debug.LogError(errorMessage, this);
                           onErrorInfo(wrapper, errorMessage);
                        }
                     }
                     else
                     {
                        string errorMessage = "Could not generate the text: " + wrapper + System.Environment.NewLine + "Error: " + responseObject.Exception;
                        Debug.LogError(errorMessage, this);
                        onErrorInfo(wrapper, errorMessage);
                     }
#else
                     Amazon.Polly.Model.SynthesizeSpeechResponse responseObject = null;

                     System.Threading.Thread worker = new System.Threading.Thread(() => callAmazon(synthesizeSpeechPresignRequest, out responseObject));
                     worker.Start();

                     do
                     {
                        yield return null;
                     } while (worker.IsAlive);

                     if (responseObject != null)
                     {
                        try
                        {
                           System.IO.File.WriteAllBytes(outputFile, responseObject.AudioStream.CTReadFully());
                           success = true;
                        }
                        catch (System.Exception ex)
                        {
                           string errorMessage = "Could not create output file: " + outputFile + System.Environment.NewLine + "Error: " + ex;
                           Debug.LogError(errorMessage, this);
                           onErrorInfo(wrapper, errorMessage);
                        }
                     }
                     else
                     {
                        string errorMessage = "Could not generate the text: " + wrapper;
                        Debug.LogError(errorMessage, this);
                        onErrorInfo(wrapper, errorMessage);
                     }
#endif
                     if (success)
                        processAudioFile(wrapper, outputFile);

                     /*
                     if (Util.Helper.isWebPlatform)
                     {
                         try
                         {
                             if (System.IO.File.Exists(outputFile))
                                 System.IO.File.Delete(outputFile);
                         }
                         catch (System.Exception ex)
                         {
                             Debug.LogError("Could not delete audio file: " + ex, this);
                         }
                     }
                     */
                  }
               }
            }
         }
#else
         Debug.LogError("'Generate' is not supported under WebGL!", this);
         yield return null;
#endif
      }

      public override IEnumerator SpeakNative(Model.Wrapper wrapper)
      {
         if (isPlatformSupported)
         {
            yield return speak(wrapper, true);
         }
         else
         {
            Debug.LogError("'SpeakNative' is not supported for the current platform!", this);
            yield return null;
         }
      }

      public override IEnumerator Speak(Model.Wrapper wrapper)
      {
         if (isPlatformSupported)
         {
            yield return speak(wrapper, false);
         }
         else
         {
            Debug.LogError("'Speak' is not supported for the current platform!", this);
            yield return null;
         }
      }

      protected override string getVoiceName(Model.Wrapper wrapper)
      {
         string voice = base.getVoiceName(wrapper);


         if (useNeuralVoices && hasNeuralVoices)
         {
            if (cachedNeuralVoices.Any(v => v.Name.Equals(voice)))
               return voice;

            Debug.LogWarning("Voice is not neural: " + voice + System.Environment.NewLine + "Speaking with the 'default' voice.", this);

            return DefaultVoiceName;
         }

         return voice;
      }

      #endregion


      #region Private methods

#if UNITY_ANDROID && !UNITY_EDITOR
      [RuntimeInitializeOnLoadMethod]
      static void usedOnlyForAOTCodeGeneration()
      {
         //Bug reported on github https://github.com/aws/aws-sdk-net/issues/477
         //IL2CPP restrictions: https://docs.unity3d.com/Manual/ScriptingRestrictions.html
         //Inspired workaround: https://docs.unity3d.com/ScriptReference/AndroidJavaObject.Get.html

         AndroidJavaObject jo = new AndroidJavaObject("android.os.Message");
         int valueString = jo.Get<int>("what");
         //string valueString = jo.Get<string>("what");
      }
#endif

//#if (!UNITY_WSA && !UNITY_WEBGL) || UNITY_EDITOR
      private void connect(Model.Wrapper wrapper)
      {
         if (!isValidCognitoCredentials)
         {
            client = null;
            string errorMessage = "Please add valid 'Cognito Credentials' to access AWS Polly!";
            Debug.LogError(errorMessage, this);
            onErrorInfo(wrapper, errorMessage);
         }
         else
         {
            if (client == null || !lastCredentials.Equals(CognitoCredentials))
            {
               if (string.IsNullOrEmpty(CognitoCredentials))
               {
                  Debug.LogError("Credentials must not be null or empty!", this);
               }
               else
               {
                  lastCredentials = CognitoCredentials;

                  Amazon.CognitoIdentity.CognitoAWSCredentials credentials = new Amazon.CognitoIdentity.CognitoAWSCredentials(CognitoCredentials, getAWSEndpoint);
                  client = new Amazon.Polly.AmazonPollyClient(credentials, getAWSEndpoint);
               }
            }
         }
      }

#if CT_RTV_AWS_20
      private void callAmazon(Amazon.Polly.Model.SynthesizeSpeechRequest request, out System.Threading.Tasks.Task<Amazon.Polly.Model.SynthesizeSpeechResponse> response)
      {
         response = System.Threading.Tasks.Task.Run(() => client.SynthesizeSpeechAsync(request, System.Threading.CancellationToken.None)); //TODO improve latency
      }
#else
      private void callAmazon(Amazon.Polly.Model.SynthesizeSpeechRequest request, out Amazon.Polly.Model.SynthesizeSpeechResponse response)
      {
         response = client.SynthesizeSpeech(request);
      }
#endif

      private IEnumerator speak(Model.Wrapper wrapper, bool isNative)
      {
         //Debug.Log("Speak: " + wrapper, this);
         connect(wrapper);

         if (client == null)
         {
            Debug.LogWarning("'client' is null! Did you enter the correct Cognito credentials?", this);
         }
         else
         {
            if (wrapper == null)
            {
               Debug.LogWarning("'wrapper' is null!", this);
            }
            else
            {
               if (string.IsNullOrEmpty(wrapper.Text))
               {
                  Debug.LogWarning("'wrapper.Text' is null or empty!", this);
               }
               else
               {
                  if (!Util.Helper.isInternetAvailable)
                  {
                     const string errorMessage = "Internet is not available - can't use AWS Polly right now!";
                     Debug.LogError(errorMessage, this);
                     onErrorInfo(wrapper, errorMessage);
                  }
                  else
                  {
                     yield return null; //return to the main process (uid)

                     string voiceCulture = getVoiceCulture(wrapper);
                     string voiceName = getVoiceName(wrapper);

                     Amazon.Polly.Model.SynthesizeSpeechRequest synthesizeSpeechPresignRequest = new Amazon.Polly.Model.SynthesizeSpeechRequest
                     {
                        Text = prepareText(wrapper, voiceCulture),
                        TextType = wrapper.ForceSSML ? Amazon.Polly.TextType.Ssml : Amazon.Polly.TextType.Text,
                        VoiceId = voiceName,
                        Engine = useNeuralVoices && hasNeuralVoices ? Amazon.Polly.Engine.Neural : Amazon.Polly.Engine.Standard,
                        OutputFormat = Amazon.Polly.OutputFormat.Ogg_vorbis
                     };

                     /*
                     if (Util.Helper.isWebPlatform)
                     {
                         synthesizeSpeechPresignRequest.OutputFormat = Amazon.Polly.OutputFormat.Pcm;
                     }
                     else
                     {
                         synthesizeSpeechPresignRequest.OutputFormat = Amazon.Polly.OutputFormat.Ogg_vorbis;
                     }
                     */
                     string _sampleRate = sampleRate.ToString();
                     synthesizeSpeechPresignRequest.SampleRate = _sampleRate.Substring(1, _sampleRate.Length - 3);

                     silence = false;

                     if (!isNative)
                        onSpeakAudioGenerationStart(wrapper);

                     string outputFile = getOutputFile(wrapper.Uid, Util.Helper.isWebPlatform);
                     bool success = false;
#if CT_RTV_AWS_20
                     System.Threading.Tasks.Task<Amazon.Polly.Model.SynthesizeSpeechResponse> responseObject = null;

                     System.Threading.Thread worker = new System.Threading.Thread(() => callAmazon(synthesizeSpeechPresignRequest, out responseObject));
                     worker.Start();

                     do
                     {
                        yield return null;
                     } while (worker.IsAlive);

                     if (responseObject.Exception == null)
                     {
                        try
                        {
                           System.IO.File.WriteAllBytes(outputFile, responseObject.Result.AudioStream.CTReadFully());
                           success = true;
                        }
                        catch (System.Exception ex)
                        {
                           string errorMessage = "Could not create output file: " + outputFile + System.Environment.NewLine + "Error: " + ex;
                           Debug.LogError(errorMessage, this);
                           onErrorInfo(wrapper, errorMessage);
                        }
                     }
                     else
                     {
                        string errorMessage = "Could not generate the text: " + wrapper + System.Environment.NewLine + "Error: " + responseObject.Exception;
                        Debug.LogError(errorMessage, this);
                        onErrorInfo(wrapper, errorMessage);
                     }
#else
                     Amazon.Polly.Model.SynthesizeSpeechResponse responseObject = null;

                     System.Threading.Thread worker = new System.Threading.Thread(() => callAmazon(synthesizeSpeechPresignRequest, out responseObject));
                     worker.Start();

                     do
                     {
                        yield return null;
                     } while (worker.IsAlive);

                     if (responseObject != null)
                     {
                        try
                        {
                           System.IO.File.WriteAllBytes(outputFile, responseObject.AudioStream.CTReadFully());
                           success = true;
                        }
                        catch (System.Exception ex)
                        {
                           string errorMessage = "Could not create output file: " + outputFile + System.Environment.NewLine + "Error: " + ex;
                           Debug.LogError(errorMessage, this);
                           onErrorInfo(wrapper, errorMessage);
                        }
                     }
                     else
                     {
                        string errorMessage = "Could not speak the text: " + wrapper;
                        Debug.LogError(errorMessage, this);
                        onErrorInfo(wrapper, errorMessage);
                     }
#endif

                     if (success)
                        yield return playAudioFile(wrapper, Util.Helper.ValidURLFromFilePath(outputFile), outputFile, AudioFileType, isNative);

                     //yield return playAudioFile(wrapper, ac, isNative);
                  }
               }
            }
         }
      }

      private string getVoiceCulture(Model.Wrapper wrapper)
      {
         if (wrapper != null && string.IsNullOrEmpty(wrapper.Voice?.Culture))
         {
            if (Util.Config.DEBUG)
               Debug.LogWarning("'wrapper.Voice' or 'wrapper.Voice.Culture' is null! Using the 'default' English voice.", this);

            //always use English as fallback
            return "en-US";
         }

         return wrapper != null ? wrapper.Voice?.Culture : "en-US";
      }

/*
      private static void copyTo(System.IO.Stream input, System.IO.Stream outputAudio)
      {
         byte[] buffer = new byte[1024];
         int bytesRead;

         while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
         {
            outputAudio.Write(buffer, 0, bytesRead);
         }
      }
*/
      private string prepareText(Model.Wrapper wrapper, string voiceCulture)
      {
         //TEST
         //wrapper.ForceSSML = false;

         if (wrapper.ForceSSML && !Speaker.Instance.AutoClearTags)
         {
            System.Text.StringBuilder sbXML = new System.Text.StringBuilder();

            sbXML.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            sbXML.Append(
               "<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"");
            sbXML.Append(voiceCulture);
            sbXML.Append("\"");
            //sbXML.Append (" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
            //sbXML.Append (" xsi:schemaLocation=\"http://www.w3.org/2001/10/synthesis http://www.w3.org/TR/speech-synthesis/synthesis.xsd\"");
            sbXML.Append(">");

            string text = (autoBreath ? "<amazon:auto-breaths>" : string.Empty) + wrapper.Text +
                          (autoBreath ? "</amazon:auto-breaths>" : string.Empty);

            if (Mathf.Abs(wrapper.Rate - 1f) > Common.Util.BaseConstants.FLOAT_TOLERANCE || Mathf.Abs(wrapper.Pitch - 1f) > Common.Util.BaseConstants.FLOAT_TOLERANCE || Mathf.Abs(wrapper.Volume - 1f) > Common.Util.BaseConstants.FLOAT_TOLERANCE)
            {
               sbXML.Append("<prosody");

               if (Mathf.Abs(wrapper.Rate - 1f) > Common.Util.BaseConstants.FLOAT_TOLERANCE)
               {
                  float _rate = wrapper.Rate > 1 ? (wrapper.Rate - 1f) * 0.5f : wrapper.Rate - 1f;

                  sbXML.Append(" rate=\"");
                  sbXML.Append(_rate >= 0f
                     ? _rate.ToString("+#0%", Util.Helper.BaseCulture)
                     : _rate.ToString("#0%", Util.Helper.BaseCulture));

                  sbXML.Append("\"");
               }

               if (Mathf.Abs(wrapper.Pitch - 1f) > Common.Util.BaseConstants.FLOAT_TOLERANCE)
               {
                  float _pitch = wrapper.Pitch - 1f;

                  sbXML.Append(" pitch=\"");
                  sbXML.Append(_pitch >= 0f
                     ? _pitch.ToString("+#0%", Util.Helper.BaseCulture)
                     : _pitch.ToString("#0%", Util.Helper.BaseCulture));

                  sbXML.Append("\"");
               }

               if (Mathf.Abs(wrapper.Volume - 1f) > Common.Util.BaseConstants.FLOAT_TOLERANCE)
               {
                  sbXML.Append(" volume=\"-");

                  int db = (int)((1f - wrapper.Volume) * 20f);

                  sbXML.Append(db);

                  sbXML.Append("dB\"");
               }

               sbXML.Append(">");

               sbXML.Append(text);

               sbXML.Append("</prosody>");
            }
            else
            {
               sbXML.Append(text);
            }

            sbXML.Append("</speak>");

            //Debug.Log(sbXML, this);

            return getValidXML(sbXML.ToString());
         }

         return wrapper.Text;
      }
//#endif

      #endregion


      #region Editor-only methods

#if UNITY_EDITOR
      public override void GenerateInEditor(Model.Wrapper wrapper)
      {
         Debug.LogError("'GenerateInEditor' is not supported for AWS Polly!", this);
      }

      public override void SpeakNativeInEditor(Model.Wrapper wrapper)
      {
         Debug.LogError("'SpeakNativeInEditor' is not supported for AWS Polly!", this);
      }
#endif

      #endregion
   }
}
#endif
// © 2018-2021 crosstales LLC (https://www.crosstales.com)