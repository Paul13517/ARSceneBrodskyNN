using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class SceneManager : MonoBehaviour
{
   private Vector2 TouchPosition;
   private ARRaycastManager ARRaycastManagerScript;

   [SerializeField] private GameObject scenePrefab;
   [SerializeField] private GameObject planePrefab;
   [SerializeField] private GameObject roomPrefab;
   List<ARRaycastHit> hits = new List<ARRaycastHit>();
    
 
   //public GameObject scenePrefab;
   void Start()
    {
        ARRaycastManagerScript = FindObjectOfType<ARRaycastManager>();

    }
  
    void Update()
    {
        ShowMarker();
    }




void ShowMarker()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        ARRaycastManagerScript.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            
            Instantiate(scenePrefab, hits[0].pose.position, scenePrefab.transform.rotation);
            StartCoroutine(TimeAwait());
                                  
        } 
        IEnumerator TimeAwait(){
        yield return new WaitForSeconds(16);
        Instantiate(planePrefab, hits[0].pose.position , planePrefab.transform.rotation);
        yield return new WaitForSeconds(8);
        Instantiate(roomPrefab, hits[0].pose.position + new Vector3 (-0.42f,0,0.65f), roomPrefab.transform.rotation);                 
    }    
  }
}
