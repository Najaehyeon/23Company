using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected AnimationHandler animationHandler;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
    }

    protected virtual void FixedUpdate()
    {
        Movment(movementDirection);

    }

    protected virtual void HandleAction()
    {

    }

    private void Movment(Vector2 direction)
    {
        direction = direction * 5;

        _rigidbody.velocity = direction;
        animationHandler.Jump(direction);
    }
}
