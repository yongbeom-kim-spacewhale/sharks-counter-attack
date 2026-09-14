using System;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 15f;
    protected float deleteThresholdPosition = -40f;
    public static event Action OnMapDeleted;

    protected virtual void Start()
    {
        // 기본 맵 타일의 초기화 작업
    }

    protected virtual void Update()
    {
        MoveMapTile();
        DeleteMap();
    }

    protected virtual void MoveMapTile()
    {
        transform.position += Vector3.back * moveSpeed * Time.deltaTime;
    }

    protected void DeleteMap()
    {
        if (transform.position.z <= deleteThresholdPosition)
        {
            OnMapDeleted?.Invoke();
            Destroy(gameObject);
        }
    }
}
