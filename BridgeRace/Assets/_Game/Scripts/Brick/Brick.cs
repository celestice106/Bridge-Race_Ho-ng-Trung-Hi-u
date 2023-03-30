using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour, IColorChanging
{
    public ColorType colorType;

    public Data brickData;

    public static Brick ins;

    [SerializeField] private MeshRenderer brickRenderer;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private BoxCollider brickCollider;
    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        TurnOffPhysics();
    }
    public void ChangeColor(ColorType color)
    {
        colorType = color;
        brickRenderer.material = brickData.GetMats(color);
    }

    public void SetRandomColor()
    {
        ChangeColor((ColorType)Random.Range(0, 3));
    }
    public void TurnOffPhysics()
    {
        brickCollider.isTrigger = true;
        rb.isKinematic = true;
    }
    public void TurnOnPhysics()
    {
        ChangeColor(ColorType.No_Color);
        Vector3 randomDir = new Vector3(Random.value, 0, Random.value) * 3.14f;
        rb.isKinematic = false;
        rb.velocity = randomDir;
        brickCollider.isTrigger = false;
        Invoke(nameof(TurnOffPhysics), 1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTag.CHARACTER))
        {
            if (other.GetComponent<Character>().colorType == colorType || colorType == ColorType.No_Color)
            {
                colorType = ColorType.No_Color;
                gameObject.SetActive(false);
                other.GetComponent<Character>().AddBrick();
                if (other.GetComponent<Player>() != null)
                {
                    DataManager.ins.score++;
                }
            }
        }
    }
}
