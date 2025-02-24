using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BGLopper : MonoBehaviour
{
    public int numBgCount = 5;

    private void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
       

        Debug.Log("Triggerd: " +  collision.name);
        if (collision.CompareTag("BackGround"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            Debug.Log("Background Width: " + widthOfBgObject);
            collision.transform.position = pos;
            return;
        }
    }

    
}
