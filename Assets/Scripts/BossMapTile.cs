using UnityEngine;

public class BossMapTile : MapTile
{
    private bool isBossDestoryed = false;

    protected override void Start()
    {
        base.Start();
        Boss.OnBossDestroyed += ResumeMovement;
    }

     void OnDestroy()
    {
        Boss.OnBossDestroyed -= ResumeMovement;
    }

    protected override void MoveMapTile()
    {
        if (transform.position.z <= 0 && !isBossDestoryed)
        {
            return;
        }

        base.MoveMapTile();
    }

    private void ResumeMovement()
    {
        isBossDestoryed = true;
    }
}
