namespace DZ_11
{
    public interface IHealable
    {
        float CurrentHealth { get; }
        float MaxHealth { get; }

        void Heal(float additiveHealth);
    }
}