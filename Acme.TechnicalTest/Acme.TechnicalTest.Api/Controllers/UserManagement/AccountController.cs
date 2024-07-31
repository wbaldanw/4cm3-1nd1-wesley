using Acme.TechnicalTest.Domain.Contracts.UserManagement;
using Acme.TechnicalTest.Domain.DTO.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Acme.TechnicalTest.Api.Controllers.UserManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAddUserUC addUserUC;
        private readonly ISigninUC signinUC;        

        public AccountController(IAddUserUC addUserUC,
                                 ISigninUC signinUC)
        {
            this.addUserUC = addUserUC;
            this.signinUC = signinUC;            
        }

        [HttpPost]
        [Route("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Add(AddUserRequest userDTO)
        {
            await addUserUC.Create(userDTO);

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("signin")]
        public async Task<ActionResult<SignInResponse>> Login([FromBody] SignInRequest request)
        {
            var data = await signinUC.SignIn(request);

            return Ok(data);
        }
    }
}
    