using System;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAbilityType{
  Damage,
  CoolTime,
  Speed,
  Range
}


public class Weapon : MonoBehaviour
{
    [SerializeField] public int weaponDamage = 1;
    [SerializeField] public float weaponSpeed = 5f;
    [SerializeField] public float _weaponCoolTime = 1f;
    public virtual float weaponCoolTime
    {
        get => _weaponCoolTime;
        set => _weaponCoolTime = value;
    }
    [SerializeField] public float weaponRange = 3f;

    public int _damageCoefficient = 1;
    
    public virtual int damageCoefficient
    {
        get => _damageCoefficient;
        set => _damageCoefficient = value;
    }
    
    public float _speedCoefficient = 0f;
    
    public virtual float speedCoefficient
    {
        get => _speedCoefficient;
        set => _speedCoefficient = value;
    }
    public float _coolTimeCoefficient = 0.025f;
    
    public virtual float coolTimeCoefficient
    {
        get => _coolTimeCoefficient;
        set => _coolTimeCoefficient = value;
    }
    public float _rangeCoefficient = 0f;
    
    public virtual float rangeCoefficient
    {
        get => _rangeCoefficient;
        set => _rangeCoefficient = value;
    }
    protected Vector3 weaponPosition;

    protected DateTime weaponLastShotTime;


    private void Awake()
    {
        weaponLastShotTime = DateTime.MinValue;
    }

    protected virtual void Start()
    {
        // weaponLastShotTime 초기화는 Awake에서만 수행
        // weaponLastShotTime = DateTime.MinValue;
        //stats = new Dictionary<string, float>
        //{
        //    {"weaponDamage", weaponDamage },
        //    {"weaponSpeed", weaponSpeed },
        //    {"weaponCoolTime", weaponCoolTime },
        //    {"weaponRange", weaponRange },
        //};
    }

    public virtual void Generate(Vector3 position)
    {
        if ((DateTime.Now - weaponLastShotTime).TotalSeconds >= weaponCoolTime)
        {
            Instantiate(gameObject, position, Quaternion.identity);
            weaponLastShotTime = DateTime.Now;
        }
    }

    public virtual void Upgrade(WeaponAbilityType weaponAbilityType) 
    {
        switch (weaponAbilityType) 
        {
            case WeaponAbilityType.Damage:
                weaponDamage += damageCoefficient;
                break;
            case WeaponAbilityType.Speed:
                weaponSpeed += speedCoefficient;
                break;
            case WeaponAbilityType.CoolTime:
                weaponCoolTime -= coolTimeCoefficient;
                break;
            case WeaponAbilityType.Range:
                weaponRange += rangeCoefficient;
                break;
        }
    }
}
