using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Immerse.Brodsky
{
    public class Scene4Controller : MonoBehaviour
    {
        [SerializeField] private GameObject _typewriters;
        [SerializeField] private TextCorridor _textCorridor;
        [SerializeField] private TextSwirl _textSwirl;
        [SerializeField] private GameObject _explosion;

        private List<Animator> _typewritesAnimators;
        
        
        private void Awake()
        {
            _typewritesAnimators = _typewriters.GetComponentsInChildren<Animator>().ToList();

            _typewritesAnimators.ForEach(x => x.enabled = false);
            _explosion.SetActive(false);
        }
        
        private void Start()
        {
            StartCoroutine(Run());
        }

        private IEnumerator Run()
        {
            StartCoroutine(_textCorridor.Run());
            
            yield return new WaitForSeconds(10);
            
            _typewritesAnimators.ForEach(x => x.enabled = true);
            
            
            yield return new WaitForSeconds(45);
            
            StartCoroutine(_textCorridor.Quake());
            
            yield return new WaitForSeconds(10);

            StartCoroutine(_textSwirl.Run());
            
            yield return new WaitForSeconds(20);
            
            _explosion.SetActive(true);
            
            yield return new WaitForSeconds(1);
            
            Destroy(_textCorridor.gameObject);
            Destroy(_textSwirl.gameObject);
        }
    }
}
