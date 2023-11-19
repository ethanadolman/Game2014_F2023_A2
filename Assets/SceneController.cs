using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private AudioSource select;
    public void LoadSceneByName(string name)
    {
        StartCoroutine(LoadScene(name));
    }

    IEnumerator LoadScene(string name)
    {
        select.Play();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(name);
    }

}
