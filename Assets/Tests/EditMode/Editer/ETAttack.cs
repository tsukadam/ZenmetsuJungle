using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ETAttack
    {
        public GameObject Player;

        [OneTimeSetUp]
        public void ETCharaMoveSetUp()
        {
            Player = GameObject.Find("Player");

        }

        [Test]
        public void TestSetRodType()
        {
            Player.GetComponent<ControllerAttack>().SetRodType("Sword");
            string Type = Player.GetComponent<ControllerAttack>().GetRodType();
            Assert.IsTrue(Type == "Sword");
        }
        [Test]
        public void TestSetGunType()
        {
            Player.GetComponent<ControllerAttack>().SetGunType("Bullet");
            string Type = Player.GetComponent<ControllerAttack>().GetGunType();
            Assert.IsTrue(Type == "Bullet");
        }
        [Test]
        public void TestSetWeapon()
        {
            Player.GetComponent<ControllerAttack>().SetWeapon("Bullet","Sword");
            string TypeGun = Player.GetComponent<ControllerAttack>().GetGunType();
            string TypeRod = Player.GetComponent<ControllerAttack>().GetRodType();
            Assert.IsTrue(TypeGun == "Bullet");
            Assert.IsTrue(TypeRod == "Sword");
        }
        [TestCase("GunBullet","RodSword")]
        [TestCase("None", "None")]
        [TestCase("hoge", "hoge")]
        public void TestMakeWeapon(string GunType,string RodType)
        {
            string Direction = Player.GetComponent<ControllerMoveGeneral>().GetDirection();
            Player.GetComponent<ControllerAttack>().SetWeapon(GunType,RodType);
            Player.GetComponent<ControllerAttack>().MakeGun();
            Player.GetComponent<ControllerAttack>().MakeRod();
        }
    }
}
