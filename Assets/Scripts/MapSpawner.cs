using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public class MapSpawner : MonoBehaviour
{
    //스테이지는 게임매니저에서 관리 
    
    [SerializeField] private GameObject[] mapPrefabs;
    [SerializeField] private GameObject[] bossMapPrefab;
   
    public float mapDeleteCount = 0; //EnemySpawner에서 사용하기 위해 public으로 교체
    private readonly Vector3 spawnPosition = new Vector3(0,0,40f);
    //추가사항
    private bool isBossMapSpawned = false; // 보스맵 상태 관리
    public event Action OnMapSpawned; //EnemySpawner를 작동시키기 위한 액션
    public event Action OnBossMapSpawned;
    [SerializeField] private float bossMapThreshold = 4; //보스맵 카운트(수정필요)
    private readonly int mapQuantityPerStage = 6;

    void Start()
    {
        MapTile.OnMapDeleted += HandleMapDeleted;
        Boss.OnBossDestroyed += HandleBossDestroyed;
        
        
        SpawnMap(Vector3.zero); 
        SpawnMap(spawnPosition);
    }

    void Update() 
    { 
        
    }
 
    void HandleBossDestroyed(){
        isBossMapSpawned = false;
        mapDeleteCount = 0;
        SpawnMap(spawnPosition);
    }
    
    void HandleMapDeleted()
    {
        mapDeleteCount++;
        if (isBossMapSpawned)
        {
            return;
        }

        if (mapDeleteCount > bossMapThreshold) 
        {
            BossMapSpawn();
        }
        else
        {
            SpawnMap(spawnPosition);
        }
    }
    
    void SpawnMap(Vector3 position)
    {
        GameManager gameManager = GameManager.Instance;
        Debug.Log("맵 스폰 됨" + gameManager.CurrentStage);
        int minIndex = (gameManager.CurrentStage-1) * mapQuantityPerStage;
        int maxIndex = gameManager.CurrentStage * mapQuantityPerStage;
        
        GameObject selectedNewMap = mapPrefabs[UnityEngine.Random.Range(minIndex, maxIndex)]; 
        GameObject newMap = Instantiate(selectedNewMap, position, Quaternion.identity);
        
        OnMapSpawned?.Invoke();//EnemySpawner를 작동시키기 위한 호출
    }

    void BossMapSpawn()
    {
        GameManager gameManager = GameManager.Instance;
        
        GameObject selecteBossMap = bossMapPrefab[gameManager.CurrentStage-1];
        GameObject bossMapSpawn = Instantiate(selecteBossMap, spawnPosition, Quaternion.identity);

        OnBossMapSpawned?.Invoke();
        isBossMapSpawned = true;
        
    }
}


