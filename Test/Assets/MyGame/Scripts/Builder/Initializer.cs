using UnityEngine;
using UnityEditor;

namespace Assets.MyGame.Scripts.Builder
{
     class Initializer 
    {
        public Characters Create(CharactersBuilder charactersBuilder, GameObject prefab, Vector3 position)
        {
            
            charactersBuilder.CreateCharacters(prefab, position);
            charactersBuilder.SetBulletPrefab();
            charactersBuilder.SetColor();
            charactersBuilder.SetDemage();
            charactersBuilder.SetFiringTimer();
            charactersBuilder.SetRunTime();
            charactersBuilder.SetHitpoint();
            charactersBuilder.SetName();
            charactersBuilder.SetSpeedBullet();
            charactersBuilder.SetSpeed();
            return charactersBuilder.Characters;
        }
    }
}