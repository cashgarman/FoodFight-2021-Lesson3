using UnityEngine;

namespace FoodFight.Scripts
{
	public class Grabber : MonoBehaviour
	{
		[SerializeField] private string _grabButtonName;
		[SerializeField] private string _triggerButtonName;

		private GrabbableObject _hoveredObject;
		private GrabbableObject _grabbedObject;
		private Animator _animator;

		private void Awake()
		{
			// Store the animator
			_animator = GetComponent<Animator>();
		}
		
		private void OnTriggerEnter(Collider other)
		{
			// Check if an object was touched
			var grabbable = other.GetComponent<GrabbableObject>();
			if (grabbable != null)
			{
				// Hover over the new object
				_hoveredObject = grabbable;
				_hoveredObject.OnHoverStart();
			}
		}

		private void OnTriggerExit(Collider other)
		{
			// If the object was the object we were hovering over
			var grabbable = other.GetComponent<GrabbableObject>();
			if (grabbable != null && grabbable == _hoveredObject)
			{
				// Unhover the old object
				_hoveredObject.OnHoverEnd();
				
				// No longer hovering over an object
				_hoveredObject = null;
			}
		}

		private void Update()
		{
			// If the grab trigger is pressed
			if (Input.GetButtonDown(_grabButtonName))
			{
				// Play the grip animation
				_animator.SetBool("Gripped", true);
				
				// If an object is being hovered over
				if (_hoveredObject != null)
				{
					// Let the object know it has been grabbed
					_hoveredObject.OnGrabbed(this);
					_grabbedObject = _hoveredObject;
					_hoveredObject = null;
				}
			}
			
			// If the grab trigger was released
			if (Input.GetButtonUp(_grabButtonName))
			{
				// Play the grip animation
				_animator.SetBool("Gripped", false);
				
				// If an object is being held
				if (_grabbedObject != null)
				{
					// Let the object know it has been dropped
					_grabbedObject.OnDropped();
					_grabbedObject = null;
				}
			}
			
			// Handle triggering any held object
			if (Input.GetButtonDown(_triggerButtonName) && _grabbedObject != null)
			{
				// Let the object know it has started being triggered
				_grabbedObject.OnTriggerStart();
			}
			if (Input.GetButtonUp(_triggerButtonName) && _grabbedObject != null)
			{
				// Let the object know it has stopped being triggered
				_grabbedObject.OnTriggerStop();
			}
		}
	}
}
