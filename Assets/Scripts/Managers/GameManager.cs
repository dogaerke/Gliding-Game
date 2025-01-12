using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayButtonUI;
    
    public static event Action<GameSession> OnSessionChanged;
    public static GameSession CurrentSession;
    


    private void Start()
    {
        ChangeState(GameSession.Default);
        PlayButtonUI.SetActive(true);

    }

    public void ChangeState(GameSession newSession)
    {
        if (CurrentSession == newSession) return;

        CurrentSession = newSession;
        OnSessionChanged?.Invoke(CurrentSession);

        switch (CurrentSession)
        {
            case GameSession.GamePreparation:
                Debug.Log("Game is in preparation...");
                //RefreshObjectPositions();
                //Nesneler burada düzenlenecek, her level başında daha sonra gameplaysessiona geçilecek coroutine olabilir
                break;

            case GameSession.Gameplay:
                PlayButtonUI.SetActive(false);
                Debug.Log("Game has started!");
                break;

            case GameSession.GameOver:
                Debug.Log("Game over!");
                break;
        }

    }
    
    public void ActivatePreparationSession()
    {
        ChangeState(GameSession.GamePreparation);
        
    }
    public void ActivateGameGameplaySession()
    {
        StartCoroutine(WaitAndActivateGamePlay(0.5f));
        
        
    }
    private IEnumerator WaitAndActivateGamePlay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ChangeState(GameSession.Gameplay);
    
    }
    public void ActivateGameOverSession()
    {
        ChangeState(GameSession.GameOver);
        
    }
}

public enum GameSession
{
    Default,
    GamePreparation,
    Gameplay,
    GameOver
}
