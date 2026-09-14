using System;
using UnityEngine;

public class DefaultSummonWeapon : Weapon
{
    private Transform player;
    [SerializeField] private float followSpeed = 1f;
    [SerializeField] private float followDistance = 2f;
    public float projectileCoolTime = 1f;
    public GameObject SummonsWeapon; // squidInkPrefab
    
    private void Awake()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.SummonPet);
    }

    public override float weaponCoolTime
    {
        get => 100000000f;
        set => base.weaponCoolTime = value;
    }

    protected override void Start()
    {
        base.Start();
        FindPlayerPosition();
    }

    void Update()
    {
        FollowPlayer();
        
        GameManager gameManager = GameManager.Instance;
        if(gameManager.isPaused) return;
        
        GenerateSummonsWeapon();
    }

    void FindPlayerPosition()
    {
        Player playerObject = FindFirstObjectByType<Player>();
        player = playerObject.transform;
    }

    float DistanceWithPlayerX()
    {
        return player.position.x - transform.position.x;
    }

    void MoveTowardPlayer()
    {
        float distanceX = DistanceWithPlayerX();
        Vector3 direction = new Vector3(distanceX, 0, 0).normalized;
        transform.position += direction * followSpeed * Time.deltaTime;
    }

    void FollowPlayer()
    {
        if (player == null) return;

        float distance = Mathf.Abs(DistanceWithPlayerX());
        if (distance > followDistance)
        {
            MoveTowardPlayer();
        }
    }

    public override void Generate(Vector3 position)
    {
        Vector3 ModifiySummonPosition = new Vector3(position.x + 2, position.y, position.z - 4);
        base.Generate(ModifiySummonPosition);
    }
    
     public override void Upgrade(WeaponAbilityType weaponAbilityType)
    {
        SummonsWeapon summonsWeaponScript = SummonsWeapon.GetComponent<SummonsWeapon>();
        
        if (summonsWeaponScript != null)
        {
            summonsWeaponScript.Upgrade(weaponAbilityType);
        }
    }

    void GenerateSummonsWeapon()
    {
        Vector3 shootPositionModifiy = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        if ((DateTime.Now - weaponLastShotTime).TotalSeconds >= projectileCoolTime)
        {
            Instantiate(SummonsWeapon, shootPositionModifiy, Quaternion.identity);
            weaponLastShotTime = DateTime.Now;
        }
    }
}
