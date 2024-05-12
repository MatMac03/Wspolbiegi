using Logika;
using Dane;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LogikaTest
{

    public class TestBall : IBall
    {
        public float x { get; set; }
        public float y { get; set; }

        private float predkoscX;
        private float predkoscY;
        private float masa;
        private float promien;

        public event PropertyChangedEventHandler PropertyChanged;

        public TestBall(float x, float y, float predkoscX, float predkoscY)
        {
            this.x = x;
            this.y = y;
            this.predkoscX = predkoscX;
            this.predkoscY = predkoscY;
        }

        public float getPromien()
        {
            return promien;
        }

        public float getXPredkosc()
        {
            return predkoscX;
        }

        public float getYPredkosc()
        {
            return predkoscY;
        }

        public float getMasa()
        {
            return masa;
        }

        public void setXPredkosc(float xPredkosc)
        {
            predkoscX = xPredkosc;
        }

        public void setYPredkosc(float yPredkosc)
        {
            predkoscY = yPredkosc;
        }

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void updatePozycja()
        {
            x += getXPredkosc();
            y += getYPredkosc();
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
            Assert.AreEqual(ball.getXPredkosc(), -5);
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
            Assert.AreEqual(ball.getYPredkosc(),-5);
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

            ball.updatePozycja();

            // Assert
            Assert.AreEqual(55, ball.x);
            Assert.AreEqual(55, ball.y);
        }

       
    }
}