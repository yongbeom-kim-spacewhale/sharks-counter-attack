using System.Collections.Generic;
using UnityEngine;
public class CardManager : Singleton<CardManager>
{
    public HashSet<Card> cardPool = new HashSet<Card>();
    private float newWeaponChance = 0.1f;
    private float upgradeCardChance = 0.7f;
    private float speedBoostChance = 0.2f;
    
    private WeaponManager weaponManager;
    
    void Start()
    {
        weaponManager = WeaponManager.Instance;
    }
    
    public void GenerateCardPool()
    {
        cardPool.Clear(); // 기존 카드 풀을 초기화
        Debug.Log("Card Pool Cleared" + cardPool.Count);
        
        List<WeaponType> availableWeaponList = weaponManager.GetAvailableWeaponList();
        bool isAvailableNewWeapon = availableWeaponList.Count > 0;

        while (cardPool.Count < UIManager.Instance.cardButtons.Length)
        {
            float rand = Random.Range(0f, 1f);
            if (isAvailableNewWeapon && rand < newWeaponChance)
            {
                int newWeaponIndex = Random.Range(0, availableWeaponList.Count);
                cardPool.Add(new Card(CardType.NewWeapon, availableWeaponList[newWeaponIndex]));
                isAvailableNewWeapon = false;
                Debug.Log("Added New Weapon Card" + availableWeaponList[newWeaponIndex]);
                continue;
            }
            
            if (rand < newWeaponChance + upgradeCardChance)
            {
                List<WeaponType> ownWeaponList = weaponManager.GetOwnWeaponList();
                int weaponsToUpradeIndex = Random.Range(0, ownWeaponList.Count);
                WeaponType weaponsToUprade = ownWeaponList[weaponsToUpradeIndex];
                WeaponAbilityType weaponAbility = (WeaponAbilityType)Random.Range(0, 4);
                cardPool.Add(new Card(CardType.WeaponUpgrade, weaponsToUprade, weaponAbilityType: weaponAbility));
                Debug.Log("Added Weapon Upgrade Card");
            }
            else if (rand < newWeaponChance + upgradeCardChance + speedBoostChance)
            {
                cardPool.Add(new Card(CardType.SpeedBoost));
                Debug.Log("Added Speed Boost Card");
            }
        }

        Debug.Log("Card Pool Generated with " + cardPool.Count + " cards.");
    }

    public void ApplyCard(Card card)
    {
        switch (card.cardType)
        {
            case CardType.NewWeapon:
                weaponManager.EquimentWeapon(card.weaponType);
                break;
            case CardType.WeaponUpgrade:
                weaponManager.UpgradeWeapon(card.weaponType, card.weaponAbilityType);
                break;
            case CardType.SpeedBoost:
                Player.Instance.SpeedBoost(1);
                break;
        }
    }
}
