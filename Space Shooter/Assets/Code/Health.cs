using UnityEngine;

namespace SpaceShooter
{
	public class Health : MonoBehaviour, IHealth
	{
		[SerializeField] private int _initialHealth;
		[SerializeField] private int _minHealth;
		[SerializeField] private int _maxHealth;

		private int _currentHealth;

		public int CurrentHealth
		{
			get
			{
				return _currentHealth;
			}
			private set
			{
				_currentHealth = Mathf.Clamp(value, _minHealth, _maxHealth);
			}
		}

		public bool IsDead
		{
			get { return CurrentHealth == _minHealth; }
		}

		protected void Awake()
		{
			CurrentHealth = _initialHealth;
		}

		public void DecreaseHealth(int amount)
		{
			// CurrentHealth = CurrentHealth - amount;
			CurrentHealth -= amount;
            if(CurrentHealth <= _minHealth)
            {
                PlayerDeath();
            }
		}

		public void IncreaseHealth(int amount)
		{
			CurrentHealth += amount;
		}
	}
}
