using Immerse.Brodsky;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARPerformanceConnector : MonoBehaviour
{
    private static ARPerformanceConnector _instance;
    public static ARPerformanceConnector Instance => _instance ??= FindObjectOfType<ARPerformanceConnector>(true);
    
    
    [SerializeField] private ARTrackedImageManager _trackedImageManager;
    

    private void Awake()
    {
        _trackedImageManager.trackedImagesChanged += OnTrackedImagedChanged;
    }

    private void OnTrackedImagedChanged(ARTrackedImagesChangedEventArgs obj)
    {
        foreach (ARTrackedImage arTrackedImage in obj.added)
        {
            string imageName = arTrackedImage.referenceImage.name;
            string[] connectionProperties = imageName.Split('-');

            BrodskySettings.RoomName = connectionProperties[0];
            BrodskySettings.IsMaster = connectionProperties[1] == "master";
            
            BrodskyEvents.OnMultiplayerSettingsSetup();
            SetEnabled(false);
        }
    }

    public void SetEnabled(bool isEnabled)
    {
        enabled = isEnabled;
        _trackedImageManager.enabled = isEnabled;
    }
}
