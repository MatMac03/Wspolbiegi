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
            var table = DataAbstractAPI.CreateDataAPI();
            table.setTableParam(400, 100, 1);
            float predkoscX = 10.0f;

            // Act
            IBall ball = table.getBalls()[0];
            ball.setXPredkosc(predkoscX);

            // Assert
            Assert.AreEqual(predkoscX, ball.getXPredkosc());
        }

        [TestMethod]
        public void setYPredkoscTest()
        {
            // Arrange
            var table = DataAbstractAPI.CreateDataAPI();
            table.setTableParam(400, 100, 1);
            float predkoscY = 10.0f;

            // Act
            IBall ball = table.getBalls()[0];
            ball.setYPredkosc(predkoscY);

            // Assert
            Assert.AreEqual(predkoscY, ball.getYPredkosc());
        }

        [TestMethod]
        public void positionTest()
        {
            var table = DataAbstractAPI.CreateDataAPI();
            table.setTableParam(400, 100, 1);
            float posX = 50;
            float posY = 50;

            IBall ball = table.getBalls()[0];
            ball.x = posX;
            ball.y = posY;

            Assert.AreEqual(posX, ball.x);
            Assert.AreEqual(posY, ball.y);
        }
    }
}
