using System.Collections.Generic;
using UnityEngine;

public class BulletPoll : MonoBehaviour {

    public GameObject prefabs;
    public List<GameObject> bulletPool = new List<GameObject>();
    public int maxCount;

    private void Awake() {
        for (int i = 0; i < maxCount; ++i) {
            bulletPool.Add(Instantiate(prefabs));
            bulletPool[i].SetActive(false);
            bulletPool[i].transform.parent = transform;
        }
    }

    public GameObject SpawnObject(Vector3 position, Quaternion rotation) {
        GameObject returnObject;
        if (bulletPool.Count > 0) {
            returnObject = bulletPool[0];
            bulletPool.RemoveAt(0);
        }
        else {
            returnObject = Instantiate(prefabs, transform, true);
        }

        returnObject.SetActive(true);
        returnObject.transform.position = position;
        returnObject.transform.rotation = rotation;
        return returnObject;
    }

    public void ReturnPool(GameObject go) {
        go.SetActive(false);
        bulletPool.Add(go);
    }

    public void ClearPool() {
        bulletPool.Clear();
    }
    
    
}