using System;
using Moq;
using Tn3270PlusDde.Client;
using Tn3270PlusDde.DdeProvider;
using Xunit;

namespace Tn3270PlusDde.UnitTest
{
    public class Tn3270PlusDdeClientTests
    {
        private const string FakeResponse = "fake";

        private readonly Mock<IDdeProvider> ddeProviderMock = new Mock<IDdeProvider>();

        [Fact]
        public void RequestPresentationSpaceSucceeds()
        {
            this.ddeProviderMock.Setup(x => x.DdeRequest(It.IsAny<string>(), "PS")).Returns(FakeResponse);
            var client = new Tn3270PlusDdeClient(this.ddeProviderMock.Object);
            var response = client.RequestPresentationSpace(string.Empty);

            Assert.Equal(FakeResponse, response);
        }

        [Fact]
        public void RequestPresentationSpaceWithLinebreaksSucceeds()
        {
            var client = new Tn3270PlusDdeClient(this.ddeProviderMock.Object, 10);
            this.ddeProviderMock.Setup(x => x.DdeRequest(It.IsAny<string>(), It.IsAny<string>())).Returns("0123456789a");
            var response = client.RequestPresentationSpace(string.Empty, true);

            Assert.Equal($"0123456789{Environment.NewLine}a", response);
        }

        [Fact]
        public void RequestKeyboardSucceeds()
        {
            this.ddeProviderMock.Setup(x => x.DdeRequest(It.IsAny<string>(), "Keyboard")).Returns(FakeResponse);
            var client = new Tn3270PlusDdeClient(this.ddeProviderMock.Object);
            var response = client.RequestKeyboard(string.Empty);

            Assert.Equal(FakeResponse, response);
        }

        [Fact]
        public void RequestActiveSessionSucceeds()
        {
            this.ddeProviderMock.Setup(x => x.DdeRequest("system", "activesession")).Returns(FakeResponse);
            var client = new Tn3270PlusDdeClient(this.ddeProviderMock.Object);
            var response = client.RequestActiveSession();

            Assert.Equal(FakeResponse, response);
        }

        [Fact]
        public void RequestSessionsSucceeds()
        {
            this.ddeProviderMock.Setup(x => x.DdeRequest("system", "sessions")).Returns(FakeResponse);
            var client = new Tn3270PlusDdeClient(this.ddeProviderMock.Object);
            var response = client.RequestSessions();

            Assert.Equal(FakeResponse, response);
        }

        [Fact]
        public void PokeKeystrokeSucceeds()
        {
            this.ddeProviderMock.Setup(x => x.DdePoke(It.IsAny<string>(), "keystroke", It.IsAny<string>())).Returns(true);
            var client = new Tn3270PlusDdeClient(this.ddeProviderMock.Object);
            var response = client.PokeKeystroke(string.Empty, string.Empty);

            Assert.True(response);
        }

        [Fact]
        public void PokeCursorPositionSucceeds()
        {
            this.ddeProviderMock.Setup(x => x.DdePoke(It.IsAny<string>(), "cursor", It.IsAny<string>())).Returns(true);
            var client = new Tn3270PlusDdeClient(this.ddeProviderMock.Object);
            var response = client.PokeCursor(string.Empty, 0);

            Assert.True(response);
        }

        [Fact]
        public void PokeCursorCoordinateSucceeds()
        {
            this.ddeProviderMock.Setup(x => x.DdePoke(It.IsAny<string>(), "cursor", It.IsAny<string>())).Returns(true);
            var client = new Tn3270PlusDdeClient(this.ddeProviderMock.Object);
            var response = client.PokeCursor(string.Empty, 0, 0);

            Assert.True(response);
        }
    }
}
