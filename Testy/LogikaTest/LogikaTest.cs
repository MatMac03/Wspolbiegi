using Logika;
using Dane;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Numerics;

namespace LogikaTest
{

    public class TestBall : IBall
    {
        public Vector2 Pos { get; set; }

        public Vector2 predkosc { get; set; }

        //private float predkoscX;
        //private float predkoscY;
        private float masa;
        private float promien;

        public event EventHandler<DataEventArgs>? ChangedPosition;


        public TestBall(float x, float y, float predkoscX, float predkoscY)
        {
            Vector2 pos = new Vector2(x, y);
            Vector2 predkosc = new Vector2(predkoscX, predkoscY);
            this.Pos = pos; this.predkosc = predkosc;
        }

        public float getPromien()
        {
            return promien;
        }

        /*public float getXPredkosc()
        {
            return predkoscX;
        }

        public float getYPredkosc()
        {
            return predkoscY;
        }*/

        public float getMasa()
        {
            return masa;
        }

        public void move()
        {
            this.Pos += predkosc;
            DataEventArgs args = new DataEventArgs(Pos);
            ChangedPosition?.Invoke(this, args);
        }

        public void destroy()
        {
            throw new NotImplementedException();
        }

        public float getXPredkosc()
        {
            throw new NotImplementedException();
        }

        public float getYPredkosc()
        {
            throw new NotImplementedException();
        }

        public void setXPredkosc(float xPredkosc)
        {
            throw new NotImplementedException();
        }

        public void setYPredkosc(float yPredkosc)
        {
            throw new NotImplementedException();
        }
    }
    [TestClass]
    public class LogikaTest
    {
        
        [TestMethod]
        public void zmienKierunekXTest()
        {
            // Arrange
            LogicAbstractAPI api = LogicAbstractAPI.CreateLogicAPI();
            TestBall[] balls = new TestBall[1];
            TestBall ball = new TestBall(50, 50, 5, 5);
            balls[0] = ball;
            api.setBalls(balls);

            // Act
            Logika.Logic.zmienKierunekX(ball);

            // Assert
            Assert.AreEqual(ball.predkosc.X, -5);
        }

        [TestMethod]
        public void zmienKierunekYTest()
        {
            // Arrange
            LogicAbstractAPI api = LogicAbstractAPI.CreateLogicAPI();
            TestBall[] balls = new TestBall[1];
            TestBall ball = new TestBall(50, 50, 5, 5);
            balls[0] = ball;
            api.setBalls(balls);

            // Act
            Logika.Logic.zmienKierunekY(ball);

            // Assert
            Assert.AreEqual(ball.predkosc.Y,-5);
        }

        [TestMethod]
        public void aktualizujPozycjeTest()
        {
            // Arrange
            LogicAbstractAPI api = LogicAbstractAPI.CreateLogicAPI();
            TestBall[] balls = new TestBall[1];
            TestBall ball = new TestBall(50, 50, 5, 5);
            balls[0] = ball;
            api.setBalls(balls);

            ball.move();

            // Assert
            Assert.AreEqual(55, ball.Pos.X);
            Assert.AreEqual(55, ball.Pos.Y);
        }

       
    }
}