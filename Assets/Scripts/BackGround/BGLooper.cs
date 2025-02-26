using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BGLopper : MonoBehaviour
{
    public int numBgCount = 5;

    public int obstacleCount = 0;
    public Vector3 obstacleLastPosition = Vector3.zero;

    private ObstacleManager obstacleManager;
    void Start()
    {
        obstacleManager = ObstacleManager.Instance;


        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        obstacleLastPosition = obstacles[0].transform.position;
        //obstacleCount = obstacles.Length;

        for (int i = 0; i < numBgCount; i++)
        {
            obstacleLastPosition = obstacleManager.SpawnRandomObstacle(obstacles[0], obstacleLastPosition);
        }

    }

    public void OnTriggerEnter2D(Collider2D _collision)
    {
        ///  backGround Loop
        if (_collision.CompareTag("BackGround"))
        {
            float widthOfBgObject = ((BoxCollider2D)_collision).size.x;
            Vector3 pos = _collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            Debug.Log("Background Width: " + widthOfBgObject);
            _collision.transform.position = pos;
            return;
        }



        Obstacle _obstacle = _collision.GetComponent<Obstacle>();
        if (_obstacle)
        {
            obstacleLastPosition = obstacleManager.SpawnRandomObstacle(_obstacle, obstacleLastPosition);
        }

        //Obstacle obstacle = collision.GetComponent<Obstacle>();
        //if (obstacle)
        //{
        //    obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        //}

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Obstacle"))
        {
            Destroy(collision.gameObject);
        }
    }

}
