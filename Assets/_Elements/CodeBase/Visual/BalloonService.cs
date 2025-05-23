using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace _Elements.CodeBase.Visual
{
    public class BalloonService
    {
        private readonly GameObject[] _balloonPrefab = Resources.LoadAll<GameObject>("Prefab/Balloon");
        private readonly Transform _container;
        private readonly ObjectPool<GameObject> _balloonPool;
        
        private readonly List<GameObject> _activeBalloons = new List<GameObject>();
        private readonly float _spawnDelay = 2f;
        private readonly float _screenWidth;
        private readonly float _screenHeight;
        
        private const int MaxBalloons = 3;

        public BalloonService(Transform container)
        {
            _container = container;
            _balloonPool = new ObjectPool<GameObject>(CreateBalloon, OnTakeFromPool, OnReturnedToPool);
            
            var mainCam = Camera.main;
            var screenBottomLeft = mainCam.ViewportToWorldPoint(new Vector3(0, 0, mainCam.nearClipPlane));
            var screenTopRight = mainCam.ViewportToWorldPoint(new Vector3(1, 1, mainCam.nearClipPlane));
            
            _screenWidth = screenTopRight.x - screenBottomLeft.x;
            _screenHeight = screenTopRight.y - screenBottomLeft.y;
        }

        public async UniTask SpawnBalloons()
        {
            while (true)
            {
                await UniTask.Delay((int)(_spawnDelay * 1000));
                
                if (_activeBalloons.Count >= MaxBalloons) continue;
                
                var balloon = _balloonPool.Get();
                SetupBalloonMovement(balloon);
            }
        }

        private void SetupBalloonMovement(GameObject balloon)
        {
            float speed = Random.Range(0.5f, 2f);
            float amplitude = Random.Range(0.5f, 2f);
            float frequency = Random.Range(0.5f, 1.5f);
            float startX = Random.Range(-_screenWidth/2, _screenWidth/2);
            float startY = Random.Range(-_screenHeight/4, _screenHeight/4);
            
            bool startFromLeft = Random.value > 0.5f;
            float startPosX = startFromLeft ? -_screenWidth/2 - 2 : _screenWidth/2 + 2;
            
            balloon.transform.position = new Vector3(startPosX, startY, 0);
            
            MoveBalloonAsync(balloon, speed, amplitude, frequency, startFromLeft).Forget();
        }

        private async UniTaskVoid MoveBalloonAsync(GameObject balloon, float speed, float amplitude, float frequency, bool moveRight)
        {
            float startTime = Time.time;
            float startX = balloon.transform.position.x;
            float startY = balloon.transform.position.y;
            
            while (balloon != null && balloon.activeInHierarchy)
            {
                if ((moveRight && balloon.transform.position.x > _screenWidth/2 + 2) ||
                    (!moveRight && balloon.transform.position.x < -_screenWidth/2 - 2))
                {
                    _balloonPool.Release(balloon);
                    break;
                }
                
                float t = (Time.time - startTime) * speed;
                float direction = moveRight ? 1 : -1;
                float x = startX + t * direction;
                float y = startY + Mathf.Sin(t * frequency) * amplitude;
                
                balloon.transform.position = new Vector3(x, y, 0);
                
                await UniTask.Yield();
            }
        }

        private GameObject CreateBalloon()
        {
            var balloon = Object.Instantiate(_balloonPrefab[Random.Range(0, _balloonPrefab.Length)], _container);
            return balloon;
        }

        private void OnTakeFromPool(GameObject balloon)
        {
            balloon.SetActive(true);
            _activeBalloons.Add(balloon);
        }

        private void OnReturnedToPool(GameObject balloon)
        {
            balloon.SetActive(false);
            _activeBalloons.Remove(balloon);
        }
    }
}