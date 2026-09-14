using UnityEngine;

public class SummonsWeapon : DefaultProjectileWeapon
{
    public override int damageCoefficient { 
        get => 2; 
        set => base.damageCoefficient = value; 
    }
    private void Awake()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ShootSummonsWeapon);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        
    }
}
