using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour {

	List<string> places = new List<string> (){"Home","Office","House"};
	private int modelno=0,modelcount=0;
	private int dropdown_value=0;
	public GameObject[] Model;	
	public Dropdown dropdown;
	public Text text;
	public string modelname;
	public bool var=true;

	void Start () 
	{
		PopulateList ();
		modelno=0;
		modelcount=Model.Length;				
	}

	void PopulateList() 
	{		
		dropdown.AddOptions (places);
	}

	public void Dropdown_IndexChanged(int index)
	{
		switch (index) 
		{
		case 0:				
			modelno = 0;
			SetModel();									
			break;
		case 1:			
			modelno = 1;	
			SetModel();				
			break;
			
		case 2:		
			modelno = 2;
			SetModel();					
			break;		
		}
	}

	IEnumerator ModelAtRun()
	{
		modelchanger();	
		if (var)
		{
			yield return null;
		}
	}

    private void SetModel()
    {
        text.text=modelno.ToString();
    }

	private void Activator()
	{
		if (dropdown_value==0)
		{
			Debug.Log("Called");
			StopCoroutine("ModelAtRun");
			modelname="Model 1";			
			StartCoroutine("ModelAtRun");
		}
		else if(dropdown_value==1)
		{
			Debug.Log("Called");
			StopCoroutine("ModelAtRun");
			modelname="Model 2";					
			StartCoroutine("ModelAtRun");
		}
		else if (dropdown_value==2)
		{
			StopCoroutine("ModelAtRun");
			modelname="Model 3";						
			StartCoroutine("ModelAtRun");
		}	
	}

	void modelchanger()
	{
		for (int i = 0; i < modelcount; i++)
		{
			if (i==modelno)
			{
				Model[i].SetActive(true);
			}
			else
			{
				Model[i].SetActive(false);
			}
		}
	}

    void Update () 
	{		
		dropdown_value=dropdown.value;
		Activator();			
	}
}
