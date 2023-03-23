using UnityEngine;

namespace CrazyMinnow.SALSA.OneClicks
{
	public class OneClickFuse : OneClickBase
	{
		/// <summary>
		/// Setup and run OneClick operation on the supplied GameObject.
		/// </summary>
		/// <param name="gameObject">Root OneClick GameObject.</param>
		/// <param name="clip">AudioClip (can be null).</param>
		public static void Setup(GameObject gameObject, AudioClip clip)
		{
			////////////////////////////////////////////////////////////////////////////////////////////////////////////
			//	SETUP Requirements:
			//		use NewViseme("viseme name") to start a new viseme.
			//		use AddShapeComponent to add blendshape configurations, passing:
			//			- string array of shape names to look for.
			//			- optional string name prefix for the component.
			//			- optional amount (default = 1.0f).

			Init();

			#region SALSA-Configuration
			NewConfiguration(OneClickConfiguration.ConfigType.Salsa);
			{
				////////////////////////////////////////////////////////
				// SMR regex searches (enable/disable/add as required).
				AddSmrSearch("^Body$");
				AddSmrSearch("^Beards$");
				AddSmrSearch("^Moustaches$");


				////////////////////////////////////////////////////////
				// Adjust SALSA settings to taste...
				// - data analysis settings
				// - data analysis settings
				autoAdjustAnalysis = true;
				autoAdjustMicrophone = false;
				audioUpdateDelay = 0.08f;
				// - advanced dynamics settings
				loCutoff = 0.015f;
				hiCutoff = 0.75f;
				useAdvDyn = true;
				advDynPrimaryBias = 0.50f;
				useAdvDynJitter = true;
				advDynJitterAmount = 0.10f;
				advDynJitterProb = 0.20f;
				advDynSecondaryMix = 0.0f;
				emphasizerTrigger = 0.0f;


				////////////////////////////////////////////////////////
				// Viseme setup...


				NewExpression("w");
				AddShapeComponent(new[] {"Facial_Blends.MouthNarrow_Left", "MouthNarrow_Left"}, 0.10f, 0f, 0.07f, "MouthNarrowL", 1f);
				AddShapeComponent(new[] {"Facial_Blends.MouthNarrow_Right", "MouthNarrow_Right"}, 0.10f, 0f, 0.07f, "MouthNarrowR", 1f);
				AddShapeComponent(new[] {"Facial_Blends.UpperLipUp_Left", "UpperLipUp_Left"}, 0.10f, 0f, 0.07f, "UpperLipUpL", 0.25f);
				AddShapeComponent(new[] {"Facial_Blends.UpperLipUp_Right", "UpperLipUp_Right"}, 0.10f, 0f, 0.07f, "UpperLipUpR", 0.25f);
				AddShapeComponent(new[] {"Facial_Blends.UpperLipOut", "UpperLipOut"}, 0.10f, 0f, 0.07f, "UpperLipOut", 0.2f);
				AddShapeComponent(new[] {"Facial_Blends.LowerLipOut", "LowerLipOut"}, 0.10f, 0f, 0.07f, "LowerLipOut", 0.2f);


				NewExpression("t");
				AddShapeComponent(new[] {"Facial_Blends.Jaw_Down", "Jaw_Down"}, 0.10f, 0f, 0.07f, "JawDown", 0.25f);
				AddShapeComponent(new[] {"Facial_Blends.LowerLipDown_Left", "LowerLipDown_Left"}, 0.10f, 0f, 0.07f, "LowerLipDownL", 0.2f);
				AddShapeComponent(new[] {"Facial_Blends.LowerLipDown_Right", "LowerLipDown_Right"}, 0.10f, 0f, 0.07f, "LowerLipDownR", 0.2f);
				AddShapeComponent(new[] {"Facial_Blends.TongueUp", "TongueUp"}, 0.10f, 0f, 0.07f, "TongueUp", 0.23f);
				AddShapeComponent(new[] {"Facial_Blends.UpperLipUp_Left", "UpperLipUp_Left"}, 0.10f, 0f, 0.07f, "UpperLipUpL", 0.2f);
				AddShapeComponent(new[] {"Facial_Blends.UpperLipUp_Right", "UpperLipUp_Right"}, 0.10f, 0f, 0.07f, "UpperLipUpR", 0.2f);


				NewExpression("f");
				AddShapeComponent(new[] {"Facial_Blends.Jaw_Down", "Jaw_Down"}, 0.10f, 0f, 0.07f, "JawDown", 0.23f);
				AddShapeComponent(new[] {"Facial_Blends.LowerLipIn", "LowerLipIn"}, 0.10f, 0f, 0.07f, "LowerLipIn", 0.77f);
				AddShapeComponent(new[] {"Facial_Blends.UpperLipUp_Left", "UpperLipUp_Left"}, 0.10f, 0f, 0.07f, "UpperLipUpL", 0.2f);
				AddShapeComponent(new[] {"Facial_Blends.UpperLipUp_Right", "UpperLipUp_Right"}, 0.10f, 0f, 0.07f, "UpperLipUpR", 0.2f);
				AddShapeComponent(new[] {"Facial_Blends.Frown_Left", "Frown_Left"}, 0.10f, 0f, 0.07f, "FrownL", 0.15f);
				AddShapeComponent(new[] {"Facial_Blends.Frown_Right", "Frown_Right"}, 0.10f, 0f, 0.07f, "FrownR", 0.15f);


				NewExpression("th");
				AddShapeComponent(new[] {"Facial_Blends.Jaw_Down", "Jaw_Down"}, 0.10f, 0f, 0.07f, "JawDown", 0.3f);
				AddShapeComponent(new[] {"Facial_Blends.TongueUp", "TongueUp"}, 0.10f, 0f, 0.07f, "TongueUp", 0.22f);
				AddShapeComponent(new[] {"Facial_Blends.MouthNarrow_Left", "MouthNarrow_Left"}, 0.10f, 0f, 0.07f, "MouthNarrowL", 0.5f);
				AddShapeComponent(new[] {"Facial_Blends.MouthNarrow_Right", "MouthNarrow_Right"}, 0.10f, 0f, 0.07f, "MouthNarrowR", 0.5f);
				AddShapeComponent(new[] {"Facial_Blends.UpperLipOut", "UpperLipOut"}, 0.10f, 0f, 0.07f, "UpperLipOut", 0.53f);
				AddShapeComponent(new[] {"Facial_Blends.MouthUp", "MouthUp"}, 0.10f, 0f, 0.07f, "MouthUp", 0.08f);


				NewExpression("ow");
				AddShapeComponent(new[] {"Facial_Blends.MouthNarrow_Left", "MouthNarrow_Left"}, 0.10f, 0f, 0.07f, "MouthNarrowL", 0.6f);
				AddShapeComponent(new[] {"Facial_Blends.MouthNarrow_Right", "MouthNarrow_Right"}, 0.10f, 0f, 0.07f, "MouthNarrowR", 0.6f);
				AddShapeComponent(new[] {"Facial_Blends.UpperLipUp_Left", "UpperLipUp_Left"}, 0.10f, 0f, 0.07f, "UpperLipUpL", 0.2f);
				AddShapeComponent(new[] {"Facial_Blends.UpperLipUp_Right", "UpperLipUp_Right"}, 0.10f, 0f, 0.07f, "UpperLipUpR", 0.2f);
				AddShapeComponent(new[] {"Facial_Blends.UpperLipOut", "UpperLipOut"}, 0.10f, 0f, 0.07f, "UpperLipOut", 0.3f);
				AddShapeComponent(new[] {"Facial_Blends.Jaw_Down", "Jaw_Down"}, 0.10f, 0f, 0.07f, "JawDown", 0.4f);
				AddShapeComponent(new[] {"Facial_Blends.LowerLipDown_Left", "LowerLipDown_Left"}, 0.10f, 0f, 0.07f, "LowerLipDownL", 0.1f);
				AddShapeComponent(new[] {"Facial_Blends.LowerLipDown_Right", "LowerLipDown_Right"}, 0.10f, 0f, 0.07f, "LowerLipDownR", 0.1f);


				NewExpression("ee");
				AddShapeComponent(new[] {"Facial_Blends.Frown_Left", "Frown_Left"}, 0.10f, 0f, 0.07f, "FrownL", 0.25f);
				AddShapeComponent(new[] {"Facial_Blends.Frown_Right", "Frown_Right"}, 0.10f, 0f, 0.07f, "FrownR", 0.25f);
				AddShapeComponent(new[] {"Facial_Blends.Smile_Left", "Smile_Left"}, 0.10f, 0f, 0.07f, "SmileL", 0.4f);
				AddShapeComponent(new[] {"Facial_Blends.Smile_Right", "Smile_Right"}, 0.10f, 0f, 0.07f, "SmileR", 0.4f);
				AddShapeComponent(new[] {"Facial_Blends.MouthOpen", "MouthOpen"}, 0.10f, 0f, 0.07f, "MouthOpen", 0.1f);
				AddShapeComponent(new[] {"Facial_Blends.UpperLipUp_Left", "UpperLipUp_Left"}, 0.10f, 0f, 0.07f, "UpperLipUpL", 0.15f);
				AddShapeComponent(new[] {"Facial_Blends.UpperLipUp_Right", "UpperLipUp_Right"}, 0.10f, 0f, 0.07f, "UpperLipUpR", 0.15f);
				AddShapeComponent(new[] {"Facial_Blends.UpperLipIn", "UpperLipIn"}, 0.10f, 0f, 0.07f, "UpperLipIn", 0.6f);
				AddShapeComponent(new[] {"Facial_Blends.LowerLipDown_Left", "LowerLipDown_Left"}, 0.10f, 0f, 0.07f, "LowerLipDownL", 0.55f);
				AddShapeComponent(new[] {"Facial_Blends.LowerLipDown_Right", "LowerLipDown_Right"}, 0.10f, 0f, 0.07f, "LowerLipDownR", 0.55f);
				AddShapeComponent(new[] {"Facial_Blends.MouthUp", "MouthUp"}, 0.10f, 0f, 0.07f, "MouthUp", 0.2f);


				NewExpression("oo");
				AddShapeComponent(new[] {"Facial_Blends.MouthOpen", "MouthOpen"}, 0.10f, 0f, 0.07f, "MouthOpen", 0.35f);
				AddShapeComponent(new[] {"Facial_Blends.MouthNarrow_Left", "MouthNarrow_Left"}, 0.10f, 0f, 0.07f, "MouthNarrowL", 0.45f);
				AddShapeComponent(new[] {"Facial_Blends.MouthNarrow_Right", "MouthNarrow_Right"}, 0.10f, 0f, 0.07f, "MouthNarrowR", 0.45f);
				AddShapeComponent(new[] {"Facial_Blends.MouthDown", "MouthDown"}, 0.10f, 0f, 0.07f, "MouthDown", 0.07f);

			}
			#endregion // SALSA-configuration

			#region EmoteR-Configuration
			NewConfiguration(OneClickConfiguration.ConfigType.Emoter);
			{
				////////////////////////////////////////////////////////
				// SMR regex searches (enable/disable/add as required).
				AddSmrSearch("^Body$");
				AddSmrSearch("^Beards$");
				AddSmrSearch("^Moustaches$");
				AddSmrSearch("^Eyelashes$");

				useRandomEmotes = false;
				isChancePerEmote = true;
				numRandomEmotesPerCycle = 0;
				randomEmoteMinTimer = 1f;
				randomEmoteMaxTimer = 2f;
				randomChance = 0.5f;
				useRandomFrac = false;
				randomFracBias = 0.5f;
				useRandomHoldDuration = false;
				randomHoldDurationMin = 0.1f;
				randomHoldDurationMax = 0.5f;


				NewExpression("exasper");
				AddEmoteFlags(false, true, false, 0.55f);
				AddShapeComponent(new[] {"Facial_Blends.EyesWide_Left", "EyesWide_Left"}, 0.2f, 0.1f, 0.15f, "EyesWideL", 0.516f);
				AddShapeComponent(new[] {"Facial_Blends.EyesWide_Right", "EyesWide_Right"}, 0.2f, 0.1f, 0.15f, "EyesWideR", 0.628f);
				AddShapeComponent(new[] {"Facial_Blends.BrowsUp_Left", "BrowsUp_Left"}, 0.15f, 0.1f, 0.15f, "BrowsUpL", 0.2f);
				AddShapeComponent(new[] {"Facial_Blends.BrowsUp_Right", "BrowsUp_Right"}, 0.15f, 0.1f, 0.15f, "BrowsUpR", 0.347f);
				AddShapeComponent(new[] {"Facial_Blends.CheekPuff_Left", "CheekPuff_Left"}, 0.15f, 0.1f, 0.15f, "CheekPuffL", 0.4f);
				AddShapeComponent(new[] {"Facial_Blends.CheekPuff_Right", "CheekPuff_Right"}, 0.15f, 0.1f, 0.15f, "CheekPuffR", 0.4f);


				NewExpression("soften");
				AddEmoteFlags(false, true, false, 0.45f);
				AddShapeComponent(new[] {"Facial_Blends.Smile_Left", "Smile_Left"}, 0.2f, 0.1f, 0.15f, "Facial_Blends.SmileL", 1f);
				AddShapeComponent(new[] {"Facial_Blends.Smile_Right", "Smile_Right"}, 0.2f, 0.1f, 0.15f, "Facial_Blends.SmileR", 1f);
				AddShapeComponent(new[] {"Facial_Blends.BrowsOuterLower_Left", "BrowsOuterLower_Left"}, 0.2f, 0.1f, 0.15f, "BrowsOuterLowerL", 1f);
				AddShapeComponent(new[] {"Facial_Blends.BrowsOuterLower_Right", "BrowsOuterLower_Right"}, 0.2f, 0.1f, 0.15f, "BrowsOuterLowerR", 1f);


				NewExpression("browsUp");
				AddEmoteFlags(false, true, false, 0.55f, true);
				AddShapeComponent(new[] {"Facial_Blends.BrowsUp_Left", "BrowsUp_Left"}, 0.2f, 0.1f, 0.15f, "BrowsUpL", 0.596f);
				AddShapeComponent(new[] {"Facial_Blends.BrowsUp_Right", "BrowsUp_Right"}, 0.15f, 0.05f, 0.15f, "BrowsUpR", 0.821f);


				NewExpression("browUp");
				AddEmoteFlags(false, true, false, 0.7f);
				AddShapeComponent(new[] {"Facial_Blends.BrowsUp_Right", "BrowsUp_Right"}, 0.2f, 0.1f, 0.15f, "BrowsUpR", 1f);
				AddShapeComponent(new[] {"Facial_Blends.BrowsOuterLower_Left", "BrowsOuterLower_Left"}, 0.2f, 0.1f, 0.15f, "BrowsOuterLowerL", 1f);


				NewExpression("squint");
				AddEmoteFlags(false, true, false, 0.7f);
				AddShapeComponent(new[] {"Facial_Blends.BrowsIn_Left", "BrowsIn_Left"}, 0.2f, 0.05f, 0.15f, "BrowsInL", 1f);
				AddShapeComponent(new[] {"Facial_Blends.BrowsIn_Right", "BrowsIn_Right"}, 0.15f, 0.05f, 0.2f, "BrowsInR", 1f);
				AddShapeComponent(new[] {"Facial_Blends.Squint_Left", "Squint_Left"}, 0.2f, 0.05f, 0.2f, "SquintL", 0.75f);
				AddShapeComponent(new[] {"Facial_Blends.Squint_Right", "Squint_Right"}, 0.2f, 0.05f, 0.2f, "SquintR", 0.75f);


				NewExpression("focus");
				AddEmoteFlags(false, true, false, 1f);
				AddShapeComponent(new[] {"Facial_Blends.Squint_Left", "Squint_Left"}, 0.2f, 0.05f, 0.1f, "SquintL", 0.6f);
				AddShapeComponent(new[] {"Facial_Blends.Squint_Right", "Squint_Right"}, 0.2f, 0.05f, 0.15f, "SquintR", 0.6f);


				NewExpression("flare");
				AddEmoteFlags(false, true, false, 0.7f);
				AddShapeComponent(new[] {"Facial_Blends.NoseScrunch_Left", "NoseScrunch_Left"}, 0.2f, 0.05f, 0.15f, "NoseScrunchL", 0.867f);
				AddShapeComponent(new[] {"Facial_Blends.NoseScrunch_Right", "NoseScrunch_Right"}, 0.2f, 0.05f, 0.15f, "NoseScrunchR", 0.781f);


				NewExpression("scrunch");
				AddEmoteFlags(false, true, false, 0.7f);
				AddShapeComponent(new[] {"Facial_Blends.NoseScrunch_Left", "NoseScrunch_Left"}, 0.2f, 0.05f, 0.15f, "NoseScrunchL", 0.5f);
				AddShapeComponent(new[] {"Facial_Blends.NoseScrunch_Right", "NoseScrunch_Right"}, 0.2f, 0.05f, 0.15f, "NoseScrunchR", 0.5f);
				AddShapeComponent(new[] {"Facial_Blends.BrowsIn_Left", "BrowsIn_Left"}, 0.2f, 0.05f, 0.15f, "BrowsInL", 0.6f);
				AddShapeComponent(new[] {"Facial_Blends.BrowsIn_Right", "BrowsIn_Right"}, 0.2f, 0.05f, 0.15f, "BrowsInR", 0.6f);
				AddShapeComponent(new[] {"Facial_Blends.BrowsDown_Left", "BrowsDown_Left"}, 0.2f, 0.05f, 0.15f, "BrowsDownL", 0.55f);
				AddShapeComponent(new[] {"Facial_Blends.BrowsDown_Right", "BrowsDown_Right"}, 0.2f, 0.05f, 0.15f, "BrowsDownR", 0.55f);

			}
			#endregion // EmoteR-configuration

			DoOneClickiness(gameObject, clip);

			if (selectedObject.GetComponent<SalsaAdvancedDynamicsSilenceAnalyzer>() == null)
				selectedObject.AddComponent<SalsaAdvancedDynamicsSilenceAnalyzer>();
		}
	}
}