using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public TextMeshProUGUI Score;
    public AudioSource audioSource;
    public AudioClip victoryNoise;

    private void Start()
    {
        GlobalVariables.score = 0;
        GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().PlayMusic();

    }

    private void Update()
    {
        Score.text = GlobalVariables.score.ToString();

        if (GlobalVariables.score >= 35)
        {
            StartCoroutine(WaitForGoodAudio());
        }
    }

    private IEnumerator WaitForGoodAudio()
    {
        audioSource.PlayOneShot(victoryNoise);

        yield return new WaitForSeconds(victoryNoise.length);

        // the following will happen when the audioSource has finished playing
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
