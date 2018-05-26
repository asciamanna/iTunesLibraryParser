iTunesLibraryParser [![Build status](https://ci.appveyor.com/api/projects/status/tsebsc61mqylaejq?svg=true)](https://ci.appveyor.com/project/asciamanna/ituneslibraryparser)
===================
The iTunes Library Parser is implemented in C# utilizing LINQ-To-XML (in memory). Given the location of a iTunesMusicLibrary.xml file it parses the PropertyList format, which is defined by the Document Type Declaration (DTD) defined here [http://www.apple.com/DTDs/PropertyList-1.0.dtd](http://www.apple.com/DTDs/PropertyList-1.0.dtd). Currently it returns a collection of tracks and collection of playlists. More features will be added periodically.

## Nuget

The nuget package is [available here](https://www.nuget.org/packages/iTunesLibraryParser/)

## Usage
```
var library = new ITunesLibrary(".\iTunesLibrary.xml");

var tracks = library.Tracks 
// returns all tracks in the iTunes Library

var playlists = library.Playlists
// returns all playlists in the iTunes Library
```

## Coming Soon
Additional features will be coming soon like filtering tracks by track criteria, returning albums, and returning compilation albums.

## Project Dependencies
NUnit 3.10.1  
Moq 4.8.2

## Contact
**Anthony Sciamanna**
<br/>
**Web:** [http://www.anthonysciamanna.com](http://www.anthonysciamanna.com)  
**Twitter:** [@asciamanna](http://www.twitter.com/asciamanna)
