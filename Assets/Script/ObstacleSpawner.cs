using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Obstacle obstaclePrefab;
    public float speed = 0.05f;
    public List<Obstacle> obstacles = new List<Obstacle>();
    public Transform parent;

    public float laneOffset = 2f;
    public float yLevel = -2f;
    public float spawnZ = 10f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 1f, 0.6f);
    }

    private void SpawnObstacle()
    {
        var o = Instantiate(obstaclePrefab, parent);
        int lane = Random.Range(0, 3);
        float xPos = (lane - 1) * laneOffset;

        o.itemPosition = new Vector3(xPos, yLevel, spawnZ);
        obstacles.Add(o);
    }

    private void Update()
    {
        foreach (var o in obstacles.ToArray())
        {
            o.itemPosition.z -= speed;

            if (o.itemPosition.z < -1f)
            {
                obstacles.Remove(o);
                Destroy(o.gameObject);
            }
        }
        
        var ordered = parent.Cast<Transform>().OrderBy(t => t.position.z).ToList();
        for (int i = 0; i < ordered.Count; i++)
            ordered[i].SetSiblingIndex(i);
    }
}
