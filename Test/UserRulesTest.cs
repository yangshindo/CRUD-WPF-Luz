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
        [InlineData("RugzvagsyangzyXuxuBLZ")]
        [InlineData("Mijaro Nomuro")]
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
        [InlineData("Ab")]
        [InlineData("123456")]
        public void NameRules_ShouldValidateNameFALSE(string stringData)
        {
            Rules = new UserRules();
            //Act
            bool actual = Rules.NameRules(stringData);

            //Assert
            Assert.False(actual);
        }


        [Theory]
        [InlineData("13387732779")]
        [InlineData("99999999999")]

        public void CPFRules_ShouldValidateCPFTrue(string stringData)
        {
            Rules = new UserRules();
            //Act
            bool actual = Rules.CPFRules(stringData);

            //Assert
            Assert.True(actual);
        }


        [Theory]
        [InlineData("1338773275")]
        [InlineData("133877327a9")]
        [InlineData("!3387732779")]

        public void CPFRules_ShouldValidateCPFFALSE(string stringData)
        {
            Rules = new UserRules();
            //Act
            bool actual = Rules.CPFRules(stringData);

            //Assert
            Assert.False(actual);
        }


        [Theory]
        [InlineData("joao@gmail.com")]
        [InlineData("1111111_@ig.com.br")]
        public void Email_ShouldValidateEmailTRUE(string stringData)
        {
            Rules = new UserRules();
            //Act
            bool actual = Rules.EmailRules(stringData);

            //Assert
            Assert.True(actual);
        }


        [Theory]
        [InlineData("#########")]
        [InlineData("joaomariofff.com")]
        [InlineData("joao30@aaaaaaa")]

        public void Email_ShouldValidateEmailFALSE(string stringData)
        {
            Rules = new UserRules();
            //Act
            bool actual = Rules.EmailRules(stringData);

            //Assert
            Assert.False(actual);
        }

        [Theory]
        [InlineData("19789dsafwqe")]
        [InlineData("Joao324978")]
        [InlineData("!akjkjlrwelrwE")]

        public void Password_ShouldValidateEmailFALSE(string stringData)
        {
            Rules = new UserRules();
            //Act
            bool actual = Rules.PasswordRules(stringData);

            //Assert
            Assert.False(actual);
        }
    }
}
