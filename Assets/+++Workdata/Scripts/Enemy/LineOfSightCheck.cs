using System.Collections;
using UnityEngine;

public class LineOfSightCheck : MonoBehaviour
{
    private GameObject targetObject;
    private Coroutine detectPlayer;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            targetObject = col.gameObject;
            Debug.Log("Follow");
            detectPlayer = StartCoroutine(DetectPlayer());
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            targetObject = null;
            Debug.Log("Do not follow");
            StopCoroutine(detectPlayer);
        }
    }

    private IEnumerator DetectPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Debug.Log("coroutine running");
        }
    }
}
