using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PereezdManager : MonoBehaviour
{
    
    [SerializeField] private Animator _catAnimator,_catOutlineAnim;
    [SerializeField] private Animator _proectorAnimator, _proectorOutlineAnim;
    [SerializeField] private Animator _boxAnimator,_boxOutlineAnim;

    [SerializeField] private GameObject _videoPlane,_proectorCone;

    [Space]
    [SerializeField] private Material _catMaterial;
    [SerializeField] private Material _proectorMaterial;
    [SerializeField] private Material _boxMaterial;
    // Start is called before the first frame update

    void Start()
    {
        SceneStart();
    }
    
    void SceneStart()
    {
        StartCoroutine(StoryLineScene());
    }

    IEnumerator StoryLineScene()
    {
        yield return new WaitForSecondsRealtime(5);
        _catAnimator.SetBool("TurnOn",true);
        _catOutlineAnim.SetBool("TurnOn",true);
        yield return new WaitForSecondsRealtime(3);
        _proectorAnimator.enabled = true;
        _proectorOutlineAnim.enabled = true;
        _catAnimator.SetBool("TurnOn",false);
        _catOutlineAnim.SetBool("TurnOn",false);
        yield return new WaitForSecondsRealtime(5);
        //ShowProectorCone();
        yield return new WaitForSecondsRealtime(3);
        _boxAnimator.enabled = true;
        _boxOutlineAnim.enabled = true;
        yield return new WaitForSecondsRealtime(3);
        _videoPlane.SetActive(true);
        ShowProectorCone();

    }

    void ShowProectorCone()
    {
        _proectorCone.SetActive(true);
        _proectorCone.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0);
        StartCoroutine(UnFade());
    }

    IEnumerator UnFade()
    {
        var render = _proectorCone.GetComponent<Renderer>();
        float time = 0;
        Color unFadeColor = new Color(1, 1, 1, 0);
        while (time < 1)
        {
            yield return new WaitForSeconds(0.001f);
            time += 0.001f;
            unFadeColor.a = time;
            render.material.color = unFadeColor;
        }
    }
}
