
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CafeSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject _People;

    void Start()
    {
        StartCoroutine(showPeople());


    }

    IEnumerator showPeople()
    {
        yield return new WaitForSeconds(3);
        _People.SetActive(true);
    }

   


}