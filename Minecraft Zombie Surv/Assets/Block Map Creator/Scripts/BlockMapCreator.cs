using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMapCreator : MonoBehaviour
{
    [SerializeField] private BlockMapManager manager;

    [Space]
    [SerializeField] private BlockInfo selectedBlock;

    [Space]
    [SerializeField] private LayerMask mapLayers;
    [SerializeField] private float creatorRadius;

    [Space]
    [SerializeField] private Vector3Int startFlatSize;
    [SerializeField] private BlockInfo startFlatBlock;

    private Camera creatorCamera;

    private void Start()
    {
        creatorCamera = Camera.main;

        manager.CreateMap();
        CreateFlat(startFlatSize);
    }

    private void Update()
    {
        Ray ray = creatorCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, creatorRadius, mapLayers))
        {
            if (Input.GetMouseButtonDown(1))
            {
                manager.PlaceBlock(Vector3Int.FloorToInt((hit.point + hit.normal * manager.BlockScale / 2) / manager.BlockScale), selectedBlock);
            }

            if (Input.GetMouseButtonDown(0))
            {
                manager.DestroyBlock(Vector3Int.FloorToInt((hit.point - hit.normal * manager.BlockScale / 2) / manager.BlockScale));
            }
        }
    }

    public void CreateFlat(Vector3Int size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                for (int z = 0; z < size.z; z++)
                {
                    manager.PlaceBlock(new Vector3Int(x, y, z), startFlatBlock);
                }
            }
        }
    }
}
