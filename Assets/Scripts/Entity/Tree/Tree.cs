using System;
using System.Threading;
using Controller;
using DG.Tweening;
using Features;
using ObjectPool;
using Strategy;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;
namespace Entity.Tree
{
    public sealed class Tree : GenericPoolableObject<Tree>, IKillable
    {
        [SerializeField] private Nameplate.Nameplate _nameplate;
        
        private HealthController _healthController;
        private bool _isKilled = true;
        
        public ITreeSpawnStrategy SpawnStrategy { get; set; }

        private void Awake()
        {
            gameObject.name = "Tree#" + RandomName();
            _healthController = new HealthController(this, _nameplate);
        }

        private string RandomName()
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(2));
            return Guid.NewGuid().ToString();
        }

        public void Heal(float amount)
        {
            if (_isKilled) return;
            _healthController.Heal(amount);
        }

        public void Damage(float amount)
        {
            if (_isKilled) return;
            _healthController.Damage(amount);
        }
        
        public void Kill()
        {
            _isKilled = true;
            transform
                .DOScale(Vector3.zero, 0.3f)
                .OnComplete(() => SpawnStrategy.Destroy(this));
        }

        public void Reset()
        {
            _isKilled = true;
            transform.localScale = Vector3.zero;
            _healthController.Reset();
            transform
                .DOScale(Vector3.one, 0.3f)
                .OnComplete(() =>
                {
                    _isKilled = false;
                });
        }

        public override void OnGetPool(IObjectPool<Tree> pool)
        {
            base.OnGetPool(pool);
            Reset();
        }
    }
}