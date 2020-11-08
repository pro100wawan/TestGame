using UnityEngine;
using UnityEditor;

namespace Assets.MyGame.Scripts.Builder
{
    class PlayerCharactersBuilder : CharactersBuilder
    {

        public override void SetBulletPrefab()
        {
            this.Characters.BulletPrefab = null;
        }

        public override void SetColor()
        {
            Color color = new Color();
            color = Color.blue;
            this.Characters.Color = color;
        }

        public override void SetDemage()
        {
            this.Characters.Demage = new Demage { demage = 1f };
        }

        public override void SetFiringTimer()
        {
            this.Characters.FiringTimer = new FiringTimer { firingTimer = 0.3f };
        }

        public override void SetHitpoint()
        {
            this.Characters.Hitpoint = new Hitpoint { hitPoint = 20f };
        }

        public override void SetName()
        {
            this.Characters.Name = new Name { name = "Player" };
        }

        public override void SetRunTime()
        {
            this.Characters.RunTime = new RunTime { runTime = 10 };
        }

        public override void SetSpeed()
        {
            this.Characters.Speed = new Speed { speed = 10f };
        }

        public override void SetSpeedBullet()
        {
            this.Characters.SpeedBullet = new SpeedBullet { speedBullet = 2000f };
        }
    }
}