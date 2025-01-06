using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip[] GunSounds;
    public AudioClip MeeleSound;

    public AudioSource PlayerAttack_AS;
    public AudioSource PlayerDieAS;

    public AudioSource ZombieAttack_AS;
    public AudioSource ZombieRise_AS;
    public AudioSource ZombieDie_AS;

    public AudioClip ZombieDie_Clip,ZombieRise_Clip;
    public AudioClip[] ZombieAttack_Clips;

    public AudioSource FenceExplosion_AS;
    public AudioClip FenceExplosion_Clip;

    public AudioSource CoinCollectedAS;
    public AudioSource OutOfAmmoAS;

    public AudioSource MainMenuAS;
    public AudioSource GamePlayAS;

    void Awake()
    {
        MakeSingleton();
    }
    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void GunSound(int index)
    {
        PlayerAttack_AS.PlayOneShot(GunSounds[index], 1f);
    }

    public void MeleeSound()
    {
        PlayerAttack_AS.PlayOneShot(MeeleSound, 1f);
    }

    public void ZombieAttack()
    {
        int index = Random.Range(0,ZombieAttack_Clips.Length);
        ZombieAttack_AS.PlayOneShot(ZombieAttack_Clips[index], .235f);
    }

    public void ZombieDieSound()
    {
        ZombieDie_AS.PlayOneShot(ZombieDie_Clip, 1f);
    }

    public void ZombieRiseSound()
    {
        ZombieRise_AS.PlayOneShot(ZombieRise_Clip, 1f);
    }

    public void FenceExplosionSound()
    {
        FenceExplosion_AS.PlayOneShot(FenceExplosion_Clip, 1f);
    }

    public void CoinCollectedSound()
    {
        CoinCollectedAS.Play();
    }

    public void PlayerDieSound()
    {
        PlayerDieAS.Play();
    }

    public void NoAmmoSound()
    {
        OutOfAmmoAS.Play();
    }

    public void MainMenuSound()
    {
        MainMenuAS.Play();
    }
    public void StopMainMenuSound()
    {
        MainMenuAS.Stop();
    }

    public void GamePlaySound()
    {
        GamePlayAS.Play();
    }
    
    public void UnPauseGamePlaySound()
    {
        GamePlayAS.UnPause();
    } 

    public void PauseGamePlaySound()
    {
        GamePlayAS.Pause();
    }
    
    public void StopGamePlaySound()
    {
        GamePlayAS.Stop();
    }
}
