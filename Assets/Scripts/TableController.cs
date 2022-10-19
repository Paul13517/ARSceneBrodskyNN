using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    [SerializeField]
    private GameObject _glasses;
    
    private void OnMouseDown()
    {
        _glasses.SetActive(true);
    }

    private IEnumerator EndTimer()
    {
        yield return new WaitForSeconds(75f);
        _glasses.SetActive(true);
    }
}
