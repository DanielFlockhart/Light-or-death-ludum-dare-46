using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip Lobby;
    public AudioClip InGame;
    public AudioClip Death;
    public AudioClip Attack;
    public AudioClip Shield;
    public AudioClip Enemy;
    public AudioClip Orb;
    public AudioClip Walking;
    public AudioClip Winning;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void lobbyMusic()
    {
        audioSource.PlayOneShot(Lobby, settingsScript.musicVal);
    }
    void InGameMusic()
    {
        audioSource.PlayOneShot(InGame, settingsScript.musicVal);

    }
    void DeathSound()
    {
        audioSource.PlayOneShot(Death, settingsScript.SFX);
    }
    void AttackSound()
    {
        audioSource.PlayOneShot(Attack, settingsScript.SFX);
    }
    void ShieldSound()
    {
        audioSource.PlayOneShot(Shield, settingsScript.SFX);

    }
    void EnemySound()
    {
        audioSource.PlayOneShot(Enemy, settingsScript.SFX);
    }
    void OrbSound()
    {
        audioSource.PlayOneShot(Orb, settingsScript.SFX);
    }
    void WalkingSound()
    {
        audioSource.PlayOneShot(Walking, settingsScript.SFX);

    }
    void WinningSound ()
    {
        audioSource.PlayOneShot(Winning, settingsScript.SFX);
    }

}
