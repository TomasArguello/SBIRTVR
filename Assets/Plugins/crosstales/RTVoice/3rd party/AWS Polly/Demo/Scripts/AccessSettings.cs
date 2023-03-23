using UnityEngine;
using UnityEngine.UI;

namespace Crosstales.RTVoice.AWSPolly
{
   /// <summary>Set the access settings for AWS Polly.</summary>
   [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_a_w_s_polly_1_1_access_settings.html")]
   public class AccessSettings : MonoBehaviour
   {
#if CT_RTV_AWS_20 || CT_RTV_AWS_45 || CT_DEVELOP

      #region Variables

      public VoiceProviderAWS Provider;

      public GameObject SettingsPanel;

      public InputField APIKey;

      public Dropdown EndpointDropdown;

      public Button OkButton;

      private string enteredKey = string.Empty;
      private Endpoint selectedEndpoint;

      private static string lastKey;
      private static Endpoint lastEndpoint;

      private Color okColor;

      private readonly System.Collections.Generic.List<Endpoint> endpoints = new System.Collections.Generic.List<Endpoint>();

      private static readonly System.Text.RegularExpressions.Regex apiRegex = new System.Text.RegularExpressions.Regex(@"[\w-]+:[0-9a-f-]{36}$");

      #endregion


      #region MonoBehaviour methods

      private void Start()
      {
         okColor = OkButton.image.color;

         if (!string.IsNullOrEmpty(lastKey))
            Provider.CognitoCredentials = lastKey;

         System.Collections.Generic.List<Dropdown.OptionData> options = new System.Collections.Generic.List<Dropdown.OptionData>();

         foreach (Endpoint ep in System.Enum.GetValues(typeof(Endpoint)))
         {
            options.Add(new Dropdown.OptionData(ep.ToString()));

            endpoints.Add(ep);
         }

         if (EndpointDropdown != null)
         {
            EndpointDropdown.ClearOptions();
            EndpointDropdown.AddOptions(options);
            EndpointDropdown.value = (int)Provider.Endpoint;
         }

         selectedEndpoint = lastEndpoint = Provider.Endpoint;

         if (string.IsNullOrEmpty(Provider.CognitoCredentials))
         {
            ShowSettings();
         }
         else
         {
            HideSettings();
            enteredKey = lastKey = APIKey.text = Provider.CognitoCredentials;
         }

         SetOkButton();
      }

      #endregion


      #region Public methods

      public void OnAPIKeyEntered(string key)
      {
         enteredKey = key ?? string.Empty;
         SetOkButton();
      }

      public void OnEndpointDropdownChanged(int index)
      {
         selectedEndpoint = endpoints[index];
      }

      public void HideSettings()
      {
         SettingsPanel.SetActive(false);

         if ((!string.IsNullOrEmpty(enteredKey) && !enteredKey.Equals(lastKey)) || (lastEndpoint != selectedEndpoint))
         {
            lastKey = Provider.CognitoCredentials = enteredKey;
            lastEndpoint = Provider.Endpoint = selectedEndpoint;
            Speaker.Instance.ReloadProvider();
         }
      }

      public void ShowSettings()
      {
         SettingsPanel.SetActive(Speaker.Instance.CustomProvider.isPlatformSupported);
      }

      public void SetOkButton()
      {
         if (apiRegex.IsMatch(enteredKey))
         {
            OkButton.interactable = true;
            OkButton.image.color = okColor;
         }
         else
         {
            OkButton.interactable = false;
            OkButton.image.color = Color.gray;
         }
      }

      #endregion

#endif
   }
}
// © 2020-2021 crosstales LLC (https://www.crosstales.com)