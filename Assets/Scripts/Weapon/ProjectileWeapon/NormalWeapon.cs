using System;
using UnityEngine;

public class NormalWeapon : DefaultProjectileWeapon
{
    //긴 쿨타임, 강력한 데미지

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ShootNormalWeapon);
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //public override void Generate(Vector3 position)
    //{
    //    base.Generate(position);
    //    AudioManager.instance.PlaySfx(AudioManager.Sfx.ShootNormalWeapon);
    //}


}
