using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    //Music
    [Header("Music")]
    [SerializeField] public AudioSource musicSound;
    
    //SFX
    [Space(5)] [Header("Sounds")] 
    private AudioSource[] m_Sounds;
    private AudioSource m_SprintSound;
    private AudioSource m_SpellHit;
    private AudioSource m_SpellAura;
    private AudioSource m_SpellLaunch;
    private AudioSource m_ClickSound;
    private AudioSource m_ProjectileAttack;
    private AudioSource m_AnvilSound;
    private AudioSource m_MimicAttack;
    private AudioSource m_EnemyHurt;
    
    
    //Volumes
    [Space(5)] [Header("Volume Variables")]
    [SerializeField] private float soundsVolume;
    [SerializeField] private float musicVolume;
    
    //Sliders
    [Space(5)] [Header("Sliders")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    void Start()
    {
        m_SprintSound = GameObject.Find("SprintSound").GetComponent<AudioSource>();
        m_SpellHit = GameObject.Find("SpellHit").GetComponent<AudioSource>();
        m_SpellAura = GameObject.Find("SpellAura").GetComponent<AudioSource>();
        m_SpellLaunch = GameObject.Find("SpellLaunch").GetComponent<AudioSource>();
        m_ClickSound = GameObject.Find("ClickSound").GetComponent<AudioSource>();
        m_ProjectileAttack = GameObject.Find("ProjectileAttack").GetComponent<AudioSource>();
        m_AnvilSound = GameObject.Find("AnvilMenu").GetComponent<AudioSource>();
        m_MimicAttack = GameObject.Find("MimicAttack").GetComponent<AudioSource>();
        m_EnemyHurt = GameObject.Find("EnemyHurt").GetComponent<AudioSource>();
        
        m_Sounds = new AudioSource[9];
        m_Sounds[0] = m_SprintSound;
        m_Sounds[1] = m_SpellHit;
        m_Sounds[2] = m_SpellAura;
        m_Sounds[3] = m_SpellLaunch;
        m_Sounds[4] = m_ClickSound;
        m_Sounds[5] = m_ProjectileAttack;
        m_Sounds[6] = m_AnvilSound;
        m_Sounds[7] = m_MimicAttack;
        m_Sounds[8] = m_EnemyHurt;

        musicSound.volume = 0.1f;
        soundsVolume = 0.1f;
        musicSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AdjustMusicVolumeValue()
    {
        musicVolume = musicSlider.value;
        musicSound.volume = musicVolume;
    }
    
    public void AdjustSoundVolumeValue()
    {
        soundsVolume = soundSlider.value;
        foreach (var sound in m_Sounds)
        {
            sound.volume = soundsVolume;
        }
    }

    public void PlayClickSound()
    {
        m_ClickSound.Play();
    }
    
    public void PlayProjectileSound()
    {
        m_ProjectileAttack.Play();
    }
    
    public void PlayAnvilSound()
    {
        m_AnvilSound.Play();
    }
    
    public void PlayMimicAttackSound()
    {
        m_MimicAttack.Play();
    }
    
    public void PlayEnemyHurt()
    {
        m_EnemyHurt.Play();
    }

    public void StopMusic()
    {
        musicSound.Stop();
    }

    public void PlaySpellLaunch()
    {
        m_SpellLaunch.Play();
    }
    
    public void PlaySprintSound()
    {
        m_SprintSound.Play();
    }
    
    public void PauseSprintSound()
    {
        m_SprintSound.Stop();
    }

    public void SetVolumes(float musicVolume, float soundVolume)
    {
        this.musicVolume = musicVolume;
        this.soundsVolume = soundVolume;
    }

    public void StopAllSounds()
    {
        foreach (var sound in m_Sounds)
        {
            sound.Stop();
        }
    }
    
}
