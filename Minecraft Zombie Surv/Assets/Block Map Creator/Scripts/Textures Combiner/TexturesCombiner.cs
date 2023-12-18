using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexturesCombiner : MonoBehaviour
{
    public Texture2D[] textures;
    public int maxTexturesPerRow = 10;

    [Button]
    public void CombineTexturesAndSave()
    {
        // ���������, ��� � ��� ���� ���� �� ���� ��������
        if (textures.Length < 1)
        {
            Debug.LogError("������ ������� ����! �������� �������� ��� �����������.");
            return;
        }

        int maxWidth = 0;
        int totalHeight = 0;

        // ������� ������ � ����� ������ ���� �������
        int rowCount = Mathf.CeilToInt((float)textures.Length / maxTexturesPerRow);
        int[] widths = new int[rowCount];
        for (int i = 0; i < textures.Length; i++)
        {
            int rowIndex = i / maxTexturesPerRow;
            widths[rowIndex] += textures[i].width;
            totalHeight = Mathf.Max(totalHeight, textures[i].height * (rowIndex + 1));
        }
        maxWidth = Mathf.Max(widths);

        // ������� ����� ��������, ������� ����� ���������� ��� ������ ��������
        Texture2D combinedTexture = new Texture2D(maxWidth, totalHeight);

        int xOffset = 0;
        int yOffset = 0;

        // ���������� ��������
        for (int i = 0; i < textures.Length; i++)
        {
            for (int x = 0; x < textures[i].width; x++)
            {
                for (int y = 0; y < textures[i].height; y++)
                {
                    // ������ ���� ������� ������� � ������������ ��������
                    combinedTexture.SetPixel(x + xOffset, y + yOffset, textures[i].GetPixel(x, y));
                }
            }
            xOffset += textures[i].width;
            if ((i + 1) % maxTexturesPerRow == 0)
            {
                xOffset = 0;
                yOffset += textures[i].height;
            }
        }

        // ��������� ���������
        combinedTexture.Apply();

        // ����������� ������������ �������� � �����
        byte[] bytes = combinedTexture.EncodeToPNG();

        // ��������� �������� � ����� Assets
        string filePath = Application.dataPath + "/combinedTexture.png";
        System.IO.File.WriteAllBytes(filePath, bytes);

        Debug.Log("�������� ������� ���������� � ��������� � " + filePath);
    }
}
