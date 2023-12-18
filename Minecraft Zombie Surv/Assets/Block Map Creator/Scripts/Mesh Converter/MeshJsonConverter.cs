using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

public class MeshJsonConverter : MonoBehaviour
{
    [SerializeField] private string savePath = "Assets/Models/mesh.json"; // ���� ��� ���������� JSON �����, ������������ ����� Assets

    public void SaveMeshToJSON(Mesh mesh)
    {
        // ���������, ��� ���� ��� ���������� �� ������
        if (string.IsNullOrEmpty(savePath))
        {
            Debug.LogError("���� ��� ���������� ����� �� ������.");
            return;
        }

        // ������� ����������, ���� ��� �� ����������
        string directoryPath = Path.GetDirectoryName(savePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // ����������� Mesh � JSON
        string json = MeshToJson(mesh);

        // ��������� JSON � ����
        File.WriteAllText(savePath, json);
        Debug.Log("Mesh ������� �������� � ���� JSON �� ����: " + savePath);
    }

    public Mesh LoadMeshFromJSON(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("���� �� ������: " + filePath);
            return null;
        }

        // ������ JSON �� �����
        string json = File.ReadAllText(filePath);

        // ����������� JSON � Mesh
        Mesh mesh = MeshFromJson(json);

        return mesh;
    }

    private string MeshToJson(Mesh mesh)
    {
        MeshData meshData = new MeshData(mesh);
        return JsonConvert.SerializeObject(meshData, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.None
        });
    }

    private Mesh MeshFromJson(string json)
    {
        MeshData meshData = JsonConvert.DeserializeObject<MeshData>(json);
        return meshData.ToMesh();
    }
}

[Serializable]
public class MeshData
{
    public int[] triangles;
    public Vector3[] vertices;
    public Vector2[] uv;

    public MeshData(Mesh mesh)
    {
        triangles = mesh.triangles;
        vertices = mesh.vertices;
        uv = mesh.uv;
    }

    public Mesh ToMesh()
    {
        Mesh mesh = new Mesh();
        mesh.triangles = triangles;
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.RecalculateNormals();
        return mesh;
    }
}
