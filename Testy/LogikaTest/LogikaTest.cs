using Logika;

namespace LogikaTest
{
    [TestClass]
    public class LogikaTest
    {
        [TestMethod]
        public void TableConstructor()
        {
            // Arrange
            int rozmiarX = 100;
            int rozmiarY = 100;
            int ilosc = 5;

            // Act
            Table table = new Table(rozmiarX, rozmiarY, ilosc);

            // Assert
            Assert.AreEqual(ilosc, table.getBalls().Length);
        }

        [TestMethod]
        public void TableCheckBorderCollisionX()
        {
            // Arrange
            int rozmiarX = 100;
            int rozmiarY = 100;
            int ilosc = 1;
            Table table = new Table(rozmiarX, rozmiarY, ilosc);
            var ball = table.getBalls()[0];
            ball.x = rozmiarX - ball.getPromien();
            ball.setXPredkosc(5);

            // Act
            table.checkBorderCollision();

            // Assert
            Assert.AreEqual(-5, ball.getXPredkosc());
        }

        [TestMethod]
        public void TableCheckBorderCollisionY()
        {
            // Arrange
            int rozmiarX = 100;
            int rozmiarY = 100;
            int ilosc = 1;
            Table table = new Table(rozmiarX, rozmiarY, ilosc);
            var ball = table.getBalls()[0];
            ball.y = rozmiarY - ball.getPromien();
            ball.setYPredkosc(5);

            // Act
            table.checkBorderCollision();

            // Assert
            Assert.AreEqual(-5, ball.getYPredkosc());
        }

        [TestMethod]
        public void TableGetPozycja()
        {
            // Arrange
            int rozmiarX = 100;
            int rozmiarY = 100;
            int ilosc = 3;
            Table table = new Table(rozmiarX, rozmiarY, ilosc);
            float[][] expectedPositions = new float[ilosc][];
            for (int i = 0; i < ilosc; i++)
            {
                expectedPositions[i] = new float[] { table.getBalls()[i].x, table.getBalls()[i].y };
            }

            // Act
            float[][] actualPositions = table.getPozycja();

            // Assert
            Assert.AreEqual(expectedPositions.Length, actualPositions.Length);
            for (int i = 0; i < expectedPositions.Length; i++)
            {
                Assert.AreEqual(expectedPositions[i][0], actualPositions[i][0]);
                Assert.AreEqual(expectedPositions[i][1], actualPositions[i][1]);
            }
        }

        [TestMethod]
        public void createBallsTest()
        {
            // Arrange
            int maxX = 100;
            int maxY = 100;
            int ilosc = 5;

            // Act
            var balls = Logika.Logic.createBalls(maxX, maxY, ilosc);

            // Assert
            Assert.AreEqual(ilosc, balls.Length);

            foreach (var ball in balls)
            {
                Assert.IsTrue(ball.x >= ball.getPromien() && ball.x <= maxX - ball.getPromien());
                Assert.IsTrue(ball.y >= ball.getPromien() && ball.y <= maxY - ball.getPromien());
            }
        }

        [TestMethod]
        public void zmienKierunekXTest()
        {
            // Arrange
            LogicAbstractAPI api = LogicAbstractAPI.CreateLogicAPI(100, 100, 1);

            // Act
            var table = api.getTable();
            var balls = table.getBalls();
            var ball = balls[0];
            ball.setXPredkosc(5);
            ball.setYPredkosc(0);
            Logika.Logic.zmienKierunekX(ball);

            // Assert
            Assert.AreEqual(ball.getXPredkosc(), -5);
        }

        [TestMethod]
        public void zmienKierunekYTest()
        {
            // Arrange
            LogicAbstractAPI api = LogicAbstractAPI.CreateLogicAPI(100, 100, 1);

            // Act
            var table = api.getTable();
            var balls = table.getBalls();
            var ball = balls[0];

            // Act
            ball.setXPredkosc(0);
            ball.setYPredkosc(5);
            Logika.Logic.zmienKierunekY(ball);

            // Assert
            Assert.AreEqual(ball.getYPredkosc(),-5);
        }

        [TestMethod]
        public void aktualizujPozycjeTest()
        {
            // Arrange
            LogicAbstractAPI api = LogicAbstractAPI.CreateLogicAPI(100, 100, 1);

            // Act
            var table = api.getTable();
            var balls = table.getBalls();
            var ball = balls[0];
            ball.x = 10;
            ball.y = 20;
            ball.setXPredkosc(2);
            ball.setYPredkosc(3);

            // Act
            Logic.aktualizujPozycje(ball);

            // Assert
            Assert.AreEqual(12, ball.x);
            Assert.AreEqual(23, ball.y);
        }

        [TestMethod]
        public void aktualizujStolTest()
        {
            // Arrange
            Table table = new Table(100, 100, 3);
            foreach (var ball in table.getBalls())
            {
                ball.x = 10;
                ball.y = 20;
                ball.setXPredkosc(2);
                ball.setYPredkosc(3);
            }

            // Act
            Logic.aktualizujStol(table);

            // Assert
            foreach (var ball in table.getBalls())
            {
                Assert.AreEqual(12, ball.x);
                Assert.AreEqual(23, ball.y);
            }
        }
        [TestMethod]
        public void SimConstructorTest()
        {
            // Arrange
            int maxX = 100;
            int maxY = 100;
            int ilosc = 5;
            Table table = new Table(maxX, maxY, ilosc);

            // Act
            Sim sim = new Sim(table);

            // Assert
            Assert.AreEqual(ilosc, sim.observableData.Count);
        }
            [TestMethod]
        public void StartSimTest()
        {
            // Arrange
            Table table = new Table(100, 100, 5);
            Sim sim = new Sim(table);

            // Act
            sim.startSim();

            // Assert
            Assert.IsTrue(sim.isRunning());
            Assert.AreEqual(table.getBalls().Length, sim.threads.Count);
        }

        [TestMethod]
        public void StopSimTest()
        {
            // Arrange
            Table table = new Table(100, 100, 5);
            Sim sim = new Sim(table);
            sim.startSim();

            // Act
            sim.stopSim();

            // Assert
            Assert.IsFalse(sim.isRunning());
            Assert.AreEqual(0, sim.threads.Count);
        }

        [TestMethod]
        public void SimGetPozycja()
        {
            // Arrange
            Table table = new Table(100, 100, 3);
            Sim sim = new Sim(table);

            // Act
            float[][] positions = sim.getPozycja();

            // Assert
            Assert.AreEqual(table.getBalls().Length, positions.Length);
            for (int i = 0; i < positions.Length; i++)
            {
                Assert.AreEqual(table.getBalls()[i].x, positions[i][0]);
                Assert.AreEqual(table.getBalls()[i].y, positions[i][1]);
            }
        }
    }
}