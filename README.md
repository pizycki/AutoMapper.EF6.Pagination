# PagiNET

[![Build status](https://ci.appveyor.com/api/projects/status/a689qih74at6hyx0/branch/master?svg=true)](https://ci.appveyor.com/project/pizycki/paginet/branch/master)

> This lib is still in early stage of development, so things will more or less change.

## What is it?

It's .NET library designed for building Web API with pagination feature.

## Requirements
Prior referencing this library make sure your project meets requirements.

The PagiNET is valid with .NET Standard 2.0. Here is a list of compatible .NET Frameworks.

The PagiNET shall work with both EF and EF.Core. _Unfortunetly I can't tell which exact EF versions are supported right now._

## Angular Example

To run angular example, clone this repo and get NPM and NuGet packages

```
# Clone repository on your disk
git clone https://github.com/pizycki/PagiNET

# Get client app packages
cd PagiNET\examples\AngularExample   # go to example project root
npm install                          # or 'yarn'
dotnet restore                       # you need ASP.NET Core SDK @2.0.0+

# Build and run example app
dotnet build & dotnet run

# The server should start and the website URL should appear in the console
# It would be something like 'http://localhost:51234'
```

After navigating to the hosted site, you should be able to see some sample components that use PagiNET as backend framework.

_**Right now there is only one component which is simple paginated grid. More components are comming soon.**_

The whole point of checking out example project is to see how API calls look like and **how PagiNET generates SQL queries**. 

Run your fav SQL profiler (ex: Express Profiler), connect to database server (default is `(localdb)\mssqllocaldb`), start profiling and change page on any componet.

You should see that PagiNET queries database only for **selected number of rows**. 

```sql
exec sp_executesql N'SELECT [p].[Id], [p].[BirthDate], [p].[Gender], [p].[Name]
FROM [Customers] AS [p]
ORDER BY [p].[Id]
OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY',N'@__p_0 int,@__p_1 int',@__p_0=20,@__p_1=20
go
```

This is massive performance tweak when your query result can have hundreds of rows. Just think about network usage and memory consumption.

## Contribute

All issues/PRs/suggestions are welcome!

If you'd like to contribute to the procjet fork this repo and make an pull request.

You can reach me on Twitter: [@pizycki](http://twitter.com/pizycki).

## TODO

- [x] Pagination
- [x] Sorting
- [ ] Multilevel sorting
- [x] Integration testing
- [x] Pager model
- [x] EF Core
- [x] .NET Standard 2.0
- [x] Example app (Angular2)
  - [x] Pager component
  - [x] List view
  - [ ] Add Cake task for quick run
- [ ] CI&CD (AppVeyor)
  - [x] Build
  - [x] Unit tests
  - [x] Integration tests (real db)
    - [x] Working localy
    - [x] On build server 
  - [ ] Nuget publish
- [ ] API improvments
  - [ ] `TakePage` IQueryable -> IQueryable
  - [ ] `SinglePage` IQueryable -> Page