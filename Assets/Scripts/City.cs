using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class City : Tile
    {
        public int MaxPower = 50000;
        public int CitySpriteFrame;

        public int Power = 35000;
        public float PowerDecreaseSeconds = .001f;
        public int PowerDecreaseAmount = -1;

        private float _powerTimer;

        void Start()
        {
            base.Start();
            Power = (int)UnityEngine.Random.Range(20000f, 45000f);
            PowerDecreaseSeconds = UnityEngine.Random.Range(.001f, .01f);
            PowerDecreaseAmount = -(int)UnityEngine.Random.Range(5f, 10f);
        }

        void Update()
        {
            _renderer.sprite = CitySprite[CitySpriteFrame];

            if (Power <= 0)
            {
                ChangeType<Normal>(_grid.Tiles);
            }

            if (_powerTimer > PowerDecreaseSeconds)
            {
                AddPower(PowerDecreaseAmount);

                _powerTimer = 0;
            }
            else
            {
                _powerTimer += Time.deltaTime;
            }
        }

        public void AddPower(int amount)
        {
            if (Power <= MaxPower)
            {
                Power = Math.Min(MaxPower, Power + amount);
            }
        }
    }
}
