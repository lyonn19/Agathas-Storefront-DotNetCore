#!/bin/bash

PROJHOME=/home/willsams/Projects/dotnet/storefront-dotnet-core
PROJNAME=Agathas.Storefront
TESTS=$PROJNAME.Tests
INFRASTRUCTURE=$PROJNAME.Infrastructure
MODELS=$PROJNAME.Models
NHIBERNATE=$PROJNAME.Repository.NHibernate
SERVICES=$PROJNAME.Services
CACHE=$PROJNAME.Services.Cache
WEBAPI=$PROJNAME.API
MVC=$PROJNAME.UI.Web.MVC

# helper function - createbranch
createbranch() { 
  git checkout -b $1 
}

# helper function - mergebranch
mergebranch() {  
  git checkout master
  git merge $1
  git branch -D $1
}

git init

echo "node_modules/
.sass-cache/
.vscode/
bin/
obj/" >> .gitignore

dotnet new sln -n $PROJNAME

dotnet new nunit -n $TESTS
cd $PROJHOME/$TESTS
dotnet add package System.Linq.Queryable
dotnet add package System.Data.SQLite.Core
dotnet add package FluentAssertions
dotnet add package Moq  
dotnet add package AutoMapper

rm UnitTest1.cs
cd $PROJHOME && dotnet sln add $TESTS/$TESTS.csproj

dotnet new classlib -n $INFRASTRUCTURE
cd $PROJHOME/$INFRASTRUCTURE
dotnet add package System.Linq.Queryable
dotnet add package log4net
dotnet add package Microsoft.AspNetCore.Http
dotnet add package Microsoft.AspNetCore.Http.Extensions
dotnet add package System.Data.DataSetExtensions
dotnet add package System.Configuration.ConfigurationManager
rm Class1.cs Properties/AssemblyInfo.cs

cd $PROJHOME/$TESTS && dotnet add reference ../$INFRASTRUCTURE/$INFRASTRUCTURE.csproj
cd $PROJHOME && dotnet sln add $INFRASTRUCTURE/$INFRASTRUCTURE.csproj

dotnet new classlib -n $MODELS
cd $PROJHOME/$MODELS
dotnet add package System.Linq.Queryable
dotnet add package System.Data.DataSetExtensions
dotnet add reference ../$INFRASTRUCTURE/$INFRASTRUCTURE.csproj
rm Class1.cs Properties/AssemblyInfo.cs

cd $PROJHOME/$TESTS && dotnet add reference ../$MODELS/$MODELS.csproj
cd $PROJHOME && dotnet sln add $MODELS/$MODELS.csproj

git add .
git commit -am "Initial commit."

createbranch implement_data_access_layer

dotnet new classlib -n $NHIBERNATE
cd $PROJHOME/$NHIBERNATE
dotnet add package System.Linq.Queryable
dotnet add package NHibernate
dotnet add package Log4Net
dotnet add reference ../$MODELS/$MODELS.csproj
dotnet add reference ../$INFRASTRUCTURE/$INFRASTRUCTURE.csproj
rm Class1.cs

cd $PROJHOME/$TESTS && dotnet add reference ../$NHIBERNATE/$NHIBERNATE.csproj
cd $PROJHOME && dotnet sln add $NHIBERNATE/$NHIBERNATE.csproj

mergebranch implement_data_access_layer

createbranch implement_business_logic_layer

dotnet new classlib -n $SERVICES
cd $PROJHOME/$SERVICES
dotnet add package System.Linq.Queryable
dotnet add package AutoMapper
dotnet add package System.Data.DataSetExtensions
#dotnet add reference ../$MODELS/$MODELS.csproj
dotnet add reference ../$NHIBERNATE/$NHIBERNATE.csproj
rm Class1.cs

cd $PROJHOME/$TESTS && dotnet add reference ../$SERVICES/$SERVICES.csproj
cd $PROJHOME && dotnet sln add $SERVICES/$SERVICES.csproj

dotnet new classlib -n $CACHE
cd $PROJHOME/$CACHE
dotnet add package System.Linq.Queryable
dotnet add package Microsoft.AspNetCore.Http
dotnet add reference ../$MODELS/$MODELS.csproj
dotnet add reference ../$INFRASTRUCTURE/$INFRASTRUCTURE.csproj
dotnet add reference ../$SERVICES/$SERVICES.csproj
rm Class1.cs

cd $PROJHOME/$TESTS && dotnet add reference ../$CACHE/$CACHE.csproj
cd $PROJHOME && dotnet sln add $CACHE/$CACHE.csproj

mergebranch implement_business_logic_layer
createbranch implement_api

dotnet new webapi -n $WEBAPI
cd $PROJHOME/$WEBAPI
dotnet add package System.Linq.Queryable
dotnet add package Autofac.Extensions.DependencyInjection
dotnet add reference ../$MODELS/$MODELS.csproj
dotnet add reference ../$INFRASTRUCTURE/$INFRASTRUCTURE.csproj
dotnet add reference ../$SERVICES/$SERVICES.csproj
dotnet add reference ../$CACHE/$CACHE.csproj

cd $PROJHOME/$TESTS && dotnet add reference ../$WEBAPI/$WEBAPI.csproj
cd $PROJHOME && dotnet sln add $WEBAPI/$WEBAPI.csproj

mergebranch implement_api
createbranch implement_presentation_layer

dotnet new webapi -n $MVC
cd $PROJHOME/$MVC
dotnet add package System.Linq.Queryable
dotnet add reference ../$MODELS/$MODELS.csproj

cd $PROJHOME/$TESTS && dotnet add reference ../$MVC/$MVC.csproj
cd $PROJHOME && dotnet sln add $MVC/$MVC.csproj

mergebranch implement_presentation_layer

cd $PROJHOME && dotnet build && dotnet run --project $MVC
