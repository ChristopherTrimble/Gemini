//Author: Nathan Evans
namespace Interfaces
{
    public interface IDamageable
    {
        float CurrentHealth { get; set; }
        void Damage(float damageDealt);
    }
}

