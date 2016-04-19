using System;

using FluentAssertions;

using Xunit;

namespace ResourceLoader.Tests
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
            var embeddedResourceStream = ResourceLoader.GetEmbeddedResourceStream(testAssembly, testFile);

            // Assert
            embeddedResourceStream.Should().NotBeNull();
        }

        [Fact]
        public void ShouldGetEmbeddedResourceBytes()
        {
            // Arrange
            var testAssembly = this.GetType().Assembly;
            var testFile = "XMLFile1.xml";

            // Act
            var embeddedResourceBytes = ResourceLoader.GetEmbeddedResourceBytes(testAssembly, testFile);

            // Assert
            embeddedResourceBytes.Should().NotBeNull();
            embeddedResourceBytes.Length.Should().Be(42);
        }

        [Fact]
        public void ShouldGetEmbeddedResourceString()
        {
            // Arrange
            var testAssembly = this.GetType().Assembly;
            var testFile = "XMLFile1.xml";
            var testContent = @"<?xml version=""1.0"" encoding=""utf-8"" ?>";

            // Act
            var embeddedResourceString = ResourceLoader.GetEmbeddedResourceString(testAssembly, testFile);

            // Assert
            embeddedResourceString.Should().NotBeNull();
            embeddedResourceString.Should().Be(testContent);
        }

        [Fact]
        public void ShouldThrowNotFoundExceptionWhenGetEmbeddedResourceString()
        {
            // Arrange
            var testAssembly = this.GetType().Assembly;
            var testFile = "unknown_file.xml";

            // Act
            Action action = () => ResourceLoader.GetEmbeddedResourceString(testAssembly, testFile);

            // Assert
            action.ShouldThrow<Exception>();
        }
    }
}
