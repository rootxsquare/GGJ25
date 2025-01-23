using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{

public AudioClip[] footstepSounds;

   public void footStepSound()
   {
        GetComponent<AudioSource>().PlayOneShot(footstepSounds[Random.Range(0,footstepSounds.Length)]);
   }
}
