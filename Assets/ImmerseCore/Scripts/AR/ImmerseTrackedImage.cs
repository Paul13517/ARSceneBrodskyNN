using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Immerse.Core.AR
{
    [RequireComponent(typeof(ARTrackedImage))]
    public class ImmerseTrackedImage : MonoBehaviour
    {
        public static event Action<ImmerseTrackedImage> TrackedImageChanged;

        [SerializeField] private float _trackingConfirmedTime = 1f;
        
        private float _trackingTime;
        private bool _isActive;
        
        public ARTrackedImage TrackedImage { get; private set; }
        public string Id { get; private set; }


        private void Start()
        {
            TrackedImage = GetComponent<ARTrackedImage>();
            Id = TrackedImage.referenceImage.name;
            
            TrackedImageChanged += image =>
            {
                _isActive = image == this;
            };
        }

        private void Update()
        {
            if (_isActive)
            {
                return;
            }

            if (TrackedImage.trackingState == TrackingState.Tracking)
            {
                _trackingTime += Time.deltaTime;
            }
            else
            {
                _trackingTime = 0f;
            }

            if (_trackingTime >= _trackingConfirmedTime)
            {
                TrackedImageChanged?.Invoke(this);
            }
        }

        public void Deactivate()
        {
            _trackingTime = 0;
            _isActive = false;
        }
    }
}