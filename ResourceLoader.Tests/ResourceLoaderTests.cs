using System.Linq;
using System.Reflection.Exceptions;
using System.Reflection.Tests.Extensions;
using System.Text;

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
        public void ShouldGetEmbeddedResourceStreams()
        {
            // Arrange
            var testAssembly = this.GetType().Assembly;
            var filePattern = "XMLFile";

            // Act
            var embeddedResourceStreams = ResourceLoader.Current.GetEmbeddedResourceStreams(testAssembly, filePattern);

            // Assert
            embeddedResourceStreams.Should().NotBeNull();
            embeddedResourceStreams.Should().HaveCount(3);
        }

        [Fact]
        public void ShouldGetEmbeddedResourceArra()
        {
            // Arrange
            IResourceLoader resourceLoader = new ResourceLoader();
            var testAssembly = this.GetType().Assembly;
            var testFile = "XMLFile1.xml";

            // Act
            var embeddedResourceByteArray = resourceLoader.GetEmbeddedResourceByteArray(testAssembly, testFile);

            // Assert
            embeddedResourceByteArray.Should().NotBeNull();
            embeddedResourceByteArray.Length.Should().Be(42);
        }

        [Fact]
        public void ShouldGetEmbeddedResourceByteArrays()
        {
            // Arrange
            IResourceLoader resourceLoader = new ResourceLoader();
            var testAssembly = this.GetType().Assembly;
            var testFile = "XMLFile";

            // Act
            var embeddedResourceBytes = resourceLoader.GetEmbeddedResourceByteArrays(testAssembly, testFile);

            // Assert
            embeddedResourceBytes.Should().NotBeNull();
            embeddedResourceBytes.Should().HaveCount(3);
            embeddedResourceBytes.ElementAt(0).Length.Should().Be(42);
            embeddedResourceBytes.ElementAt(1).Length.Should().Be(349);
            embeddedResourceBytes.ElementAt(2).Length.Should().Be(353);
        }

        [Fact]
        public void ShouldGetEmbeddedResourceString()
        {
            // Arrange
            IResourceLoader resourceLoader = new ResourceLoader();
            var testAssembly = this.GetType().Assembly;
            var filePattern = "XMLFile1.xml";
            var testContent = @"<?xml version=""1.0"" encoding=""utf-8"" ?>";

            // Act
            var embeddedResourceString = resourceLoader.GetEmbeddedResourceString(testAssembly, filePattern);

            // Assert
            embeddedResourceString.Should().NotBeNull();
            embeddedResourceString.FirstLine().Should().Be(testContent);
        }

        [Fact]
        public void ShouldGetEmbeddedResourceStrings()
        {
            // Arrange
            IResourceLoader resourceLoader = new ResourceLoader();
            var testAssembly = this.GetType().Assembly;
            var testFile = "XMLFile";

            // Act
            var embeddedResourceStrings = resourceLoader.GetEmbeddedResourceStrings(testAssembly, testFile);

            // Assert
            embeddedResourceStrings.Should().NotBeNull();
            embeddedResourceStrings.Should().HaveCount(3);

            embeddedResourceStrings.ElementAt(0).FirstLine().Should().Be("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            embeddedResourceStrings.ElementAt(1).FirstLine().Should().Be("<?xml version=\"1.0\" encoding=\"iso-8859-1\" ?>");
            embeddedResourceStrings.ElementAt(2).FirstLine().Should().Be("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        }

        [Fact]
        public void ShouldGetEmbeddedResourceStringWithEncoding()
        {
            // Arrange
            IResourceLoader resourceLoader = new ResourceLoader();
            var testAssembly = this.GetType().Assembly;
            var testFile = "XMLFile1_ISO-8859-1.xml";
            var testContent = @"<?xml version=""1.0"" encoding=""iso-8859-1"" ?>";
            var encoding = Encoding.GetEncoding("ISO-8859-1");

            // Act
            var embeddedResourceString = resourceLoader.GetEmbeddedResourceString(testAssembly, testFile, encoding);

            // Assert
            embeddedResourceString.Should().NotBeNull();
            embeddedResourceString.FirstLine().Should().Be(testContent);
            embeddedResourceString.Line(2).Should().Contain("®");
            embeddedResourceString.Line(2).Should().NotContain("�");
        }

        [Fact]
        public void ShouldGetEmbeddedResourceStringWithEncodingMismatch()
        {
            // Arrange
            IResourceLoader resourceLoader = new ResourceLoader();
            var testAssembly = this.GetType().Assembly;
            var testFile = "XMLFile1_ISO-8859-1.xml";
            var testContent = @"<?xml version=""1.0"" encoding=""iso-8859-1"" ?>";
            var encoding = Encoding.UTF8;

            // Act
            var embeddedResourceString = resourceLoader.GetEmbeddedResourceString(testAssembly, testFile, encoding);

            // Assert
            embeddedResourceString.Should().NotBeNull();
            embeddedResourceString.FirstLine().Should().Be(testContent);
            embeddedResourceString.Line(2).Should().Contain("�");
            embeddedResourceString.Line(2).Should().NotContain("®");
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
