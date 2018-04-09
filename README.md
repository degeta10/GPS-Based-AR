# GPS_Based_AR
---
# A new feature has been added. Different objects in different locations can be augmented.
---
# Introduction

* This is a basic GPS based android app in Unity using Vuforia SDK
* This app keeps track of our current GPS coordinates and when we are close to a specific location an object gets augmented
* In the app you can add places of your choice (For Example - Home,Office) and its GPS coordinates in the location script
* The distance at which you wish the object to augment can also be entered in the script
* This app is more effective when the device (your phone) contains a gyroscope

---
# Instructions For Single Object in Different Locations
---

#### First : License

    Replace the vuforia license key with yours! 

#### Second: Enter Coordinates and Radius

* Select ARCamera in scene heirarchy,

* In its Inspector, enter your Latitude and Longitude!

* Enter a radius of around 100 for inital testing.

        (Optional)

        You can also Enter coordinates for Multiple Location too,

        First, 
         Enter the name of the places in the list. (in location.cs, line 8)

        Second,
            Go to function Dropdown_IndexChanged() in location.cs.
            Enter originalLatitude and OriginalLongitude for each places in your list according to index.        


#### Third: Build and Test on your phone!

Make sure Location is switched on and its in "Device only" mode! (Most Preferred)

    Wait Like 5-10 seconds for your device to get located.
   
    
---   
# Instructions For Different Objects in Different Locations
---
#### First : License

    Replace the vuforia license key with yours! 
    
#### Second: Open "MultipleObjects" scene    

#### Third: Enter number of models 

* Select ARCamera in scene heirarchy,

* In its Inspector, enter number of models and drag the prefab in heirarchy to the slots

* Enter a radius of around 100 for inital testing.

#### Fourth: Enter coordinates for Multiple Location 

* Enter the name of the places in the list. (in LocationForMultipleObjects.cs, line 8)

* Go to function Dropdown_IndexChanged() in the script,
            Enter originalLatitude and OriginalLongitude for each places in your list according to index.
            
            if adding new location in the list, dont forget to assign the variable "modelno".
            
* Add cases if for any extra location added in Activator() in line 181,
            Just copy-paste the above case's code.

* By default 2 cases for 2 location are already added. 

#### Fifth: Build and Test on your phone!

Make sure Location is switched on and its in "Device only" mode! (Most Preferred)

    Wait Like 5-10 seconds for your device to get located.        


## Thanks
