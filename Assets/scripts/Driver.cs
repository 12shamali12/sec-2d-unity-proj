using Mono.Cecil.Cil;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.UIElements;
using TMPro;
using Unity.VisualScripting;


public class Driver : MonoBehaviour
{
    bool has_package = false;
    float destroy_delay = 0.08f;
    [SerializeField] TMP_Text boost_text;
    [SerializeField] TMP_Text delivery_text, del_text;

    float move_speed = 9.5f;
    float rotate_speed = 170f;
    float boost_speed = 13f;
    float normal_speed = 10f;
    public ParticleSystem cd;
    bool is_boosted = false;
    void Start()
    {
        boost_text.gameObject.SetActive(false);
        GetComponent<ParticleSystem>().Stop();
        cd = GetComponent<ParticleSystem>();
        del_text.gameObject.SetActive(false);
                delivery_text.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        ParticleSystem.MainModule ps = cd.main;

        if (collision.CompareTag("boost") && !is_boosted)
        {   boost_text.gameObject.SetActive(true);
            is_boosted = true;
            move_speed = boost_speed;
            rotate_speed = 200;
            Destroy(collision.gameObject, 0.08f);
            if (!has_package)
            {
                ps.startColor = Color.grey;
                cd.Play();
            }
            else
            {
                ps.startColor = Color.burlywood;
            }
        }

        if (collision.CompareTag("package"))
        {
            if (has_package)
                Debug.Log("already have package");
            else
            {   del_text.gameObject.SetActive(true);
                delivery_text.gameObject.SetActive(true);
                has_package = true;
                if (is_boosted)
                    ps.startColor = Color.burlywood;
                else
                    ps.startColor = Color.lightPink;
                GetComponent<ParticleSystem>().Play();
                Debug.Log("package picked up");
                Destroy(collision.gameObject, destroy_delay);
            }
        }

        if (collision.CompareTag("customer"))
        {
            if (has_package)
            {   del_text.gameObject.SetActive(false);
                delivery_text.gameObject.SetActive(false);
                has_package = false;
                if (is_boosted) { ps.startColor = Color.grey; }
                else { cd.Stop(); }
                Debug.Log("delivered");
                Destroy(collision.gameObject, destroy_delay);
            }
            else
            {
                Debug.Log("NO package to deliver");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   boost_text.gameObject.SetActive(false);
        ParticleSystem.MainModule ps = cd.main;
        is_boosted = false;
        move_speed = normal_speed;
        rotate_speed = 170;
        if (has_package) { ps.startColor = Color.lightPink; }
        else
            cd.Stop();
    }

    void Update()
    {

        float move = 0f;
        float steer = 0f;


        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
        {
            /* if (Keyboard.current.shiftKey.isPressed)
                 move = 1.5f;
             else*/
            move = 1f;
        }
        else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
        {
            /*if (Keyboard.current.shiftKey.isPressed)
                move = -1.25f;
            else*/
            move = -1f;
        }
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            /*if (Keyboard.current.shiftKey.isPressed)
                steer = -1.25f;
            else*/
            steer = -1f;
        }
        else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {/*if (Keyboard.current.shiftKey.isPressed)
                steer = 1.25f;
                else*/
            steer = 1f;
        }

        float steer_amount = steer * rotate_speed * Time.deltaTime;
        float speed_amount = move * move_speed * Time.deltaTime;
        transform.Rotate(0, 0, steer_amount);
        transform.Translate(0, speed_amount, 0);

    }
}
