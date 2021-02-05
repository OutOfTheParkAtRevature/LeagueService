using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Repo
{
    public class Repo
    {
        private readonly ProgContext _progContext;
        private readonly ILogger _logger;
        public DbSet<Team> teams;
        public DbSet<Role> roles;


        public Repo(ProgContext progContext, ILogger<Repo> logger)
        {
            _progContext = progContext;
            _logger = logger;
            //this.roles = _progContext.Roles;
            this.teams = _progContext.Teams;
        }

        public async Task<Team> GetTeamById(int id)
        {
            return await teams.FindAsync(id);
        }
        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await teams.ToListAsync();
        }
        public async Task<Role> GetRoleById(int id)
        {
            return await roles.FindAsync(id);
        }
        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await roles.ToListAsync();
        }

            /// <summary>
            /// Get user Role
            /// </summary>
            /// <param name="id">UserID</param>
            /// <returns>RoleID</returns>
        //    public async Task<Role> GetRoleById(int id)
        //{
        //    return await _repo.GetRoleById(id);
        //}
        /// <summary>
        /// Get list of user Roles
        /// </summary>
        /// <returns>list of Roles</returns>
        //public async Task<IEnumerable<Role>> GetRoles()
        //{
        //    return await _repo.GetRoles();
        //}
        /// <summary>
        /// Edit User to change Role
        /// </summary>
        /// <param name="userId">UserID</param>
        /// <param name="roleId">RoleID</param>
        /// <returns>Role added</returns>
        //public async Task<Role> EditUserRole(Guid userId, int roleId)
        //{
        //    User tUser = await GetUserById(userId);
        //    tUser.RoleID = roleId;
        //    await _repo.CommitSave();
        //    return await GetRoleById(roleId);
        //}
    }
}
