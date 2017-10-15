using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
	[RequireComponent(typeof(IHealth))]
	public abstract class SpaceShipBase : MonoBehaviour, IDamageReceiver
	{
		public enum Type
		{
			Player,
			Enemy
		}

		// Backing field for the property Speed.
		// SerializeField attribute forces Unity to serialize this variable
		// in order to make it editable inside the editor.
		[SerializeField]
		private float _speed = 1.5f;
        [SerializeField]
        private int _lives = 1;

        private Weapon[] _weapons;

		// A property for accessing _speed variable. Getter is set public so the value
		// can be read anywhere. Setter is protected which means that the value can be
		// set only from this class and from any class derived from this class.
		public float Speed
		{
			get { return _speed; }
			protected set { _speed = value; }
		}

		public Weapon[] Weapons
		{
			get { return _weapons; }
		}

		// An autoproperty. Backing fields are generated automatically by the 
		// compiler.
		public IHealth Health { get; protected set; }

		public abstract Type UnitType { get; }

		protected virtual void Awake()
		{
			// GetComponentsInChildren returns all the components of type T 
			// (Weapon in this case) from the GameObject's child hierarchy.
			// If the parameter 'includeInactive' is set true, the components are fetched
			// form inactive child GameObjects too.
			_weapons = GetComponentsInChildren<Weapon>(includeInactive: true);
			foreach(Weapon weapon in _weapons)
			{
				// Initialize all the weapons.
				weapon.Init(this);
			}

			Health = GetComponent<IHealth>();
		}

		protected void Shoot()
		{
			// Go through every weapon which is stored to the Weapons array and call their
			// Shoot methods.
			foreach (Weapon weapon in Weapons)
			{
				weapon.Shoot();
			}
		}

		// Abstract methods don't have their implementations defined in this class.
		// This also makes the whole class abstract. This means no objects can be 
		// instantiated from this class. The method has to be defined in the class
		// which is derived from this class. Objects can be instantiated from derived
		// non-abstract classes.
		protected abstract void Move();

		protected virtual void Update()
		{
			try
			{
				Move();
			}
			catch (System.NotImplementedException exception)
			{
				Debug.Log(exception.Message);
			}
			catch (System.Exception exception)
			{
				Debug.LogException(exception);
			}
		}

		public void TakeDamage(int amount)
		{
			Health.DecreaseHealth(amount);

			if (Health.IsDead)
			{
				Die();
			}
		}

		protected virtual void Die()
		{
			Destroy(gameObject);
		}

		protected Projectile GetPooledProjectile()
		{
			return LevelContoller.Current.GetProjectile(UnitType);
		}

		protected bool ReturnPooledProjectile(Projectile projectile)
		{
			return LevelContoller.Current.ReturnProjectile(UnitType, projectile);
		}
	}
}
