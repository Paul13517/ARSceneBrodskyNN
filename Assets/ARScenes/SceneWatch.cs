using System;
using System.Collections;
using Immerse.Core;
using TMPro;
using UnityEngine;

namespace ARScenes
{
    public class SceneWatch : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        private double time;


        private void Awake()
        {
            _text.text = "00:00";
        }

        private void Start()
        {
            StartCoroutine(Run());
        }

        private IEnumerator Run()
        {
            var delay = new WaitForSeconds(1);
            while (true)
            {
                _text.text = time.ToHumanTime();
                yield return delay;
                time++;
            }
        }
    }
}