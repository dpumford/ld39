using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Generator : Tile
    {
        public float PowerIncreaseSeconds = 1;
        public float NextFrameSeconds = 0.1f;

        private float _powerTimer;

        private int _powerToSend;

        private int _animationFrame;
        private float _animationTimer;

        void Start()
        {
            base.Start();
        }

        void OnMouseOver()
        {
            base.OnMouseOver();
        }

        void OnMouseExit()
        {
            base.OnMouseExit();
        }

        void Update()
        {
            _renderer.sprite = GeneratorSprite[_animationFrame];

            if (Input.GetKeyUp(KeyCode.P))
            {
                foreach (var path in _grid.ValidConnections)
                {
                    if (_powerSystem.TotalPower > _powerSystem.PowerCreateCost)
                    {
                        var newPower = Instantiate(PowerPrefab, transform.position, Quaternion.identity);
                        newPower.Path = path;

                        _powerSystem.AddPower(-1 * _powerSystem.PowerCreateCost);
                    }
                }
            }

            if (_animationTimer > NextFrameSeconds)
            {
                _animationFrame = (_animationFrame + 1) % GeneratorSprite.Length;
                _animationTimer = 0;
            }
            else
            {
                _animationTimer += Time.deltaTime;
            }

            if (_powerTimer > PowerIncreaseSeconds)
            {
                _powerSystem.AddPower(_powerSystem.PowerGenerateAmount);
                _powerTimer = 0;
            }
            else
            {
                _powerTimer += Time.deltaTime;
            }
        }

        public void SetPowerToSend(int PowerAmount)
        {
            _powerToSend = PowerAmount;
        }

        public void SendPower(int CityIndex)
        {
            List<Tile> cityPath = _grid.ValidConnections.First(connection => connection.ElementAt(0).CityIndex == CityIndex);
            if (_powerSystem.TotalPower >= _powerToSend)
            {
                var newPower = Instantiate(PowerPrefab, transform.position, Quaternion.identity);
                newPower.Path = cityPath;
                newPower.CarriedPower = _powerToSend;
                _powerSystem.AddPower(-1 * _powerToSend);
            }
        }
    }
}
