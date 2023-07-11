using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;
    public Sprite slicedFruitSprite; // Reference to the sliced fruit image

    private Rigidbody fruitRb;
    private Collider fruitCollider;

    private ParticleSystem fruitParticleSystem;

    public int point = 1;

    private void Awake()
    {
        fruitRb = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        fruitParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    public void Slice(Vector3 direction, Vector3 position, float force)
    {
        FindAnyObjectByType<GameManager>().IncreseScore(point);

        whole.SetActive(false);
        sliced.SetActive(true);

        fruitCollider.enabled = false;
        fruitParticleSystem.Play();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody slice in slices)
        {
            slice.velocity = fruitRb.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }

        // Show the sliced fruit image
        ShowSlicedFruitImage();
    }

    private void ShowSlicedFruitImage()
    {
        GameObject slicedFruitImage = new GameObject("SlicedFruitImage");
        SpriteRenderer spriteRenderer = slicedFruitImage.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = slicedFruitSprite;
        slicedFruitImage.transform.position = transform.position;

        // Adjust the position and scale of the sliced fruit image as needed
        // slicedFruitImage.transform.position = ...
        // slicedFruitImage.transform.localScale = ...

        StartCoroutine(DestroySlicedFruitImage(slicedFruitImage));
    }

    private IEnumerator DestroySlicedFruitImage(GameObject slicedFruitImage)
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        Destroy(slicedFruitImage); // Destroy the sliced fruit image
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            if (blade != null)
            {
                Slice(blade.direction, blade.transform.position, blade.SliceForce);
                fruitCollider.enabled = false; // Disable the fruit collider after slicing
                StartCoroutine(DestroyFruit()); // Start coroutine to destroy the fruit
            }
        }
    }

    private IEnumerator DestroyFruit()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        Destroy(gameObject); // Destroy the fruit GameObject
    }
}