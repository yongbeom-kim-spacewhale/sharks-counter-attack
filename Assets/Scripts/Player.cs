using System;
using UnityEngine;

public class Player : Singleton<Player>
{
    
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float moveStopThreshold = 9f;
    [SerializeField] private int currentHealthPoint = 10;
    [SerializeField] private int baseMaxHealthPoint = 10;
    [SerializeField] private int level = 1;
    [SerializeField] private int exp = 0;
    
    private int currentMaxHealthPoint;
    
    public event Action<int> OnLevelUp; //레벨업 이벤트
    public event Action<int> OnExpIncreased; //경험치 증가 이벤트
    public event Action<int> OnHpChanged;  // HP 변경 이벤트
    public event Action OnPlayerDead;     // 플레이어 사망 이벤트
    private WeaponManager weaponManager;
    

    void Start()
    {
        OnHpChanged += UpdateHpUI;
        OnPlayerDead += HandlePlayerDeath;
        OnLevelUp += HandlePlayerLevelup;
        OnExpIncreased += HandlePlayerExpIncreased;
        
        currentMaxHealthPoint = GetMaxHpForLevel(level);
        currentHealthPoint = currentMaxHealthPoint;
        weaponManager = GetComponentInChildren<WeaponManager>();


    }
    void Update()
    {
        Move();
        weaponManager.FireOwnWeapon();
    }
    
    void OnDestroy()
    {
        //플레이어 파괴시 메모리 누수 방지를 위해 이벤트 핸들러 제거
        OnHpChanged -= UpdateHpUI;
        OnPlayerDead -= HandlePlayerDeath;
        OnLevelUp -= HandlePlayerLevelup;
        OnExpIncreased -= HandlePlayerExpIncreased;
    }

    
    private void HandlePlayerExpIncreased(int currentExp){
        Debug.Log($"경험치 획득! 현재 경험치 : {currentExp} 남은 경험치: {GetExpThresholdForLevel(level) - currentExp}");
    }
    
    private void HandlePlayerLevelup(int currentLevel){
        Debug.Log($"레베루 업! 현재 경험치: {exp} 레벨 : {currentLevel} 필요량:{GetExpThresholdForLevel(currentLevel)}");
    }
    
     private void HandlePlayerDeath()
    {
        Debug.Log("플레이어 사망~!");
    }
    
    private void UpdateHpUI(int currentHp)
    {
        Debug.Log($"플레이어 HP: {currentHp}");
    }
    
    public void TakeDamage(int damage){
        AudioManager.instance.PlaySfx(AudioManager.Sfx.PlayerHitSound);
        currentHealthPoint -= damage;
        
        OnHpChanged?.Invoke(currentHealthPoint);
        
        if(currentHealthPoint<=0){
            currentHealthPoint = 0;
            OnPlayerDead?.Invoke();
        }
    }
    
    public void IncreaseExp(int quantity)
    {
        exp += quantity;
        OnExpIncreased?.Invoke(exp);
        CheckLevelUp();
    }
    
    private int GetExpThresholdForLevel(int currentLevel)
    {
        return 10 + ((currentLevel-1) * 4); //레벨당 필요경험치 수정 필요
    }
    
    private void CheckLevelUp()
    {
        while (exp >= GetExpThresholdForLevel(level))
        {
            exp -= GetExpThresholdForLevel(level);
            LevelUp();
        }
    }
    
    public void SpeedBoost(float boostAmount)
    {
        moveSpeed += boostAmount;
    }
    
    private void LevelUp()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        level++;

        int prevMaxHealthPoint = currentMaxHealthPoint;
        currentMaxHealthPoint = GetMaxHpForLevel(level);
        
        int healthPointIncrease = currentMaxHealthPoint - prevMaxHealthPoint;
        IncreaseHealth(healthPointIncrease); 
        
        OnLevelUp?.Invoke(level); // 외부에서 넘겨준 레벨업 이벤트 핸들러 호출. ex) 능력 카드 선택창...
    }
    
    void IncreaseHealth(int quantity){
        int tempNewHealthPoint = currentHealthPoint + quantity;
        
        currentHealthPoint = Mathf.Clamp(tempNewHealthPoint, 0, currentMaxHealthPoint);
    }

    public void DecreaseHealth(int quantity){
        currentHealthPoint -= quantity;
    }
    
    private int GetMaxHpForLevel(int currentLevel)
    {
        return baseMaxHealthPoint + ((currentLevel - 1) * 4); ////레벨당 체력 몇 증가할지 고민
    }

    
    void Move()
    {
        /* 
        Input.GetAxis = 가속과 감속을 이용한 부드러운 입력변화
        Input.GetAxisRaw = 디지털 신호처럼 입력이 바로변화함
        Input.GetKeyDown을 이용한 움직임 = 위와 동일하게 감가속이 없음
        */
        
        Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        bool canMoveLeft =  transform.position.x > -Math.Abs(moveStopThreshold);
        bool canMoveRight = transform.position.x < Math.Abs(moveStopThreshold);
        
        if (Input.GetKey(KeyCode.LeftArrow)&& canMoveLeft)
        {
            transform.position -= moveTo;

        }
        else if (Input.GetKey(KeyCode.RightArrow) && canMoveRight)
        {
            transform.position += moveTo;
        }
    }
}
