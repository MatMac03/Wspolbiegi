using Dane;
using System.ComponentModel;
using System.Numerics;

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
            Vector2 predkosc = new Vector2(predkoscX, 0);
            ball.predkosc = predkosc;

            // Assert
            Assert.AreEqual(predkoscX, ball.predkosc.X);
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
            Vector2 predkosc = new Vector2(0, predkoscY);
            ball.predkosc = predkosc;

            // Assert
            Assert.AreEqual(predkoscY, ball.predkosc.Y);
        }

        /*[TestMethod]
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
        }*/
    }
}
