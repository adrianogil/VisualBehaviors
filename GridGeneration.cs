using UnityEngine;
using System;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class Integer3
{
    public int x = 1;
    public int y = 1;
    public int z = 1;
}

public class GridGeneration : Generate {

    public Integer3 gridSize;

	public override GameObject GenerateObject(GameObject prefab)
    {
        Debug.Log("GridGeneration::GenerateObject");

        GameObject grid = new GameObject("Grid");

        GridItem gridItemPrefab;

        if ((gridItemPrefab = prefab.GetComponent<GridItem>()) == null)
        {
            return base.GenerateObject(prefab);
        }

        GameObject gridItemObject;
        Vector3 itemPosition;

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int z = 0; z < gridSize.z; z++)
                {
                    itemPosition.x = gridItemPrefab.itemSize.x * x;
                    itemPosition.y = gridItemPrefab.itemSize.y * y;
                    itemPosition.z = gridItemPrefab.itemSize.z * z;

                    gridItemObject = gridItemPrefab.GenerateItself();
                    gridItemObject.transform.position = itemPosition;
                    gridItemObject.transform.parent = grid.transform;
                }
            }
        }

        return grid;
    }

    public void OnValidate()
    {
        if(gridSize.x < 1)
            gridSize.x = 1;
        if(gridSize.y < 1)
            gridSize.y = 1;
        if(gridSize.z < 1)
            gridSize.z = 1;
    }

    public void OnDrawGizmosSelected()
    {
       GridItem gridItemPrefab;

        if ((gridItemPrefab = prefab.GetComponent<GridItem>()) == null)
        {
            return;
        }

        GameObject gridItemObject;
        Vector3 itemPosition;

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int z = 0; z < gridSize.z; z++)
                {
                    itemPosition.x = gridItemPrefab.itemSize.x * x;
                    itemPosition.y = gridItemPrefab.itemSize.y * y;
                    itemPosition.z = gridItemPrefab.itemSize.z * z;

                    Gizmos.DrawWireCube(itemPosition, gridItemPrefab.itemSize);
                }
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GridGeneration))]
public class GridGenerationEditor : GenerateEditor {

    public override void GenerateOnEditor()
    {
        GridGeneration generate = target as GridGeneration;

        if (generate == null)
        {
            return;
        }

        generate.SetupGeneratedObject(generate.GenerateObject(generate.prefab));
    }

}
#endif
