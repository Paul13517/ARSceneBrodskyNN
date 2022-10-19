using Immerse.Core.AR;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Immerse.Brodsky.AR
{
    /// <summary>
    /// Instantiates and destroys AR scene
    /// </summary>
    public class BrodskyARExperience : ImmerseARExperience
    {
        [SerializeField] private PlaneObjectPlacer _objectPlacer;
        [SerializeField] private ARPlaneManager _planeManager;
        [SerializeField] private ARAnchorManager _anchorManager;

        private GameObject _scenePrefab;
        

        private void Awake()
        {
            BrodskyEvents.ChapterChanged += OnChapterChanged;
            BrodskyEvents.SwitchedToAR += EnableAR;
        }

        private void OnChapterChanged(Chapter chapter)
        {
            if (chapter.HasAR)
            {
                _scenePrefab = chapter.ar;
            }
        }

        public override void EnableAR(bool isEnabled)
        {
            enabled = isEnabled;
            _objectPlacer.Enable(isEnabled);
            _planeManager.enabled = isEnabled;
            _anchorManager.enabled = isEnabled;
            
            if(isEnabled)
            {
                _objectPlacer.PlaceScene(_scenePrefab, instantiatedScene => 
                {
                    _instantiatedScene = instantiatedScene;
                });
            }
            else
            {
                DestroyScene();
            }
        }
    }
}