using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    private Player playerScript;
    public AudioSource ballAudio;
    public AudioClip crystalHitClip;
    public AudioClip glassBreakSound;
    private void Awake()
    {
        playerScript = FindAnyObjectByType<Player>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Crystal"))
        {
            int addToCurrentBallCount = playerScript.currentBallCount += 5;
            playerScript.currentBallCountText.text = addToCurrentBallCount.ToString();

            playerScript.UpdateScore(playerScript.score + 10);

            ballAudio.PlayOneShot(crystalHitClip, 1f);
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            // Generate a random pitch between 0.9f and 1.1f
            float randomPitch = Random.Range(0.65f, 1.1f);

            // Set the pitch of the AudioSource before playing the sound
            ballAudio.pitch = randomPitch;

            // Play the sound with the random pitch
            ballAudio.PlayOneShot(glassBreakSound, 1f);

            // Reset the pitch to its default value if necessary
            ballAudio.pitch = 1f;
        }
    }
}
