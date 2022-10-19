using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ProgrammManager : MonoBehaviour
{
private ARRaycastManager ARRaycastManagerScript;

    private Vector2 TouchPosition;

    public GameObject ObjectToSpawn;
    void Start()
    {
        ARRaycastManagerScript = FindObjectOfType<ARRaycastManager>();
        

    }

    // Update is called once per frame
    void Update()
    {
        ShowMarker();
    }

    void ShowMarker()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        ARRaycastManagerScript.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
         
            Instantiate(ObjectToSpawn, hits[0].pose.position, ObjectToSpawn.transform.rotation);
                     
        }
    }
/*
    private ARRaycastManager ARRaycastManagerScript;

    private Vector2 TouchPosition;

    public GameObject[] ObjectToSpawn;
    void Start()
    {
        ARRaycastManagerScript = FindObjectOfType<ARRaycastManager>();
        

    }

    // Update is called once per frame
    void Update()
    {
        ShowMarker();
    }

    void ShowMarker()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        ARRaycastManagerScript.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

       
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began){
          foreach(GameObject obj in ObjectToSpawn){
            Instantiate(obj, hits[0].pose.position, obj.transform.rotation);
            
            
            }
        }
    }*/
