using ExtendedDatabase;
using NUnit.Framework;
using System;

namespace Tests
{
    public class ExtendedDatabaseTests
    {
        private Person[] testablePeople =
        {
            new Person(12, "Marry"),
            new Person(32, "John"),
            new Person(11, "Stacy"),
            new Person(54, "Arthur")
        };

        private Person[] tooManyPeople =
{
            new Person(111, "Marry"),
            new Person(212, "Marryy"),
            new Person(312, "Mary"),
            new Person(412, "Marryy"),
            new Person(532, "Joh"),
            new Person(632, "John"),
            new Person(732, "Johnn"),
            new Person(832, "Jo"),
            new Person(911, "Stacy"),
            new Person(1011, "Stac"),
            new Person(118, "Stace"),
            new Person(191, "Stacyy"),
            new Person(141, "Staccccy"),
            new Person(524, "Arthurrr"),
            new Person(854, "Arthurrrr"),
            new Person(574, "Arthurr"),
            new Person(549, "Arthur"),
            new Person(541, "Arthuuur")
        };

        private Person personToAdd = new Person(13, "Peter");

        private Person[] rangeToAdd =
{
            new Person(20, "Harry"),
            new Person(21, "Olivia")
        };

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(111, "Nelly")]
        [TestCase(123, "Jelly")]
        public void CheckPersonConstructor(long id, string userName)
        {
            var person = new Person(id: id, userName: userName);

            // Assert
            Assert.AreEqual(person.Id, id);
            Assert.AreEqual(person.UserName, userName);
        }

        [Test]
        public void InitializingDatabaseWithNoMembers()
        {
            // Arrange
            var db = new ExtendedDatabase.ExtendedDatabase();

            // Assert
            Assert.AreEqual(db.Count, 0);
        }

        [Test]
        public void InitializingDatabaseWithSomeMembers()
        {
            // Arrange
            var db = new ExtendedDatabase.ExtendedDatabase(testablePeople);

            // Assert
            Assert.AreEqual(db.Count, 4);
        }

        // ADD TESTS
        [Test]
        public void DatabaseCountShouldIncreaseWithAddOperation()
        {
            // Arrange
            var db = new ExtendedDatabase.ExtendedDatabase(testablePeople);

            // Act
            db.Add(personToAdd);

            // Assert
            Assert.AreEqual(db.Count, 5);
        }

        [Test]
        public void DatabaseShouldThrowExceptionIfMoreThan16PeopleAdded()
        {
            // Assert
            Assert.That(() => new ExtendedDatabase.ExtendedDatabase(tooManyPeople),
                Throws.ArgumentException.With.Message.EqualTo("Provided data length should be in range [0..16]!"));
        }

        [Test]
        [TestCase(1, "Alex")]
        public void DatabaseShouldThrowExceptionIfPersonWithSameNameAdded(int id, string name)
        {
            // Arrange
            var db = new ExtendedDatabase.ExtendedDatabase();

            // Act
            db.Add(new Person(id, name));

            // Assert
            Assert.That(() => db.Add(new Person(2, name)),
                Throws.InvalidOperationException.With.Message.EqualTo("There is already user with this username!"));
        }

        [Test]
        [TestCase(1, "Alex")]
        public void DatabaseShouldThrowExceptionIfPersonWithSameIDAdded(int id, string name)
        {
            // Arrange
            var db = new ExtendedDatabase.ExtendedDatabase();

            // Act
            db.Add(new Person(id, name));

            // Assert
            Assert.That(() => db.Add(new Person(id, name + "a")),
                Throws.InvalidOperationException.With.Message.EqualTo("There is already user with this Id!"));
        }



        // REMOVE TESTS
        [Test]
        public void CheckRemoveOperation()
        {
            // Arrange
            var db = new ExtendedDatabase.ExtendedDatabase(testablePeople);
            var count = db.Count;

            // Act
            db.Remove();

            // Arrange
            Assert.AreEqual(db.Count, count - 1);
        }

        [Test]
        public void RemoveOperationShouldThrowExceptionIfDatabaseIsEmpty()
        {
            // Arrange
            var db = new ExtendedDatabase.ExtendedDatabase();

            // Arrange
            Assert.That(() => db.Remove(),
                Throws.InvalidOperationException.With.Message.EqualTo("Cannot remove from an empty database!"));
        }


        // FindByUsername
        [Test]
        [TestCase("Alex")]
        public void FindByUsernameOperationShouldThrowExceptionIfUserNotFound(string nameToFind)
        {
            // Arrange
            var db = new ExtendedDatabase.ExtendedDatabase();

            // Arrange
            Assert.That(() => db.FindByUsername(nameToFind),
                Throws.InvalidOperationException.With.Message.EqualTo("No user is present by this username!"));
        }

        [Test]
        [TestCase("Marry")]
        public void FindByUsernameOperationShouldFindUserByItsName(string nameToFind)
        {
            // Arrange
            var db = new ExtendedDatabase.ExtendedDatabase(testablePeople);

            // Act
            var result = db.FindByUsername(nameToFind);

            // Arrange
            Assert.AreEqual(result.UserName, nameToFind);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void FindByUsernameOperationShouldThrowExceptionIfUserToFindIsNull(string testString)
        {
            // Arrange
            var db = new ExtendedDatabase.ExtendedDatabase(testablePeople);

            // Arrange
            //Assert.That(() => db.FindByUsername(testString),
            //    Throws.ArgumentNullException.With.Message.EqualTo("Username parameter is null!"));

            Assert.Throws<ArgumentNullException>(() => db.FindByUsername(testString))
                .Message.Equals("Username parameter is null!");
        }


        // FindById
        [Test]
        [TestCase(123)]
        public void FindByIdOperationShouldThrowExceptionIfUserNotFound(long idToFind)
        {
            // Arrange
            var db = new ExtendedDatabase.ExtendedDatabase();

            // Arrange
            Assert.That(() => db.FindById(idToFind),
                Throws.InvalidOperationException.With.Message.EqualTo("No user is present by this ID!"));
        }

        [Test]
        [TestCase(54)] // "Arthur"
        public void FindByIdOperationShouldFindUserByItsId(long idToFind)
        {
            // Arrange
            var db = new ExtendedDatabase.ExtendedDatabase(testablePeople);

            // Act
            var result = db.FindById(idToFind);

            // Arrange
            Assert.AreEqual(result.Id, idToFind);
        }
    }
}