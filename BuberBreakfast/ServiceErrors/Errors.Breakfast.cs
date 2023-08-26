using ErrorOr;
namespace BuberBreakfast.ServiceErrors;

public static class Errors{
    public static class Breakfast{
        
        public static Error InvalidName  => Error.Validation(
            code:"Breakfast.notFound",
            description :$"Breakfast name must me atleast {Models.Breakfast.MinNameLength} character long and at most {Models.Breakfast.MaxNameLength} character long."
        );
        public static Error InvalidDescription  => Error.Validation(
            code:"Breakfast.notFound",
            description :$"Breakfast Description must me atleast {Models.Breakfast.MinDescriptionLength} character long and at most {Models.Breakfast.MaxDescriptionLength} character long."
        );
        public static Error NotFound => Error.NotFound(
            code:"Breakfast.notFound",
            description :"Breakfast not found "
        );
        
    }
}