using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Collider spawnArea;

    public GameObject[] fruitPrefabs;

    public GameObject bomb;

    //chance for bomb Spawn
    [Range(0f, 1f)]
    public float bombChance = 0.05f;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;


    public float minAngle = -15f;
    public float maxAngle = 15f;

    //Force to move up

    public float minForce = 18f;
    public float maxForce = 22f;


    public float maxLifetime = 5f;

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);

        while (enabled)
        {
            if (fruitPrefabs.Length > 0)
            {
                GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

                if(Random.value <bombChance)
                {
                    prefab = bomb;

                }   

                Vector3 position = new Vector3(
                    Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                    Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                    Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
                );

                //For rotating the Spawner to z axis to through the frute in different side

                Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

                GameObject fruit = Instantiate(prefab, position, rotation);
                Destroy(fruit, maxLifetime);

                //Add Force to the fruit 
                float force = Random.Range(minForce, maxForce);
                fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);
            }

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
