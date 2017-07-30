using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class City : Tile
    {
        public int MaxPower = 100;
        public int CitySpriteFrame;

        public int Power = 100;
        public float PowerDecreaseSeconds = 1;
        public int PowerDecreaseAmount = -1;

        private float _powerTimer;

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
