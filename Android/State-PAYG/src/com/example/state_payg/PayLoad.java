package com.example.state_payg;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import android.location.Location;

public class PayLoad {
		
	public String UserID;
	public String DeviceID;
	public String VehicleID;
	public Date SendTime;
	public List<Point> Points;
	
	public PayLoad(Location...location)
	{
		Points = new ArrayList<Point>();
		for (Location insertLocation : location) {
			Points.add(new Point(insertLocation));			
		}
	}	
	
	public class Point{
		public double Lon;
		public double Lat;
		public double Speed;
		public Date UTCTime;
		
		public Point(Location location)
		{
			Lon 	= location.getLongitude();
			Lat 	= location.getLatitude();
			Speed 	= location.getSpeed();
			UTCTime = new Date();
		}
	}
}
