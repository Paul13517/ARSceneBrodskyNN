using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Immerse.Brodsky
{
    public class TextCorridor : MonoBehaviour
    {
        private List<Animator> _animators;


        private void Awake()
        {
            _animators = GetComponentsInChildren<Animator>().ToList();

            _animators.ForEach(x => x.enabled = false);
            _animators.ForEach(x => x.speed = .15f);
        }

        public IEnumerator Run()
        {
            int index = -1;
            float delay = 24f;
            float speed = .15f;
            float speedStep = .15f;
            
            while (index < _animators.Count - 2)
            {
                _animators[++index].enabled = true;
                _animators[++index].enabled = true;

                yield return new WaitForSeconds(delay);

                speed = Mathf.Clamp01(speed + speedStep);
                _animators.ForEach(x => x.speed = speed);

                delay = speed >= 1 ? delay / 2 : delay * (speed - speedStep) / speed;
            }
        }
        
        public IEnumerator Quake()
        {
            var toRotation = transform.localRotation.eulerAngles + new Vector3(90, 0, 0);
            LeanTween.rotateLocal(gameObject, toRotation, 15);

            var position = transform.position;
            int quakeDuration = 10;
            
            while (quakeDuration > 0)
            {
                float timer = 1f;
                while (timer > 0)
                {
                    transform.localPosition += new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f));
            
                    timer -= Time.deltaTime;
                    yield return null;
                }

                transform.localPosition = position;
                quakeDuration--;
            }

            var toPosition = new Vector3(-0.4f, 0, -2);
            LeanTween.moveLocal(gameObject, toPosition, 5);
        }
    }
}