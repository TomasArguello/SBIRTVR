using UnityEngine;

namespace Crosstales.RTVoice.AWSPolly
{
   /// <summary>Kills AWS Polly at the end of the scene.</summary>
   [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_a_w_s_polly_1_1_killer.html")]
   public class Killer : MonoBehaviour
   {
      public GameObject AWSPolly;

#if CT_DEVELOP
      private void OnDestroy()
      {
         Destroy(AWSPolly);
      }

#endif
   }
}
// © 2020-2021 crosstales LLC (https://www.crosstales.com)