using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickUpType
{
    medkit,
    ammo
}
public class PickUpController : MonoBehaviour
{
    public int ammoReplenish;
    public int healthToHeal;
    public PickUpType pickUpType;
    private PlayerController playerController;
    private WeaponController weaponController;

    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        weaponController = GameObject.Find("Player").GetComponent<WeaponController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //If pickup is medkit
            if(pickUpType == PickUpType.medkit)
            {
                //check if player health is equals to max health just to avoid removing the pickup when is not necesary
                if (playerController.CurrentLives != playerController.MaxLives)
                {
                    // check if the health is less or equals to max health minus the health to heal so we dont overheal
                    if (playerController.CurrentLives <= playerController.MaxLives - healthToHeal)
                    {
                        playerController.CurrentLives += healthToHeal;
                    }
                    else
                    {
                        playerController.CurrentLives =  playerController.MaxLives;
                    }
                    //remove the pickup and update health bar
                    gameObject.SetActive(false);
                    HUDController.instance.UpdateHealthBar(playerController.CurrentLives, false);
                }
            }
            else
            {
                //check if player current ammo is equals to max ammo just to avoid removing the pickup when is not necesary
                if (weaponController.currentAmmo != weaponController.maxAmmo)
                {
                    // check if the ammo is less or equals to max ammo minus the ammo to replenish so we dont give player more ammo than intended max ammo
                    if (weaponController.currentAmmo <= weaponController.maxAmmo - ammoReplenish)
                    {
                        weaponController.currentAmmo += ammoReplenish;
                    }
                    else
                    {
                        weaponController.currentAmmo = weaponController.maxAmmo;
                    }
                    //remove the pickup
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
