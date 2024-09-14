using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{
    [SerializeField] Canvas impactCanva;
    [SerializeField] float impactTime = 0.3f;
    void Start()
    {        
        impactCanva.enabled = false;
    }
    public void ShowDamageImpact()
    {
        StartCoroutine(ShowSplatter());
    }
    IEnumerator ShowSplatter()
    {
        impactCanva.enabled = true;
        yield return new WaitForSeconds(impactTime);
        impactCanva.enabled = false;
    }
}
