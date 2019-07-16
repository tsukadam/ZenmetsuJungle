using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{

    public class ETCharaGeneral
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
                Player.GetComponent<ControllerCharaGeneral>().SetPosition(NewPosition);
            Vector3 GetPosition = Player.GetComponent<ControllerCharaGeneral>().GetPosition();
            Assert.IsTrue(NewPosition == GetPosition);
        }

        [TestCase(10f,0)]
        [TestCase(-10f,0)]
        [TestCase(0, 10f)]
        [TestCase(0, -10f)]
        public void TestAddPosition(float MoveAmountX, float MoveAmountY)
        {
            Vector3 Position1 = Player.GetComponent<ControllerCharaGeneral>().GetPosition();
            var X1 = Position1.x;
            var Y1 = Position1.y;

                Player.GetComponent<ControllerCharaGeneral>().AddPosition(MoveAmountX,MoveAmountY);

            Vector3 Position2 = Player.GetComponent<ControllerCharaGeneral>().GetPosition();
            var X2 = Position2.x;
            var Y2 = Position2.y;
   
                Assert.IsTrue(X2 == X1+MoveAmountX);
                Assert.IsTrue(Y2 == Y1+MoveAmountY);
 
        }
        [Test]
        public void TestSetGetCollision()
        {
            Player.GetComponent<ControllerCharaGeneral>().SetCollision("Enter");
            string StateCollision = Player.GetComponent<ControllerCharaGeneral>().GetCollision();
            Assert.IsTrue(StateCollision == "Enter");

        }
        [Test]
        public void TestSetGetTrigger()
        {
            Player.GetComponent<ControllerCharaGeneral>().SetTrigger("Enter");
            string StateTrigger =Player.GetComponent<ControllerCharaGeneral>().GetTrigger();
            Assert.IsTrue(StateTrigger == "Enter");

        }
        [Test]
        public void TestCollision()
        {
            Player.GetComponent<ControllerCharaGeneral>().AddPosition(0,-480f);
            Assert.IsTrue(Player.GetComponent<ControllerCharaGeneral>().GetCollision() != "Exit");
                }
        [Test]
        public void TestTrigger()
        {
            Player.GetComponent<ControllerCharaGeneral>().AddPosition(0, -480f);
            Assert.IsTrue(Player.GetComponent<ControllerCharaGeneral>().GetTrigger() != "Exit");
        }
        [Test]
        public void TestSetGetDirection()
        {
            string StateCollision;
            Player.GetComponent<ControllerCharaGeneral>().SetDirection("Up");
            StateCollision = Player.GetComponent<ControllerCharaGeneral>().GetDirection();
            Assert.IsTrue(StateCollision == "Up");
            Player.GetComponent<ControllerCharaGeneral>().SetDirection("Down");
            StateCollision = Player.GetComponent<ControllerCharaGeneral>().GetDirection();
            Assert.IsTrue(StateCollision == "Down");
            Player.GetComponent<ControllerCharaGeneral>().SetDirection("Left");
            StateCollision = Player.GetComponent<ControllerCharaGeneral>().GetDirection();
            Assert.IsTrue(StateCollision == "Left");
            Player.GetComponent<ControllerCharaGeneral>().SetDirection("Right");
            StateCollision = Player.GetComponent<ControllerCharaGeneral>().GetDirection();
            Assert.IsTrue(StateCollision == "Right");
        }
        [Test]
        public void TestAddDirection()
        {
            string StateCollision;
            Player.GetComponent<ControllerCharaGeneral>().AddDirection(-1f,0);
            StateCollision = Player.GetComponent<ControllerCharaGeneral>().GetDirection();
            Assert.IsTrue(StateCollision == "Left");
            Player.GetComponent<ControllerCharaGeneral>().AddDirection(1f,0);
            StateCollision = Player.GetComponent<ControllerCharaGeneral>().GetDirection();
            Assert.IsTrue(StateCollision == "Right");
            Player.GetComponent<ControllerCharaGeneral>().AddDirection(0, -1f);
            StateCollision = Player.GetComponent<ControllerCharaGeneral>().GetDirection();
            Assert.IsTrue(StateCollision == "Down");
            Player.GetComponent<ControllerCharaGeneral>().AddDirection(0, 1f);
            StateCollision = Player.GetComponent<ControllerCharaGeneral>().GetDirection();
            Assert.IsTrue(StateCollision == "Up");
            Player.GetComponent<ControllerCharaGeneral>().AddDirection(-1f, -1f);
            StateCollision = Player.GetComponent<ControllerCharaGeneral>().GetDirection();
            Assert.IsTrue(StateCollision == "LeftDown");
            Player.GetComponent<ControllerCharaGeneral>().AddDirection(-1f, 1f);
            StateCollision = Player.GetComponent<ControllerCharaGeneral>().GetDirection();
            Assert.IsTrue(StateCollision == "LeftUp");
            Player.GetComponent<ControllerCharaGeneral>().AddDirection(1f, 1f);
            StateCollision = Player.GetComponent<ControllerCharaGeneral>().GetDirection();
            Assert.IsTrue(StateCollision == "RightUp");
            Player.GetComponent<ControllerCharaGeneral>().AddDirection(1f, -1f);
            StateCollision = Player.GetComponent<ControllerCharaGeneral>().GetDirection();
            Assert.IsTrue(StateCollision == "RightDown");
        }
        [Test]
        public void TestGetAntiDirection()
        {
            string StateAnti;
            Player.GetComponent<ControllerCharaGeneral>().AddDirection(-1f,0);
            StateAnti = Player.GetComponent<ControllerCharaGeneral>().GetAntiDirection();
            Assert.IsTrue(StateAnti == "Right");
            Player.GetComponent<ControllerCharaGeneral>().AddDirection(1f,0);
            StateAnti = Player.GetComponent<ControllerCharaGeneral>().GetAntiDirection();
            Assert.IsTrue(StateAnti == "Left");
            Player.GetComponent<ControllerCharaGeneral>().AddDirection(0, 1f);
            StateAnti = Player.GetComponent<ControllerCharaGeneral>().GetAntiDirection();
            Assert.IsTrue(StateAnti == "Down");
            Player.GetComponent<ControllerCharaGeneral>().AddDirection(0, -1f);
            StateAnti = Player.GetComponent<ControllerCharaGeneral>().GetAntiDirection();
            Assert.IsTrue(StateAnti == "Up");
            Player.GetComponent<ControllerCharaGeneral>().AddDirection(-1f, -1f);
            StateAnti = Player.GetComponent<ControllerCharaGeneral>().GetAntiDirection();
            Assert.IsTrue(StateAnti == "RightUp");
            Player.GetComponent<ControllerCharaGeneral>().AddDirection(-1f, 1f);
            StateAnti = Player.GetComponent<ControllerCharaGeneral>().GetAntiDirection();
            Assert.IsTrue(StateAnti == "RightDown");
            Player.GetComponent<ControllerCharaGeneral>().AddDirection(1f, 1f);
            StateAnti = Player.GetComponent<ControllerCharaGeneral>().GetAntiDirection();
            Assert.IsTrue(StateAnti == "LeftDown");
            Player.GetComponent<ControllerCharaGeneral>().AddDirection(1f, -1f);
            StateAnti = Player.GetComponent<ControllerCharaGeneral>().GetAntiDirection();
            Assert.IsTrue(StateAnti == "LeftUp");

        }
        [Test]
        public void TestSetGetSwitchCollisionKnockBack()
        {
            Player.GetComponent<ControllerCharaGeneral>().SetSwitchCollisionKnockBack(true);
            bool Switch = Player.GetComponent<ControllerCharaGeneral>().GetSwitchCollisionKnockBack();
            Assert.IsTrue(Switch == true);
        }
        [Test]
        public void TestSetGetSwitchDamagedKnockBack()
        {
            Player.GetComponent<ControllerCharaGeneral>().SetSwitchDamagedKnockBack(true);
            bool Switch = Player.GetComponent<ControllerCharaGeneral>().GetSwitchDamagedKnockBack();
            Assert.IsTrue(Switch == true);

        }
        [Test]
        public void TestSetGetCharaType()
        {
            Player.GetComponent<ControllerCharaGeneral>().SetCharaType("Player");
            string Type = Player.GetComponent<ControllerCharaGeneral>().GetCharaType();
            Assert.IsTrue(Type == "Player");
        }
    }
}
