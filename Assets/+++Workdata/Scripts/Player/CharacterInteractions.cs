using UnityEngine;

public class CharacterInteractions : MonoBehaviour
{
    //Checks if character has entered win zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            GameController.Instance.WinGame();
        }
    }
}
