using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Immerse.Core.AR
{
    /// <summary>
    /// Switches between AR and UI cameras
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        private static CameraController _instance;
        public static CameraController Instance => _instance ??= FindObjectOfType<CameraController>(true);

        [SerializeField] private GameObject _arSession;
        [SerializeField] private GameObject _uiSession;


        private void Awake()
        {
            InitializeAR();
        }

        private async Task InitializeAR()
        {
            EnableAR(true);
            await Task.Delay(TimeSpan.FromSeconds(1));
            EnableAR(false);
        }

        public void EnableAR(bool isEnabled)
        {
// #if UNITY_EDITOR
//             isEnabled = false;
// #endif
            _arSession.SetActive(isEnabled);
            _uiSession.SetActive(!isEnabled);
        }
    }
}