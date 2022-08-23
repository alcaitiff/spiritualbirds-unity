using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsController : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabBullet;
    public GameManager gm;
    private int lastBulletsValue = 0;
    public List<GameObject> bullets = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(gm && gm.player){
            int num = gm.player.getMaxBullets() - gm.player.bullets.Count;
            if (num != lastBulletsValue) {
                if (num > lastBulletsValue) {
                    addBullets(num - lastBulletsValue);
                } else {
                    removeBullets(lastBulletsValue - num);
                }
                lastBulletsValue = num;
            }
        }
    }
    private void addBullets(int num){
        while (num > 0) {
          Vector3 pos = new Vector3(transform.position.x+(0.3f*bullets.Count),transform.position.y,transform.position.z);
          GameObject bullet = Instantiate(prefabBullet, pos, Quaternion.identity);
          Animator animator = bullet.GetComponent<Animator>();
          animator.SetBool("UI", true); 
          bullet.transform.localScale*=0.3f;
          bullets.Add(bullet);
          --num;
        }
    }
    private void removeBullets(int num){
        while (num > 0) {
          GameObject bullet = bullets[bullets.Count -1];
          bullets.Remove(bullet);
          Animator animator = bullet.GetComponent<Animator>();
          animator.SetBool("Hit", true); 
          --num;
        }
    }
}
