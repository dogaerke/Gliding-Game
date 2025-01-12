using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    
    private float _touchCurrentX; 
    private float _touchMaxX;
    
    public float DirectionX { get; private set; }
    public float MagnitudeX { get; private set; }
    public bool IsTouchActive { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    IsTouchActive = true;
                    
                    _touchCurrentX = touch.position.x;
                    _touchMaxX = touch.position.x;
                    Debug.Log("Dokunma başladı: " + touch.position);

                    break;

                case TouchPhase.Moved:
                    _touchCurrentX = touch.position.x;
                    
                    if (_touchCurrentX > _touchMaxX)
                    {
                        _touchMaxX = _touchCurrentX; // Sağdaki pozisyon güncellenir
                    }
                    
                    DirectionX = Mathf.Sign(_touchCurrentX - _touchMaxX);
                    MagnitudeX = Mathf.Abs(_touchCurrentX - _touchMaxX); 
                    Debug.Log($"Dokunma hareket ediyor. Yön: {DirectionX}, Büyüklük: {MagnitudeX}");
                    break;

                case TouchPhase.Ended:
                    _touchCurrentX = touch.position.x;

                    DirectionX = Mathf.Sign(_touchCurrentX - _touchMaxX);
                    MagnitudeX = Mathf.Abs(_touchCurrentX - _touchMaxX); 
                    
                    Debug.Log($"Dokunma Bitti. Yön: {DirectionX}, Büyüklük: {MagnitudeX}");
                    
                    IsTouchActive = false;
                    break;
                
            }
        }
    }
}
