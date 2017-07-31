using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class FallenMeteor : Tile
    {
        void Start()
        {
            base.Start();
        }

        void OnMouseOver()
        {
            base.OnMouseOver();

            if (Input.GetMouseButtonUp(1))
            {
                _powerSystem.AddPower(_powerSystem.FallenMeteorPower);
                ChangeType<Normal>(_grid.Tiles);
            }
        }

        void OnMouseExit()
        {
            base.OnMouseExit();
        }

        void Update()
        {
            _renderer.sprite = FallenMeteorSprite;

            base.Update();
        }
    }
}
