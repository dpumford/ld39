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
        public int MeteorHitsLeft = 3;
        public int MaxMeteorHits = 3;
        public float Speed = 16f;

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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("collision detected");
            collision.gameObject.GetComponent<Power>().Speed = Speed;
        }

        void Update()
        {
            Enabled = _grid.IsTileConnected(this);

            _renderer.sprite = Enabled ? EnabledRoad : DisabledRoad;

            base.Update();

            var meteorDamagePercent = (MeteorHitsLeft + 1) * 1f / (MaxMeteorHits + 1);
            _renderer.color = new Color(_renderer.color.r * meteorDamagePercent, _renderer.color.g * meteorDamagePercent, _renderer.color.b * meteorDamagePercent);
        }

        public void MeteorHit()
        {
            SoundPlayer.PlayOneShot(meteorImpact);
            if (MeteorHitsLeft <= 0)
            {
                ChangeType<Normal>(_grid.Tiles);
            }

            MeteorHitsLeft--;
        }
    }
}
