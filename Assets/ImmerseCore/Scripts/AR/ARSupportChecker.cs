using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Immerse.Core.AR
{
    public class ARSupportChecker : MonoBehaviour
    {
        public static Action<bool> ARSupported;
        

        public static IEnumerator CheckAR()
        {
#if UNITY_EDITOR
            ARSupported?.Invoke(true);
            yield break;
#endif
            
            if (ARSession.state == ARSessionState.None || ARSession.state == ARSessionState.CheckingAvailability)
            {
                yield return ARSession.CheckAvailability();
            }

            while (ARSession.state == ARSessionState.Installing)
            {
                yield return null;
            }

            if (ARSession.state == ARSessionState.NeedsInstall)
            {
                Debug.Log("<color=green>Installing AR provider...</color>");
                yield return ARSession.Install();
            }

            if (ARSession.state == ARSessionState.Unsupported)
            {
                Debug.LogError("<color=red>device is unsupported</color>");
                ARSupported?.Invoke(false);
            }
            else
            {
                Debug.Log("<color=green>device is supported</color>");
                ARSupported?.Invoke(true);
            }
        }
    }
}