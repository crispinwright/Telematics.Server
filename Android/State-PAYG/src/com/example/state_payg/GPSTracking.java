package com.example.state_payg;

import java.util.ArrayList;
import java.util.List;

import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.os.IBinder;

import com.example.state_payg.Interfaces.GPSTrackingListener;

public class GPSTracking extends Service implements LocationListener{

	List<GPSTrackingListener> gpsTrackingListeners = new ArrayList<GPSTrackingListener>();
	protected LocationManager locationManager;	
	
	private final Context _context;
	private static final long MIN_DISTANCE_CHANGE_FOR_UPDATES = 10; // 10 meters 
	private static final long MIN_TIME_BW_UPDATES = 1000 * 60 * 1; // 1 minute	
	
	public GPSTracking(Context context){
		_context 				= context;
		locationManager 		= (LocationManager)_context.getSystemService(LOCATION_SERVICE);
		locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER
													,MIN_TIME_BW_UPDATES
													,MIN_DISTANCE_CHANGE_FOR_UPDATES
													,this);
	}	
	
	public void addGPSTrackingListener(GPSTrackingListener toListen)
	{
		gpsTrackingListeners.add(toListen);
	}
	
	public boolean IsGPSEnabled()
	{
		return locationManager !=null && locationManager.isProviderEnabled(LocationManager.GPS_PROVIDER);
	}
	
	public Location getLastLocation() {
		return locationManager.getLastKnownLocation(LocationManager.GPS_PROVIDER);
	}
	
	@Override
 	public void onLocationChanged(Location arg0) {

		for(GPSTrackingListener listener : gpsTrackingListeners){
			listener.onChangeLocation(arg0);
		}
		
	}

	@Override
	public void onProviderDisabled(String arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onProviderEnabled(String arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onStatusChanged(String arg0, int arg1, Bundle arg2) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public IBinder onBind(Intent arg0) {
		// TODO Auto-generated method stub
		return null;
	}	
	
}
