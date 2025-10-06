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
    }

    void Update(){

        float move = 0f;
        float steer = 0f;



        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
        {
            if (Keyboard.current.shiftKey.isPressed)
                move = 1.5f;

            else
                move = 1f;
            
            
        }
        else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
        {
            if (Keyboard.current.shiftKey.isPressed)
                move = -1.25f;

            else
                move = -1f;
        }
       
       if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
       {
           if (Keyboard.current.shiftKey.isPressed)
               steer = -1.25f;
           else
               steer = -1f;
       }
       else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {if (Keyboard.current.shiftKey.isPressed)
                steer = 1.25f;
                else
                steer = 1f;
        }

        float steer_amount = steer * rotate_speed * Time.deltaTime;
        float speed_amount = move * move_speed * Time.deltaTime;
        transform.Rotate(0, 0, steer_amount);
        transform.Translate(0, speed_amount, 0);

    }
}
