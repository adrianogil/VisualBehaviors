using UnityEngine;
using System.Collections;

public class GridItem : MonoBehaviour {

    public Vector3 itemSize;

	public virtual GameObject GenerateItself()
    {
        GameObject generated = Instantiate(gameObject) as GameObject;

        return generated;
    }
}
