using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    //아래장애물 1
    //아래장애물 2
    //아래장애물 3

    //아래장애물 1 +아이템
    //아래장애물 2 +아이템
    //아래장애물 3 +아이템

    //윗장애물 1
    //윗장애물 2
    //윗장애물 3

    //윗장애물 1 +아이템
    //윗장애물 2 +아이템
    //윗장애물 3 +아이템

    //스라이드장애물 1
    //스라이드장애물 2
    //스라이드장애물 3

    //스라이드장애물 1 +아이템
    //스라이드장애물 2 +아이템
    //스라이드장애물 3 +아이템

    public float high_box_x_Min = 2.5f;
    public float widthPadding = 20f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //player 스크립트 받으면 하자
    }

    public Vector3 SetRandomPlace(Vector3 lastPosition, GameObject randomPrefab)
    {
        // 마직막 위치 +y축으로 10만큼 더해주기 

        if (randomPrefab.gameObject.name.Contains("Down"))
        {
            lastPosition = new Vector3(lastPosition.x, -5.5f);
        }
        else if (randomPrefab.gameObject.name.Contains("Up"))
        {
            lastPosition = new Vector3(lastPosition.x, 2.53f);
        }
        else
        {
            Debug.Log("prefab 생성오류입니다!!");
        }
        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);

        // randomPrefab 받아와서 생성 
        Instantiate(randomPrefab, new Vector3(placePosition.x, placePosition.y), Quaternion.identity);
        //transform.position = placePosition; //이 오브젝트를 그대로 다음 y축에 복사 하는거잔아

        return placePosition; //리턴의 이유가 처음 lastPosition의 위치값을 받아야되기 때문임 //그럼 do while처럼 한번만 하는게 성능상 좋은데
    }

}
