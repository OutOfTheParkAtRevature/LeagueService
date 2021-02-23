using LeagueService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using Model.DataTransfer;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LeagueService.Tests
{
    public class VendorControllerTests
    {
        /// <summary>
        /// Tests the CreateVendor() method of VendorController
        /// </summary>
        [Fact]
        public async void TestForCreateLeague()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueService")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                VendorController vendorController = new VendorController(logic);
                var vendorDto = new CreateVendorDto
                {
                    VendorInfo = "chicken tenders",
                    VendorName = "bojangles"
                };

                var createVendor = await vendorController.CreateVendor(vendorDto);
                Assert.IsAssignableFrom<Vendor>((createVendor as OkObjectResult).Value);
                var createVendor2 = await vendorController.CreateVendor(vendorDto);
                Assert.IsAssignableFrom<string>((createVendor2 as ConflictObjectResult).Value);
            }
        }

        /// <summary>
        /// Tests the GetAllVendors() method of VendorController
        /// </summary>
        [Fact]
        public async void TestForGetAllVendors()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueService")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                VendorController vendorController = new VendorController(logic);
                var vendor = new Vendor 
                {
                    VendorID = Guid.NewGuid(),
                    VendorInfo = "chicken tenders",
                    VendorName = "bojangles"
                };
                r.Vendors.Add(vendor);
                await r.CommitSave();

                var getVendors = await vendorController.GetAllVendors();
                Assert.IsAssignableFrom<IEnumerable<Vendor>>((getVendors as OkObjectResult).Value);
            }
        }

        /// <summary>
        /// Tests the GetVendorById() method of VendorController
        /// </summary>
        [Fact]
        public async void TestForGetVendorById()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueService")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                VendorController vendorController = new VendorController(logic);
                var vendor = new Vendor
                {
                    VendorID = Guid.NewGuid(),
                    VendorInfo = "chicken tenders",
                    VendorName = "bojangles"
                };

                var getVendor = await vendorController.GetVendorById(vendor.VendorID);
                Assert.IsAssignableFrom<string>((getVendor as NotFoundObjectResult).Value);

                r.Vendors.Add(vendor);
                await r.CommitSave();

                var getVendor2 = await vendorController.GetVendorById(vendor.VendorID);
                Assert.IsAssignableFrom<Vendor>((getVendor2 as OkObjectResult).Value);


            }
        }

        /// <summary>
        /// Tests the GetVendorByName() method of VendorController
        /// </summary>
        [Fact]
        public async void TestForGetVendorByName()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3LeagueService")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                VendorController vendorController = new VendorController(logic);
                var vendor = new Vendor
                {
                    VendorID = Guid.NewGuid(),
                    VendorInfo = "chicken tenders",
                    VendorName = "bojangles"
                };

                var getVendor = await vendorController.GetVendorByName(vendor.VendorName);
                Assert.IsAssignableFrom<string>((getVendor as NotFoundObjectResult).Value);

                r.Vendors.Add(vendor);
                await r.CommitSave();

                var getVendor2 = await vendorController.GetVendorByName(vendor.VendorName);
                Assert.IsAssignableFrom<Vendor>((getVendor2 as OkObjectResult).Value);
            }
        }

        /// <summary>
        /// Tests the EditVendor() method of VendorController
        /// </summary>
        //[Fact]
        //public async void TestForEditVendor()
        //{
        //    var options = new DbContextOptionsBuilder<LeagueContext>()
        //    .UseInMemoryDatabase(databaseName: "p3LeagueService")
        //    .Options;

        //    using (var context = new LeagueContext(options))
        //    {
        //        context.Database.EnsureDeleted();
        //        context.Database.EnsureCreated();

        //        Repo r = new Repo(context, new NullLogger<Repo>());
        //        Logic logic = new Logic(r, new NullLogger<Repo>());
        //        VendorController vendorController = new VendorController(logic);
        //        var vendor = new Vendor
        //        {
        //            VendorID = Guid.NewGuid(),
        //            VendorInfo = "chicken tenders",
        //            VendorName = "bojangles"
        //        };
        //        var vendorDto = new EditVendorDto
        //        {
        //            VendorInfo = "chicken tenders",
        //            VendorName = "bojangles"
        //        };
        //        var editVendor = await vendorController.EditVendor(vendor.VendorID, vendorDto);
        //        Assert.IsAssignableFrom<string>((editVendor as NotFoundObjectResult).Value);

        //        r.Vendors.Add(vendor);
        //        await r.CommitSave();

        //        var editVendor2 = await vendorController.EditVendor(vendor.VendorID, vendorDto);
        //        Assert.IsAssignableFrom<Vendor>((editVendor2 as OkObjectResult).Value);
        //    }
        //}

        /// <summary>
        /// Tests the DeleteVendor() method of VendorController
        /// </summary>
        [Fact]
        public async void TestForDeleteVendor()
        {
            var options = new DbContextOptionsBuilder<LeagueContext>()
            .UseInMemoryDatabase(databaseName: "p3VendorController")
            .Options;

            using (var context = new LeagueContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                VendorController vendorController = new VendorController(logic);
                var vendor = new Vendor
                {
                    VendorID = Guid.NewGuid(),
                    VendorInfo = "chicken tenders",
                    VendorName = "bojangles"
                };

                var deleteVendor = await vendorController.DeleteVendor(vendor.VendorID);
                Assert.IsAssignableFrom<string>((deleteVendor as NotFoundObjectResult).Value);

                r.Vendors.Add(vendor);
                await r.CommitSave();

                var deleteVendor2 = await vendorController.DeleteVendor(vendor.VendorID);
                Assert.IsAssignableFrom<bool>((deleteVendor2 as OkObjectResult).Value);


            }
        }
    }
}
