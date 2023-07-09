using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool instance;
    private List<GameObject> pooledObjects = new List<GameObject>();
    public int bulletPoolAmount;
    public int explosionPoolAmount;

    //projectiles
    [SerializeField] private GameObject bulletPrefab;

    //explosion prefabs
    [SerializeField] private GameObject explosionPrefab;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update

    void Start()
    {
        //instantiate pool with amount wanted to pool
        for (int i = 0; i < bulletPoolAmount; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
        for (int i = 0; i < explosionPoolAmount; i++)
        {
            GameObject obj = Instantiate(explosionPrefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    public List<GameObject> getPoolObjects()
    {
        return pooledObjects;
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            //if we find an available bullet that we can shoot
            // if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == "Projectile")
            // {
            //     return pooledObjects[i];
            // }
            // else if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == "Explosion")
            // {
            //     return pooledObjects[i];
            // }

            if (!pooledObjects[i].activeInHierarchy)
            {
                 return pooledObjects[i];
            }


        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
