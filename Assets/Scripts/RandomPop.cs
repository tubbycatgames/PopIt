using UnityEngine;

public class RandomPop : MonoBehaviour 
{

    public AudioClip[] sounds;

    private AudioSource source;

    public void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlayPop()
    {
        source.clip = sounds[Random.Range(0, sounds.Length)];
        source.Play();
    }
}
