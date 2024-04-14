using Dane;
using System.ComponentModel;

namespace DaneTest
{
    [TestClass]
    public class BallTests
    {
        [TestMethod]
        public void setXPredkoscTest()
        {
            // Arrange
            var api = DataAbstractAPI.CreateDataAPI(400, 100);
            float predkoscX = 10.0f;

            // Act
            api.setXPredkosc(predkoscX);

            // Assert
            Assert.AreEqual(predkoscX, api.getXPredkosc());
        }

        [TestMethod]
        public void setYPredkoscTest()
        {
            // Arrange
            var api = DataAbstractAPI.CreateDataAPI(400, 100);
            float predkoscY = 10.0f;

            // Act
            api.setYPredkosc(predkoscY);

            // Assert
            Assert.AreEqual(predkoscY, api.getYPredkosc());
        }

        [TestMethod]
        public void positionTest()
        {
            var ball = DataAbstractAPI.CreateDataAPI(100, 100);
            float posX = 50;
            float posY = 50;

            ball.x = posX;
            ball.y = posY;

            Assert.AreEqual(posX, ball.x);
            Assert.AreEqual(posY, ball.y);
        }

        [TestMethod]
        public void BallWithinBoundsTrue()
        {
            // Arrange
            int rozmiarX = 400;
            int rozmiarY = 100;
            Ball ball = new Ball(rozmiarX, rozmiarY);
            ball.x = 200;
            ball.y = 50;
            ball.setXPredkosc(5);
            ball.setYPredkosc(5);

            // Act
            bool result = ball.IsWithinBounds(rozmiarX, rozmiarY);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BallWithinBoundsFalse()
        {
            // Arrange
            int rozmiarX = 400;
            int rozmiarY = 100;
            Ball ball = new Ball(rozmiarX, rozmiarY);
            ball.x = 450;
            ball.y = 50;
            ball.setXPredkosc(5);
            ball.setYPredkosc(5);

            // Act
            bool result = ball.IsWithinBounds(rozmiarX, rozmiarY);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
