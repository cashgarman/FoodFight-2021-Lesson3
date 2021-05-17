using UnityEngine;
using Random = UnityEngine.Random;

namespace FoodFight.Scripts
{
	public class FoodfightGame : MonoBehaviour
	{
		[SerializeField] private Target _targetPrefab;
		[SerializeField] private BoxCollider _spawnArea;
		[SerializeField] private float _respawnDelay = 2f;
		[SerializeField] private Foodstuff[] _foodstuffPrefabs;

		private Foodstuff _nextFoodstuff;
		private Vector3 _nextFoodstuffPosition;
		private Quaternion _nextFoodstuffRotation;

		private void Start()
		{
			// Spawn the first target
			SpawnTarget();
			
			// Spawn the first food
			SpawnFood();
		}

		public void OnTargetHit()
		{
			// Spawn a target
			SpawnTarget();
		}

		private void SpawnFood()
		{
			// Get a random food prefab
			var randomFoodPrefab = _foodstuffPrefabs[Random.Range(0, _foodstuffPrefabs.Length)];
			
			// Spawn the food in the world
			var newFood = Instantiate(randomFoodPrefab, transform.position, transform.rotation);
			
			// Let the food know about the game
			newFood._game = this;
		}

		public void OnFoodstuffGrabbed()
		{
			// Respawn the food after a delay
			Invoke(nameof(SpawnFood), _respawnDelay);
		}

		private void SpawnTarget()
		{
			// Spawn a new target
			var newTarget = Instantiate(_targetPrefab, GetRandomTargetPosition(), _targetPrefab.transform.rotation);
			
			// Let the new target know about this game object so it can communicate
			newTarget._game = this;
		}

		private Vector3 GetRandomTargetPosition()
		{
			// Return a random position inside the spawn area
			return new Vector3(Random.Range(_spawnArea.bounds.min.x, _spawnArea.bounds.max.x),
				Random.Range(_spawnArea.bounds.min.y, _spawnArea.bounds.max.y),
				Random.Range(_spawnArea.bounds.min.z, _spawnArea.bounds.max.z));
		}
	}
}