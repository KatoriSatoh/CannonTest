using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    [SerializeField]
    private Animator blackScreen;
    [SerializeField]
    private GameObject filler;
    [SerializeField]
    private GameObject btnReplay;

    private bool isGameOver = false;
    public bool IsGameOver {
        get {
            return isGameOver;
        }
    }

    public static GameMgr Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		else
		{
			Instance = this;
		}
    }

    private void Start()
    {
        blackScreen.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        StartCoroutine(EndGame());
    }

    public void ReloadScene()
    {
        blackScreen.SetTrigger("reload");
    }

    IEnumerator EndGame()
    {
        isGameOver = true;
        yield return new WaitForSeconds(1);
        filler.SetActive(true);
        btnReplay.SetActive(true);
    }
}
