using FightingArena;
using NUnit.Framework;
using System;

namespace Tests
{
    public class WarriorTests
    {
        private const int MIN_ATTACK_HP = 30;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("Ivan", 100, 10)]
        [TestCase("Goro", 200, 4)]
        public void CheckWarriorConstuctor(string name, int damage, int hp)
        {
            // Arrange
            var warrior = new Warrior(name, damage, hp);

            // Assert
            Assert.AreEqual(warrior.Name, name);
            Assert.AreEqual(warrior.Damage, damage);
            Assert.AreEqual(warrior.HP, hp);
        }

        [Test]
        [TestCase("", 100, 10)]
        [TestCase(null, 200, 4)]
        [TestCase("   ", 200, 4)]
        public void NameSetterShouldThrowExceptionWithNullOrEmptyInput
            (string name, int damage, int hp)
        {
            // Assert
            Assert.Throws<ArgumentException>(
                () => new Warrior(name, damage, hp))
                .Message.Equals("Name should not be empty or whitespace!");
        }

        [Test]
        [TestCase("Ivan", 0, 10)]
        [TestCase("Goro", -20, 4)]
        public void DamageSetterShouldThrowExceptionWithZeroOrNegativeInput
            (string name, int damage, int hp)
        {
            // Assert
            Assert.Throws<ArgumentException>(
                () => new Warrior(name, damage, hp))
                .Message.Equals("Damage value should be positive!");
        }

        [Test]
        [TestCase("Ivan", 100, -10)]
        [TestCase("Goro", 200, -4)]
        public void HpSetterShouldThrowExceptionWithNegativeInput
            (string name, int damage, int hp)
        {
            // Assert
            Assert.Throws<ArgumentException>(
                () => new Warrior(name, damage, hp))
                .Message.Equals("HP should not be negative!");
        }

        [Test]
        [TestCase("Ivan", 100, 20)]
        [TestCase("Goro", 200, 10)]
        public void AttackMethodShouldThrowExceptionIfAttackerHPIsLessThanMinimum
            (string name, int damage, int hp)
        {
            // Arrange
            var warriorToAttack = new Warrior("Dummy", 100, 40); 

            // Assert
            Assert.Throws<InvalidOperationException>(
                () => new Warrior(name, damage, hp).Attack(warriorToAttack))
                .Message.Equals("Your HP is too low in order to attack other warriors!");
        }

        [Test]
        [TestCase("Ivan", 100, 20)]
        [TestCase("Goro", 200, 10)]
        public void AttackMethodShouldThrowExceptionIfAttackedHPIsLessThanMinimum
            (string name, int damage, int hp)
        {
            // Arrange
            var attackingWarrior = new Warrior("Dummy", 100, 40);
            var warriorToAttack = new Warrior(name, damage, hp);

            // Assert
            Assert.Throws<InvalidOperationException>(
                () => attackingWarrior.Attack(warriorToAttack))
                .Message.Equals($"Enemy HP must be greater than {MIN_ATTACK_HP} in order to attack him!");
        }

        [Test]
        [TestCase("Ivan", 100, 20)]
        [TestCase("Goro", 200, 10)]
        public void AttackMethodShouldThrowExceptionIfAttackerHpIsLessThanAttackedHP
            (string name, int damage, int hp)
        {
            // Arrange
            var attackingWarrior = new Warrior(name, damage, hp);
            var warriorToAttack = new Warrior("Dummy", 100, 40);

            // Assert
            Assert.Throws<InvalidOperationException>(
                () => attackingWarrior.Attack(warriorToAttack))
                .Message.Equals("You are trying to attack too strong enemy");
        }

        [Test]
        [TestCase("Ivan", 10, 200)]
        [TestCase("Goro", 20, 100)]
        public void AttackMethodShouldDecreaseAttackerHP(string name, int damage, int hp)
        {
            // Arrange
            var attackingWarrior = new Warrior(name, damage, hp);
            var warriorToAttack = new Warrior("Dummy", 5, 40);

            // Act
            attackingWarrior.Attack(warriorToAttack);

            // Assert
            Assert.That(attackingWarrior.HP, Is.EqualTo(hp - warriorToAttack.Damage));
        }

        [Test]
        [TestCase("Ivan", 10, 40)]
        [TestCase("Goro", 20, 100)]
        public void AttackMethodShouldDecreaseAttackedWarriorHP(string name, int damage, int hp)
        {
            // Arrange
            var attackingWarrior = new Warrior("Dummy", 50, 400);
            var warriorToAttack = new Warrior(name, damage, hp);

            // Act
            attackingWarrior.Attack(warriorToAttack);

            // Assert
            if (attackingWarrior.Damage > warriorToAttack.HP)
            {
                Assert.That(warriorToAttack.HP, Is.EqualTo(0));
            }
            else
            {
                Assert.That(warriorToAttack.HP, Is.EqualTo(hp - attackingWarrior.Damage));
            }
        }
    }
}