using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationForMultipleObjects : MonoBehaviour {

	List<string> places = new List<string> (){"Home","Office"};

	private float currentLongitude=0f;
	private float currentLatitude=0f;
	private int modelno=0;
	private int modelcount=0;
	private int dropdown_value=0;
	private bool changed=false;

	public static LocationForMultipleObjects Instance{ set; get; }
    public float originalLatitude;
	public float originalLongitude;
	public float radius;
	public Text range,Distance;
	public GameObject[] Model;
	public Dropdown dropdown;

	public void Awake()
	{	
		range.text="Initializing";
		modelno=0;
		dropdown_value=0;	//By default first option in dropdown is selected
		modelcount=Model.Length;		
		Distance.text = " Distance: "+"0 m";
	}

	public void Start()
	{
		Instance = this;
		DontDestroyOnLoad (gameObject);
		PopulateList ();
		changed=true;
	}

	void PopulateList() 
	{		
		dropdown.AddOptions (places);
	}

	public void Dropdown_IndexChanged(int index)
	{
		//Add cases for more location
		switch (index) 
		{
		case 0:	//Home			
			originalLatitude = 9.991158f;
			originalLongitude = 76.28171f;
			modelno = 0;
			changed=true;		//Indicates dropdown value changed
			break;
			
		case 1:	//Office		
			originalLatitude = 9.991285f;
			originalLongitude = 76.283507f;
			modelno = 1;
			changed=true;		//Indicates dropdown value changed
			break;			
		}			
	}

	IEnumerator GetCoordinates ()
	{
		range.text="Getting GPS";
		while (true) 
		{
			if (!Input.location.isEnabledByUser)
			{
				Debug.Log ("Location is Not enabled by user ");
				range.text="Location is Not enabled by user";
				yield break;
			}
			
			Input.location.Start (1f, .1f);
			int maxwait = 20;
			while (Input.location.status == LocationServiceStatus.Initializing && maxwait > 0) {
				yield return new WaitForSeconds (1);
				maxwait--;
			}

			if (maxwait <= 0) {
				Debug.Log ("Timed Out");
				yield break;
			}

			if (Input.location.status == LocationServiceStatus.Failed) 
			{
				Debug.Log ("Unable to determine location");
				range.text="Failed GPS";
				yield break;
			} 
			else 
			{ 
				currentLatitude = Input.location.lastData.latitude;
				currentLongitude = Input.location.lastData.longitude;
				Vector2 pass = new Vector2 (currentLatitude,currentLongitude);
				Calc (originalLatitude, originalLongitude, currentLatitude, currentLongitude, radius);				
			}
		}		
	}

	Vector2 CartesianToPolar ( Vector3 point  ){
		Vector2 polar;

		//calc longitude
		polar.y = Mathf.Atan2(point.x,point.z);

		//this is easier to write and read than sqrt(pow(x,2), pow(y,2))!
		Vector2 xzLen = new Vector2(point.x,point.z);
		//xzLen = xzLen.magnitude;

		//atan2 does the magic
		polar.x = Mathf.Atan2(-point.y,xzLen.x);

		//convert to deg
		polar *= Mathf.Rad2Deg;
		print (polar.x+"    "+polar.y);
		return polar;
	}

	Vector3 PolarToCartesian ( Vector2 polar  ){

		//an origin vector, representing lat,lon of 0,0. 
		Vector3 origin= new Vector3(originalLatitude,originalLongitude,1);

		//build a quaternion using euler angles for lat,lon
		Quaternion rotation= Quaternion.Euler(polar.x,polar.y,0);

		//transform our reference vector by the rotation. Easy-peasy!
		Vector3 point=rotation*origin;
		print(point.x+"    "+point.y);
		return point;
	}

	public void Calc(float xc, float yc, float xp, float yp,float r)
	{
		float distance=0f,x=0f,y=0f,r1,r2,r3,c;
		float R = 6378.137f;

		r1 = xc * Mathf.Deg2Rad;
		r2 = xp * Mathf.Deg2Rad;
		x = (xp - xc)*Mathf.Deg2Rad;
		y = (yp - yc)*Mathf.Deg2Rad;
		r3 = Mathf.Sin (x / 2) * Mathf.Sin (x / 2) + Mathf.Cos(r1) * Mathf.Cos(r2) * Mathf.Sin(y/2) * Mathf.Sin(y/2);
		c = 2 * Mathf.Atan2(Mathf.Sqrt(r3), Mathf.Sqrt(1-r3)); 
		distance = Mathf.RoundToInt(R * c * 1000f);
		Distance.text = " Distance: "+distance+" m";
		if (distance <= r)
		{
			range.text="In range";
			ModelChanger();
		}
		else
		{			
			range.text="Not In range";
			ModelChanger();
		}
	}

	void ModelChanger()
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
	
	private void Activator()
	{		
		// Can add more cases for more locations here
		switch(dropdown_value)
		{
			case 0:	
					StopCoroutine(GetCoordinates());						
					StartCoroutine (GetCoordinates()); //Restarting the coroutine
					changed=false;					//Is set to false so the Activator is not called until dropdown value is changed
					break;

			case 1:
					StopCoroutine(GetCoordinates());						
					StartCoroutine (GetCoordinates()); //Restarting the coroutine
					changed=false;					//Is set to false so the Activator is not called until dropdown value is changed
					break;
		}			
	}

	public void Update()
	{  
		dropdown_value=dropdown.value;	//keeps track of dropdown value

		if (changed)	//if dropdown value changed call activator
		{
			Activator();							
		}	

		currentLatitude = Input.location.lastData.latitude;
		currentLongitude = Input.location.lastData.longitude;		
	}

}
