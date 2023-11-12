using DG.Tweening;
using UnityEngine;
namespace Entity.Nameplate
{
    public class Nameplate : MonoBehaviour
    {
        private Camera _camera;
        private Vector3 _cameraPosition;
        private Renderer _renderer;

        private void Awake()
        {
            _camera = Camera.main;
            _cameraPosition = Vector3.zero;
            _renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            LookAtCamera();
        }

        private void LookAtCamera()
        {
            if (Vector3.Distance(_camera.transform.position, _cameraPosition) < 1e-3)
                return;
            transform.LookAt(_camera.transform);
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
            _cameraPosition = _camera.transform.position;
        }

        public void SetHealthGauge(float value)
        {
            value = Mathf.Clamp01(value);
            transform.DOScale(new Vector3(3f * value, 0.5f, 0.5f), 0.1f);
            _renderer?.material?.DOColor(Color.Lerp(Color.black, Color.red, value), 0.1f);
        }
    }
}