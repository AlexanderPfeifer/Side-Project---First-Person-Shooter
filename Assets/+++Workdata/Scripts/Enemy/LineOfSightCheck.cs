using System.Collections;
using UnityEngine;

public class LineOfSightCheck : MonoBehaviour
{
    private GameObject targetObject;
    private Coroutine detectPlayer;
    private EnemyBehaviour enemyBehaviour;
    [SerializeField] private LayerMask coverLayer;

    private void Start() => enemyBehaviour = GetComponentInParent<EnemyBehaviour>();
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            targetObject = col.gameObject;
            detectPlayer = StartCoroutine(DetectPlayer());
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            targetObject = null;
            StopCoroutine(detectPlayer);
        }
    }

    private IEnumerator DetectPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            var direction = targetObject.transform.position - transform.position;
            var distance = Vector3.Distance(transform.position, targetObject.transform.position);
            var targetAngle = Vector3.Angle(transform.forward, direction);
            
            if (targetAngle < 60 && !IsCharacterCovered(direction, distance))
            {
                enemyBehaviour.hasTarget = true;
            }
            else
            {
                enemyBehaviour.hasTarget = false;
            }
        }
    }

    bool IsCharacterCovered(Vector3 targetDirection, float distanceToTarget)
    {
        RaycastHit[] hits = new RaycastHit[2];
        var ray = new Ray(transform.position, targetDirection);
        var amountOfHits = Physics.RaycastNonAlloc(ray, hits, distanceToTarget, coverLayer);
        return amountOfHits > 0;
    }
}
