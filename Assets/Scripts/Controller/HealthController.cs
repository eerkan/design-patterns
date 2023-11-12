using Entity.Nameplate;
using Features;
using Observer;
using UnityEngine;
namespace Controller
{
    public sealed class HealthController : IHealtable
    {
        public readonly ReactiveProperty<float> CurrentHealth = new();
        private readonly IKillable _killable;
        private readonly Nameplate _nameplate;

        public HealthController(IKillable killable, Nameplate nameplate)
        {
            _killable = killable;
            _nameplate = nameplate;
            
            CurrentHealth.RegisterObserver(
                _ => true, 
                new Observer<float>(_ => UpdateNameplate())
            );
            CurrentHealth.RegisterObserver(
                health => Mathf.Approximately(health, 0f), 
                new Observer<float>(_ => _killable?.Kill())
            );
            
            Reset();
        }

        private void UpdateNameplate()
        {
            _nameplate.SetHealthGauge(CurrentHealth.Value);
        }
        
        public void Heal(float amount)
        {
            CurrentHealth.Value = Mathf.Clamp01(CurrentHealth.Value + amount);
        }

        public void Damage(float amount)
        {
            CurrentHealth.Value = Mathf.Clamp01(CurrentHealth.Value - amount);
        }

        public void Reset()
        {
            CurrentHealth.Value = 1f;
        }
    }
}