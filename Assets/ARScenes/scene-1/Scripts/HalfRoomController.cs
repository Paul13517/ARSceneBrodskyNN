using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfRoomController : MonoBehaviour
{
    [SerializeField] private GameObject _room;
    [SerializeField] private GameObject _plan;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForShow());
    }

    IEnumerator WaitForShow()
    {
        yield return new WaitForSeconds(16);
        _plan.SetActive(true);
        yield return new WaitForSeconds(8);
        _room.SetActive(true);

    }

   
}
