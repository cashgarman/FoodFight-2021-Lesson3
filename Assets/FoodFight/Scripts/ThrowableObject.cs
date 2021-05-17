using System.Collections.Generic;
using UnityEngine;

namespace FoodFight.Scripts
{
    public class ThrowableObject : GrabbableObject
    {
        protected FixedJoint _joint;
        private Queue<Vector3> _previousVelocities = new Queue<Vector3>();
        private Vector3 _prevPosition;
        private int _numVelocitySamples = 10;

        [SerializeField] private float _throwBoost;

        public override void OnGrabbed(Grabber hand)
        {
            // Add a fixed joint between this object's rigid body and the hand's rigid body
            _joint = gameObject.AddComponent<FixedJoint>();
            _joint.connectedBody = hand.GetComponent<Rigidbody>();
        }
        
        public override void OnDropped()
        {
            // Remove the fixed joint
            Destroy(_joint);

            // Calculate the average velocity from all the velocity samples
            var averageVelocity = Vector3.zero;
            foreach(var velocity in _previousVelocities)
            {
                averageVelocity += velocity;
            }
            averageVelocity /= _previousVelocities.Count;

            // Apply the calculated averaged velocity to the rigid body to throw it (with an optional boost)
            GetComponent<Rigidbody>().velocity = averageVelocity * _throwBoost;
        }
        
        private void FixedUpdate()  // Note: Fixed update so this is framerate independent
        {
            // Calculate the velocity of the object since the last update
            var velocity = transform.position - _prevPosition;

            // Remember the position from this update so we can use it to calculate the velocity in the next update
            _prevPosition = transform.position;

            // Add this calculated velocity to the list of previous velocities
            _previousVelocities.Enqueue(velocity);

            // Make sure we don't store too many previous velocity samples
            if(_previousVelocities.Count > _numVelocitySamples)
            {
                // Toss out the oldest sample
                _previousVelocities.Dequeue();
            }
        }
    }
}
