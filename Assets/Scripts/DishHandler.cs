using UnityEngine;

public class DishHandler : MonoBehaviour
{
    private FoodController.FoodType?[] Dish = new FoodController.FoodType?[6];
    [SerializeField] private Sprite[] FinalSprites;

    private int Index;
    public bool DishComplete { get; private set; }
    public bool IsVegetarianDish;

    public bool BuildDish(FoodController.FoodType food)
    {
        if (DishComplete) return false;

        if (!IsValid(food)) return false;

        Dish[Index] = food;

        if ((Index == 1 || Index == 2) && food == FoodController.FoodType.Bread)
            Index = 4;
        else
            Index++;

        return true;
    }

    private bool IsValid(FoodController.FoodType food) => Index switch
    {
        0 or 3 => food == FoodController.FoodType.Bread,

        1 or 2 => food == FoodController.FoodType.Bread ||
                  food == FoodController.FoodType.Filling,

        4 => food != FoodController.FoodType.Sauce ||
             Finish(food),

        5 => food == FoodController.FoodType.Sauce &&
             Finish(food),

        _ => false
    };

    private bool Finish(FoodController.FoodType food)
    {
        DishComplete = true;
        Index = 0;

        var sr = GetComponent<SpriteRenderer>();

        sr.sprite =
            IsVegetarianDish ? FinalSprites[0] :
            Dish[4] == FoodController.FoodType.Topping ? FinalSprites[1] :
            FinalSprites[2];

        return true;
    }
}