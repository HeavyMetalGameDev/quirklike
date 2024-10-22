using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    List<GameObject> pool;
    [SerializeField] GameObject objectPrefab;
    [SerializeField] int poolSize = 1;
    [SerializeField] int automaticResizeCount = 5;

    void Start()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            AddObjectToPool();
        }
    }

    void AddObjectToPool()
    {
        GameObject newObj = Instantiate(objectPrefab, transform.position,Quaternion.identity);
        newObj.SetActive(false);
        pool.Add(newObj);
    }

    void ResizePool(int size) //only resize to be bigger for now
    {
        int numToIncrease = size - poolSize;
        for (int i = 0; i < numToIncrease;i++)
        {
            AddObjectToPool();
        }
        poolSize = size;
    }

    public GameObject GetFreeItem()
    {
        foreach(GameObject obj in pool) //not the most efficient method but does the job for now
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        int endIndex = poolSize - 1; //end of the pool
        ResizePool(poolSize + automaticResizeCount);
        GameObject newObj = pool[endIndex + 1];
        newObj.SetActive(true);
        return newObj; //we made more so this should now exist and be inactive.
    }

}
