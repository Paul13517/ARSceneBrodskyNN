using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesController : MonoBehaviour
{
    private const float FlyingSpeed = 5f;

    [SerializeField]
    private GameObject[] _toggleObjects;
    [SerializeField]
    private Transform _cameraTransform;
    [SerializeField]
    private Transform _tableTransform;
    [SerializeField]
    private GameObject _eyeAnimationObject;

    private bool _startMove;
    private bool _endMove;


    private void Update()
    {
        if (_startMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, _cameraTransform.position, FlyingSpeed * Time.deltaTime);

            if (transform.position == _cameraTransform.position)
            {
                StartCoroutine(ApplyGlasses(true));
                _startMove = false;
            }
            
            Rotate(_cameraTransform.position);
        }

        if (_endMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, _tableTransform.position, FlyingSpeed * Time.deltaTime);

            if (transform.position == _tableTransform.position)
            {
                _endMove = false;
            }
            
            Rotate(_tableTransform.position);
        }
    }

    public void MoveToCamera()
    {
        _startMove = true;
    }

    public void MoveToTable()
    {
        StartCoroutine(ApplyGlasses(false));
    }

    private IEnumerator ApplyGlasses(bool state)
    {
        _eyeAnimationObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        
        if (!state)
        {
            //gameObject.SetActive(true);
            _endMove = true;
        }
        
        foreach (var o in _toggleObjects)
        {
            o.SetActive(state);
        }
        
        yield return new WaitForSeconds(0.7f);

        _eyeAnimationObject.SetActive(false);
        
        // if (state)
        // {
        //     gameObject.SetActive(false);
        // }
        
    }
    
    private void Rotate(Vector3 to)
    {
        Vector3 dir = (to - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.back);
        rotation.z = 0f;
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.1f);
    }
}
