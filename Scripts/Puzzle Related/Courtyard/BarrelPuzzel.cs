// Assets/Scripts/KeyScript.cs
using UnityEngine;

public class BarrelPuzzle : MonoBehaviour
{
    [SerializeField]
    private Vector3 StautePosition; // The position to move the door to when opened
    [SerializeField]
    private GameObject WhiteLadyStatue;

    private bool hasMoved = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasMoved && collision.gameObject.CompareTag("barrelPuzzle"))
        {
            WhiteLadyStatue.transform.position += StautePosition;
            hasMoved = true;
        }
    }
}