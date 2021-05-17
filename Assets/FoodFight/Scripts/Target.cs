using System;
using UnityEngine;

namespace FoodFight.Scripts
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _moveAmount;
        
        private float _startingXPosition;
        public FoodfightGame _game;

        private void Awake()
        {
            _startingXPosition = transform.position.x;
        }

        void Update()
        {
            // Move the target back and forth
            var newPosition = transform.position;
            newPosition.x = _startingXPosition + Mathf.Sin(Time.time * _moveSpeed) * _moveAmount;
            transform.position = newPosition;
        }

        private void OnCollisionEnter(Collision other)
        {
            // If the target was hit by a foodstuff
            var foodstuff = other.gameObject.GetComponent<Foodstuff>();
            if (foodstuff != null)
            {
                // Destroy the foodstuff and the target
                Destroy(foodstuff.gameObject);
                Destroy(gameObject);
                
                // Let the game know a target was hit
                _game.OnTargetHit();
            }
        }
    }
}
