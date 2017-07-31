using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Normal : Tile
    {
        public float MeteorSurvivePercent = 0.3f;

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
                newRoad.MaxMeteorHits = _powerSystem.MeteorHits;
                newRoad.MeteorHitsLeft = _powerSystem.MeteorHits;

                SoundPlayer.PlayOneShot(buildRoadClip);
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

        public override void MeteorHit()
        {
            base.MeteorHit();

            if (Random.Range(0f, 1f) > MeteorSurvivePercent)
            {
                ChangeType<FallenMeteor>(_grid.Tiles);
            }
        }
    }
}
