package com.example.state_payg;

import java.io.UnsupportedEncodingException;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.util.EntityUtils;
import com.google.gson.Gson;
import android.location.Location;
import android.os.AsyncTask;

public class ConnectionTask extends AsyncTask<Location, Void, Void> {

	private List<Location> 	locationList	= new ArrayList<Location>();
	private static final String METHOD_URL	= "http://telematicsserver.azurewebsites.net/api/geo";
	private String _imei 					= "";
	
	public ConnectionTask(String imei){
		_imei = imei;
	}
	
	protected Void doInBackground(Location... location) {	
		locationList.add(location[0]);		
		
		//If we have more than 10 location points (10 minutes -> 100 meters) then send an update with this information
		if(locationList.size() > 10){
			String result = callWebMethod(location);
			System.out.println(result);
			locationList.clear();
		}
		 
		return null;
	}
	
	private String callWebMethod(Location... location){
		Gson gson 				= new Gson();
		String responseString 	= "";
		PayLoad payLoad 		= new PayLoad(location);
		payLoad.SendTime 		= new Date();
		payLoad.DeviceID 		= _imei;
		payLoad.UserID 			= "1";
		payLoad.VehicleID 		= "1";
		HttpClient httpClient 	= new DefaultHttpClient();
		HttpPost httpPost 		= new HttpPost(METHOD_URL);
		String jsonPayload		= gson.toJson(payLoad);		
	    
		try {
			httpPost.setHeader("Accept", "application/json");
		    httpPost.setHeader("Content-type", "application/json");
			httpPost.setEntity(new StringEntity(jsonPayload));
		} catch (UnsupportedEncodingException e) {			
			e.printStackTrace();
		}
		
		try
		{
			HttpResponse response 	= httpClient.execute(httpPost);
			HttpEntity entity 		= response.getEntity();
			
			if(entity!=null){
				responseString 		= EntityUtils.toString(entity);
			}
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}		
		
		return responseString;
	}		
}
