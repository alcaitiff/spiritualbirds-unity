using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats
{
    public int killed = 0;
    public int slipped = 0;
    public int index = 0;

    public EnemyStats(int index){
        this.index=index;
    }
    
    public EnemyStats clear(){
        killed = 0;
        slipped = 0;
        return this;
    }
}
