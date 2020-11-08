using UnityEngine;
using Assets.MyGame.Scripts.Builder;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Assets.MyGame.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public GameObject charactersPrefab;
        public Characters player;
        public List<Characters> targets;
        public bool startGame;
        public GameObject restartPanel;

        private void Awake()
        {
            CharactersInitialization();
            startGame = false;
        }

        private void Start()
        {
            
        }

        public void StartGame()
        {
            startGame = true;
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void EndGame()
        {
            startGame = false;
            restartPanel.SetActive(true);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void CharactersInitialization()
        {
            SpawnController[] spawnTemp = FindObjectsOfType<SpawnController>();
            
            for (int i = 0; i < spawnTemp.Length; i++)
            {
                if(spawnTemp[i].typeSpawn == SpawnController.TypeSpawn.PLAYER) CreatePlayer(spawnTemp[i].transform.position);
                else { CreateBots(spawnTemp[i].transform.position); }
                Destroy(spawnTemp[i].gameObject);
            }

        }

        public void CreateBots(Vector3 position)
        {
            Initializer initialize = new Initializer();
            CharactersBuilder builder = gameObject.AddComponent<AICharactersBuilder>();
            Characters bot = initialize.Create(builder, charactersPrefab, position);
            targets.Add(bot);
        }
        public void CreatePlayer(Vector3 position)
        {
            Initializer initialize = new Initializer();
            CharactersBuilder builder = gameObject.AddComponent<PlayerCharactersBuilder>();
            Characters cha = initialize.Create(builder, charactersPrefab, position);
            player = cha;
        }
    }
}