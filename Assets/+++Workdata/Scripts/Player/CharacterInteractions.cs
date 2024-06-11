using UnityEngine;

public class CharacterInteractions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            GameController.Instance.WinGame();
        }
    }
}
