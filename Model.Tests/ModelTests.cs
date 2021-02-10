using Model.DataTransfer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Model.Tests
{
    public class ModelTests
    {
        /// <summary>
        /// Checks the data annotations of Models to make sure they aren't being violated
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private IList<ValidationResult> ValidateModel(object model)
        {
            var result = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            Validator.TryValidateObject(model, validationContext, result, true);
            // if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);

            return result;
        } 

        /// <summary>
        /// Makes sure Team model works with valid data
        /// </summary>
        [Fact]
        public void ValidateTeam()
        {
            var team = new Team()
            {
                TeamID = 1,
                Name = "Broncos",
                Wins = 3,
                Losses = 1, 
                CarpoolID = Guid.NewGuid()
            };

            var results = ValidateModel(team);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Makes sure Role Model works with valid data
        /// </summary>
        [Fact]
        public void ValidateRole()
        {
            var role = new Role()
            {
                RoleID = 1,
                RoleName = "Coach"
            };

            var results = ValidateModel(role);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Validates the EditTeamDto Model works with proper data
        /// </summary>
        [Fact]
        public void ValidateEditTeamDto()
        {
            var teamEdit = new EditTeamDto()
            {
                Name = "little ponies",
                Wins = 0,
                Losses = 10
            };

            var errorcount = ValidateModel(teamEdit).Count;
            Assert.Equal(0, errorcount);
        }
    }
}
