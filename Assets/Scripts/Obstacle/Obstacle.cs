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
    public float widthPadding = 10f;
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

    public Vector3 SetRandomPlace(Vector3 lastPosition)
    {
        // ������ ��ġ +y������ 10��ŭ �����ֱ� 
        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);

        //transform.position = placePosition; //�� ������Ʈ�� �״�� ���� y�࿡ ���� �ϴ°��ܾ�

        return placePosition; //������ ������ ó�� lastPosition�� ��ġ���� �޾ƾߵǱ� ������ //�׷� do whileó�� �ѹ��� �ϴ°� ���ɻ� ������

        //�� �޼ҵ�� y���� ���� �Ѱ��ִ°ž� 
        
    }

}
