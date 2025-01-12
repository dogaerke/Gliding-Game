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
    
    private float _deltaX;
    private float _normalizedX;
    private bool _isPressed;
    private static readonly int IsReleased = Animator.StringToHash("IsReleased");


    private void Start()
    {
        InputManager.OnInputStart += HandleInputStart;
        InputManager.OnInputEnd += HandleInputEnd;
    }

    private void OnDestroy()
    {
        InputManager.OnInputStart -= HandleInputStart;
        InputManager.OnInputEnd -= HandleInputEnd;

    }

    private void Update()
    {
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
    private void HandleInputStart(Vector2 touchPos)
    {
        _isPressed = true;
        animator.SetBool(IsReleased, false);

    }
    
    private void HandleInputEnd(Vector2 touchPos)
    {
        _isPressed = false;
        animator.SetBool(IsReleased, true);
        animator.CrossFadeInFixedTime("Armature_Release_Stick", 0.25f, 0);
        
    }
    

}
