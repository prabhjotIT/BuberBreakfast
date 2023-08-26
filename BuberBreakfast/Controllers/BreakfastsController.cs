using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.Services.Breakfasts;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
namespace BuberBreakfast.Controllers;

public class BreakfastsController:ApiController{
    private readonly IBreakfastService _breakfastService;

    public BreakfastsController(IBreakfastService ibreakfastService){
        _breakfastService=ibreakfastService;
    }

    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request )
    {

        //changing things inside request because i dont want to change it before coming here so my clients doesnt care how things work internally 
        //and i can convert version as i continue 
        ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.Create(
                 
             request.Name,
             request.Description,
             request.StartDateTime,
             request.EndDateTime,
             request.Savory,
             request.Sweet
        );
        if(requestToBreakfastResult.IsError){
            return Problem(requestToBreakfastResult.Errors);
        }
        Breakfast breakfast= requestToBreakfastResult.Value;
        ErrorOr<Created> createBreakfastResult = _breakfastService.CreateBreakfast(breakfast);
        return createBreakfastResult.Match(
            created=> CreatedAsGetBreakfast(breakfast),
            errors=> Problem(errors)
        );
       // if (createBreakfastResult.IsError) return Problem(createBreakfastResult.Errors);
       // return CreatedAsGetBreakfast(breakfast);

    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id )
    {
        ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakFast(id);
        return getBreakfastResult.Match(
            breakfast=>Ok(MapBreakfastResponse(breakfast)),
            errors => Problem(errors)); 
        //another way of doing the same thing 
        //if (getBreakfastResult.IsError && getBreakfastResult.FirstError == Errors.Breakfast.NotFound)
        //    return NotFound();
        //
        //var breakfast = getBreakfastResult.Value;

        //BreakfastResponse BreakfastResponse = MapBreakfastResponse(breakfast);

        //return Ok(BreakfastResponse);

    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request ){
        ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.Create(
               
             request.Name,
             request.Description,
             request.StartDateTime,
             request.EndDateTime,
             request.Savory,
             request.Sweet,
             id
        );
        if(requestToBreakfastResult.IsError){
            return Problem(requestToBreakfastResult.Errors);
        }
        Breakfast breakfast= requestToBreakfastResult.Value;
        ErrorOr<UpsertedBreakfast> upsertBreakfastResult = _breakfastService.UpsertBreakfast(breakfast);
        return upsertBreakfastResult.Match(
            upserted => upserted.IsNewlyCreated? CreatedAsGetBreakfast(breakfast): NoContent(),
            errors => Problem(errors)
            
        );
        
        

    }
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id ){

        ErrorOr<Deleted> deletedBreakfastResult = _breakfastService.DeletBreakfast(id);
        return deletedBreakfastResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
        
    }
    
    private static BreakfastResponse MapBreakfastResponse(Breakfast breakfast)
    {
        return new BreakfastResponse(breakfast.Id,
        breakfast.Name,
        breakfast.Description,
        breakfast.StartDateTime,
        breakfast.EndDateTime,
        breakfast.LastModifiedDateTime,
        breakfast.Savory,
        breakfast.Sweet);
    }
    
    private IActionResult CreatedAsGetBreakfast(Breakfast breakfast)
    {
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new { id = breakfast.Id },
            value: MapBreakfastResponse(breakfast)
            );
    }


}