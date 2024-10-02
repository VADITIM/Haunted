using UnityEngine;
using System.Collections;

public class Debris : MonoBehaviour
{
    public GameObject debris;
    public GameObject debrisContent;
    public GameObject debrisItem; 
    public int numberOfItems = 5; // Ensure this is set to a value greater than 1
    public float shootForce = 20f; 
    private float spawnDelay = 0.05f; // Delay between item spawns

    private bool used = false;
    
    private bool isDestroyed = false;
    
    public void DestroyDebris()
    {
        if (!isDestroyed)
        {
            debris.SetActive(false);
            debrisContent.SetActive(true);
            isDestroyed = true;
        }
    }
    
    public void Loot()
    {
        if (used)
        {
            return;
        }
        Destroy(debrisContent);
        StartCoroutine(ShootItems());
    }
    
    private IEnumerator ShootItems()
    {
        used = true;
        for (int i = 0; i < numberOfItems; i++)
        {
            Debug.Log($"Spawning item {i + 1} of {numberOfItems}");
            GameObject item = Instantiate(debrisItem, debris.transform.position, Quaternion.identity);
            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 randomDirection = Random.insideUnitSphere;
                rb.AddForce(randomDirection * shootForce, ForceMode.Impulse);
            }
            yield return new WaitForSeconds(spawnDelay);
        }
        Destroy(debris); 
    }
}