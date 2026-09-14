using System.Collections.Generic;

using UnityEngine;


public enum WeaponType{
  Normal,
  Strong,
  Pet,
}

public class WeaponManager : Singleton<WeaponManager>
{
    [SerializeField] private GameObject[] weaponsList = new GameObject[3];
    [SerializeField] private GameObject[] ownWeapons = new GameObject[3];
    
    public void FireOwnWeapon()
    {
        GameManager gameManager = GameManager.Instance;
        if(gameManager.isPaused) return;

        foreach (GameObject ownWeapon in ownWeapons)
        {
            if(ownWeapon == null) continue;
            
            Weapon weapon = ownWeapon.GetComponent<Weapon>();
            weapon.Generate(transform.position);
        }
    }

    //새로운 무기 선택시 리스트에 넣는 메소드
    public void EquimentWeapon(WeaponType weaponType)
    {
        GameObject newWeapon = weaponsList[(int)weaponType];
        ownWeapons[(int)weaponType] = newWeapon;
    }
    
    public void UpgradeWeapon(WeaponType weaponType, WeaponAbilityType abilityType)
    {
        if(ownWeapons[(int)weaponType] == null)
        {
            Debug.Log("업그레이드 하려는 능력치:" + abilityType + "의 해당 무기:" + weaponType + "가 없습니다.");
            return;
        }
        
        GameObject weapon = ownWeapons[(int)weaponType];
        Weapon weaponScript = weapon.GetComponent<Weapon>();
        weaponScript.Upgrade(abilityType);
    }
    
    public List<WeaponType> GetAvailableWeaponList()
    {
        List<WeaponType> availableWeapons = new List<WeaponType>();
        
        for (int i =1; i < 3; i++)
        {
            if(ownWeapons[i] == null)
            {
                availableWeapons.Add((WeaponType)(i));
            }
        }
        
        return availableWeapons;
    }
    
    public List<WeaponType> GetOwnWeaponList()
    {
        List<WeaponType> ownWeaponsList = new List<WeaponType>();
        for (int i = 0; i < 3; i++)
        {
            if(ownWeapons[i] != null)
            {
                ownWeaponsList.Add((WeaponType)(i));
            }
        }
        return ownWeaponsList;
    }

}
