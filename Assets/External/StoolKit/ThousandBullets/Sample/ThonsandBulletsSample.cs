using System.Collections.Generic;
using UnityEngine;

class ThonsandBulletsSample : MonoBehaviour
{
    [SerializeField] private Material _mat;
    public int n = 20000;
    private Mesh _mesh;

    private List<MaterialPropertyBlock> blocks = new List<MaterialPropertyBlock>();
    private List<Vector3> pos = new List<Vector3>();

    void Start()
    {
        // 動的Mesh生成
        _mesh = new Mesh();
        _mesh.vertices = new Vector3[] {
            new Vector3 (-0.5f, -0.5f),
            new Vector3 (-0.5f, 0.5f),
            new Vector3 (0.5f, 0.5f),
            new Vector3 (0.5f, -0.5f),
        };
        _mesh.uv = new Vector2[]
        {
            new Vector2(0,0),
            new Vector2(1f,0),
            new Vector2(1f,1f),
            new Vector2(0,1f),
        };
        _mesh.triangles = new int[] {
            0, 1, 2,
            0, 2, 3,
        };
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();

        for (int i = 0; i < n; i++)
        {
            var block = new MaterialPropertyBlock();
            block.SetColor("_Color", new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f)));
            blocks.Add(block);
        }
    }


    void Update()
    {
        for (int i = 0; i < n; i++)
        {
            Vector3 pos = new Vector3(0, 0, i);
            Graphics.DrawMesh(_mesh, pos, Quaternion.identity, _mat, 0, Camera.main, 0, blocks[i]);
        }
    }
}
