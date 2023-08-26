
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;

[Route("/error")]
public class ErrorController: ControllerBase{
    public IActionResult Error(){
        return Problem();
    }
}