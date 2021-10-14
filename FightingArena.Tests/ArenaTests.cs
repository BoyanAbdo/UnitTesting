using FightingArena;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class ArenaTests
    {
        private List<Warrior> warriorsList = new List<Warrior>
        {
            new Warrior("Ivan", 10, 100),
            new Warrior("Tina", 20, 200),
            new Warrior("Goro", 15, 150),
            new Warrior("Lilly", 22, 220)
        };


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void EnrollMethodShouldEnrollWarriors()
        {
            // Arrange
            var arena = new Arena();

            // Act
            foreach (var warrior in warriorsList)
            {
                arena.Enroll(warrior);
            }

            // Assert
            Assert.That(arena.Warriors.Count, Is.EqualTo(warriorsList.Count));
            Assert.That(arena.Warriors.Count, Is.EqualTo(arena.Count));
        }

        [Test]
        public void EnrollMethodShouldThrowExceptionIfWarriorWithSameNameIsAdded()
        {
            // Arrange
            var arena = new Arena();
            var warriorToEnroll = new Warrior("Test", 10, 100);

            // Act
            arena.Enroll(warriorToEnroll);

            // Assert
            Assert
                .Throws<InvalidOperationException>(() => arena.Enroll(warriorToEnroll))
                .Message.Equals("Warrior is already enrolled for the fights!");
        }

        [Test]
        [TestCase("Miro", "Tina")]
        public void FightMethodShouldThrowExceptionIfWarriorsDoNotExist
            (string attackerName, string defenderName)
        {
            // Arrange
            var arena = new Arena();

            // Act
            foreach (var warrior in warriorsList)
            {
                arena.Enroll(warrior);
            }

            // Assert
            Assert.Throws<InvalidOperationException>(
                () => arena.Fight(attackerName, defenderName))
                .Message.Equals($"There is no fighter with name {attackerName} enrolled for the fights!");
        }

        [Test]
        [TestCase("Goro", "Tina")]
        [TestCase("Goro", "Ivan")]
        [TestCase("Tina", "Lilly")]
        [TestCase("Lilly", "Ivan")]
        public void FightMethodShouldSuccessfullyExecute
            (string attackerName, string defenderName)
        {
            // Arrange
            var arena = new Arena();
            foreach (var warrior in warriorsList)
            {
                arena.Enroll(warrior);
            }

            Warrior attacker = arena.Warriors
                .FirstOrDefault(w => w.Name == attackerName);
            Warrior defender = arena.Warriors
                .FirstOrDefault(w => w.Name == defenderName);

            int defenderInitialHP = defender.HP;

            // Act
            arena.Fight(attackerName, defenderName);

            // Assert
            Assert.That(defender.HP, Is.EqualTo(defenderInitialHP - attacker.Damage));
        }
    }
}

