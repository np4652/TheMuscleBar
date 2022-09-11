﻿using FluentMigrator;
using TheMuscleBar.Models;
using System;

namespace TheMuscleBar.AppCode.Migrations
{
    [Migration(202106280002)]
    public class InitialSeed_202106280002 : Migration
    {
        public override void Down()
        {
            //Delete.FromTable("Employees")
            //    .Row(new
            //    {
            //        EmployeeId = new Guid("59c0d403-71ce-4ac8-9c2c-b0e54e7c043b"),
            //        Age = 34,
            //        Name = "Test Employee",
            //        Position = "Test Position",
            //        CompanyId = new Guid("67fbac34-1ee1-4697-b916-1748861dd275")
            //    });
            //Delete.FromTable("Companies")
            //    .Row(new
            //    {
            //        CompanyId = new Guid("67fbac34-1ee1-4697-b916-1748861dd275"),
            //        Address = "Test Address",
            //        Country = "USA",
            //        Name = "Test Name"
            //    });
        }
        public override void Up()
        {
            Insert.IntoTable("ApplicationRole")
                .Row(new
                {
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }).Row(new
                {
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = "Fos",
                    NormalizedName = "FOS"
                }).Row(new
                {
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                });
            Insert.IntoTable("Users")
              .Row(new
              {
                  UserId = Guid.NewGuid().ToString(),
                  AccessFailedCount = 0,
                  ConcurrencyStamp = "411ecb83-6ae9-4c86-9364-e85fc290e9ea",
                  Email = "admin@roundpay.net",
                  EmailConfirmed = false,
                  LockoutEnabled = false,
                  LockoutEnd = DateTime.Now,
                  NormalizedEmail = "ADMIN@ROUNDPAY.NET",
                  NormalizedUserName = "ADMIN@ROUNDPAY.NET",
                  PasswordHash = "AQAAAAEAACcQAAAAEBAePcp00ZZ3yX9g7osGDRy0shH6up80DmruJmCASZz+Yq43qPac2Xy2pBrE6DkiBA==",
                  PhoneNumberConfirmed = false,
                  PhoneNumber = "9044004486",
                  SecurityStamp = "",
                  TwoFactorEnabled = false,
                  UserName = "admin@roundpay.net"
              });
            Insert.IntoTable("UserRoles")
              .Row(new
              {
                  UserId = 1,
                  RoleId = 1
              });
            //Insert.IntoTable("Companies")
            //    .Row(new Company
            //    {
            //        CompanyId = new Guid("67fbac34-1ee1-4697-b916-1748861dd275"),
            //        Address = "Test Address",
            //        Country = "USA",
            //        Name = "Test Name"
            //    });
        }
    }
}