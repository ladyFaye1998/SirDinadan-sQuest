using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShot : StateMachineBehaviour
{
    public AudioClip soundToPlay;
    public float volume = 1f;
    public bool playOnEnter = true, playOnExit = false, playAfterDelay = false;

    public float playDelay = 0.25f;
    private float timeSinceEntered = 0;
    private bool hasDelayedSoundPlayed = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnEnter)
        {
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
        }

        timeSinceEntered = 0f;
        hasDelayedSoundPlayed = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playAfterDelay && !hasDelayedSoundPlayed)
        {
            timeSinceEntered += Time.deltaTime;

            if(timeSinceEntered > playDelay)
            {
                AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
                hasDelayedSoundPlayed = true;
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnExit)
        {
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
        }

    }
}
