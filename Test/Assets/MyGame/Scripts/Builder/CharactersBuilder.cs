using UnityEngine;
using UnityEditor;

namespace Assets.MyGame.Scripts.Builder
{
    abstract class CharactersBuilder : MonoBehaviour
    {
        public Characters Characters { get; set; }

        public void CreateCharacters(GameObject charactersPrefab, Vector3 position)
        {
            GameObject objectCharacter = Instantiate(charactersPrefab, position, Quaternion.identity);

            if(objectCharacter.GetComponent<Characters>() == null)
            {
                Characters = objectCharacter.AddComponent<Characters>();
            }
            else
            {
                Characters = objectCharacter.GetComponent<Characters>();
            }
        }

        public abstract void SetName();
        public abstract void SetHitpoint();
        public abstract void SetSpeedBullet();
        public abstract void SetSpeed();
        public abstract void SetDemage();
        public abstract void SetFiringTimer();
        public abstract void SetRunTime();
        public abstract void SetBulletPrefab();
        public abstract void SetColor();

    }
}