# GPS_Based_AR

# Introduction

* This is a basic GPS based android app in Unity using Vuforia SDK
* This app keeps track of our current GPS coordinates and when we are close to a specific location an object gets augmented
* In the app you can add places of your choice (For Example - Home,Office) and its GPS coordinates in the location script
* The distance at which you wish the object to augment can also be entered in the script
* This app is more effective when the device (your phone) contains a gyroscope


# Instructions

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
    


## Thanks

