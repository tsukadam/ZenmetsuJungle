using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    [TestFixture]
    public class ETKey : MonoBehaviour
    {
        public GameObject Controller;

        [OneTimeSetUp]
        public void ETKeyTestSetUp() { 
        Controller=GameObject.Find("ControllerGame");

        }
            [Test]
        public void CheckInitNumericKey()//初期値チェック
        {
            var state = new StateKey();
            var stateLeft = state.GetStateLeft();
            var stateRight = state.GetStateRight();
            var stateTop = state.GetStateTop();
            var stateBottom = state.GetStateBottom();
            Assert.AreEqual(stateLeft,0);
            Assert.AreEqual(stateRight, 0);
            Assert.AreEqual(stateTop, 0);
            Assert.AreEqual(stateBottom, 0);
        }
        [Test]
        public void CheckMoveLeft()
        {
            GameObject Player1 = Controller.GetComponent<ControllerKey>().Player;
            Vector3 PositionPlayer1 = Player1.GetComponent<RectTransform>().localPosition;
            var X1 = PositionPlayer1.x;
            var Y1 = PositionPlayer1.y;

            Controller.GetComponent<ControllerKey>().MovePlayerLeft();

            GameObject Player2 = Controller.GetComponent<ControllerKey>().Player;
            Vector3 positionPlayer2 = Player2.GetComponent<RectTransform>().localPosition;
            var X2 = positionPlayer2.x;
            var Y2 = positionPlayer2.y;

            Assert.IsTrue(X2 < X1);

        }

    }
}
