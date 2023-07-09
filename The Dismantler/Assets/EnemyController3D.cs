using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

//[] enemy ai
//[] enemy attributes
/*

- dismantle percentage
- 

*/
public class EnemyController3D : MonoBehaviour, Targettable
{

    public float dismantlePercentage = 0f; // current dismantle percentage
    bool isDismantled = false;
    private void Start()
    {

    }
    public void OnHit()
    {

        // this.gameObject.SetActive(false);
          //if object is explodable
      if (this.gameObject.layer == 3)
        {
            //explode
            isDismantled = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
       if(isDismantled)
       {
            OnDeathExplode();
       }
    }
    public void OnDeathExplode()
    {
       
        //  GameObject bullet = BulletPool.instance.GetPooledObject();
        List<GameObject> pooledObjects = ObjectPool.instance.getPoolObjects();
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == "Explosion")
            {
                // spawn a sphere at a random position within explosion radius
                GameObject sphere = pooledObjects[i];
                sphere.SetActive(true);
            }
        }

        //enemy gets set as false
        this.gameObject.SetActive(false);

    }




}
