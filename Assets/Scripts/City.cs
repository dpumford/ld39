using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class City : Tile
    {
        public int Power = 100;
        public float PowerDecreaseSeconds = 10;

        private float _powerTimer;

        void Update()
        {
            _renderer.sprite = CitySprite;

            if (Power <= 0)
            {
                ChangeType<Normal>(_grid.Tiles);
            }

            if (_powerTimer > PowerDecreaseSeconds)
            {
                AddPower(-1 * _powerSystem.PowerConsumeAmount);

                _powerTimer = 0;
            }
            else
            {
                _powerTimer += Time.deltaTime;
            }
        }

        public void AddPower(int amount)
        {
            Power += amount;
        }
    }
}
