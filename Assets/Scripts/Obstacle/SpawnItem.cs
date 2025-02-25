using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnItem : MonoBehaviour
{
    Vector3 _position = Vector3.zero;
    [SerializeField] private List<GameObject> itemPrefabs;
    SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        SpawnItems();
        renderer =GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnItems()
    {
        if (itemPrefabs.Count == 0)
        {
            Debug.Log("itemPrefabs 설정되지 않았습니다");
        }
        GameObject randomitem = itemPrefabs[Random.Range(0, itemPrefabs.Count)];
        
        //이것의 이름은 itme 이름이지 
        if (gameObject.transform.parent.name.Contains("Down"))
        {
            _position = gameObject.transform.parent.transform.position+new Vector3(0, 3.8f); 
            
        }
        else if (gameObject.transform.parent.name.Contains("Up"))
        {
            _position = gameObject.transform.parent.transform.position + new Vector3(0, -6.5f);
        }
        else
        {
            Debug.Log("prefab 생성오류입니다!!");
        }


        Instantiate(randomitem,_position,Quaternion.identity);
    }
}
