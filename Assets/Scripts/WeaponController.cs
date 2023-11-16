using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform outPosition;
    public int currentAmmo;
    public int maxAmmo;
    public bool infiniteAmmo;

    public float bulletSpeed;
    public float shootRate;
    public int damage;

    private ObjectPool pool;
    private float lastShotTime;
    private bool isPlayer=false;

    private void Awake()
    {
        if (GetComponent<PlayerController>())
        {
            isPlayer = true;
        }
        pool = GetComponent<ObjectPool>();
    }

    public bool CanShoot()
    {
        if(Time.time - lastShotTime >= shootRate)
        {
            if(currentAmmo>0 || infiniteAmmo)
            {
                return true;
            }
        }
        return false;
    }

    public void Shoot()
    { 
        lastShotTime = Time.time;

        //reduce the ammo
        if (!infiniteAmmo)
        {
            currentAmmo--;
        }
        //Get a bullet
        GameObject bullet = pool.GetGameObject();

        //Locate the bullet at out position
        bullet.transform.position = outPosition.position;
        bullet.transform.rotation = outPosition.rotation;
        bullet.GetComponent<BulletController>().Damage = damage;
        if(isPlayer)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
            RaycastHit hit;
            Vector3 targetPoint;
            if (Physics.Raycast(ray,out hit))
            {
                targetPoint= hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(35);
            }
            bullet.GetComponent<Rigidbody>().velocity = (targetPoint - bullet.transform.position).normalized * bulletSpeed;
        }
        else
        {
            bullet.GetComponent<Rigidbody>().velocity = outPosition.forward * bulletSpeed;
        }
     
        

        
    }



}
