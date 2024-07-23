using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public float spawnBuffer = 1.0f; // Distance outside the camera view to spawn enemies

    public GameObject enemyPrefab;
    public float spawnDistanceOutsideView = 1.0f; // Distance outside the camera view to spawn enemies

    private float spawntimer = 0;
    private float spawninterval = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        spawntimer += Time.deltaTime;

        if (spawntimer > spawninterval)
        {
            spawntimer = 0;
            SpawnEnemyOutsideCameraView();
        }
    }



    private void SpawnEnemyOutsideCameraView()
    {
        Vector2 spawnPosition = CalculateSpawnPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector2 CalculateSpawnPosition()
    {
        Camera mainCamera = Camera.main;
        float screenHeight = 2f * mainCamera.orthographicSize;
        float screenWidth = screenHeight * mainCamera.aspect;

        Vector2 cameraPosition = mainCamera.transform.position;

        // Determine the edges of the camera's view
        float leftBound = cameraPosition.x - screenWidth / 2f - spawnBuffer;
        float rightBound = cameraPosition.x + screenWidth / 2f + spawnBuffer;
        float upperBound = cameraPosition.y + screenHeight / 2f + spawnBuffer;
        float lowerBound = cameraPosition.y - screenHeight / 2f - spawnBuffer;

        // Randomly choose an edge to spawn the enemy
        Vector2 spawnPosition = Vector2.zero;
        switch (Random.Range(0, 4)) // Randomly pick one of four sides
        {
            case 0: // Top
                spawnPosition = new Vector2(Random.Range(leftBound + spawnBuffer, rightBound - spawnBuffer), upperBound);
                break;
            case 1: // Bottom
                spawnPosition = new Vector2(Random.Range(leftBound + spawnBuffer, rightBound - spawnBuffer), lowerBound);
                break;
            case 2: // Left
                spawnPosition = new Vector2(leftBound, Random.Range(lowerBound + spawnBuffer, upperBound - spawnBuffer));
                break;
            case 3: // Right
                spawnPosition = new Vector2(rightBound, Random.Range(lowerBound + spawnBuffer, upperBound - spawnBuffer));
                break;
        }

        return spawnPosition;
    }

}
