using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSelectionController : MonoBehaviour{
    public List<GameObject> items = new List<GameObject>();
    private List<GameObject> active = new List<GameObject>();

    public void getSuperIndexes(){
        List<int> indexes = getRandom(((int[])Enum.GetValues(typeof(SuperIndexes))).ToList(),3);
        for(int i=0;i<3;i++){
            GameObject e = Instantiate(items[indexes[i]], transform);
            e.transform.position+=new Vector3((i-1)*2.5f,0,0);
            e.GetComponent<SuperItemUIController>().index=indexes[i];
            active.Add(e);
        }
    }
    
    public List<int> getRandom(List<int> list,int num){
        return list.OrderBy(x => UnityEngine.Random.value).Take(num).ToList();
    }

    public void clear(){
        foreach(GameObject item in active){
            Destroy(item);
        }
    }
}
