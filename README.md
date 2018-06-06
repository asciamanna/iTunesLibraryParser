iTunesLibraryParser 
===================
[![Build status](https://ci.appveyor.com/api/projects/status/tsebsc61mqylaejq?svg=true)](https://ci.appveyor.com/project/asciamanna/ituneslibraryparser)
[![Coverage Status](https://coveralls.io/repos/github/asciamanna/iTunesLibraryParser/badge.svg?branch=master)](https://coveralls.io/github/asciamanna/iTunesLibraryParser?branch=master)
[![NuGet version](https://img.shields.io/nuget/v/ITunesLibraryParser.svg)](https://www.nuget.org/packages/iTunesLibraryParser/)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/20f1e8648cc74b158fbbb09528fd9e2e)](https://app.codacy.com/app/asciamanna/iTunesLibraryParser?utm_source=github.com&utm_medium=referral&utm_content=asciamanna/iTunesLibraryParser&utm_campaign=badger)

The iTunes Library Parser is implemented in C# utilizing LINQ-To-XML. Given the location of a iTunes Music Library XML file it parses the PropertyList format, which is defined by the Document Type Declaration (DTD) defined here [http://www.apple.com/DTDs/PropertyList-1.0.dtd](http://www.apple.com/DTDs/PropertyList-1.0.dtd). It supports parsing tracks, albums, and playlists.  More features will be added periodically.

## Nuget

The nuget package is [available here](https://www.nuget.org/packages/iTunesLibraryParser/)

## Usage
```csharp
var library = new ITunesLibrary("iTunesLibrary.xml");

var tracks = library.Tracks 
// returns all tracks in the iTunes Library

var albums = library.Albums
// returns all albums in the iTunes Library

var playlists = library.Playlists
// returns all playlists in the iTunes Library
```

## Versioning
iTunesLibaryParser will be maintained under the [Semantic Versioning guidelines](http://semver.org). Releases will follow this format:

```
<major>.<minor>.<build>
```

 * If a release breaks backward compatibility the major version will be bumped (resetting minor and build back to zero). 
 * New features and updates without breaking backward compatibility will bump the minor version (resetting the build to zero)
 * Bug fixes and small miscellaneous changes increase the build number

## Performance Testing

14,500 tracks -> 800ms  
2400 albums -> 1.4s  
100 playlists -> 2.2s  

## Coming Soon
Additional features will be coming soon like filtering tracks by track criteria.

## Project Dependencies
coveralls.io 1.4.2  
NUnit 3.10.1   
NUnit.ConsoleRunner 3.8.0   
Moq 4.8.2   
OpenCover 4.6.519   

## Contact
**Anthony Sciamanna**
<br/>
**Web:** [http://www.anthonysciamanna.com](http://www.anthonysciamanna.com)  
**Twitter:** [@asciamanna](http://www.twitter.com/asciamanna)
