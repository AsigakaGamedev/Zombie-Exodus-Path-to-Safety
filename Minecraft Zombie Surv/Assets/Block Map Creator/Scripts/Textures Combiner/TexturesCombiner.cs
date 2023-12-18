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
        // Проверяем, что у нас есть хотя бы одна текстура
        if (textures.Length < 1)
        {
            Debug.LogError("Массив текстур пуст! Добавьте текстуры для объединения.");
            return;
        }

        int maxWidth = 0;
        int totalHeight = 0;

        // Находим ширину и общую высоту всех текстур
        int rowCount = Mathf.CeilToInt((float)textures.Length / maxTexturesPerRow);
        int[] widths = new int[rowCount];
        for (int i = 0; i < textures.Length; i++)
        {
            int rowIndex = i / maxTexturesPerRow;
            widths[rowIndex] += textures[i].width;
            totalHeight = Mathf.Max(totalHeight, textures[i].height * (rowIndex + 1));
        }
        maxWidth = Mathf.Max(widths);

        // Создаем новую текстуру, которая будет объединять все другие текстуры
        Texture2D combinedTexture = new Texture2D(maxWidth, totalHeight);

        int xOffset = 0;
        int yOffset = 0;

        // Объединяем текстуры
        for (int i = 0; i < textures.Length; i++)
        {
            for (int x = 0; x < textures[i].width; x++)
            {
                for (int y = 0; y < textures[i].height; y++)
                {
                    // Сеттим цвет каждого пикселя в объединенную текстуру
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

        // Применяем изменения
        combinedTexture.Apply();

        // Преобразуем объединенную текстуру в байты
        byte[] bytes = combinedTexture.EncodeToPNG();

        // Сохраняем текстуру в папке Assets
        string filePath = Application.dataPath + "/combinedTexture.png";
        System.IO.File.WriteAllBytes(filePath, bytes);

        Debug.Log("Текстура успешно объединена и сохранена в " + filePath);
    }
}
