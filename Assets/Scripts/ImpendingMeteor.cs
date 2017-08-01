using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class ImpendingMeteor : MonoBehaviour
    {
        public Sprite[] ImpendingMeteorSprites;
        public int CurrentSprite = 0;
        public float ImpendingMeteorAnimationSeconds = 0.1f;

        public float SecondsToImpact = 30f;
        public Tile Target;

        private float _impactTimer;
        private float _impendingMeteorAnimationTimer;
        private SpriteRenderer _renderer;
        private UiController _uiController;

        void Start()
        {
            _uiController = GameObject.FindObjectOfType<UiController>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (_impendingMeteorAnimationTimer > ImpendingMeteorAnimationSeconds)
            {
                CurrentSprite = (CurrentSprite + 1) % ImpendingMeteorSprites.Length;

                _impendingMeteorAnimationTimer = 0f;
            }
            else
            {
                _impendingMeteorAnimationTimer += Time.deltaTime;
            }

            _renderer.sprite = ImpendingMeteorSprites[CurrentSprite % ImpendingMeteorSprites.Length];

            if (_impactTimer > SecondsToImpact)
            {
                _uiController.DecrementLiveMeteors();
                Target.MeteorHit();
                Destroy(gameObject);
            }
            else
            {
                _impactTimer += Time.deltaTime;
            }
        }
    }
}
