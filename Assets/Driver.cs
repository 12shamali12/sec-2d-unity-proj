using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Driver : MonoBehaviour
{
    [SerializeField] float move_speed;
    [SerializeField] float rotate_speed;
   void Start()
    {
        rotate_speed = 170f;
        move_speed = 10f;
     Debug.Log("Hello World");   
    }

    void Update(){

        float move = 0f;
        float steer = 0f;
        
        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
        {
            move = 1f;
        }
        else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
        {
            move = -1f;
        }
       
       
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            steer = -1f; 
        }
        else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            steer = 1f;
        }
        float steer_amount = steer * rotate_speed * Time.deltaTime;
        float speed_amount = move * move_speed * Time.deltaTime;
        transform.Rotate(0, 0, steer_amount);
        transform.Translate(0, speed_amount, 0);

    }
}
