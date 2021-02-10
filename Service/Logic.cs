using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Model;
using Model.DataTransfer;
using Repository;

namespace Service
{
    public class Logic
    {
        public Logic() { }
        public Logic(Repo repo, ILogger<Repo> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        private readonly Repo _repo;
        private readonly ILogger<Repo> _logger;
        /// <summary>
        /// Get Team by ID
        /// </summary>
        /// <param name="id">TeamID</param>
        /// <returns>Team</returns>
        public async Task<Team> GetTeamById(Guid id)
        {
            return await _repo.GetTeamById(id);
        }
        /// <summary>
        /// Get list of Teams
        /// </summary>
        /// <returns>Teams</returns>
        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await _repo.GetTeams();
        }
        /// <summary>
        /// Edit Team
        /// </summary>
        /// <param name="id">Team to edit</param>
        /// <param name="editTeamDto">New information</param>
        /// <returns>modified Team</returns>
        public async Task<Team> EditTeam(Guid id, EditTeamDto editTeamDto)
        {
            Team tTeam = await GetTeamById(id);
            if (tTeam.Name != editTeamDto.Name && editTeamDto.Name != "") { tTeam.Name = editTeamDto.Name; }
            if (tTeam.Wins != editTeamDto.Wins && editTeamDto.Wins >= 0) { tTeam.Wins = editTeamDto.Wins; }
            if (tTeam.Losses != editTeamDto.Losses && editTeamDto.Losses >= 0) { tTeam.Losses = editTeamDto.Losses; }
            await _repo.CommitSave();
            return tTeam;
        }
        public async Task<Vendor> AddVendor(string vendorName, string vendorInfo)
        {
            Vendor newVendor = new Vendor()
            {
                VendorID = Guid.NewGuid(),
                VendorName = vendorName,
                VendorInfo = vendorInfo
            };
            await _repo.Vendors.AddAsync(newVendor);
            return newVendor;
        }
    }
}