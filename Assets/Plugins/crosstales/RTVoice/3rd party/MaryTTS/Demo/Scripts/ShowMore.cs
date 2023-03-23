using UnityEngine;

namespace Crosstales.RTVoice.MaryTTS
{
   /// <summary>Shows the details for MaryTTS.</summary>
   [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_mary_t_t_s_1_1_show_more.html")]
   public class ShowMore : MonoBehaviour
   {
      #region Public methods

      public void Show()
      {
         Util.Helper.OpenURL("http://mary.dfki.de/");
      }

      #endregion
   }
}
// © 2020-2021 crosstales LLC (https://www.crosstales.com)