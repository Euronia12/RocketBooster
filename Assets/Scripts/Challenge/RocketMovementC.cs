using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketMovementC : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private readonly float SPEED = 10f;
    private readonly float ROTATIONSPEED = 0.02f;

    private float highScore = -1;

    public static Action<float> OnHighScoreChanged;
    
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(CheckedHighScore());
    }

    IEnumerator CheckedHighScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (!(highScore < transform.position.y)) 
                continue;
            highScore = transform.position.y;
            OnHighScoreChanged?.Invoke(highScore);
        }

    }

    private void FixedUpdate()
    {
 
    }

    public void ApplyMovement(float inputX)
    {
        Rotate(inputX);       
    }

    public void ApplyBoost(float boosterSpeed)
    {        
        _rb2d.AddForce(transform.up * SPEED * boosterSpeed, ForceMode2D.Impulse);
    }

    private void Rotate(float inputX)
    {
        // 움직임에 따라 회전을 바꿈 -> 회전을 바꾸고 그 방향으로 발사를 해야 그쪽으로 가겠죠?
        float angle = -(float)Mathf.Rad2Deg * inputX;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0,angle), ROTATIONSPEED);
        transform.up = transform.TransformDirection(Vector2.up);
    }

    
}