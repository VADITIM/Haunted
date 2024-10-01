using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] Material redMat;
    [SerializeField] Material lightMat;

    MeshRenderer mr;
    Rigidbody rb;

    private float flashDuration = 0.1f;
    string playerTag = "Player";



    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(playerTag)) return;
        StartCoroutine(Flashing());
    }


    // FLASHES COUNTER NOT CORRECT
    private IEnumerator Flashing()
    {
        int flashCounter = 20;
        int flashes = 0;
        while (flashes < flashCounter)
        {
            mr.material = redMat;
            yield return new WaitForSeconds(flashDuration);
            mr.material = lightMat;
            yield return new WaitForSeconds(flashDuration);
            flashes++;

            if (flashes == flashCounter)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
