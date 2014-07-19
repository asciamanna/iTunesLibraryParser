iTunesLibraryParser
===================
The iTunesLibrary Parser is implemented in C# using LINQ-To-XML.

##Build Status
[![Build status](https://ci.appveyor.com/api/projects/status/tsebsc61mqylaejq)](https://ci.appveyor.com/project/asciamanna/ituneslibraryparser)

##Supported
It currently only parses iTunes Library Track information. Given the location of the iTunesMusicLibrary.xml file it parses the PropertyList format, which is defined by the Document Type Declaration (DTD) defined here [http://www.apple.com/DTDs/PropertyList-1.0.dtd](http://www.apple.com/DTDs/PropertyList-1.0.dtd). It then returns a collection of C# track objects.

##Usage
This approach stores all of the data in memory so it would not be recommended for huge libraries. I have tested it on an iTunes library consisting of 9500 tracks and it performs well.

##Coming Soon
The ability to parse playlist information.

##Project Dependencies
NUnit 2.6.3  


##Contact
**Anthony Sciamanna**
<br/>
**Email:** asciamanna@gmail.com  
**Web:** [http://www.anthonysciamanna.com](http://www.anthonysciamanna.com)  
**Twitter:** [@asciamanna](http://www.twitter.com/asciamanna)
