using System.Collections;
using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    public GameObject catPrefab;

    public float minSpawnPause = 5f;
    public float maxSpawnPause = 10f;

    public float minAttackDelay = 0.5f;
    public float maxAttackDelay = 2.0f;
    public float moveSpeed = 10f;

    public float widthOffset = 20f;
    public float heightOffset = 5f;

    private float screenWidth;
    private float screenHeight;

    private Vector2 spawnPosition = new Vector2(0, 0);
    private bool canSpawn = true;

    public Transform player;

    void Start()
    {
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        screenHeight = Camera.main.orthographicSize;

        StartCoroutine(SpawnCatRoutine());
    }

    IEnumerator SpawnCatRoutine()
    {
        while (true)
        {
            if (canSpawn)
            {
                SpawnCat();
                yield return new WaitForSeconds(Random.Range(minSpawnPause, maxSpawnPause));
            }
            yield return null;
        }
    }

    void SpawnCat()
    {
        canSpawn = false;
        bool spawnOnLeft = Random.Range(0f, 2f) >= 1f;
        spawnPosition.y = Random.Range(-(screenHeight - heightOffset), screenHeight - heightOffset);
        spawnPosition.x = spawnOnLeft ? -(screenWidth + widthOffset) : screenWidth + widthOffset;
        GameObject newCat = Instantiate(catPrefab, spawnPosition + new Vector2(player.position.x, player.position.y), Quaternion.identity);
        newCat.transform.rotation = Quaternion.Euler(0, 0, spawnOnLeft ? 90 : -90);
        StartCoroutine(MoveCat(newCat));
    }

    IEnumerator MoveCat(GameObject cat)
    {
        yield return new WaitForSeconds(Random.Range(minAttackDelay, maxAttackDelay));
        // TODO: Shadow 
    
        while (cat != null)
        {
            cat.transform.Translate(Vector2.down * (moveSpeed * Time.deltaTime));
    
            if (IsOutOfBounds(cat.transform.position))
            {
                Destroy(cat);
            }
    
            yield return new WaitForEndOfFrame();
        }
        
        canSpawn = true;  
    }
    
    bool IsOutOfBounds(Vector2 position)
    {
        if (position.x > screenWidth + widthOffset || position.x < -screenWidth - widthOffset)
        {
            return true;
        }
    
        return false;
    }
}