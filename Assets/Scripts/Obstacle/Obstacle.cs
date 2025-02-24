using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    //�Ʒ���ֹ� 1
    //�Ʒ���ֹ� 2
    //�Ʒ���ֹ� 3

    //�Ʒ���ֹ� 1 +������
    //�Ʒ���ֹ� 2 +������
    //�Ʒ���ֹ� 3 +������

    //����ֹ� 1
    //����ֹ� 2
    //����ֹ� 3

    //����ֹ� 1 +������
    //����ֹ� 2 +������
    //����ֹ� 3 +������

    //�����̵���ֹ� 1
    //�����̵���ֹ� 2
    //�����̵���ֹ� 3

    //�����̵���ֹ� 1 +������
    //�����̵���ֹ� 2 +������
    //�����̵���ֹ� 3 +������

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
        //player ��ũ��Ʈ ������ ����
    }

    public Vector3 SetRandomPlace(Vector3 lastPosition, GameObject randomPrefab)
    {
        // ������ ��ġ +y������ 10��ŭ �����ֱ� 

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
            Debug.Log("prefab ���������Դϴ�!!");
        }
        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);

        // randomPrefab �޾ƿͼ� ���� 
        Instantiate(randomPrefab, new Vector3(placePosition.x, placePosition.y), Quaternion.identity);
        //transform.position = placePosition; //�� ������Ʈ�� �״�� ���� y�࿡ ���� �ϴ°��ܾ�

        return placePosition; //������ ������ ó�� lastPosition�� ��ġ���� �޾ƾߵǱ� ������ //�׷� do whileó�� �ѹ��� �ϴ°� ���ɻ� ������
    }

}
