namespace SpaceShooter
{
	public interface IHealth
	{
        int InitialHealth { get; }
		int CurrentHealth { get; }
		bool IsDead { get; }
		void IncreaseHealth( int amount );
		void DecreaseHealth( int amount );
	}
}
