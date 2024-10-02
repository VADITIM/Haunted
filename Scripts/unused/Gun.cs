using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed = 10f;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var bulletObject = Instantiate(bullet,transform.position, transform.rotation);

            var rb = bulletObject.GetComponent<Rigidbody>();
            rb.velocity = bulletObject.transform.forward * bulletSpeed;
        }
    }
}
