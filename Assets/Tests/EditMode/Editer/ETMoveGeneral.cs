using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{

    public class ETMoveGeneral
    {
        public GameObject Player;

        [OneTimeSetUp]
        public void ETCharaMoveSetUp()
        {
            Player = GameObject.Find("Player");

        }
        [Test]
        public void TestSetGetPosition()
        {
            Vector3 NewPosition = new Vector3(0, 0, 0);
                Player.GetComponent<ControllerMoveGeneral>().SetPosition(NewPosition);
            Vector3 GetPosition = Player.GetComponent<ControllerMoveGeneral>().GetPosition();
            Assert.IsTrue(NewPosition == GetPosition);
        }

        [TestCase("X",10f)]
        [TestCase("X", -10f)]
        [TestCase("Y", 10f)]
        [TestCase("Y", -10f)]
        public void TestAddPosition(string Dimention,float MoveAmount)
        {
            Vector3 Position1 = Player.GetComponent<ControllerMoveGeneral>().GetPosition();
            var X1 = Position1.x;
            var Y1 = Position1.y;

                Player.GetComponent<ControllerMoveGeneral>().AddPosition(Dimention,MoveAmount);

            Vector3 Position2 = Player.GetComponent<ControllerMoveGeneral>().GetPosition();
            var X2 = Position2.x;
            var Y2 = Position2.y;

            if (Dimention == "X")
            {
                Assert.IsTrue(X2 == X1+MoveAmount);
            }
            if (Dimention == "Y")
            {
                Assert.IsTrue(Y2 == Y1+MoveAmount);
            }
        }
        [Test]
        public void TestSetGetCollision()
        {
            Player.GetComponent<ControllerMoveGeneral>().SetCollision("Enter");
            string StateCollision = Player.GetComponent<ControllerMoveGeneral>().GetCollision();
            Assert.IsTrue(StateCollision == "Enter");

        }
        [Test]
        public void TestSetGetTrigger()
        {
            Player.GetComponent<ControllerMoveGeneral>().SetTrigger("Enter");
            string StateTrigger =Player.GetComponent<ControllerMoveGeneral>().GetTrigger();
            Assert.IsTrue(StateTrigger == "Enter");

        }
        [Test]
        public void TestCollision()
        {
            Player.GetComponent<ControllerMoveGeneral>().AddPosition("Y",-480f);
            Assert.IsTrue(Player.GetComponent<ControllerMoveGeneral>().GetCollision() != "Exit");
                }
        [Test]
        public void TestTrigger()
        {
            Player.GetComponent<ControllerMoveGeneral>().AddPosition("Y", -480f);
            Assert.IsTrue(Player.GetComponent<ControllerMoveGeneral>().GetTrigger() != "Exit");
        }
        [Test]
        public void TestSetGetDirection()
        {
            string StateCollision;
            Player.GetComponent<ControllerMoveGeneral>().SetDirection("Up");
            StateCollision = Player.GetComponent<ControllerMoveGeneral>().GetDirection();
            Assert.IsTrue(StateCollision == "Up");
            Player.GetComponent<ControllerMoveGeneral>().SetDirection("Down");
            StateCollision = Player.GetComponent<ControllerMoveGeneral>().GetDirection();
            Assert.IsTrue(StateCollision == "Down");
            Player.GetComponent<ControllerMoveGeneral>().SetDirection("Left");
            StateCollision = Player.GetComponent<ControllerMoveGeneral>().GetDirection();
            Assert.IsTrue(StateCollision == "Left");
            Player.GetComponent<ControllerMoveGeneral>().SetDirection("Right");
            StateCollision = Player.GetComponent<ControllerMoveGeneral>().GetDirection();
            Assert.IsTrue(StateCollision == "Right");
        }
        [Test]
        public void TestAddDirection()
        {
            string StateCollision;
            Player.GetComponent<ControllerMoveGeneral>().AddDirection("X",-1f);
            StateCollision = Player.GetComponent<ControllerMoveGeneral>().GetDirection();
            Assert.IsTrue(StateCollision == "Left");
            Player.GetComponent<ControllerMoveGeneral>().AddDirection("X", 1f);
            StateCollision = Player.GetComponent<ControllerMoveGeneral>().GetDirection();
            Assert.IsTrue(StateCollision == "Right");
            Player.GetComponent<ControllerMoveGeneral>().AddDirection("Y", -1f);
            StateCollision = Player.GetComponent<ControllerMoveGeneral>().GetDirection();
            Assert.IsTrue(StateCollision == "Down");
            Player.GetComponent<ControllerMoveGeneral>().AddDirection("Y", 1f);
            StateCollision = Player.GetComponent<ControllerMoveGeneral>().GetDirection();
            Assert.IsTrue(StateCollision == "Up");
        }
        [Test]
        public void TestGetAntiDirection()
        {
            string StateAnti;
            Player.GetComponent<ControllerMoveGeneral>().AddDirection("X", -1f);
            StateAnti = Player.GetComponent<ControllerMoveGeneral>().GetAntiDirection();
            Assert.IsTrue(StateAnti == "Right");
            Player.GetComponent<ControllerMoveGeneral>().AddDirection("X", 1f);
            StateAnti = Player.GetComponent<ControllerMoveGeneral>().GetAntiDirection();
            Assert.IsTrue(StateAnti == "Left");
            Player.GetComponent<ControllerMoveGeneral>().AddDirection("Y", 1f);
            StateAnti = Player.GetComponent<ControllerMoveGeneral>().GetAntiDirection();
            Assert.IsTrue(StateAnti == "Down");
            Player.GetComponent<ControllerMoveGeneral>().AddDirection("Y", -1f);
            StateAnti = Player.GetComponent<ControllerMoveGeneral>().GetAntiDirection();
            Assert.IsTrue(StateAnti == "Up");

        }
        [Test]
        public void TestSetGetSwitchCollisionKnockBack()
        {
            Player.GetComponent<ControllerMoveGeneral>().SetSwitchCollisionKnockBack(1);
            int State = Player.GetComponent<ControllerMoveGeneral>().GetSwitchCollisionKnockBack();
            Assert.IsTrue(State == 1);
        }
        [Test]
        public void TestSetGetSwitchDamagedKnockBack()
        {
            Player.GetComponent<ControllerMoveGeneral>().SetSwitchDamagedKnockBack(1);
            int State = Player.GetComponent<ControllerMoveGeneral>().GetSwitchDamagedKnockBack();
            Assert.IsTrue(State == 1);

        }

    }
}
