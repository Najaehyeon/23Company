using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    // ��� �̵� �ӵ� ���� (1~200 ���� �� ���� ����)
    [SerializeField][Range(1f, 200f)] float speed = 3f;

    // ����� �ݺ��� ������ �� (�ػ� ���� ����)
    [SerializeField] float posValue;

    // ����� �ʱ� ��ġ
    Vector2 startPos;

    // ����� ���ο� ��ġ ���� ����
    float newPos;

    void Start()
    {
        startPos = transform.position; // ���� ������Ʈ�� �ʱ� ��ġ ����
    }

    void Update()
    {
        // Mathf.Repeat�� ����Ͽ� posValue ���� �ʰ����� �ʵ��� �ݺ����� �� ���
        newPos = Mathf.Repeat(Time.time * speed, posValue);

        // ����� �������� �̵�, �ʱ� ��ġ���� newPos��ŭ �̵���Ŵ
        transform.position = startPos + Vector2.left * newPos;
    }
}
