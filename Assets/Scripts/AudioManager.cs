using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    private AudioSource bgmPlayer;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume = 1.0f; // 전체 효과음의 기본 볼륨
    public int channels;
    private AudioSource[] sfxPlayer;
    private int channeIndex;

    [Header("#SFX Volumes")]
    [SerializeField] private float[] sfxVolumes; // 각 사운드 효과의 볼륨을 저장하는 배열

    public enum Sfx
    {
        OceanEnemyDeath,
        BossEnemyDeath,
        ShootNormalWeapon,
        ShootStrongWeapon,
        SummonPet,
        ShootSummonsWeapon,
        GetExpOrb,
        LevelUp,
        CardSelect,
        PlayerHitSound
    }

    protected override void Awake()
    {
        base.Awake();
        instance = this;
        init();
    }

    void init()
    {
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayer = new AudioSource[channels];

        for (int index = 0; index < sfxPlayer.Length; index++)
        {
            sfxPlayer[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayer[index].playOnAwake = false;
            sfxPlayer[index].volume = sfxVolume;
        }

        // Initialize default volumes for each SFX if not set in the inspector
        if (sfxVolumes == null || sfxVolumes.Length != sfxClips.Length)
        {
            sfxVolumes = new float[sfxClips.Length];
            for (int i = 0; i < sfxVolumes.Length; i++)
            {
                sfxVolumes[i] = 1.0f; // Set default volume to 1.0 (100%)
            }
        }
    }

    public void PlayBgm(bool isPlay)
    {
        if (isPlay)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int index = 0; index < sfxPlayer.Length; index++)
        {
            int loopIndex = (index + channeIndex) % sfxPlayer.Length;

            if (sfxPlayer[loopIndex].isPlaying)
            {
                continue;
            }

            channeIndex = loopIndex;
            sfxPlayer[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayer[loopIndex].volume = sfxVolume * sfxVolumes[(int)sfx]; // 전체 볼륨과 개별 볼륨을 곱하여 최종 볼륨 설정
            sfxPlayer[loopIndex].Play();
            break;
        }
    }
}




