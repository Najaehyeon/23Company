using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private static ObstacleManager obstacleManager;
    public static ObstacleManager Instance { get { return obstacleManager; } }

    [SerializeField] private List<GameObject> obstaclePrefabs;
    // Start is called before the first frame update

    private void Awake()
    {
        obstacleManager = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public Vector3 SpawnRandomObstacle(Obstacle _obstacle, Vector3 _obstacleLastPosition)
    {
        if (obstaclePrefabs.Count ==0)
        {
            Debug.Log("obstaclePrefabs �������� �ʾҽ��ϴ�");
        }
        //ObstacleManager > Prefabs list�� �ִ°� �Ѱ� �������� �����ͼ� 
        GameObject randomPrefab=obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];

        
        Vector3 obstacleLastPosition = _obstacle.SetRandomPlace(_obstacleLastPosition, randomPrefab);


        return obstacleLastPosition;

    }
}
