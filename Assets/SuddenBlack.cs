using UnityEngine;
using UnityEngine.SceneManagement;

public class SuddenBlack : MonoBehaviour
{
    public GameObject black_image;
    public AudioSource audio_source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void JokeEnd(){
        black_image.SetActive(true);
        audio_source.Play();
        Invoke("SwitchTheScene", 3.0f);
    }

    public void SwitchTheScene(){
        SceneManager.LoadScene("JokeEnd");
    }


}
