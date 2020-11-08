using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Assets.MyGame.Scripts.Builder
{
    public class Characters : MonoBehaviour
    {
        public Name Name { get; set; }
        public Hitpoint Hitpoint { get; set; }
        public SpeedBullet SpeedBullet { get; set; }
        public Speed Speed { get; set; }
        public Demage Demage { get; set; }
        public FiringTimer FiringTimer { get; set; }
        public RunTime RunTime { get; set; }
        public GameObject BulletPrefab { get; set; }
        public Color Color { get; set; }
        public UnityEngine.AI.NavMeshAgent Agent { get; private set; }

        public string nameCharacter;
        public float  hitpoint;
        public float  speedBullet;
        public float  demage;
        public float  firingTimer;
        public float  runTime;

        private FloatingJoystick floatingJoystick;
        private GameManager gameManager;

        protected bool fire;
        private float  secondgametime;
        private float  secondruntime;
        private int    seconrun;
        private GameObject target;
        private Transform  weapon;
        private Slider     HP;

        private void Awake()
        {
            Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            gameManager = FindObjectOfType<GameManager>();
            BulletPrefab = gameManager.bulletPrefab;
            target = new GameObject();
            target.transform.position = GetRandomLocation();
            foreach (Transform child in transform)
            {
                if (child.name == "Weapon") weapon = child;
            }
            HP = GetComponentInChildren<Slider>();
        }
        private void Start()
        {
            nameCharacter = Name.name;
            hitpoint = Hitpoint.hitPoint;
            speedBullet = SpeedBullet.speedBullet;
            demage = Demage.demage;
            firingTimer = FiringTimer.firingTimer;
            runTime = RunTime.runTime;
            Agent.speed = Speed.speed;
            fire = false;
            BulletPrefab = gameManager.bulletPrefab;
            GetComponent<MeshRenderer>().material.color = Color;
            HP.maxValue = Hitpoint.hitPoint;
            HP.value = Hitpoint.hitPoint;

            if (Name.name == "Player")
            {
                JoystickPlayerExample joystick = this.gameObject.AddComponent<JoystickPlayerExample>();
                joystick.floatingJoystick = FindObjectOfType<FloatingJoystick>();
                joystick.rb = this.GetComponent<Rigidbody>();
                joystick.speed = Speed.speed;
                floatingJoystick = joystick.floatingJoystick;
            }
        }

        private void Update()
        {
            if (gameManager.startGame)
            {
                StateCharacters();
                if (fire)
                    Firing();
            }

        }
        public void StateCharacters()
        {
            if (Name.name == "Player")
            {
                if (floatingJoystick.isMove)
                {
                    fire = false;
                    target = null;
                }
                else
                {
                    if (gameManager.targets.Count == 0) fire = false;
                    else fire = true;

                    if (target == null)
                    {
                        if (gameManager.targets.Count != 0)
                            target = TargetSearch();
                    }
                    else
                    {
                        transform.LookAt(target.transform);
                    }
                }
            }
            else
            {
                Runer();
                fire = true;
                if (gameManager.player != null)
                {
                    GameObject player = gameManager.player.gameObject;
                    transform.LookAt(player.transform);
                }

            }
        }
        public void Firing()
        {
            secondgametime += Time.deltaTime;
            if (secondgametime >= FiringTimer.firingTimer)
            {
                secondgametime = 0;
                Shot();
            }
        }
        public void Runer()
        {
            Agent.SetDestination(target.transform.position);

            secondruntime += Time.deltaTime;
            if (secondruntime >= 1)
            {
                seconrun += 1;
                secondruntime = 0;
            }

            if (seconrun >= RunTime.runTime)
            {
                RunTime.runTime = Random.Range(5, 10);
                seconrun = 0;
                target.transform.position = GetRandomLocation();
            }
        }
        public void Shot()
        {
            GameObject clone;
            clone = Instantiate(BulletPrefab, weapon.position, weapon.rotation);
            clone.GetComponent<Rigidbody>().AddForce(transform.forward * SpeedBullet.speedBullet);
            clone.GetComponent<Bullet>().characters = this.gameObject.GetComponent<Characters>();
        }
        public GameObject TargetSearch()
        {
            var sersh = gameManager.targets.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).FirstOrDefault();
            return sersh.gameObject;
        }
        public Vector3 GetRandomLocation()
        {
            NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

            int t = UnityEngine.Random.Range(0, navMeshData.indices.Length);

            Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t]], UnityEngine.Random.value);
            Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t]], UnityEngine.Random.value);
            return point;
        }
        public void ApplyDamage(float damage)
        {
            Hitpoint.hitPoint -= damage;
            HP.value = Hitpoint.hitPoint;
            if (Hitpoint.hitPoint <= 0f)
                Destroyer();
        }
        public void Miss()
        {
            fire = false;
        }
        public void Destroyer()
        {
            if (Name.name == "Player")
            {
                gameManager.EndGame();
                Destroy(gameObject);
            }
            else
            {
                for (int i = 0; i < gameManager.targets.Count; i++)
                {
                    if (gameManager.targets[i].gameObject == this.gameObject)
                    {
                        gameManager.targets.Remove(gameManager.targets[i]);
                        Destroy(this.gameObject);
                        break;
                    }
                }
            }
        }
    }


    public class FiringTimer
    {
        public float firingTimer { get; set; }
    }

    public class RunTime
    {
        public float runTime { get; set; }
    }

    public class Demage
    {
        public float demage { get; set; }
    }

    public class SpeedBullet
    {
        public float speedBullet { get; set; }
    }
    public class Speed
    {
        public float speed { get; set; }
    }

    public class Hitpoint
    {
        public float hitPoint { get; set; }
    }

    public class Name
    {
        public string name { get; set; }
    }
}