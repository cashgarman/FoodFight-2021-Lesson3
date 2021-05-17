using UnityEngine;

namespace FoodFight.Scripts
{
    public class Foodstuff : ThrowableObject
    {
        public FoodfightGame _game;
        [SerializeField] public Foodstuff _prefab;

        public override void OnHoverStart()
        {
            // No hover effects
        }
        
        public override void OnHoverEnd()
        {
            // No hover effects
        }

        public override void OnGrabbed(Grabber hand)
        {
            // Let the game know the food was grabber
            _game.OnFoodstuffGrabbed();
            
            base.OnGrabbed(hand);
        }
    }
}
