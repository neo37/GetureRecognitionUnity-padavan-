using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using System.IO;
using LitJson;

public class WriteGestures36 : MonoBehaviour {
	public class Vector36
	{
		public double x;
		public double y;
		public double z;
		
	}
	
	public class CheckHandsGestures
	{
		public double id;// = 1;
		public string name;
		public Vector36 CheckPalmNormValue = new Vector36();
		public double CheckThumb;//=0.08f;
		public double CheckIndex;//=0.097f;
		public double CheckMiddle;//=0.107f;
		public double CheckRing;//=0.097f;
		public double CheckPinky;//=0.087f;
		
	}
	protected Vector tP1;
	protected Vector tP2;
	protected Vector tP3;
	protected Vector tP4;
	protected Vector tP0;	
	protected Vector hB;
	LeapProvider provider;
	JsonData savingToJ;
	
	CheckHandsGestures CheckG1 = new CheckHandsGestures();
	
	float rast0 = 0;
	float rast1 = 0;
	float rast2 = 0;
	float rast3 = 0;
	float rast4 = 0;
	
	void Start () 
	{
	provider = FindObjectOfType<LeapProvider>() as LeapProvider;
	
	
	
	
	//Vector36 CheckG1.CheckPalmNormValue = new Vector36();
	
	
	
	}
	
	void Update ()
	{
		Frame frame = provider.CurrentFrame;
		List<Hand> hands = frame.Hands;		
        for (int h = 0; h < hands.Count; h++)
        {
            Hand hand = hands[h];			
		  if (hand.IsRight)
            {
				hB = hand.PalmPosition;
				
				
				List<Finger> fingers = hand.Fingers;
				foreach(Finger finger in fingers){
					
					
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
					
			}
		
	    }
		CheckG1.id=1d;
		CheckG1.name="someName";
	
		CheckG1.CheckPalmNormValue.x=(double)hB.x;	
		CheckG1.CheckPalmNormValue.y = (double)hB.y;
		CheckG1.CheckPalmNormValue.z =(double)hB.z;
	
		CheckG1.CheckThumb=(double)rast0;
		CheckG1.CheckIndex=(double)rast1;
		CheckG1.CheckMiddle=(double)rast2;
		CheckG1.CheckRing=(double)rast3;
		CheckG1.CheckPinky=(double)rast4;
	
		savingToJ = JsonMapper.ToJson(CheckG1);
		//Debug.Log(savingToJ);
		if (Input.GetKey(KeyCode.W))
		{
			
        File.WriteAllText(Application.dataPath + "/OneGesture.json",savingToJ.ToString());
        Debug.Log("yo");
            
		}
	}
}
