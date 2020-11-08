using Assets.MyGame.Scripts;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour {

    public enum Mode {Player, Cursor};

	public Mode face; 
    public float smooth = 2.5f; 
    public float offset; 
    public GameObject boundsMap; 
	public bool useBounds = true; 

	private Transform player;
	private Vector3 min, max, direction;
	private static CameraFollow inst;
	private Camera cam;
	private GameManager manager;

	void Awake()
	{
        inst = this;
		cam = GetComponent<Camera>();
        cam.orthographic = true;
        manager = FindObjectOfType<GameManager>();
		CalculateBounds();
        
    }

    private void Start()
    {
		FindPlayer();
	}

	public static void FindPlayer()
	{
        inst.FindPlayer_inst();
    }

	public static void CalculateBounds()
	{
        inst.CalculateBounds_inst();
    }

    void CalculateBounds_inst()
    {
        if (boundsMap == null) return;
        Bounds bounds = CameraBounds();
        min = bounds.max + boundsMap.GetComponent<MeshRenderer>().bounds.min;
        max = bounds.min + boundsMap.GetComponent<MeshRenderer>().bounds.max;
    }

    void FindPlayer_inst()
    {
		if (manager.player)
			player = manager.player.gameObject.transform;

        if (player)
        {
            if (face == Mode.Player) direction = player.right;
            Vector3 position = player.position + direction * offset;
            position.z = transform.position.z;
            transform.position = MoveInside(position, new Vector3(min.x, position.y + 10, min.z), new Vector3(max.x, position.y + 10, max.z));
        }
    }

    void UseCameraBounds_inst(bool value)
    {
        useBounds = value;
    }

    Bounds CameraBounds()
	{
		float height = cam.orthographicSize * 2;
		return new Bounds(Vector3.zero, new Vector3(height * cam.aspect, 0, height));
	}

	Vector3 MoveInside(Vector3 current, Vector3 pMin, Vector3 pMax)
	{
		if(!useBounds || boundsMap == null) return current;
		current = Vector3.Max(current, pMin);
		current = Vector3.Min(current, pMax);
		return current;
	}

	void Follow()
	{
		if(face == Mode.Player) direction = player.right;
		Vector3 position = player.position + direction * offset;
		position.y = transform.position.y;
		position = MoveInside(position, new Vector3(min.x, position.y, min.z), new Vector3(max.x, position.y, max.z));
		transform.position = Vector3.Lerp(transform.position, position, smooth * Time.deltaTime);
	}

	void LateUpdate()
	{
		if(player)
		{
			Follow();
		}
	}
}
