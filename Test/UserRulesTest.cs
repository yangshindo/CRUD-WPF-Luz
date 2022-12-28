using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aula_Fernando.Models;
using Xunit;

namespace Test
{
    public class UserRulesTest
    {
        public UserRules Rules { get; set; }

        

        [Theory]
        [InlineData("João Mário Bastos")]
        [InlineData("Pedro de Alcântara Francisco Antônio João Carlos Xavier de Paula Miguel Rafael Joaquim José Gonzaga Pascoal Cipriano Serafim de Bragança e Bourbon")]
        public void NameRules_ShouldValidateNameTRUE(string stringData)
        {
            Rules = new UserRules();
            //Act
            bool actual = Rules.NameRules(stringData);

            //Assert
            Assert.True(actual);
        }

        [Theory]
        [InlineData("J@cinto Leite Aquino Rego")]
        [InlineData("Jacinto L3ite Aquino Rego")]
        public void NameRules_ShouldValidateNameFALSE(string stringData)
        {
            Rules = new UserRules();
            //Act
            bool actual = Rules.NameRules(stringData);

            //Assert
            Assert.False(actual);
        }
    }
}
