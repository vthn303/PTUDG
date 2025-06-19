using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] destroyNoise;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void PlayRandomDestroyNoise()
    {
        //Choose a random number
        int clipToPlay = Random.Range(0, destroyNoise.Length);
        //play that clip
        destroyNoise[clipToPlay].Play();
    }

}
