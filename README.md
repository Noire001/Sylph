# hurricaneapi
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/ad5fc727d6254510a068dc1cf0848a99)](https://www.codacy.com?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=Emoclaw/hurricaneapi&amp;utm_campaign=Badge_Grade)

hurricaneapi is an ASP.NET web service that parses tropical cyclone CSV data from [NOAA IBTrACS](https://www.ncdc.noaa.gov/ibtracs/ "NOAA IBTrACS"), stores it in a MongoDB database and provides a convenient RESTful API to access it. <br> **Currently WIP**

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
[NET  5.0.1 SDK](https://dotnet.microsoft.com/download/dotnet/5.0 "NET  5.0.1 SDK")
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
You must enter the above MongoDB details correctly between the empty quotation marks. The database and collection have to be created manually. hurricaneapi does not create them and will fail if they do not exist.

See: [Create a Database in MongoDB](https://www.mongodb.com/basics/create-database) 
### Build
Simply build using your IDE (Visual Studio, Rider). You must create your own launch profile for local testing. Here's a sample `launchSettings.json` used for the Kestrel server in Rider:
```json
{
  "profiles": {
    "hurricaneapi": {
      "commandName": "Project",
      "launchBrowser": true,
      "applicationUrl": "https://localhost:5001;http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
       }
     }
   }
 }
```
### Deploy
Your deployment environment must support ASP.NET Core 5.0, like [Azure](https://azure.microsoft.com/ "Azure"). 
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
| sort | string | desc | sort tropical cyclones by asceding (`asc`) or descending (`desc`) order based on their ID (effectively starting time)
#### Sample Query
`https://{host}/hurricane/api?startdate=1604447200&enddate=1608847400&active=1`

<details>
<summary>Response</summary>

```json
[
  {
    "id": "2020360S16057",
    "name": "CHALANE",
    "datapoints": [
      {
        "lat": -15.8,
        "lon": 56.5,
        "time": 1608847200,
        "speed": 9
      },
      {
        "lat": -15.8703,
        "lon": 56.0302,
        "time": 1608858000,
        "speed": 9
      },
      {
        "lat": -15.9,
        "lon": 55.6,
        "time": 1608868800,
        "speed": 8
      },
      {
        "lat": -15.8772,
        "lon": 55.2222,
        "time": 1608879600,
        "speed": 8
      },
      {
        "lat": -15.9,
        "lon": 54.8,
        "time": 1608890400,
        "speed": 10
      },
      {
        "lat": -16.1229,
        "lon": 54.1781,
        "time": 1608901200,
        "speed": 12
      },
      {
        "lat": -16.3,
        "lon": 53.6,
        "time": 1608912000,
        "speed": 8
      },
      {
        "lat": -16.1721,
        "lon": 53.3122,
        "time": 1608922800,
        "speed": 6
      },
      {
        "lat": -16,
        "lon": 53.1,
        "time": 1608933600,
        "speed": 5
      },
      {
        "lat": -15.9896,
        "lon": 52.8247,
        "time": 1608944400,
        "speed": 7
      },
      {
        "lat": -16.1,
        "lon": 52.4,
        "time": 1608955200,
        "speed": 11
      },
      {
        "lat": -16.298,
        "lon": 51.6876,
        "time": 1608966000,
        "speed": 15
      },
      {
        "lat": -16.6,
        "lon": 50.9,
        "time": 1608976800,
        "speed": 15
      },
      {
        "lat": -16.9997,
        "lon": 50.2627,
        "time": 1608987600,
        "speed": 14
      },
      {
        "lat": -17.4,
        "lon": 49.7,
        "time": 1608998400,
        "speed": 12
      },
      {
        "lat": -17.6947,
        "lon": 49.1923,
        "time": 1609009200,
        "speed": 11
      },
      {
        "lat": -17.9,
        "lon": 48.7,
        "time": 1609020000,
        "speed": 10
      },
      {
        "lat": -18.015,
        "lon": 48.1497,
        "time": 1609030800,
        "speed": 11
      },
      {
        "lat": -18.1,
        "lon": 47.6,
        "time": 1609041600,
        "speed": 10
      },
      {
        "lat": -18.2048,
        "lon": 47.0921,
        "time": 1609052400,
        "speed": 10
      },
      {
        "lat": -18.4,
        "lon": 46.6,
        "time": 1609063200,
        "speed": 11
      },
      {
        "lat": -18.8,
        "lon": 46.078,
        "time": 1609074000,
        "speed": 12
      },
      {
        "lat": -19.2,
        "lon": 45.6,
        "time": 1609084800,
        "speed": 10
      },
      {
        "lat": -19.372,
        "lon": 45.2643,
        "time": 1609095600,
        "speed": 7
      },
      {
        "lat": -19.5,
        "lon": 44.9,
        "time": 1609106400,
        "speed": 10
      },
      {
        "lat": -19.7627,
        "lon": 44.3436,
        "time": 1609117200,
        "speed": 12
      },
      {
        "lat": -20,
        "lon": 43.7,
        "time": 1609128000,
        "speed": 13
      },
      {
        "lat": -20.1136,
        "lon": 43.0336,
        "time": 1609138800,
        "speed": 12
      },
      {
        "lat": -20.1321,
        "lon": 42.4238,
        "time": 1609149600,
        "speed": 10
      },
      {
        "lat": -20.0883,
        "lon": 41.9322,
        "time": 1609160400,
        "speed": 8
      },
      {
        "lat": -20,
        "lon": 41.6,
        "time": 1609171200,
        "speed": 3
      },
      {
        "lat": -19.9043,
        "lon": 41.666,
        "time": 1609182000,
        "speed": 2
      },
      {
        "lat": -19.8,
        "lon": 41.6,
        "time": 1609192800,
        "speed": 8
      },
      {
        "lat": -19.6861,
        "lon": 40.8546,
        "time": 1609203600,
        "speed": 16
      },
      {
        "lat": -19.6,
        "lon": 39.9,
        "time": 1609214400,
        "speed": 16
      },
      {
        "lat": -19.5774,
        "lon": 39.1327,
        "time": 1609225200,
        "speed": 13
      },
      {
        "lat": -19.6,
        "lon": 38.5,
        "time": 1609236000,
        "speed": 10
      },
      {
        "lat": -19.6865,
        "lon": 38.1155,
        "time": 1609246800,
        "speed": 8
      },
      {
        "lat": -19.7,
        "lon": 37.7,
        "time": 1609257600,
        "speed": 11
      }
    ],
    "active": true,
    "maxSpeed": 16,
    "averageSpeed": 10.076923
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
| averageSpeed | float | Knots | n/a | The average speed of the storm throughout its lifetime.
| datapoints | array | n/a | n/a | An array which holds some of the storm's data at different points in time

* #### datapoints

  | property | type | unit | IBTrACS Column | description
  |------|------|------|------|------|
  | lat | float | degrees north | LAT | The latitude at this timestamp.
  | lon | float | degrees east | LON | The longitude at this timestamp.
  | time | long | UNIX seconds | ISO_TIME (converted) | The UNIX timestamp of this data point.
  | speed | int | Knots | STORM_SPEED | The speed at this timestamp.

(1 Knot = 1.852km/h | 1.151mph)


For information on IBTrACS columns, [click here](https://www.ncdc.noaa.gov/ibtracs/pdf/IBTrACS_version4_Technical_Details.pdf).
