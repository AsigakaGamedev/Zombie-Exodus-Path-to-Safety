using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMapManager : MonoBehaviour
{
    [SerializeField] private List<BlockInfo> blocksDatabase;

    [Space]
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshCollider meshCollider;
    [SerializeField] private MeshJsonConverter meshCoverter;

    [Space]
    [SerializeField] private float blockScale = 1;
    [SerializeField] private Vector3Int mapSize;

    [Header("Texturing")]
    [SerializeField] private Texture2D blocksAtlas;
    [SerializeField] private float blockResol = 128;
    [SerializeField] private int atlasWidth = 4;
    [SerializeField] private int atlasHeight = 1;

    private Mesh mesh;

    private List<Vector3> verticies = new List<Vector3>();
    private List<Vector2> uvs = new List<Vector2>();
    private List<int> triangles = new List<int>();

    private int[,,] blocks;

    public float BlockScale { get => blockScale; }

    private void Awake()
    {
        blocks = new int[mapSize.x, mapSize.y, mapSize.z];

        RegenerateMesh();
    }

    #region Generation

    [Button]
    public void RegenerateMesh()
    {
        mesh = new Mesh();

        verticies.Clear();
        uvs.Clear();
        triangles.Clear();

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                for (int z = 0; z < mapSize.z; z++)
                {
                    GenerateBlock(new Vector3Int(x, y, z), blocksDatabase[blocks[x, y, z]]);
                }
            }
        }

        mesh.vertices = verticies.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.triangles = triangles.ToArray();

        mesh.Optimize();

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = mesh;
    }

    public void PlaceBlock(Vector3Int pos, BlockInfo blockInfo)
    {
        blocks[pos.x, pos.y, pos.z] = blocksDatabase.IndexOf(blockInfo);
        RegenerateMesh();
    }

    public void DestroyBlock(Vector3Int pos)
    {
        blocks[pos.x, pos.y, pos.z] = 0;
        RegenerateMesh();
    }

    private void GenerateBlock(Vector3Int pos, BlockInfo blockInfo)
    {
        if (GetBlockAtPosition(pos) == 0) return;

        if (GetBlockAtPosition(pos + Vector3Int.right) == 0) GenerateRightSide(pos, blockInfo);
        if (GetBlockAtPosition(pos + Vector3Int.left) == 0) GenerateLeftSide(pos, blockInfo);
        if (GetBlockAtPosition(pos + Vector3Int.forward) == 0) GenerateFrontSide(pos, blockInfo);
        if (GetBlockAtPosition(pos + Vector3Int.back) == 0) GenerateBackSide(pos, blockInfo);
        if (GetBlockAtPosition(pos + Vector3Int.up) == 0) GenerateTopSide(pos, blockInfo);
        if (GetBlockAtPosition(pos + Vector3Int.down) == 0) GenerateBottomSide(pos, blockInfo);
    }

    private int GetBlockAtPosition(Vector3Int pos)
    {
        if (pos.x >= 0 && pos.x < mapSize.x &&
            pos.y >= 0 && pos.y < mapSize.y &&
            pos.z >= 0 && pos.z < mapSize.z)
        {
            return blocks[pos.x, pos.y, pos.z];
        }
        else
        {
            return 0;
        }
    }

    private void GenerateLeftSide(Vector3Int pos, BlockInfo blockInfo)
    {
        verticies.Add((new Vector3(0, 0, 0) + pos) * blockScale);
        verticies.Add((new Vector3(0, 0, 1) + pos) * blockScale);
        verticies.Add((new Vector3(0, 1, 0) + pos) * blockScale);
        verticies.Add((new Vector3(0, 1, 1) + pos) * blockScale);

        AddLastVerticies(blockInfo.TextureHorizontalSideID.x, blockInfo.TextureHorizontalSideID.y);
    }

    private void GenerateRightSide(Vector3Int pos, BlockInfo blockInfo)
    {
        verticies.Add((new Vector3(1, 0, 0) + pos) * blockScale);
        verticies.Add((new Vector3(1, 1, 0) + pos) * blockScale);
        verticies.Add((new Vector3(1, 0, 1) + pos) * blockScale);
        verticies.Add((new Vector3(1, 1, 1) + pos) * blockScale);

        AddLastVerticies(blockInfo.TextureHorizontalSideID.x, blockInfo.TextureHorizontalSideID.y);
    }

    private void GenerateBackSide(Vector3Int pos, BlockInfo blockInfo)
    {
        verticies.Add((new Vector3(0, 0, 0) + pos) * blockScale);
        verticies.Add((new Vector3(0, 1, 0) + pos) * blockScale);
        verticies.Add((new Vector3(1, 0, 0) + pos) * blockScale);
        verticies.Add((new Vector3(1, 1, 0) + pos) * blockScale);

        AddLastVerticies(blockInfo.TextureHorizontalSideID.x, blockInfo.TextureHorizontalSideID.y);
    }

    private void GenerateFrontSide(Vector3Int pos, BlockInfo blockInfo)
    {
        verticies.Add((new Vector3(0, 0, 1) + pos) * blockScale);
        verticies.Add((new Vector3(1, 0, 1) + pos) * blockScale);
        verticies.Add((new Vector3(0, 1, 1) + pos) * blockScale);
        verticies.Add((new Vector3(1, 1, 1) + pos) * blockScale);

        AddLastVerticies(blockInfo.TextureHorizontalSideID.x, blockInfo.TextureHorizontalSideID.y);
    }

    private void GenerateTopSide(Vector3Int pos, BlockInfo blockInfo)
    {
        verticies.Add((new Vector3(0, 1, 0) + pos) * blockScale);
        verticies.Add((new Vector3(0, 1, 1) + pos) * blockScale);
        verticies.Add((new Vector3(1, 1, 0) + pos) * blockScale);
        verticies.Add((new Vector3(1, 1, 1) + pos) * blockScale);

        AddLastVerticies(blockInfo.TextureTopSideID.x, blockInfo.TextureTopSideID.y);
    }

    private void GenerateBottomSide(Vector3Int pos, BlockInfo blockInfo)
    {
        verticies.Add((new Vector3(0, 0, 0) + pos) * blockScale);
        verticies.Add((new Vector3(1, 0, 0) + pos) * blockScale);
        verticies.Add((new Vector3(0, 0, 1) + pos) * blockScale);
        verticies.Add((new Vector3(1, 0, 1) + pos) * blockScale);

        AddLastVerticies(blockInfo.TextureBottomSideID.x, blockInfo.TextureBottomSideID.y);
    }

    private void AddLastVerticies(int xUVOffset, int yUVOffset)
    {
        foreach (Vector2 uvCoord in GetUVForBlock(xUVOffset, yUVOffset))
        {
            uvs.Add(uvCoord);
        }
        triangles.Add(verticies.Count - 4);
        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 2);

        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 1);
        triangles.Add(verticies.Count - 2);
    }

    public Vector2[] GetUVForBlock(int x, int y)
    {
        // ���������, ��� ����� ����� ��������� � �������� ����������� ������
        if (x < 0 || x >= atlasWidth || y < 0 || y >= atlasHeight)
        {
            Debug.LogError("�������� ���������� ����� � ���������� ������.");
            return new Vector2[4];
        }

        // ��������� UV ���������� ��� ���������� �����
        float uMin = (float)(x * blockResol) / (float)(atlasWidth * blockResol);
        float vMin = (float)(y * blockResol) / (float)(atlasHeight * blockResol);
        float uMax = (float)((x + 1) * blockResol) / (float)(atlasWidth * blockResol);
        float vMax = (float)((y + 1) * blockResol) / (float)(atlasHeight * blockResol);

        Vector2[] uvCoordinates = new Vector2[4];
        uvCoordinates[0] = new Vector2(uMin, vMin); // ����� ������ ����
        uvCoordinates[1] = new Vector2(uMax, vMin); // ������ ������ ����
        uvCoordinates[2] = new Vector2(uMin, vMax); // ����� ������� ����
        uvCoordinates[3] = new Vector2(uMax, vMax); // ������ ������� ����

        return uvCoordinates;
    }

    #endregion

    #region Save Load

    [Button]
    public void SaveMap()
    {
        meshCoverter.SaveMeshToJSON(mesh);
    }

    [Button]
    public void LoadMap()
    {

    }

    #endregion
}
