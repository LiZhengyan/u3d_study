using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unibill;
using System;



public class unibi_init : MonoBehaviour {

	List<ProductDefinition> wps=new List<ProductDefinition>();
	// Use this for initialization
	void Start () {

		Unibiller.onBillerReady += onBillerReady;
//		Unibiller.onTransactionsRestored += onTransactionsRestored;
//		Unibiller.onPurchaseCancelled += onCancelled;
//		Unibiller.onPurchaseFailed += onFailed;
//		Unibiller.onPurchaseCompleteEvent += onPurchased;
//		Unibiller.onPurchaseDeferred += onDeferred;
//		Unibiller.onDownloadProgressedEvent += (item, progress) => {
//			Debug.Log(item + " " + progress);
//		};
//
////		wps.Add ( new ProductDefinition ("com.apugame.lmsj.d60",  PurchaseType.Consumable));
////		wps.Add ( new ProductDefinition ("com.apugame.lmsj.d120", PurchaseType.Consumable));
////		wps.Add ( new ProductDefinition ("com.apugame.lmsj.d180", PurchaseType.Consumable));
//
		var prods = new List<ProductDefinition>() {
			new ProductDefinition(
				"zuan120",
				PurchaseType.Consumable
				),
			new ProductDefinition(
				"zuan180",
				PurchaseType.NonConsumable
				)
		};

		Unibiller.Initialise(prods);

//		Unibiller.onBillerReady += (state) => {
//			if (UnibillState.SUCCESS == state) {
//				UnityEngine.Debug.Log("onBillerReady:" + state);
//				Unibiller.initiatePurchase(Unibiller.AllPurchasableItems[0]);
//			}
//			UnityEngine.Debug.Log("onBillerReady:" + state);
//		};
//		Unibiller.Initialise();


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void onBillerReady(UnibillState state) {
		UnityEngine.Debug.Log("onBillerReady:" + state);
	}

	void OnGUI(){
		if(GUI.Button(new Rect(50,250,200,130), "Button1"))  
		{
			var prods = new List<ProductDefinition>() {
				new ProductDefinition(
					"com.apugame.lmsj.d120",
					PurchaseType.Consumable
					),
				new ProductDefinition(
					"com.apugame.lmsj.d180r",
					PurchaseType.NonConsumable
					)
			};
			
			Unibiller.Initialise(prods);
		}
	}

//	public class ProductDefinition {
//		public string PlatformSpecificId { get; }
//		public PurchaseType Type { get; }
//	}

}
