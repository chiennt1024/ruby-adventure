using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] bool vertical;
    [SerializeField] float changeTime = 3f;
    [SerializeField] ParticleSystem smokeEffect;
    Rigidbody2D rigidbody2D;
    Animator animator;
    float timer;
    int direction = 1;
    bool broken = true;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(!broken) {
            return;
        }
        timer -= Time.deltaTime;
        if(timer < 0) {
            direction = -direction;
            timer = changeTime;
        }
    }
    void FixedUpdate() {
        if(!broken) {
            return;
        }
        Vector2 position = rigidbody2D.position;
        if(vertical) {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        } else {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        rigidbody2D.MovePosition(position);
    }
    public void Fix() {
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
    }
    void OnCollisionEnter2D(Collision2D other) {
        RubyController player = other.gameObject.GetComponent<RubyController>();
        if(player != null) {
            player.ChangeHealth(-1);
        }
    }
}
