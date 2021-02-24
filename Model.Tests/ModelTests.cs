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
                TeamID = Guid.NewGuid(),
                CarpoolID = Guid.NewGuid(),
                LeagueID = Guid.NewGuid(),
                StatLineID = Guid.NewGuid(),
                Name = "Broncos",
                Wins = 3,
                Losses = 1
            };

            var results = ValidateModel(team);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Makes sure Sport model works with valid data
        /// </summary>
        [Fact]
        public void ValidateSport()
        {
            var sport = new Sport
            {
                SportID = 5,
                SportName = "football"
            };

            var results = ValidateModel(sport);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Makes sure Vendor model works with valid data
        /// </summary>
        [Fact]
        public void ValidateVendor()
        {
            var vendor = new Vendor
            {
                VendorID = Guid.NewGuid(),
                VendorInfo = "hotdog",
                VendorName = "weinerhut"
            };

            var results = ValidateModel(vendor);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Makes sure League model works with valid data
        /// </summary>
        [Fact]
        public void ValidateLeague()
        {
            var league = new League
            {
                LeagueID = Guid.NewGuid(),
                LeagueName = "Gamesfortots",
                SportID = 8
            };

            var results = ValidateModel(league);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Validates the CreateLeagueDto Model works with proper data
        /// </summary>
        [Fact]
        public void ValidateCreateLeagueDto()
        {
            var league = new CreateLeagueDto()
            {
                LeagueName = "sports",
                SportName = "basketball"
            };

            var errorcount = ValidateModel(league).Count;
            Assert.Equal(0, errorcount);
        }

        /// <summary>
        /// Validates the CreateCarpoolDto Model works with proper data
        /// </summary>
        [Fact]
        public void ValidateCreateCarpoolDto()
        {
            var carpool = new CreateCarpoolDto()
            {
                CarpoolID = Guid.NewGuid(),
                UserID = "tom"
            };

            var errorcount = ValidateModel(carpool).Count;
            Assert.Equal(0, errorcount);
        }

        /// <summary>
        /// Validates the CreateVendorDto Model works with proper data
        /// </summary>
        [Fact]
        public void ValidateCreateVendorDto()
        {
            var vendor = new CreateVendorDto()
            {
                VendorInfo = "hamburger",
                VendorName = "wendys"
            };

            var errorcount = ValidateModel(vendor).Count;
            Assert.Equal(0, errorcount);
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

        /// <summary>
        /// Validates the EditVendorDto Model works with proper data
        /// </summary>
        [Fact]
        public void ValidateEditVendorDto()
        {
            var vendor = new CreateVendorDto()
            {
                VendorInfo = "hamburger",
                VendorName = "wendys"
            };

            var errorcount = ValidateModel(vendor).Count;
            Assert.Equal(0, errorcount);
        }
    }
}
