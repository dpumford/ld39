using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Road : Tile
    {
        public bool Enabled = true;

        void Start()
        {
            base.Start();
        }

        void OnMouseOver()
        {
            if (Input.GetMouseButtonUp(1) && _powerSystem.TotalPower > _powerSystem.RoadDestroyCost)
            {
                _powerSystem.AddPower(_powerSystem.RoadDestroyCost * -1);
                ChangeType<Normal>(_grid.Tiles);
            }

            base.OnMouseOver();
        }

        void OnMouseExit()
        {
            base.OnMouseExit();
        }

        void Update()
        {
            Enabled = _grid.IsTileConnected(this);

            _renderer.sprite = Enabled ? EnabledRoad : DisabledRoad;

            base.Update();
        }
    }
}
