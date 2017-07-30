﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Generator : Tile
    {
        public float PowerIncreaseSeconds = 1;

        private float _powerTimer;

        private int _powerToSend;

        void Update()
        {
            _renderer.sprite = GeneratorSprite;

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
            foreach (var path in _grid.ValidConnections)
            {
                List<Tile> cityPath = _grid.ValidConnections.First(connection => connection.Find(tile => tile.CityIndex == CityIndex));
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
}
