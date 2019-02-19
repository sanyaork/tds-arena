using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPos : MonoBehaviour
{
    public enemyAI enemyStat;
    public int multiplication;

    // Update is called once per frame
   public void Damage(int damage)
    {
        enemyStat.TakeAwayHealth(damage * multiplication);    
    }
}
