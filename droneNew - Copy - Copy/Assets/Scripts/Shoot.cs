
using UnityEngine;

public class Shoot : MonoBehaviour
{
   
   public float damage = 10f;
   public float range = 100f;
   public float fireRateTimer = 0.1f;

    void Update()
    {  
          Fire();
    }

    void Fire()
{
     RaycastHit hit;
     if(Physics.Raycast(transform.position,transform.forward,out hit, range))
     {fireRateTimer -= Time.deltaTime;
     if(fireRateTimer <= 0 )
        {
             Debug.Log(hit.transform.name);
             fireRateTimer = 0.1f;
        }
     }
     else
     {
         fireRateTimer = 0.1f;
     }
}
}
