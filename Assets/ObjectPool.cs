using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject clone;
    [SerializeField] GameObject prafab;
    Queue<GameObject> pool = new Queue<GameObject>();

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject temp = Instantiate(prafab, clone.transform);
            temp.SetActive(false);
            pool.Enqueue(temp);
        }
    }

    public GameObject Pop()
    {
        GameObject popObj = pool.Dequeue();
        popObj.SetActive(true);
        return popObj;
    }
    public void ReturnPool(GameObject returnObj)
    {
        returnObj.SetActive(false);
        returnObj.transform.SetParent(clone.transform);
        returnObj.GetComponent<SoundComponent>().audioSource.loop = false;
        pool.Enqueue(returnObj);
    }
}
