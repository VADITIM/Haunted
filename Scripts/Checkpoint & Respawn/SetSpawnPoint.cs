// using UnityEngine;

// public class SetSpawnPoint : MonoBehaviour
// {
//     public GameObject newSpawnPoint; 

//     void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             PlayerSpawn playerSpawn = other.GetComponent<PlayerSpawn>();
//             if (playerSpawn != null)
//             {
//                 playerSpawn.SetSpawnPoint(newSpawnPoint.transform.position);
//             }
//         }
//     }
// }