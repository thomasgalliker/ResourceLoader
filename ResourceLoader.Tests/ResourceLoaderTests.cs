using System.Reflection.Exceptions;

using FluentAssertions;

using Xunit;

namespace System.Reflection.Tests
{
    public class ResourceLoaderTests
    {
        [Fact]
        public void ShouldGetEmbeddedResourceStream()
        {
            // Arrange
            var testAssembly = this.GetType().Assembly;
            var testFile = "XMLFile1.xml";

            // Act
            var embeddedResourceStream = ResourceLoader.Current.GetEmbeddedResourceStream(testAssembly, testFile);

            // Assert
            embeddedResourceStream.Should().NotBeNull();
        }

        [Fact]
        public void ShouldGetEmbeddedResourceBytes()
        {
            // Arrange
            IResourceLoader resourceLoader = new ResourceLoader();
            var testAssembly = this.GetType().Assembly;
            var testFile = "XMLFile1.xml";

            // Act
            var embeddedResourceBytes = resourceLoader.GetEmbeddedResourceBytes(testAssembly, testFile);

            // Assert
            embeddedResourceBytes.Should().NotBeNull();
            embeddedResourceBytes.Length.Should().Be(42);
        }

        [Fact]
        public void ShouldGetEmbeddedResourceString()
        {
            // Arrange
            IResourceLoader resourceLoader = new ResourceLoader();
            var testAssembly = this.GetType().Assembly;
            var testFile = "XMLFile1.xml";
            var testContent = @"<?xml version=""1.0"" encoding=""utf-8"" ?>";

            // Act
            var embeddedResourceString = resourceLoader.GetEmbeddedResourceString(testAssembly, testFile);

            // Assert
            embeddedResourceString.Should().NotBeNull();
            embeddedResourceString.Should().Be(testContent);
        }

        [Fact]
        public void ShouldThrowResourceNotFoundExceptionWhenGetEmbeddedResourceString()
        {
            // Arrange
            IResourceLoader resourceLoader = new ResourceLoader();
            var testAssembly = this.GetType().Assembly;
            var testFile = "unknown_file.xml";

            // Act
            Action action = () => resourceLoader.GetEmbeddedResourceString(testAssembly, testFile);

            // Assert
            action.ShouldThrow<ResourceNotFoundException>();
        }

        [Fact]
        public void ShouldThrowMultipleResourcesFoundExceptionWhenGetEmbeddedResourceString()
        {
            // Arrange
            IResourceLoader resourceLoader = new ResourceLoader();
            var testAssembly = this.GetType().Assembly;
            var testFile = ".xml";

            // Act
            Action action = () => resourceLoader.GetEmbeddedResourceString(testAssembly, testFile);

            // Assert
            action.ShouldThrow<MultipleResourcesFoundException>();
        }

        [Fact]
        public void ShouldReturnStaticResourceLoader()
        {
            // Act
            var resourceLoader = ResourceLoader.Current;

            // Assert
            resourceLoader.Should().NotBeNull();
            resourceLoader.Should().BeOfType<ResourceLoader>();
            resourceLoader.Should().BeAssignableTo<IResourceLoader>();
        }
    }
}
