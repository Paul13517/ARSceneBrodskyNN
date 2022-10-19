using UnityEngine;

namespace Immerse.Core.AR
{
    public abstract class ImmerseARExperience : MonoBehaviour
    {
        protected GameObject _instantiatedScene;
        
        public abstract void EnableAR(bool isEnabled);
        
        protected void OnEnable()
        {
            EnableAR(true);
        }
        
        protected void OnDisable()
        {
            EnableAR(false);
        }

        public virtual void DestroyScene()
        {
            if (!_instantiatedScene)
            {
                return;
            }
            
            Destroy(_instantiatedScene);
            _instantiatedScene = null;
        }
    }
}