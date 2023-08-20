# Callsign Toolkit by N1CCK
*A collection of tools for amateur radio operators to look up and display calsign and callbook return information in a standardized manner*

## Basic usage
**Option 1**
```csharp
using CallsignToolkit;
BaseLookup lookup = new HamCallDevLookup("w1aw");
lookup.PerformLookup();
Console.WriteLine(lookup.Serialize()); 
```

**Option 2**:
```csharp
using CallsignToolkit;
BaseLookup lookup = new HamCallDevLookup();
(HamCallDevLookup)lookup.PerformLookup("w1aw");
Console.WriteLine(lookup.Serialize()); 
```

**Option 3**:
```csharp
using CallsignToolkit;
HamCallDevLookup lookup = new("w1aw");
lookup.PerformLookup();
Console.WriteLine(lookup.Serialize());
```

**Option 4**:
```csharp
using CallsignToolkit;
HamCallDevLookup lookup = new();
lookup.PerformLookup("w1aw");
Console.WriteLine(lookup.Serialize());
```

_Example output_:
```json
{
  "License": {
    "DMRID": [
      3109478,
      310938
    ],
    "FRN": 4511143,
    "FileNumber": 9324023,
    "LicenseKey": 780866,
    "GrantDate": "2020-12-08T00:00:00",
    "EffectiveDate": "2020-12-08T00:00:00",
    "ExpirationDate": "2031-02-26T00:00:00",
    "Callsign": "w1aw",
    "LicenseClass": ""
  },
  "AmateurName": {
    "FullName": "ARRL HQ OPERATORS CLUB",
    "FirstName": "ARRL HQ OPERATORS CLUB",
    "MiddleInitial": "",
    "LastName": null,
    "FullNameReverse": ""
  },
  "QSLMethods": {
    "LastLOTWUpload": "0001-01-01T00:00:00",
    "QSLManager": "",
    "UseEQSL": false,
    "UseLOTW": false,
    "UsePaperQSL": false
  },
  "Address": {
    "Address1": "225 MAIN ST",
    "Address2": "",
    "POBoxNumber": "",
    "City": "NEWINGTON",
    "State": "CT",
    "PostalCode": "06111"
  }
}
```

## Base Class
The base class is CallSignToolkit.LookupDetails.  It is not intended to be used directly, as it does not have any lookup methods built in.

### Provided Properties
- **License** : _CallsignToolkit.License_
- **AmateurName** : _CallsignToolkit.Utilities.Name_
- **Address** : _CallsignToolkit.Utilities.Address_

### Provided Method
- **Serialize()** : _string_ - returns a JSON formatted serialization of all public properties on the object
### Abstract Methods
- **PerformLookup()** : _Task_ - Performs a lookup on the callsign and populates the object with the results
- **PerformLookup(string callsign)** : _Task_ - Clears the existing object, then performs a lookup on the passed callsign and populates the object with the results
- **ClearResults()** : _void_ - Clears all results from the object and resets the callsign

## Base Helper Classes
The base helper classes are objects to support serialization and standardization of the lookup properties.  They are contained as objects on the base class, but could be used seperately if needed or desired.

### CallsignToolkit.License
#### Provided Properties
- Callsign : string
- LicenseClass : string
#### Provided Methods
- **License()** : _constructor_
- **License(string callsign)** : _constructor_
- **License(string callsign, string LicenseClass)** : _constructor_
- **IsValidUSCall()** : _bool_ - Uses a regex to validate the callsign is potentially a valid US callsign.  Does not validate against any authoritative database



### CallsignToolkit.Utilities.Address
#### Provided Properties
- **Address1** : _string_
- **Address2** : _string_
- **POBoxNumber** : _string_
- **City** : _string_
- **State** : _string_
- **PostalCode** : _string_
- **Country** : _string_
- **Coordinates** : _CallsignToolkit.Utilities.LatLong_

#### Provided Methods
- **Address()** : _constructor_
- **Address(string address1, string city, string state, string postalCode, string country)** : _constructor_
- **GetCoordinates()** : _CallsignToolkit.Utilities.LatLong_ - uses the API provided by [maps.co](https://maps.co) to convert an address to latitude and longitude.

### CallsignToolkit.Utilities.LatLong
#### Provided Properties
- **Latitude** : _double_
- **Longitude** : _double_

#### Provided Methods
- **LatLong()** : _constructor_
- **LatLong(double latitude, double longitude)** : _constructor_
- **LatLong(string latitude, string longitude)** : _constructor_

### CallsignToolkit.Utilities.Locators
#### Provided Properties
- **GeoCoordinates** : _CallsignToolkit.Utilities.LatLong_
- **GridSquare** : _string_

#### Provided Methods
None yet.

### CallsignToolkit.Utilities.Name
#### Provided Properties
- **FirstName** : _string_
- **LastName** : _string_
- **FullName** : _string_ - If not set directly, returns concatenation of "FirstName LastName"
- **FullNameReverse** : _string_ - readonly; returns concatenation of "LastName, FirstName"

#### Provided Methods
None yet.

## HamCallDevLookup : CallSignToolkit.LookupDetails
Extends the base lookup class to provide a lookup method for the API provided by [HamCall.dev](https://hamcall.dev).  This API is free and loads quickly, but currently provides only US based callsign lookups.

### Extended properties
#### HamCallDevLicense : CallsignToolkit.License
- **DMRID** : _List<int>_ - List of DMR ID's associated with the callsign
- **FRN** : _int_ - The FCC FRN associated with the callsign
- **FileNumber** : _int_ - The ULS File number associated with the callsign
- **LicenseKey** : _int_ - The ULS License Key associated with the callsign
- **GrantDate** : _DateTime_ - The date the license was granted
- **EffectiveDate** : _DateTime_ - The date the last change to the license was effective
- **ExpirationDate** : _DateTime_ - The date the license expires

#### HamCallDevName : CallsignToolkit.Utilities.Name
- **FullName** : _string_ - The full name of the licensee, including middle initial (if available)
- **MiddleInitial** : _string_ - The middle initial of the licensee (if available)

#### HamCallDevQSL : CallsignToolkit.Utilities.QSLMethods
- **LastLOTWUpload** : _DateTime_ - The date of the last upload to LoTW

### Extended Methods
- **HamCallDevLoookup()** : _constructor_
- **HamCallDevLookup(string callsign)** : _constructor_ 

## QRZLookup : CallSignToolkit.LookupDetails
Extends the base lookup class to provide a lookup method for the API provided by [QRZ](http://qrz.com).  This is a paid service, and provides callbook details for most countries offering amateur radio licenses.

### Extended properties
#### QRZLookup : CallsignToolkit.LookupDetails
- **Aliases** : _List<License>_ - List of other callsigns associated with the licensee.
- **ImageURL** : _Uri_ - URL to the QRZ.com image of the licensee
- 
#### QRZAddress : CallsignToolkit.Utilities.Address
- **Attention** : _string_ - Attention to line
- **County** : _string_ - County
- **Country** : _string_ - Full country name
- **EmailAddress** : _MailAddress_ - Email address object. Sets email and display name.
- **WebAddress** : _Uri_ - Web address object.

#### QRZLicense : CallsignToolkit.License
- **GrantDate** : _DateTime_ - The date the license was granted
- **ExpirationDate** : _DateTime_ - The date the license expires
- **ServiceCodes** : _string_ - The FCC service codes associated with the license grant

#### QRZLocators : CallsignToolkit.Utilities.Locators
- **FIPSCode** : _string_ - The FIPS code associated with the county of the callsign address
- **DXCC** : _string_ - The DXCC entity associated with the callsign address
- **TeleAreaCode** : _string_ - The telephone area code associated with the callsign address
- **CQZone** : _string_ - The CQ Zone associated with the callsign address
- **IOTA** : _string_ - The IOTA code associated with the callsign address

#### QRZName : CallsignToolkit.Utilities.Name
- **Nickname** : _string_ - The nickname of the licensee shown on QRZ


## QRZCQLookup : CallSignToolkit.LookupDetails
Not yet implemented.

## Ham365Lookup : CallSignToolkit.LookupDetails
Not yet implemented.

## HamQTHLookup : CallSignToolkit.LookupDetails
Not yet implemented.

# Contributors
This work is licensed under the [RPL v. 1.5](https://opensource.org/license/rpl-1-5/), and welcomes use and upstream fixes.

Thank you to the following for their contributions to this project.
## Primary Author and Maintainer
* Nick Booth (N1CCK)

## Additional Contributions