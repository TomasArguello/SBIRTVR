using UnityEngine;

namespace CrazyMinnow.SALSA.OneClicks
{
	public class OneClickFuseEyes : MonoBehaviour
	{
		public static void Setup(GameObject go)
		{
			string body = "body";
			string[] eyelash = new string[] {"eyelash", "default"};
			string head = "head";
			string eyeL = "lefteye";
			string eyeR = "righteye";
			string blinkL = "blink_left";
			string blinkR = "blink_right";

			if (go)
			{
				Eyes eyes = go.GetComponent<Eyes>();
				if (eyes == null)
				{
					eyes = go.AddComponent<Eyes>();
				}
				else
				{
					DestroyImmediate(eyes);
					eyes = go.AddComponent<Eyes>();
				}
				QueueProcessor qp = go.GetComponent<QueueProcessor>();
				if (qp == null) qp = go.AddComponent<QueueProcessor>();

				// System Properties
                eyes.characterRoot = go.transform;
                eyes.queueProcessor = qp;

                // Heads - Bone_Rotation
                eyes.BuildHeadTemplate(Eyes.HeadTemplates.Bone_Rotation_XY);
                eyes.heads[0].expData.controllerVars[0].bone = Eyes.FindTransform(eyes.characterRoot, head);
                eyes.heads[0].expData.name = "head";
                eyes.heads[0].expData.components[0].name = "head";
                eyes.headTargetOffset.y = 0.052f;
                eyes.CaptureMin(ref eyes.heads);
                eyes.CaptureMax(ref eyes.heads);

                // Eyes - Bone_Rotation
                eyes.BuildEyeTemplate(Eyes.EyeTemplates.Bone_Rotation);
                eyes.eyes[0].expData.controllerVars[0].bone = Eyes.FindTransform(eyes.characterRoot, eyeL);
                eyes.eyes[0].expData.name = "eyeL";
                eyes.eyes[0].expData.components[0].name = "eyeL";
                eyes.eyes[1].expData.controllerVars[0].bone = Eyes.FindTransform(eyes.characterRoot, eyeR);
                eyes.eyes[1].expData.name = "eyeR";
                eyes.eyes[1].expData.components[0].name = "eyeR";
                eyes.CaptureMin(ref eyes.eyes);
                eyes.CaptureMax(ref eyes.eyes);

                // Blinklids - Bone_Rotation
                eyes.BuildEyelidTemplate(Eyes.EyelidTemplates.BlendShapes, Eyes.EyelidSelection.Upper); // includes left/right eyelid
                eyes.AddEyelidShapeExpression(ref eyes.blinklids); // add eyelash left
                eyes.AddEyelidShapeExpression(ref eyes.blinklids); // add eyelash right
                float blinkMax = 0.75f;
                // Left eyelid
                eyes.blinklids[0].expData.controllerVars[0].smr = Eyes.FindTransform(eyes.characterRoot, body).GetComponent<SkinnedMeshRenderer>();
				eyes.blinklids[0].expData.controllerVars[0].blendIndex = Eyes.FindBlendIndex(eyes.blinklids[0].expData.controllerVars[0].smr, blinkL);
                eyes.blinklids[0].expData.controllerVars[0].maxShape = blinkMax;
                eyes.blinklids[0].expData.name = "eyelidL";
                // Right eyelid
                eyes.blinklids[1].expData.controllerVars[0].smr = eyes.blinklids[0].expData.controllerVars[0].smr;
                eyes.blinklids[1].expData.controllerVars[0].blendIndex = Eyes.FindBlendIndex(eyes.blinklids[1].expData.controllerVars[0].smr, blinkR);
                eyes.blinklids[1].expData.controllerVars[0].maxShape = blinkMax;
                eyes.blinklids[1].expData.name = "eyelidR";
                // Left eyelash
                eyes.blinklids[2].expData.controllerVars[0].smr = Eyes.FindTransform(eyes.characterRoot, eyelash).GetComponent<SkinnedMeshRenderer>();
                eyes.blinklids[2].expData.controllerVars[0].blendIndex = Eyes.FindBlendIndex(eyes.blinklids[2].expData.controllerVars[0].smr, blinkL);
                eyes.blinklids[2].expData.controllerVars[0].maxShape = blinkMax;
                eyes.blinklids[2].expData.name = "eyelashL";
                // Right eyelash
                eyes.blinklids[3].expData.controllerVars[0].smr = eyes.blinklids[2].expData.controllerVars[0].smr;
                eyes.blinklids[3].expData.controllerVars[0].blendIndex = Eyes.FindBlendIndex(eyes.blinklids[3].expData.controllerVars[0].smr, blinkR);
                eyes.blinklids[3].expData.controllerVars[0].maxShape = blinkMax;
                eyes.blinklids[3].expData.name = "eyelashR";
                
                // Tracklids
                eyes.CopyBlinkToTrack();
                // Set track eye index
                eyes.tracklids[0].referenceIdx = 0; // left
                eyes.tracklids[1].referenceIdx = 1; // right
                eyes.tracklids[2].referenceIdx = 0; // left
                eyes.tracklids[3].referenceIdx = 1; // right

                // Initialize the Eyes module
                eyes.Initialize();
			}
		}
	}
}