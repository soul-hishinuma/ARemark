using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main2Post : MonoBehaviour
{
    public void OnClick()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        var async = SceneManager.LoadSceneAsync("PostScene");

        async.allowSceneActivation = false;
        yield return new WaitForSeconds(0);
        async.allowSceneActivation = true;
    }
}
