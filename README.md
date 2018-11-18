# ResourceLoader
[![Version](https://img.shields.io/nuget/v/ResourceLoader.svg)](https://www.nuget.org/packages/ResourceLoader)  [![Downloads](https://img.shields.io/nuget/dt/ResourceLoader.svg)](https://www.nuget.org/packages/ResourceLoader)

<img src="https://raw.githubusercontent.com/thomasgalliker/ResourceLoader/master/ResourceLoader.png" width="100" height="100" alt="ResourceLoader" align="right">
ResourceLoader is a utility for reading embedded resources, such as XML files, images, etc. from assemblies.

### Download and Install ResourceLoader
This library is available on NuGet: https://www.nuget.org/packages/ResourceLoader/
Use the following command to install ResourceLoader using NuGet package manager console:

    PM> Install-Package ResourceLoader

You can use this library in any .Net project which is compatible to PCL (e.g. Xamarin Android, iOS, Windows Phone, Windows Store, Universal Apps, etc.)

### API Usage
#### Reading an embedded XML file
The following unit test shows how an XML file (XMLFile1.xml) can be read from the current assembly.
```
// Arrange
IResourceLoader resourceLoader = new ResourceLoader();

// Act
string embeddedResourceString = resourceLoader.GetEmbeddedResourceString(this.GetType().Assembly, "XMLFile1.xml");

// Assert
embeddedResourceString.Should().NotBeNull();
embeddedResourceString.Should().Be(@"<?xml version=""1.0"" encoding=""utf-8"" ?>");
```

#### Using singleton implementation ResourceLoader.Current
If you do not use an IoC framework, you can call ResourceLoader.Current directly to get a singleton instance of IResourceLoader.
```
string embeddedResourceString = ResourceLoader.Current.GetEmbeddedResourceString(this.GetType().Assembly, "XMLFile1.xml");
```

### License
This project is Copyright &copy; 2018 [Thomas Galliker](https://ch.linkedin.com/in/thomasgalliker). Free for non-commercial use. For commercial use please contact the author.
