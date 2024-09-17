using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    [SerializeField] StackPartController[] stackPartControllers = null;
    // Start is called before the first frame update
    public void ShatterAllPart()
    {
        if(transform.parent != null)
        {
            transform.parent = null;
            FindObjectOfType<Ball>().IncreaseBronkenStacks();
        }

        foreach(StackPartController item in stackPartControllers)
        {
            item.Shatter();
        }
        StartCoroutine(RemoveParts());
    }
    IEnumerator RemoveParts()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
