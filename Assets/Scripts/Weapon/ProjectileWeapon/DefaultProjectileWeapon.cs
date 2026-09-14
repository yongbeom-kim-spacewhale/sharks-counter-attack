using UnityEngine;

public class DefaultProjectileWeapon : Weapon
{
    protected virtual void Update()
    {
        ProjectileWeaponMove();
        WeaponDeleteThreshold();
    }

    void ProjectileWeaponMove()
    {
        transform.Translate(Vector3.forward * weaponSpeed * Time.deltaTime);
    }

    void WeaponDeleteThreshold()
    {
        if (transform.position.z >= weaponRange)
        {
            Destroy(gameObject);
        }
    }
}
