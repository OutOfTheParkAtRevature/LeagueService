using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DataTransfer;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeagueService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, League Manager, Head Coach, Assistant Coach, Parent")]
    public class VendorController : ControllerBase
    {
        private readonly Logic _logic;

        public VendorController(Logic logic)
        {
            _logic = logic;
        }

        // GET: api/<VendorController>
        [HttpGet]
        public async Task<IActionResult> GetAllVendors()
        {
            return Ok(await _logic.GetVendors());
        }

        // GET api/<VendorController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendorById(Guid id)
        {
            Vendor vendor = await _logic.GetVendorById(id);
            if (vendor == null) return NotFound("Vendor with that ID not found.");
            return Ok(vendor);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetVendorByName(string name)
        {
            Vendor vendor = await _logic.GetVendorByName(name);
            if (vendor == null) return NotFound("Vendor with that name not found.");
            return Ok(vendor);
        }

        // POST api/<VendorController>
        [HttpPost]
        [Authorize(Roles = "Admin, League Manager, Head Coach")]
        public async Task<IActionResult> CreateVendor([FromBody] CreateVendorDto createVendorDto)
        {
            if (await _logic.VendorExists(createVendorDto.VendorName) == true) return Conflict("Vendor already exists.");
            return Ok(await _logic.AddVendor(createVendorDto));

        }

        // PUT api/<VendorController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, League Manager, Head Coach")]
        public async Task<IActionResult> EditVendor(Guid id, [FromBody] CreateVendorDto evd)
        {
            Vendor vendorToEdit = await _logic.GetVendorById(id);
            Vendor vendor = await _logic.GetVendorByName(evd.VendorName);
            if (vendorToEdit == null) return NotFound("No Vendoor with that ID was found.");
            if (vendor != null && evd.VendorInfo == vendor.VendorInfo) return Conflict("That vendor already exists in the DB.");
            return Ok(await _logic.EditVendor(id, evd));
        }

        // DELETE api/<VendorController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, League Manager, Head Coach")]
        public async Task<IActionResult> DeleteVendor(Guid id)
        {
            Vendor vendorInDb = await _logic.GetVendorById(id);
            if (vendorInDb == null) return NotFound("No Vendoor with that ID was found.");
            return Ok(await _logic.DeleteVendor(vendorInDb));
        }
    }
}
