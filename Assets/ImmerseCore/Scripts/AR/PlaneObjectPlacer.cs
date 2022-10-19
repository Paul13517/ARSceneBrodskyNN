using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Immerse.Core.AR
{
    /// <summary>
    /// Detects horizontal plane and places scene on user input
    /// </summary>
    public class PlaneObjectPlacer : MonoBehaviour
    {
        [SerializeField] private ARRaycastManager _raycastManager;
        [SerializeField] private ARAnchorManager _anchorManager;
        [SerializeField] private Camera _arCamera;
        [SerializeField] private GameObject _placementIndicator;
        
        private Pose _placementPose;
        private bool _placementPoseIsValid;
        
        private bool _assetSpawned;

        private GameObject _scenePrefab;
        private Action<GameObject> _onSceneSpawnedCallback;


        private void Update()
        {
            if (_assetSpawned || !_scenePrefab)
            {
                return;
            }

            UpdatePlacementPose();
            UpdatePlacementIndicator();

            if (_placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PlaceObject();
            }
        }
        
        public void Enable(bool isEnabled)
        {
            _placementIndicator.SetActive(isEnabled);
            _raycastManager.enabled = isEnabled;
            _anchorManager.enabled = isEnabled;
        }

        public void PlaceScene(GameObject scenePrefab, Action<GameObject> callback)
        {
            _scenePrefab = scenePrefab;
            _onSceneSpawnedCallback = callback;
            _assetSpawned = false;
        }

        private void UpdatePlacementPose()
        {
            var screenCenter = _arCamera.ViewportToScreenPoint(new Vector3(.5f, .5f));
            var hits = new List<ARRaycastHit>();

            _raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

            _placementPoseIsValid = hits.Count > 0;
            
            if (_placementPoseIsValid)
            {
                _placementPose = hits[0].pose;
                
                var cameraForward = _arCamera.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, -cameraForward.z).normalized;
                _placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            }
        }

        private void PlaceObject()
        {
            var targetRotation = Quaternion.Euler(0, 180 + _arCamera.transform.rotation.eulerAngles.y, 0);
            var spawnedScene = Instantiate(_scenePrefab, _placementPose.position, targetRotation);

            var anchor = _anchorManager.AddAnchor(_placementPose);
            spawnedScene.transform.parent = anchor.transform;
            
            _assetSpawned = true;
            _placementIndicator.SetActive(false);
            
            _onSceneSpawnedCallback?.Invoke(spawnedScene);
        }

        private void UpdatePlacementIndicator()
        {
            if (_placementPoseIsValid)
            {
                _placementIndicator.SetActive(true);
                _placementIndicator.transform.SetPositionAndRotation(_placementPose.position, _placementPose.rotation);
            }
            else
            {
                _placementIndicator.SetActive(false);
            }
        }
    }
}