using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using thesis_lawyer.Models;

namespace thesis_lawyer.Tests.Models
{
    [TestFixture]
    public class ChatTests
    {
        [Test]
        public void Id_Property_Should_Have_KeyAttribute()
        {
            // Arrange
            var idProperty = typeof(Chat).GetProperty("Id");

            // Act
            var keyAttribute = idProperty.GetCustomAttributes(typeof(KeyAttribute), true).FirstOrDefault() as KeyAttribute;

            // Assert
            Assert.NotNull(keyAttribute);
        }

        [Test]
        public void User_Property_Should_Have_JsonIgnoreAttribute()
        {
            // Arrange
            var userProperty = typeof(Chat).GetProperty("User");

            // Act
            var jsonIgnoreAttribute = userProperty.GetCustomAttributes(typeof(JsonIgnoreAttribute), true).FirstOrDefault() as JsonIgnoreAttribute;

            // Assert
            Assert.NotNull(jsonIgnoreAttribute);
        }


        [Test]
        public void Messages_Property_Should_Have_VirtualKeyword()
        {
            // Arrange
            var messagesProperty = typeof(Chat).GetProperty("Messages");

            // Act
            var isVirtual = messagesProperty.GetGetMethod().IsVirtual;

            // Assert
            Assert.IsTrue(isVirtual);
        }

        [Test]
        public void Messages_Property_Should_Not_Have_JsonIgnoreAttribute()
        {
            // Arrange
            var messagesProperty = typeof(Chat).GetProperty("Messages");

            // Act
            var jsonIgnoreAttribute = messagesProperty.GetCustomAttributes(typeof(JsonIgnoreAttribute), true).FirstOrDefault() as JsonIgnoreAttribute;

            // Assert
            Assert.Null(jsonIgnoreAttribute);
        }


      

        [Test]
        public void Messages_Property_Should_Have_ICollectionInterface()
        {
            // Arrange
            var messagesProperty = typeof(Chat).GetProperty("Messages");

            // Act
            var propertyType = messagesProperty.PropertyType;

            // Assert
            Assert.IsTrue(propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(ICollection<>));
        }

        [Test]
        public void Messages_Property_Should_Have_HistoryChat_Type()
        {
            // Arrange
            var messagesProperty = typeof(Chat).GetProperty("Messages");

            // Act
            var genericArgumentType = messagesProperty.PropertyType.GetGenericArguments().FirstOrDefault();

            // Assert
            Assert.AreEqual(typeof(HistoryChat), genericArgumentType);
        }
    }
}
