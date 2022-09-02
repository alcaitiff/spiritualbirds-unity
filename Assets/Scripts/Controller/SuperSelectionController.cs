using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSelectionController : MonoBehaviour{
    public List<GameObject> items = new List<GameObject>();
    private List<GameObject> active = new List<GameObject>();

    public void getSuperIndexes(List<int> current){
        List<int> available = Enum.GetValues(typeof(SuperIndexes)).Cast<SuperIndexes>().Cast<int>().Except(current).ToList();
        List<int> indexes = getRandom(available,3);
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
        int s = active.Count;
        for(int i=s;i>0;i--){
            GameObject o = active[i-1];
            active.Remove(o);
            Destroy(o);
        }
    }
}
