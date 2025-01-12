using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    public static Vector2 TouchCurrent{ get; private set; } 
    public static Vector2 TouchStart{ get; private set; }
    public static Vector2 TouchEnd{ get; private set; }
    
    
    public static event Action<Vector2> OnInputStart;
    public static event Action<Vector2> OnInputEnd;
    

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    TouchStart = touch.position;
                    TouchCurrent = touch.position;
                    OnInputStart?.Invoke(TouchStart);

                    //Debug.Log("Dokunma başladı: " + touch.position);
        
                    break;
        
                case TouchPhase.Moved:
                    TouchCurrent = touch.position;
                    
                    if (TouchCurrent.x > TouchStart.x)
                    {
                        TouchStart = TouchCurrent;
                    }
                    
                    // Direction = Mathf.Sign(TouchCurrent.x - TouchMax.x);
                    // Magnitude = Mathf.Abs(TouchCurrent.x - TouchMax.x) / Screen.width; 
                    //Debug.Log($"Dokunma hareket ediyor. Yön: {Direction}, Büyüklük: {Magnitude}");
                    break;
        
                case TouchPhase.Ended:
                    TouchEnd = touch.position;
                    OnInputEnd?.Invoke(TouchEnd);
                    
                    //Direction = Mathf.Sign(TouchCurrent - TouchMax);
                    //Magnitude = Mathf.Abs(TouchCurrent - TouchMax); 
                    
                    //Debug.Log($"Dokunma Bitti. Yön: {Direction}, Büyüklük: {Magnitude}");
                    
                    break;

            }
        }
    }
}
