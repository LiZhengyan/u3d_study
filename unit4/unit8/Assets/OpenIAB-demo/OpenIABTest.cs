/*******************************************************************************
 * Copyright 2012-2014 One Platform Foundation
 *
 *       Licensed under the Apache License, Version 2.0 (the "License");
 *       you may not use this file except in compliance with the License.
 *       You may obtain a copy of the License at
 *
 *           http://www.apache.org/licenses/LICENSE-2.0
 *
 *       Unless required by applicable law or agreed to in writing, software
 *       distributed under the License is distributed on an "AS IS" BASIS,
 *       WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *       See the License for the specific language governing permissions and
 *       limitations under the License.
 ******************************************************************************/
using UnityEngine;
using OnePF;
using System.Collections.Generic;

/**
 * Example of OpenIAB usage
 */ 
public class OpenIABTest : MonoBehaviour
{
//	private const string SKU="com.apugame.lmsj";

	string _label = "";
	bool _isInitialized = false;
	Inventory _inventory = null;

//	private const string SKU25 		= "com.apugame.lmsj.zuan300";
	private const string SKU6 = "com.apugame.lmsj.zuan300";
//	private const string SKU30 		= "com.apugame.lmsj.zuan300";
//	private const string SKU68 		= "com.apugame.lmsj.zuan680";
//	private const string SKU168 	= "com.apugame.lmsj.zuan1680";
//	private const string SKU328 	= "com.apugame.lmsj.zuan3280";
//	private const string SKU648 	= "com.apugame.lmsj.zuan6480";
//	private const string SKU128 	= "com.apugame.lmsj.zuan6480";
//	private const string SKU88 		= "com.apugame.lmsj.zuan1280";

	private void OnEnable ()
	{
		// Listen to all events for illustration purposes
		OpenIABEventManager.billingSupportedEvent += billingSupportedEvent;
		OpenIABEventManager.billingNotSupportedEvent += billingNotSupportedEvent;
		OpenIABEventManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		OpenIABEventManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
		OpenIABEventManager.purchaseSucceededEvent += purchaseSucceededEvent;
		OpenIABEventManager.purchaseFailedEvent += purchaseFailedEvent;
		OpenIABEventManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
		OpenIABEventManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
	}

	private void OnDisable ()
	{
		// Remove all event handlers
		OpenIABEventManager.billingSupportedEvent -= billingSupportedEvent;
		OpenIABEventManager.billingNotSupportedEvent -= billingNotSupportedEvent;
		OpenIABEventManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
		OpenIABEventManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
		OpenIABEventManager.purchaseSucceededEvent -= purchaseSucceededEvent;
		OpenIABEventManager.purchaseFailedEvent -= purchaseFailedEvent;
		OpenIABEventManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
		OpenIABEventManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
	}

	private void Start ()
	{
		// Map skus for different stores       
		OpenIAB.mapSku (SKU6, OpenIAB_Android.STORE_GOOGLE, SKU6);


		OpenIAB.mapSku (SKU6, OpenIAB_iOS.STORE, SKU6);
	}

	const float X_OFFSET = 10.0f;
	const float Y_OFFSET = 10.0f;
	const int SMALL_SCREEN_SIZE = 800;
	const int LARGE_FONT_SIZE = 34;
	const int SMALL_FONT_SIZE = 24;
	const int LARGE_WIDTH = 380;
	const int SMALL_WIDTH = 160;
	const int LARGE_HEIGHT = 100;
	const int SMALL_HEIGHT = 40;
	int _column = 0;
	int _row = 0;

	private bool Button (string text)
	{
		float width = Screen.width / 2.0f - X_OFFSET * 2;
		float height = (Screen.width >= SMALL_SCREEN_SIZE || Screen.height >= SMALL_SCREEN_SIZE) ? LARGE_HEIGHT : SMALL_HEIGHT;
        
		bool click = GUI.Button (new Rect (
            X_OFFSET + _column * X_OFFSET * 2 + _column * width, 
            Y_OFFSET + _row * Y_OFFSET + _row * height, 
            width, height),
            text);

		++_column;
		if (_column > 1) {
			_column = 0;
			++_row;
		}

		return click;
	}

	private void OnGUI ()
	{
		_column = 0;
		_row = 0;
        
		GUI.skin.button.fontSize = (Screen.width >= SMALL_SCREEN_SIZE || Screen.height >= SMALL_SCREEN_SIZE) ? LARGE_FONT_SIZE : SMALL_FONT_SIZE;

		if (Button ("Initialize OpenIAB")) {
			// Application public key
			var googlePublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAt/hZlHtiYki1tMRawAaoP902NWxOt1ydDkv+YYv/uZRzJweU+3EImpSxdwpfDFs6jsgkP16D8vH5wYoBUFnVPWqxsL02dR2T3UDK0PLkHCtHjiOyZHd3VijKMTaL/9md6OWZ1tL4QYQp6G1+pS0DzL5U30X0lbkLhPvGOeHBASjOh4qSQIvUfZRmqAfElVVe1bO+1ZhErHAhF7c0JoN4E8UG3w8tppuiE4t9kHwQVcLOLm6gPuDWF0nZ8A5eDqdbeFgIVo7b/IiWjjRizvh2rmx81maUQSaG7SRY51KA41DPIJwfThSE5bN2b+k1yFa9+3krRpgV37gAFbsYDR4cEwIDAQAB";
//			var googlePublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAt/hZlHtiYki1tMRawAaoP902NWxOt1ydDkv+YYv/uZRzJweU+3EImpSxdwpfDFs6jsgkP16D8vH5wYoBUFnVPWqxsL02dR2T3UDK0PLkHCtHjiOyZHd3VijKMTaL/9md6OWZ1tL4QYQp6G1+pS0DzL5U30X0lbkLhPvGOeHBASjOh4qSQIvUfZRmqAfElVVe1bO+1ZhErHAhF7c0JoN4E8UG3w8tppuiE4t9kHwQVcLOLm6gPuDWF0nZ8A5eDqdbeFgIVo7b/IiWjjRizvh2rmx81maUQSaG7SRY51KA41DPIJwfThSE5bN2b+k1yFa9+3krRpgV37gAFbsYDR4cEwIDAQAB";
//			var googlePublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAhYsZVj5c+OxRxqaRiW92FqhSSnGQPAkNnJ9B4BY1lnrYl/xLp7nXJUuH0YCNv8iGuOz2eYX6pYJhGhYu34f0LQmcQALWI5Dzr8fc3pmkHf4fhPeXw9f6lf7GHk0jYc0zCHAyl0iN/bbFMNeX8W5fKPvSbmmUfi8IpmExu0dVSZ63QgHqOMEVeYSY0uupfMsWUe632aHC8SrVTZp3EbSWVSKqZp1PAjmGDMQ2V0mPeCOBEDrh9ZINAljEguf9y4k3VlAOxyFBPPahoDNCqrueOUQuGs0StxBwzPgXHrDh3pulw/D632pZZokZ4rhmBMDd0l+oah0eCzrC5hYRg2cvvQIDAQAB";
			var options = new Options ();
			options.checkInventoryTimeoutMs = Options.INVENTORY_CHECK_TIMEOUT_MS * 2;
			options.discoveryTimeoutMs = Options.DISCOVER_TIMEOUT_MS * 2;
			options.checkInventory = false;
			options.verifyMode = OptionsVerifyMode.VERIFY_SKIP;
			options.prefferedStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE };
			options.availableStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE };
			options.storeKeys = new Dictionary<string, string> { {OpenIAB_Android.STORE_GOOGLE, googlePublicKey} };
			options.storeSearchStrategy = SearchStrategy.INSTALLER_THEN_BEST_FIT;

			// Transmit options and start the service
			OpenIAB.init (options);
		}

		if (!_isInitialized)
			return;

		if (Button ("Query Inventory")) {
			OpenIAB.queryInventory (new string[] { SKU6 });
		}

		if (Button ("Purchase Product")) {
			OpenIAB.purchaseProduct (SKU6);
		}

		if (Button ("Consume Product")) {
			if (_inventory != null && _inventory.HasPurchase (SKU6))
				OpenIAB.consumeProduct (_inventory.GetPurchase (SKU6));
		}

// Android specific buttons
#if UNITY_ANDROID
        if (Button("Test Purchase"))
        {
            OpenIAB.purchaseProduct("android.test.purchased");
        }

        if (Button("Test Consume"))
        {
            if (_inventory != null && _inventory.HasPurchase("android.test.purchased"))
                OpenIAB.consumeProduct(_inventory.GetPurchase("android.test.purchased"));
        }

        if (Button("Test Item Unavailable"))
        {
            OpenIAB.purchaseProduct("android.test.item_unavailable");
        }

        if (Button("Test Purchase Canceled"))
        {
            OpenIAB.purchaseProduct("android.test.canceled");
        }
#endif
	}

	private void billingSupportedEvent ()
	{
		_isInitialized = true;
		Debug.Log ("billingSupportedEvent");
	}

	private void billingNotSupportedEvent (string error)
	{
		Debug.Log ("billingNotSupportedEvent: " + error);
	}

	private void queryInventorySucceededEvent (Inventory inventory)
	{
		Debug.Log ("queryInventorySucceededEvent: " + inventory);
		if (inventory != null) {
			_label = inventory.ToString ();
			_inventory = inventory;
		}
	}

	private void queryInventoryFailedEvent (string error)
	{
		Debug.Log ("queryInventoryFailedEvent: " + error);
		_label = error;
	}

	private void purchaseSucceededEvent (Purchase purchase)
	{
		Debug.Log ("purchaseSucceededEvent: " + purchase);
		_label = "PURCHASED:" + purchase.ToString ();
	}

	private void purchaseFailedEvent (int errorCode, string errorMessage)
	{
		Debug.Log ("purchaseFailedEvent: " + errorMessage);
		_label = "Purchase Failed: " + errorMessage;
	}

	private void consumePurchaseSucceededEvent (Purchase purchase)
	{
		Debug.Log ("consumePurchaseSucceededEvent: " + purchase);
		_label = "CONSUMED: " + purchase.ToString ();
	}

	private void consumePurchaseFailedEvent (string error)
	{
		Debug.Log ("consumePurchaseFailedEvent: " + error);
		_label = "Consume Failed: " + error;
	}
}