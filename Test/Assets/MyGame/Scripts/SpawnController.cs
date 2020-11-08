using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class SpawnController : MonoBehaviour
{
    public enum TypeSpawn
    {
        PLAYER = 0,
        BOT = 1
    }

    //public enum TypeCharaсters //типы ботов
    //{
    //    NAME1 = 0,
    //    NAME2 = 1,
    //    NAME3 = 2,
    //    NAME4 = 3
    //}

    public TypeSpawn typeSpawn;

    void OnValidate()
    {
        Material materialTemp = matSetup(Color.blue);
        if (typeSpawn == TypeSpawn.BOT)
        {
            materialTemp = matSetup(Color.green);
            this.gameObject.GetComponent<MeshRenderer>().material = materialTemp;
        }
        else { this.gameObject.GetComponent<MeshRenderer>().material = materialTemp; }
    }

    Material matSetup(Color col)
    {
        Material mat = new Material(Shader.Find("Standard"));
        mat.SetColor("_Color", col);
        return mat;
    }
}
