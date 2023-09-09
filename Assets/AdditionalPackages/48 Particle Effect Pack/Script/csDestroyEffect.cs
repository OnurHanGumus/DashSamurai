using UnityEngine;
using System.Collections;

public class csDestroyEffect : MonoBehaviour 
{
    private void OnEnable()
    {
        StartCoroutine(Deactivate());
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
