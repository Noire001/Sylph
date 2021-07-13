# Sylph

Sylph is an ASP.NET web service that parses tropical cyclone CSV data from [NOAA IBTrACS](https://www.ncdc.noaa.gov/ibtracs/ "NOAA IBTrACS"), stores it in a MongoDB database and provides a RESTful API to access it. Sylph is highly configurable, but does not provide real-time data, as IBTrACS updates twice weekly.  <br>
Sylph is free software licensed under AGPL-3.0-or-later. <br>
**Currently WIP**

#### Table of Contents

* [Compilation](#Compilation)
  * [Prerequisites](#Prerequisites)
  * [Configuration](#Configuration)
  * [Build](#Build)
  * [Deploy](#Deploy)
  
* [Usage](#Usage)
  * [API Endpoint](#api-endpoint)
  * [Queries](#queries)
  * [Response Properties](#response-properties)
  


## Compilation
### Prerequisites
[.NET  5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0 ".NET  5 SDK")
### Configuration
After cloning this repository, create an `appsettings.json` file at the project root folder with the following:
```json
{
  "HurricaneDatabaseSettings": {
    "HurricaneCollectionName": "",
    "DatabaseName": "",
    "ConnectionString": ""
  }
}
```
Fill in the MongoDB details. The database and collection have to be created manually. Sylph does not create them and will fail if they do not exist.

See: [Create a Database in MongoDB](https://www.mongodb.com/basics/create-database) 
### Build
Simply build using your IDE (Visual Studio, Rider) or dotnet. You must create your own launch profile for local testing, if the one included doesn't work.
<br>The following NuGet packages are required:

* `MongoDB.Driver` 2.11.3+
* `Quartz` 3.2.2+
* `Quartz.AspNetCore` 3.2.2+

These should be installed automatically.

### Deploy
Your deployment environment must support ASP.NET Core 5.0, like [Azure](https://azure.microsoft.com/ "Azure"). 
You may be able to use an older version if you configure it. <br>
The parsed data is small enough to fit in a free [MongoDB Atlas](https://www.mongodb.com/cloud/atlas "MongoDB Atlas") instance.

## Usage
### API Endpoint
`https://{host}/hurricane/api`

### Queries
| parameter | type | default | description | unit |
|------|------|------|------|------|
| startdate | long/int64   | 0 | Limit to tropical cyclones **started after** the specified time. <br> Effectively datapoints.0.time >= startdate | UNIX seconds |
| enddate | long/int64 | Int64.MaxValue | Limit to tropical cyclones **started before** the specified time. <br> Effectively datapoints.0.time <= enddate | UNIX seconds |
| maxspeed | ushort/int32  | Int32.MaxValue | Limit to tropical cyclones whose maximum speed has not exceeded this value | Knots |
| active | short/int16 | 2 | Limit to tropical cyclones that are active, inactive or both. <br> `active=1` returns active tropical cyclones, `active=0` returns inactive ones. <br> Any other value returns both active and inactive.|
| name | string | *empty* | Limit to tropical cyclones whose `name` field contains the specified string. |
| sort | string | desc | sort tropical cyclones by ascending (`asc`) or descending (`desc`) order based on their ID (effectively starting time)
#### Sample Query
`https://{host}/hurricane/api?startdate=1604447200&enddate=1608847400&active=1`

<details>
<summary>Response</summary>

```json
[
  {
    "id": "2020360S16057",
    "name": "CHALANE",
    "active": true,
    "maxSpeed": 18,
    "firstActive": 1608847200,
    "lastActive": 1609279200,
    "datapoints": [
      {
        "lat": -15.8,
        "lon": 56.5,
        "time": 1608847200,
        "stormSpeed": 9,
        "windSpeed": 35,
        "cat": 0,
        "dist2land": 492
      },
      {
        "lat": -15.8703,
        "lon": 56.0302,
        "time": 1608858000,
        "stormSpeed": 9,
        "windSpeed": 35,
        "cat": 0,
        "dist2land": 495
      },
      {
        "lat": -15.9,
        "lon": 55.6,
        "time": 1608868800,
        "stormSpeed": 8,
        "windSpeed": 35,
        "cat": 0,
        "dist2land": 511
      },
      {
        "lat": -15.8772,
        "lon": 55.2222,
        "time": 1608879600,
        "stormSpeed": 8,
        "windSpeed": 35,
        "cat": 0,
        "dist2land": 506
      },
      {
        "lat": -15.9,
        "lon": 54.8,
        "time": 1608890400,
        "stormSpeed": 10,
        "windSpeed": 35,
        "cat": 0,
        "dist2land": 463
      },
      {
        "lat": -16.1229,
        "lon": 54.1781,
        "time": 1608901200,
        "stormSpeed": 12,
        "windSpeed": 35,
        "cat": 0,
        "dist2land": 403
      },
      {
        "lat": -16.3,
        "lon": 53.6,
        "time": 1608912000,
        "stormSpeed": 8,
        "windSpeed": 35,
        "cat": 0,
        "dist2land": 346
      },
      {
        "lat": -16.1721,
        "lon": 53.3122,
        "time": 1608922800,
        "stormSpeed": 6,
        "windSpeed": 35,
        "cat": 0,
        "dist2land": 312
      },
      {
        "lat": -16,
        "lon": 53.1,
        "time": 1608933600,
        "stormSpeed": 5,
        "windSpeed": 35,
        "cat": 0,
        "dist2land": 286
      },
      {
        "lat": -15.9896,
        "lon": 52.8247,
        "time": 1608944400,
        "stormSpeed": 7,
        "windSpeed": 35,
        "cat": 0,
        "dist2land": 255
      },
      {
        "lat": -16.1,
        "lon": 52.4,
        "time": 1608955200,
        "stormSpeed": 11,
        "windSpeed": 35,
        "cat": 0,
        "dist2land": 217
      },
      {
        "lat": -16.298,
        "lon": 51.6876,
        "time": 1608966000,
        "stormSpeed": 15,
        "windSpeed": 35,
        "cat": 0,
        "dist2land": 159
      },
      {
        "lat": -16.6,
        "lon": 50.9,
        "time": 1608976800,
        "stormSpeed": 15,
        "windSpeed": 35,
        "cat": 0,
        "dist2land": 100
      },
      {
        "lat": -16.9997,
        "lon": 50.2627,
        "time": 1608987600,
        "stormSpeed": 14,
        "windSpeed": 35,
        "cat": 0,
        "dist2land": 57
      },
      {
        "lat": -17.4,
        "lon": 49.7,
        "time": 1608998400,
        "stormSpeed": 12,
        "windSpeed": 35,
        "cat": 0,
        "dist2land": 31
      },
      {
        "lat": -17.6947,
        "lon": 49.1923,
        "time": 1609009200,
        "stormSpeed": 11,
        "windSpeed": 32,
        "cat": -1,
        "dist2land": 0
      },
      {
        "lat": -17.9,
        "lon": 48.7,
        "time": 1609020000,
        "stormSpeed": 10,
        "windSpeed": 29,
        "cat": -1,
        "dist2land": 0
      },
      {
        "lat": -18.015,
        "lon": 48.1497,
        "time": 1609030800,
        "stormSpeed": 11,
        "windSpeed": 27,
        "cat": -1,
        "dist2land": 0
      },
      {
        "lat": -18.1,
        "lon": 47.6,
        "time": 1609041600,
        "stormSpeed": 10,
        "windSpeed": 25,
        "cat": -1,
        "dist2land": 0
      },
      {
        "lat": -18.2048,
        "lon": 47.0921,
        "time": 1609052400,
        "stormSpeed": 10,
        "windSpeed": 25,
        "cat": -1,
        "dist2land": 0
      },
      {
        "lat": -18.4,
        "lon": 46.6,
        "time": 1609063200,
        "stormSpeed": 11,
        "windSpeed": 25,
        "cat": -1,
        "dist2land": 0
      },
      {
        "lat": -18.8,
        "lon": 46.078,
        "time": 1609074000,
        "stormSpeed": 12,
        "windSpeed": 27,
        "cat": -1,
        "dist2land": 0
      },
      {
        "lat": -19.2,
        "lon": 45.6,
        "time": 1609084800,
        "stormSpeed": 10,
        "windSpeed": 29,
        "cat": -1,
        "dist2land": 0
      },
      {
        "lat": -19.372,
        "lon": 45.2643,
        "time": 1609095600,
        "stormSpeed": 7,
        "windSpeed": 29,
        "cat": -1,
        "dist2land": 0
      },
      {
        "lat": -19.5,
        "lon": 44.9,
        "time": 1609106400,
        "stormSpeed": 10,
        "windSpeed": 29,
        "cat": -1,
        "dist2land": 0
      },
      {
        "lat": -19.7627,
        "lon": 44.3436,
        "time": 1609117200,
        "stormSpeed": 12,
        "windSpeed": 32,
        "cat": -1,
        "dist2land": 10
      },
      {
        "lat": -20,
        "lon": 43.7,
        "time": 1609128000,
        "stormSpeed": 13,
        "windSpeed": 35,
        "cat": 0,
        "dist2land": 69
      },
      {
        "lat": -20.1136,
        "lon": 43.0336,
        "time": 1609138800,
        "stormSpeed": 12,
        "windSpeed": 36,
        "cat": 0,
        "dist2land": 122
      },
      {
        "lat": -20.1321,
        "lon": 42.4238,
        "time": 1609149600,
        "stormSpeed": 10,
        "windSpeed": 37,
        "cat": 0,
        "dist2land": 174
      },
      {
        "lat": -20.0883,
        "lon": 41.9322,
        "time": 1609160400,
        "stormSpeed": 8,
        "windSpeed": 38,
        "cat": 0,
        "dist2land": 220
      },
      {
        "lat": -20,
        "lon": 41.6,
        "time": 1609171200,
        "stormSpeed": 3,
        "windSpeed": 39,
        "cat": 0,
        "dist2land": 252
      },
      {
        "lat": -19.9043,
        "lon": 41.666,
        "time": 1609182000,
        "stormSpeed": 2,
        "windSpeed": 42,
        "cat": 0,
        "dist2land": 250
      },
      {
        "lat": -19.8,
        "lon": 41.6,
        "time": 1609192800,
        "stormSpeed": 8,
        "windSpeed": 45,
        "cat": 0,
        "dist2land": 264
      },
      {
        "lat": -19.6861,
        "lon": 40.8546,
        "time": 1609203600,
        "stormSpeed": 16,
        "windSpeed": 45,
        "cat": 0,
        "dist2land": 330
      },
      {
        "lat": -19.6,
        "lon": 39.9,
        "time": 1609214400,
        "stormSpeed": 16,
        "windSpeed": 45,
        "cat": 0,
        "dist2land": 302
      },
      {
        "lat": -19.5774,
        "lon": 39.1327,
        "time": 1609225200,
        "stormSpeed": 13,
        "windSpeed": 49,
        "cat": 0,
        "dist2land": 277
      },
      {
        "lat": -19.6,
        "lon": 38.5,
        "time": 1609236000,
        "stormSpeed": 10,
        "windSpeed": 54,
        "cat": 0,
        "dist2land": 229
      },
      {
        "lat": -19.6865,
        "lon": 38.1155,
        "time": 1609246800,
        "stormSpeed": 8,
        "windSpeed": 57,
        "cat": 0,
        "dist2land": 208
      },
      {
        "lat": -19.7,
        "lon": 37.7,
        "time": 1609257600,
        "stormSpeed": 11,
        "windSpeed": 60,
        "cat": 0,
        "dist2land": 172
      },
      {
        "lat": -19.514,
        "lon": 36.9938,
        "time": 1609268400,
        "stormSpeed": 16,
        "windSpeed": 60,
        "cat": 0,
        "dist2land": 99
      },
      {
        "lat": -19.2,
        "lon": 36.1,
        "time": 1609279200,
        "stormSpeed": 18,
        "windSpeed": 60,
        "cat": 0,
        "dist2land": 30
      }
    ]
  }
]
```
</details>

### Response Properties


| property | type | unit | IBTrACS Column | description
|------|------|------|------|------|
| id | string | n/a | SID | Storm Identifier
| name | string | n/a | NAME | A name provided by the agency. These can change over time.
| active | boolean | n/a | n/a | Whether the storm is currently active or not.
| maxSpeed | int | Knots | n/a | The maximum speed the storm has reached in its lifetime.
| firstActive | long | UNIX seconds | ISO_TIME | The first time this storm was observed to be active.
| lastActive | long | UNIX seconds | ISO_TIME | The last time this storm was observed to be active.
| datapoints | array | n/a | n/a | An array which holds some of the storm's data at different points in time

* #### datapoints

  | property | type | unit | IBTrACS Column | description
  |------|------|------|------|------|
  | lat | float | degrees north | LAT | The latitude at this timestamp.
  | lon | float | degrees east | LON | The longitude at this timestamp.
  | time | long | UNIX seconds | ISO_TIME | The UNIX timestamp of this data point.
  | stormSpeed | int | Knots | STORM_SPEED | The storm speed at this timestamp.
  | windSpeed | int | Knots | USA_WIND | The wind speed at this timestamp.
  | cat | int | Knots | STORM_SPEED | The Saffir-Simpson scale category of this storm at this timestamp. <br> -5 = Unknown [XX]<br>-4 = Post-tropical [EX, ET, PT]<br />-3 = Miscellaneous disturbances [WV, LO, DB, DS, IN, MD]<br />-2 = Subtropical [SS, SD]<br />Tropical systems classified based on wind speeds [TD, TS, HU, TY,, TC, ST, HR]<br />-1 = Tropical depression (W<34)<br />0 = Tropical storm [34<W<64]<br />1 = Category 1 [64<=W<83]<br />2 = Category 2 [83<=W<96]<br />3 = Category 3 [96<=W<113]<br />4 = Category 4 [113<=W<137]<br />5 = Category 5 [W >= 137] |
  | dist2land | int |kilometers | DIST2LAND | Distance to land from the current position. Includes all continents and islands larger than 1400 km^2.

(1 Knot = 1.852km/h | 1.151mph)


For information on IBTrACS columns, [click here](https://www.ncdc.noaa.gov/ibtracs/pdf/IBTrACS_version4_Technical_Details.pdf).
