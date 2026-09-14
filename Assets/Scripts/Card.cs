using System;

public enum CardType {
    WeaponUpgrade,  // 무기 업그레이드 카드
    NewWeapon,      // 새로운 무기 획득 카드
    SpeedBoost      // 이동 속도 증가 카드
}

public class Card {
    public CardType cardType;  // 카드의 종류
    public WeaponType weaponType;
    public WeaponAbilityType weaponAbilityType;

    public Card(CardType cardType, WeaponType weaponType = WeaponType.Normal, WeaponAbilityType weaponAbilityType = WeaponAbilityType.Damage) {
        this.cardType = cardType;
        this.weaponType = weaponType;
        this.weaponAbilityType = weaponAbilityType;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(cardType, weaponType, weaponAbilityType);
    }
    public override bool Equals(object obj)
    {
    if (obj is Card other)  {
      return other.cardType == cardType &&
              other.weaponType == weaponType &&
              other.weaponAbilityType == weaponAbilityType;
    }
    
    return false;
    }
}
