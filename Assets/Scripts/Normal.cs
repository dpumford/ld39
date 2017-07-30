using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Normal : Tile
    {
        void Start()
        {
            base.Start();
        }

        void OnMouseOver()
        {
            if (Input.GetMouseButtonUp(0) && _powerSystem.TotalPower > _powerSystem.RoadCreateCost)
            {
                _powerSystem.AddPower(_powerSystem.RoadCreateCost * -1);

                Road newRoad = ChangeType<Road>(_grid.Tiles);
                newRoad.Speed = _powerSystem.RoadSpeed;
                buildRoadSound.Play();
                newRoad.MaxMeteorHits = _powerSystem.MeteorHits;
                newRoad.MeteorHitsLeft = _powerSystem.MeteorHits;
            }

            base.OnMouseOver();
        }

        void OnMouseExist()
        {
            base.OnMouseExit();
        }

        void Update()
        {
            _renderer.sprite = NormalSprite;

            base.Update();
        }
    }
}
