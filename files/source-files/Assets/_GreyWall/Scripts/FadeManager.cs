using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    // Componentes;
    private MenuManager _menuManager = null;
    private Animator _anim = null;

    // Variaveis;
    [SerializeField]
    private int _nextSceneIndex = 0;

    private void Start()
    {
        _menuManager = GetComponentInParent<MenuManager>();
        _anim = GetComponent<Animator>();
    }
    
    public void FadeStart()
    {
        _anim.SetTrigger("FadeOut");
        StartCoroutine(FadeComplete());
    }
    
    IEnumerator FadeComplete()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(_nextSceneIndex);
    }

    public void FadeToDie()
    {
        StartCoroutine(DeathRoutine());
    }

    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(3);
        _anim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(_nextSceneIndex);
    }
}
