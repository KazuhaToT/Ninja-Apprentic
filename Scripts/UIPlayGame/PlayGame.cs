using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    [SerializeField] protected GameObject options;

    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        options.SetActive(false);
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    public void OnButtonClickPlay()
    {
        SceneManager.LoadScene("Home 1");
    }
    public void OnButtonClickBack()
    {
        Application.Quit();
    }
    public void OnButtonClickOption()
    {
        options.SetActive(true);
    }
    public void OnButtonClickContinue()
    {
        options.SetActive(false);
    }

}
