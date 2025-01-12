using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickAnimationController : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator animator;
    
    [Header("Options")]
    [SerializeField] private float sensitivity = 2f;
    
    private static readonly int IsReleased = Animator.StringToHash("IsReleased");
    private float _deltaX;
    private float _normalizedX;
    private bool _isPressed;


    private void Start()
    {
        GameManager.OnSessionChanged += HandleGameSession;
    }


    private void OnDestroy()
    {
        GameManager.OnSessionChanged -= HandleGameSession;
    }
    

    private void Update()
    {
        if (GameManager.CurrentSession != GameSession.Gameplay)return;
        
        Vector2 touchStart = InputManager.TouchStart;
        Vector2 touchCurrent = InputManager.TouchCurrent;

        _deltaX = Mathf.Abs(touchStart.x - touchCurrent.x) * sensitivity / Screen.width;
        _normalizedX = Mathf.Clamp01(_deltaX);
        
        Debug.Log(_normalizedX);
        if (_isPressed)
        {
            animator.Play("Armature_Bend_Stick", 0, _normalizedX);
        }

    }
    
    private void HandleGameSession(GameSession currentSession)
    {
        switch (currentSession)
        {
            case GameSession.Gameplay:
                InputManager.OnInputStart += HandleInputStart;
                InputManager.OnInputEnd += HandleInputEnd;
                Debug.Log("Gameplay");
                break;
            case GameSession.GameOver:
                InputManager.OnInputStart -= HandleInputStart;
                InputManager.OnInputEnd -= HandleInputEnd;
                break;
        }
    }
    
    private void HandleInputStart(Vector2 touchPos)
    {
        _isPressed = true;
        animator.SetBool(IsReleased, false);

    }
    
    private void HandleInputEnd(Vector2 touchPos)
    {
        _isPressed = false;
        animator.SetBool(IsReleased, true);

        animator.speed = Mathf.Lerp(0.5f, 1.5f, _normalizedX);
        animator.CrossFade("Armature_Release_Stick", 0.2f, 0);
        
    }
    

}
