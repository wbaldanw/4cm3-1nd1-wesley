using Acme.TechnicalTest.Domain.Contracts.TenantManagement;
using Acme.TechnicalTest.Domain.Contracts.UserManagement;
using Acme.TechnicalTest.Domain.Domain.UserManagement;
using Acme.TechnicalTest.Domain.DTO.UserManagement;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.TechnicalTest.Application.UseCases.UserManagement
{
    public class AddUserUC : IAddUserUC
    {
        private readonly UserManager<User> userManager;
        private readonly ITenantRepository tenantRepository;

        public AddUserUC(UserManager<User> userManager, ITenantRepository tenantRepository)
        {
            this.userManager = userManager;
            this.tenantRepository = tenantRepository;
        }

        public async Task Create(AddUserRequest dto)
        {
            var tenant = await tenantRepository.GetById(dto.TenantId);
            if (tenant is null)
                throw new ArgumentException($"Tenant with id {dto.TenantId} not found.");

            var user = new User(dto.Email, dto.FullName, tenant);

            var result = await userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Concat(result.Errors.Select(e => e.Description + Environment.NewLine));

                if (result.Errors.Any(x => x.Code == "DuplicateEmail"))
                    throw new ArgumentException($"Email {dto.Email} is already taken.");

                if (result.Errors.Any(x => x.Code.Contains("Password")))
                    throw new ArgumentException($"Password is not valid.{Environment.NewLine}{errors}");

                throw new Exception(errors);
            }
        }
    }
}
