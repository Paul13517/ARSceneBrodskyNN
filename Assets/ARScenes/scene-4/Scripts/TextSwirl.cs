using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Immerse.Brodsky
{
    public class TextSwirl : MonoBehaviour
    {
        private List<Animator> _animators;


        private void Awake()
        {
            _animators = GetComponentsInChildren<Animator>().ToList();

            _animators.ForEach(x => x.enabled = false);
        }
        
        public IEnumerator Run()
        {
            int index = -1;
            float delay = 2f;
            
            while (index < _animators.Count - 2)
            {
                _animators[++index].enabled = true;
                _animators[++index].enabled = true;

                yield return new WaitForSeconds(delay);
            }
        }
    }
}