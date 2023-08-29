# Inventory Management System

## Prerequisite to run the WebAPI

    ### Tool/IDE
        1. Visual Studio
        2. SQL Server -2020
        3. Postman (Installation is not MUST - web version can be used) for .Net Core WebAPI testing 
        
    ### Packages required
        1. Dapper
        2. Microsoft.Data.SQLClient
        3. Microsoft.ASPNetCore.App
        4. Microsoft.NetCore.App
        5. Coverlet.Collector
        6. Microsoft.Net.Test.SDK
        7. Xunit
        8. XUnit.runner.visualstudio

## How to setup the database 
Admin log into SQL.
Open this file in SQL: "~\InventoryMgmt\InventroyManagement.sql"
Parse & Execute above file.
**OUTPUT:**
New database created with name : InventroyManagement
New User created with name : IMUser, Password : IMPwd, Access rights assigned : Owner
Newly created - All required Master and Transactional Tables with relationships defined, Stored Procedures
PS: This script do not contain any dummy data for reference

## How to set up 'Inventory Management' solution in development environment
Get Inventory Management code from git URL: "https://github.com/KosGoswami/InventoryMgmt"
Open InventoryMgmt.sln using visual studio
Build & run InventoryMgmt.sln
**OUTPUT:**
Build successful
Running WepAPI, ready to be tested/verified

## Additional Parameter added - requestID
This is a string type
To be passed for:
    1. All GET request: As querystring in the URL. Ex - /beer/1?requestid=IPFWebApp
    2. All PUT & POST request: To set in respective object having property as RequestId
This is a MUST/Required parameter for a handshake between other client aaplication/service/etc,
and this webAPI; who is consuming this webAPI.
Also, we are logging this requestId for error tracing.

## Not considered
Maintaining users at database level
For ALL POST & PUT request: We are setting CreatedBy or UpdatedBy fields respectivily, as int Type (Not NULL)
Assumption:
1. User details being mantained & handled by the requesting client aaplication/service/etc.
2. Client is sending a valid UserId to be stored in respective InventoryManagement database columns under specific tables.
   
## Actions for all GET/PUT/POST requests
PS: Have used POSTMAN to call below actions

    **Beer**
        Get: Beer/2?requestid=IPFWebApp
             Beer?requestid=IPFWebApp
        Post : Beer
        Put : Beer        
 
    **Bar**
        Get: Bar/2?requestid=IPFWebApp
             Bar?requestid=IPFWebApp
             Post : Bar
             Put : Bar

    **Brewery**
        Get: Brewery/2?requestid=IPFWebApp
             Brewery?requestid=IPFWebApp
             Post : Brewery
             Post : Brewery         

    **BeerBar**
        Get: Bar/Beer/2?requestid=IPFWebApp
             Bar/Beer?requestid=IPFWebApp
             Post : BeerBar
             Post : BeerBar

    **BeerBrewery**
        Get: Brewery/Beer/2?requestid=IPFWebApp
             Brewery/Beer?requestid=IPFWebApp         
             Post : BeerBrewery
             Post : BeerBrewery

## Request & Response Object for reference
Kindly refer "~\InventoryMgmt\IM_RequestResponse.docx"

## Any Help Needed
Please contact me




