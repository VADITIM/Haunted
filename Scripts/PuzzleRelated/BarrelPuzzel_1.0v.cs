using UnityEngine;

public class BarrelPuzzle : MonoBehaviour
{
    [SerializeField]
    private Vector3 StautePosition; 
    [SerializeField]
    private GameObject WhiteLadyStatue;
    [SerializeField]
    private AudioSource audioSource; 
    [SerializeField]
    private AudioClip soundClip; 

    private bool hasMoved = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasMoved && collision.gameObject.CompareTag("barrelPuzzle"))
        {
            WhiteLadyStatue.transform.position += StautePosition;
            hasMoved = true;

            if (audioSource != null && soundClip != null)
            {
                audioSource.PlayOneShot(soundClip);
            }
        }
    }
}