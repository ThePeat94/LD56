using System.Collections;
using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject catPrefab;

    public float minSpawnPause = 10f;
    public float maxSpawnPause = 20f;
    public float timeFactor = 0.00001f;
    public float timeFactorSpeed = 0.00005f;
    private float currentTimefactor = 1; 

    public float minAttackDelay = 0.5f;
    public float maxAttackDelay = 2.0f;
    public float moveSpeed = 10f;

    public float widthOffset = 20f;
    public float heightOffset = 5f;

    private float screenWidth;
    private float screenHeight;

    private Vector2 spawnPosition = new Vector2(0, 0);
    public bool canSpawn = true;

    public Transform player;

    public GameObject catShadow;
    
    private AudioSource audioSource;
    public AudioClip attackClip;
    public AudioClip shadowClip;

    void Start()
    {
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        screenHeight = Camera.main.orthographicSize;
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(SpawnCatRoutine());
    }

    IEnumerator SpawnCatRoutine()
    {
        while (true)
        {
            currentTimefactor -= timeFactor; 
            Debug.Log(currentTimefactor);
            if (canSpawn)
            {
                var spawnPause = Random.Range(minSpawnPause, maxSpawnPause) * currentTimefactor;
                yield return new WaitForSeconds(spawnPause);
                SpawnCat();
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
        GameObject newCat = Instantiate(catPrefab, spawnPosition + new Vector2(player.position.x, player.position.y),
            Quaternion.identity);
        newCat.transform.rotation = Quaternion.Euler(0, 0, spawnOnLeft ? 90 : -90);
        StartCoroutine(MoveCat(newCat));
    }

    IEnumerator MoveCat(GameObject cat)
    {
        audioSource.clip = shadowClip;
        audioSource.Play();
        var delay = Random.Range(minAttackDelay, maxAttackDelay);
        GameObject newCatShadow = Instantiate(catShadow,
            cat.transform.position, Quaternion.identity);
        newCatShadow.transform.rotation = cat.transform.rotation;

        Vector2 screenMiddle = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));

        float distanceToMiddle = Mathf.Abs(newCatShadow.transform.position.x - screenMiddle.x);
        float shadowSpeed = distanceToMiddle / delay;
        float elapsedTime = 0f;
        while (elapsedTime < delay)
        {
            float step = shadowSpeed * Time.deltaTime;
            newCatShadow.transform.Translate(Vector2.down * step);

            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / delay);
            var spriteRenderer = newCatShadow.GetComponentInChildren<SpriteRenderer>();
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Destroy(newCatShadow);
        audioSource.clip = attackClip;
        audioSource.Play();
        moveSpeed += timeFactorSpeed;
        while (cat != null)
        {
            cat.transform.Translate(Vector2.down * (moveSpeed * Time.deltaTime));

            if (IsOutOfBounds(cat.transform.position))
            {
                Destroy(cat);
                Destroy(newCatShadow);
            }

            yield return new WaitForEndOfFrame();
        }

        canSpawn = true;
    }

    bool IsOutOfBounds(Vector2 position)
    {
        if (position.x > 150 || position.x < -150)
        {
            return true;
        }

        return false;
    }
}