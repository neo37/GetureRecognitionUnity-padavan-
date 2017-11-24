using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using System.IO;
using LitJson;

public class LeapHiii : MonoBehaviour {
	
	public GameObject CubeToDrive;
	
	private string jsonString;
	private JsonData itemData;
	protected Vector tP1;
	protected Vector tP2;
	protected Vector tP3;
	protected Vector tP4;
	protected Vector tP0;	
	
	protected Vector TipPosB;

	
	protected Vector hB;
	protected Vector PalmNormalR;

	float rassta;
	float rBig;
	
	float deltaRast = 0.03f;
	//float deltaAngle= 0.2f;
	
	public float speed=3.6f;
	
	float rast0=0;
	float rast1=0;
	float rast2=0;
	float rast3=0;
	float rast4=0;
	public class Vector36
	{
		public double x;
		public double y;
		public double z;
		
	}
	
	public class CheckHandsGesturesI
	{
		public double id;// = 1;
		public string name;
		public Vector36 CheckPalmNormValueI = new Vector36();
		public double CheckThumb;//=0.08f;
		public double CheckIndex;//=0.097f;
		public double CheckMiddle;//=0.107f;
		public double CheckRing;//=0.097f;
		public double CheckPinky;//=0.087f;
		
	}
	
	public class CheckHandsGestures
	{
		public int id;// = 1;
		public string name;
		public Vector CheckPalmNormValue;
		public float CheckThumb;//=0.08f;
		public float CheckIndex;//=0.097f;
		public float CheckMiddle;//=0.107f;
		public float CheckRing;//=0.097f;
		public float CheckPinky;//=0.087f;
		
	}
	Hand hhand;
    LeapProvider provider;
	Finger fingerB;
	
	JsonData savingToJ;
	
	CheckHandsGesturesI CheckG2 = new CheckHandsGesturesI();
	
	List<CheckHandsGestures> checkRights = new List<CheckHandsGestures>();

	void Start () {		
		jsonString = File.ReadAllText(Application.dataPath + "/Gestures.json");
		itemData=JsonMapper.ToObject(jsonString);		
		
		CheckHandsGestures CheckG1 = new CheckHandsGestures();		
		for (int i = 0; i < itemData["RightHandChek"].Count; i++)
		{
			CheckG1.id=i;
			CheckG1.name=(string)itemData["RightHandChek"][i]["name"];
			CheckG1.CheckPalmNormValue = new Vector((float)(double)itemData["RightHandChek"]
			[i]["CheckPalmNormValue"]["x"],(float)(double)itemData["RightHandChek"]
			[i]["CheckPalmNormValue"]["y"], (float)(double)itemData["RightHandChek"]
			[i]["CheckPalmNormValue"]["z"]);
			CheckG1.CheckThumb=(float)(double)itemData["RightHandChek"][i]["CheckThumb"];
			CheckG1.CheckIndex=(float)(double)itemData["RightHandChek"][i]["CheckIndex"];
			CheckG1.CheckMiddle=(float)(double)itemData["RightHandChek"][i]["CheckMiddle"];
			CheckG1.CheckRing=(float)(double)itemData["RightHandChek"][i]["CheckRing"];
			CheckG1.CheckPinky=(float)(double)itemData["RightHandChek"][i]["CheckPinky"];
			checkRights.Add(CheckG1);
			CheckG1 = new CheckHandsGestures();					
		}
		
		
		foreach(CheckHandsGestures checkRight in checkRights) 
			{ 
				Debug.Log(checkRight.CheckPalmNormValue);	
				Debug.Log("Hi");
			}
		
		//string json = JsonUtility.ToJson(CheckG1);	
		
		provider = FindObjectOfType<LeapProvider>() as LeapProvider;
		
	}
	

	void Update () {
		Frame frame = provider.CurrentFrame;
		List<Hand> hands = frame.Hands;
		int lol=-1;
        for (int h = 0; h < hands.Count; h++)
        {
			
            Hand hand = hands[h];
			
            if (hand.IsLeft)
            {
              transform.position = hand.PalmPosition.ToVector3() +
                                   hand.PalmNormal.ToVector3() *
                                  (transform.localScale.y * .5f + .02f);
              transform.rotation = hand.Basis.rotation.ToQuaternion();
			  
          }
		  if (hand.IsRight)
            {
				PalmNormalR = hand.PalmNormal;
				hB = hand.PalmPosition;
				
			//	int ExecutableGestureByVector = -1;
				List<Finger> fingers = hand.Fingers;
				foreach(Finger finger in fingers)
				{
			
					switch ((int)finger.Type) 
					{
					case 0: 
					tP0=finger.TipPosition;
					 break;
					case 1: 
					tP1=finger.TipPosition;
					 break;
					case 2: 
					tP2=finger.TipPosition;
					 break;
					case 3: 
					tP3=finger.TipPosition;
					 break;
					case 4: 
					tP4=finger.TipPosition;
					 break;
					}
				}

					rast0=Mathf.Sqrt(Mathf.Pow(hB.x-tP0.x,2f)+Mathf.Pow(hB.y-tP0.y,2f)+Mathf.Pow(hB.z-tP0.z,2f));
					rast1=Mathf.Sqrt(Mathf.Pow(hB.x-tP1.x,2f)+Mathf.Pow(hB.y-tP1.y,2f)+Mathf.Pow(hB.z-tP1.z,2f));
					rast2=Mathf.Sqrt(Mathf.Pow(hB.x-tP2.x,2f)+Mathf.Pow(hB.y-tP2.y,2f)+Mathf.Pow(hB.z-tP2.z,2f));
					rast3=Mathf.Sqrt(Mathf.Pow(hB.x-tP3.x,2f)+Mathf.Pow(hB.y-tP3.y,2f)+Mathf.Pow(hB.z-tP3.z,2f));
					rast4=Mathf.Sqrt(Mathf.Pow(hB.x-tP4.x,2f)+Mathf.Pow(hB.y-tP4.y,2f)+Mathf.Pow(hB.z-tP4.z,2f));
					
					foreach(CheckHandsGestures checkRight in checkRights) 
					{ 
						if (/*(hand.PalmNormal.AngleTo(checkRight.CheckPalmNormValue)<deltaAngle) &*/ //If you uncoment this - it will be more exactly/more correctly!
						(Mathf.Abs(rast0-checkRight.CheckThumb)<deltaRast)  &
						(Mathf.Abs(rast1-checkRight.CheckIndex)<deltaRast)  &
						(Mathf.Abs(rast2-checkRight.CheckMiddle)<deltaRast) &
						(Mathf.Abs(rast3-checkRight.CheckRing)<deltaRast)   &
						(Mathf.Abs(rast4-checkRight.CheckPinky)<deltaRast) 	 )
						{
							
							lol=checkRight.id;
							Debug.Log("DaDaDa");
						} 
						
					}
	
          }
				switch (lol) 
					{
						case 0:
						CubeToDrive.transform.Rotate(speed,0,0);
						//CubeToDrive.transform.position += CubeToDrive.transform.up * speed;
						Debug.Log(lol);
						 break;
						case 1:
						
						CubeToDrive.transform.Rotate(0,speed,0);
						Debug.Log(lol);
						 break;
						case 2:
						
						CubeToDrive.transform.Rotate(0,0,speed);
						Debug.Log(lol);
						 break;
						case 3:
						Debug.Log(lol);
						 break;					
					}

		if (Input.GetKey(KeyCode.W))
		{
		CheckG2.id=1d;
		CheckG2.name="someName";
	
		CheckG2.CheckPalmNormValueI.x=(double)hB.x;	
		CheckG2.CheckPalmNormValueI.y = (double)hB.y;
		CheckG2.CheckPalmNormValueI.z =(double)hB.z;
	
		CheckG2.CheckThumb=(double)rast0;
		CheckG2.CheckIndex=(double)rast1;
		CheckG2.CheckMiddle=(double)rast2;
		CheckG2.CheckRing=(double)rast3;
		CheckG2.CheckPinky=(double)rast4;
	
		savingToJ = JsonMapper.ToJson(CheckG2);
        File.WriteAllText(Application.dataPath + "/OneGesture.json",savingToJ.ToString());
        Debug.Log("yo");
            
		}
       }
	}
}
