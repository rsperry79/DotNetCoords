DotNetCoords PCL
===================


This version of DotNetCoords is a C# 4.5 Portable Class Library for targeting multiple devices. It was written to make a Xamarin App to assist Search and Rescue Teams and as such it needed to be turned into a PCL library and have some basic tests written for it.  This branch is also to include basic documentation in GitHub so its abilities can be found via search.

Without the work of the original authors this version would not have been possible. If you wish to support this please support the project itself, or one of the original authors.


This version has Regex string detections added
----------

Uses
-------------

 -  Calculate Distance between points.
 
 - Convert from one map format to another.
	 - USNG / MGRS 
	 - LatLon
	 - ECEF
	 - OS
	 - UTM
	 - Irish
 - Convert from one map datum to another .
	 - ED50
	 - ETRF89
	 - Ireland 1965
	 - OSGB36
	 - WGS84
	 -   Nad27
		 - Multiple variants

Example usage 
-------------


```
using System;
using DotNetCoords;

namespace TestDotNetCoords
{
  class Program
  {
    static void Main(string[] args)
    {
      // create an OS grid reference object
      OSRef osRef = new OSRef(535598, 182120);
      Console.WriteLine("OS reference is " + osRef.ToString());
      Console.WriteLine("Grid reference is " + osRef.ToSixFigureString());

      LatLng latLng = osRef.ToLatLng();
      Console.WriteLine("Lat/long using OSGB36 datum is " + latLng.ToString());

      latLng.ToWGS84();
      Console.WriteLine("Lat/long using WGS84 datum is " + latLng.ToString());

      MGRSRef mgrsRef = latLng.ToMGRSRef();
      Console.WriteLine("MGRS reference is " + mgrsRef.ToString());

      UTMRef utmRef = mgrsRef.ToUTMRef();
      Console.WriteLine("UTM reference is " + utmRef.ToString());

      Console.ReadLine();
    }
  }
}
```

History
-------------

This branch is an offshoot of  **Chris Bell's** [1] C# implementation of **JCoords**[2]. JCoords  was based upon PHPcoord v2.2.

----------


Changes
-------------

 - Migrated to a PCL
 - Written Tests for parts I needed to verify in the MS Test framework.
 - Changed UTM to LatLon accuracy to reflect 8 digit format, accuracy needed for cm resolution from the original 12 digit format. 

----------





Licensing 
-------------------
This software product is available under the GNU General Public License (GPL) which permits the use of this product subject to a number of conditions as described in the license. 








  [1]: http://www.doogal.co.uk/dotnetcoords.php "DotNetCoordsOriginal"
  [2]: http://www.jstott.me.uk/jcoord/ "JCoords"
