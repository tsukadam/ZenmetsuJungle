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
        public void TestSetGetWeaponType()
        {
            Player.GetComponent<ControllerAttack>().SetWeaponTypeDetail("Sword");
            string Type = Player.GetComponent<ControllerAttack>().GetWeaponTypeDetail();
            Assert.IsTrue(Type == "Sword");
        }
        [Test]
        public void TestEquipWeapon()
        {
            Player.GetComponent<ControllerAttack>().EquipWeapon("GunBullet");
            string WeaponType = Player.GetComponent<ControllerAttack>().GetWeaponTypeDetail();
            GameObject WeaponPrefab= Player.GetComponent<ControllerAttack>().GetWeaponPrefab();
            Assert.IsTrue(WeaponPrefab != null);
            Assert.IsTrue(WeaponType == "GunBullet");
        }
        [TestCase("GunBullet")]
        [TestCase("None")]
        [TestCase("hoge")]
        public void TestMakeWeapon(string WeaponType)
        {
            string Direction = Player.GetComponent<ControllerCharaGeneral>().GetDirection();
            Player.GetComponent<ControllerAttack>().EquipWeapon(WeaponType);
            Player.GetComponent<ControllerAttack>().MakeWeapon();
        }
    }
}
