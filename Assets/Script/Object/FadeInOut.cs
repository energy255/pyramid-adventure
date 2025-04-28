using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInOut : Singleton<FadeInOut>
{
    public Animator animator;
    private string SceneName;


    void Start()
    {
        FadeOut();
    }

    void Update()
    {
        
    }

    public void FadeIn(string sceneName)
    {
        animator.SetTrigger("In");
        SceneName = sceneName;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneName);
    }

    public void FadeOut()
    {
        animator.SetTrigger("Out");
    }
}
