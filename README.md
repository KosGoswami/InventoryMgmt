# Inventory Management System
    
## Tools to be installed (Prerequisite)
    1. Visual Studio
    2. SQL Server -2020
    
## Packages to be downloaded (Prerequisite)
    1. Dapper
    2. Microsoft.Data.SQLClient
    3. Microsoft.ASPNetCore.App
    4. Microsoft.NetCore.App
    5. Coverlet.Collector
    6. Microsoft.Net.Test.SDK
    7. Xunit
    8. XUnit.runner.visualstudio

## How to setup the database 
Create new database : Name : InventroyManagement
create new user : name : IMUser, Password: IMPwd with all the access
Log in using credentials run database script from the folder file name : "InventroyManagement.sql"

## How to set up 'Inventory Management' solution in development environment
Get Inventory Management code from git URL: "https://github.com/KosGoswami/InventoryMgmt"
Open InventoryMgmt.sln using visual studio
Build & run InventoryMgmt.sln for sanity check

## How to verify the action
In Postman send a request to a valid url

    1. Beer
        Get: Beer/2?requestid=123
             Beer?requestid=123
        Post : Beer
        Put : Beer        
 
    2. Bar
        Get: Bar/2?requestid=123
             Bar?requestid=123
             Post : Bar
             Put : Bar

    3. Brewery
        Get: Brewery/2?requestid=123
             Brewery?requestid=123
             Post : Brewery
             Post : Brewery         

    4. BeerBar
        Get: Bar/Beer/2?requestid=123
             Bar/Beer?requestid=123
             Post : BeerBar
             Post : BeerBar

    5. BeerBrewery
        Get: Brewery/Beer/2?requestid=123
             Brewery/Beer?requestid=123         
             Post : BeerBrewery
             Post : BeerBrewery


For more inforamtion regarding request and response, kindly refer "IM_RequestResponse.docx"             