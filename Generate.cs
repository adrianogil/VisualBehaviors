using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
#endif

public enum GenerationPosition
{
	SamePosition = 0,
	ObjectAsReference = 1
}

public class Generate : ConditionBasedBehavior {
	public GameObject prefab;

	[HideInInspector]
	public GenerationPosition generationPosition;

	[HideInInspector]
	public Transform referencePosition;

	// Update is called once per frame
	public override void OnConditionSatisfied()
	{
		Debug.Log("Generate::OnConditionSatisfied");
		GameObject generated = Instantiate(prefab) as GameObject;
		if (generationPosition == GenerationPosition.SamePosition)
			generated.transform.position = transform.position;
		else if (generationPosition == GenerationPosition.ObjectAsReference)
			generated.transform.position = referencePosition.position;
		//prefab.transform.parent = transform;
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(Generate))]
public class GenerateEditor : ConditionBasedEditor {


	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		Generate generate = target as Generate;

		if (generate == null)
		{
			return;
		}

		int selected =  (int) generate.generationPosition;
		string[] options = new string[]
		{
			"Same position", "Using an object as reference "
		};
		EditorGUILayout.BeginHorizontal();
		selected = EditorGUILayout.Popup("Generation position", selected, options);
		EditorGUILayout.EndHorizontal();
		generate.generationPosition = (GenerationPosition) selected;

		EditorGUI.indentLevel = 1;

		if (generate.generationPosition == GenerationPosition.ObjectAsReference)
		{
			generate.referencePosition = (Transform) EditorGUILayout.ObjectField("Reference object ", generate.referencePosition, typeof(Transform), true);
		}
		

	}


}
#endif


