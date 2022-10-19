using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class OcclusionSupportDetector : MonoBehaviour
{
    private IEnumerator Start()
    {
        var arOcclusionManager = GetComponent<AROcclusionManager>();
        while (arOcclusionManager.descriptor.environmentDepthImageSupported == Supported.Unknown)
        {
            yield return null;
        }

        if (arOcclusionManager.descriptor.environmentDepthImageSupported == Supported.Unsupported)
        {
            Debug.Log("This device does not support occlusion.");
            arOcclusionManager.enabled = false;
        }

        Debug.Log("This device supports occlusion.");
    }
}