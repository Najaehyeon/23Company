//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BaseController : MonoBehaviour
//{
//    protected Rigidbody2D _rigidbody;

//    [SerializeField] private SpriteRenderer characterRenderer;

//    protected AnimationHandler animationHandler;

//    protected virtual void Awake()
//    {
//        _rigidbody = GetComponent<Rigidbody2D>();
//        animationHandler = GetComponent<AnimationHandler>();
//    }

//    private void Movment(Vector2 direction)
//    {
//        direction = direction * 5;

//        _rigidbody.velocity = direction;
//        animationHandler.Jump(direction);
//    }
//}
