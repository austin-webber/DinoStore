using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ATCG_Enemy : MonoBehaviour
{
    public float speed;
    public AudioSource audioSource;
    public AudioClip goodSound;
    public AudioClip badSound;
    private bool soundPlayed;
    SpriteRenderer spriteRenderer;

    public CameraShake cameraShake;

    private void Awake()
    {
        soundPlayed = false;
        cameraShake = FindObjectOfType<CameraShake>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (gameObject.transform.position.x < -8.5 && !soundPlayed)
        {
            soundPlayed = true;
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            StartCoroutine(WaitForBadAudio(badSound));
        }
    }

    // coroutine that takes an audio clip as a parameter.
    // plays the audio, shakes the screen, waits for the clip to finish
    // restarts the scene
    private IEnumerator WaitForBadAudio(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);

        StartCoroutine(cameraShake.Shake(.15f, .3f));
        yield return new WaitForSeconds(audioClip.length);

        // the following will happen when the audioSource has finished playing
        Destroy(gameObject);
        GlobalVariables.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator WaitForGoodAudio(AudioClip audioClip, Collider2D collider2D)
    {
        audioSource.PlayOneShot(audioClip);

        yield return new WaitForSeconds(audioClip.length);

        // the following will happen when the audioSource has finished playing
        Destroy(gameObject);
        Destroy(collider2D.gameObject);

    }

    private void DisableSpriteRenderers(Collider2D collider2D)
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        collider2D.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.tag.Equals("A") && other.tag.Equals("T"))
        {
            DisableSpriteRenderers(other);
            StartCoroutine(WaitForGoodAudio(goodSound, other));
            GlobalVariables.score += 1;
        }
        else if (gameObject.tag.Equals("T") && other.tag.Equals("A"))
        {
            DisableSpriteRenderers(other);
            StartCoroutine(WaitForGoodAudio(goodSound, other));
            GlobalVariables.score += 1;
        }
        else if (gameObject.tag.Equals("C") && other.tag.Equals("G"))
        {
            DisableSpriteRenderers(other);
            StartCoroutine(WaitForGoodAudio(goodSound, other));
            GlobalVariables.score += 1;
        }
        else if (gameObject.tag.Equals("G") && other.tag.Equals("C"))
        {
            DisableSpriteRenderers(other);
            StartCoroutine(WaitForGoodAudio(goodSound, other));
            GlobalVariables.score += 1;
        }
        else
        {
            Debug.Log("Wrong Letters Collided");
            DisableSpriteRenderers(other);
            StartCoroutine(WaitForBadAudio(badSound));
        }
    }
}
