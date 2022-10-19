using System.Collections;
using UnityEngine;


namespace Immerse.Core
{
    public class GyroCameraController : MonoBehaviour
    {
        [SerializeField] private float _smoothing = 0.1f;
        
        private float _appliedGyroYAngle;
        private float _calibrationYAngle;
        private Transform _rawGyroRotation;
        private float _tempSmoothing;

        private void Start()
        {
            Input.gyro.enabled = true;
            Application.targetFrameRate = 60;
        
            _rawGyroRotation = new GameObject("GyroRaw").transform;
            _rawGyroRotation.rotation = transform.rotation;
        }

        private void Update()
        {
            ApplyGyroRotation();
            ApplyCalibration();
        
            transform.rotation = Quaternion.Slerp(transform.rotation, _rawGyroRotation.rotation, _smoothing);
        }

        public void Calibrate()
        {
            if (!_rawGyroRotation)
            {
                return;
            }
            
            StartCoroutine(CalibrateYAngle());
        }

        private IEnumerator CalibrateYAngle()
        {
            _rawGyroRotation.rotation = transform.rotation;
            _tempSmoothing = _smoothing;
            _smoothing = 1;
            _calibrationYAngle = _appliedGyroYAngle; // Offsets the y angle in case it wasn't 0 at edit time.
            yield return null;
            _smoothing = _tempSmoothing;
            
            _calibrationYAngle += 360 - _rawGyroRotation.eulerAngles.z;
        }

        private void ApplyGyroRotation()
        {
            _rawGyroRotation.rotation = Input.gyro.attitude;
            _rawGyroRotation.Rotate(0f, 0f, 180f, Space.Self); // Swap "handedness" of quaternion from gyro.
            _rawGyroRotation.Rotate(90f, 180f, 0f, Space.World); // Rotate to make sense as a camera pointing out the back of your device.
            _appliedGyroYAngle = _rawGyroRotation.eulerAngles.y; // Save the angle around y axis for use in calibration.
        }
        
        private void ApplyCalibration()
        {
            _rawGyroRotation.Rotate(0f, -_calibrationYAngle, 0,  Space.World); // Rotates y angle back however much it deviated when calibrationYAngle was saved.
        }
    }
}