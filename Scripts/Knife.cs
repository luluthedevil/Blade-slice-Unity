using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Knife : MonoBehaviour
{

    public Rigidbody rigibd;

    public float force = 5f;
    public float torque = 20f;

    private float timeWhenWeStartedFlying;

    private Vector2 startSwipe;
    private Vector2 endSwipe;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!rigibd.isKinematic)
            return;

        // 0 is left mouse button
        // 1 is middle mouse button
        // 2 is right mouse button
        if (Input.GetMouseButtonDown(0))
        {
            startSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            endSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Swipe();
        }
    }

    void Swipe()
    {
        Vector2 swipe = endSwipe - startSwipe;

        rigibd.isKinematic = false;

        timeWhenWeStartedFlying = Time.time;

        rigibd.AddForce((swipe * force), ForceMode.Impulse);
        rigibd.AddTorque(0f, 0f, -torque, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "WoodenBlock")
        {
            rigibd.isKinematic = true;
        }
        else
        {
            Restart();
        }
    }

    void OnCollisionEnter()
    {
        float timeInAir = Time.time - timeWhenWeStartedFlying;

        if (!rigibd.isKinematic && timeInAir >= .06f)
        {
            Restart();
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
