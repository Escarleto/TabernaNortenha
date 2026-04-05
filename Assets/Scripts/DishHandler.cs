using UnityEngine;
using System.Collections.Generic;

public class DishHandler : MonoBehaviour
{
    private FoodController.FoodType?[] Dish = new FoodController.FoodType?[7];
    private List<FoodController> FoodsInDish = new List<FoodController>();

    [SerializeField] private Sprite[] FinalSprites;
    private FoodController LastFood;

    private int Index;
    public bool DishComplete { get; private set; }
    public bool IsVegetarianDish = true;

    public bool BuildDish(FoodController Food)
    {
        if (DishComplete) return false;

        FoodController.FoodType FoodType = Food.Type;
        if (!IsValid(FoodType)) return false;

        Dish[Index] = FoodType;
        FoodsInDish.Add(Food);

        if (LastFood != null)
            Food.gameObject.GetComponent<SpriteRenderer>().sortingOrder = LastFood.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;

        LastFood = Food;

        if (Index == 2 && FoodType == FoodController.FoodType.Bread)
        {
            Index = 4;
            return true;
        }

        if ((Index == 5  || Index == 6) && FoodType == FoodController.FoodType.Sauce)
        {
            Finish();
            return true;
        }
        
        Index++;
        Debug.Log(Index);
        return true;
    }

    private bool IsValid(FoodController.FoodType Food) => Index switch
    {
        0 or 3 => Food == FoodController.FoodType.Bread,

        1 or 2 => Food == FoodController.FoodType.Bread ||
                  Food == FoodController.FoodType.Filling,

        4 => Food == FoodController.FoodType.Topping,

        5 => Food == FoodController.FoodType.Topping ||
             Food == FoodController.FoodType.Sauce,

        6 => Food == FoodController.FoodType.Sauce,

        _ => false
    };

    private void Finish()
    {
        DishComplete = true;

        var sr = GetComponent<SpriteRenderer>();

        sr.sprite =
            IsVegetarianDish ? FinalSprites[0] :
            Dish[4] == FoodController.FoodType.Topping ? FinalSprites[1] :
            FinalSprites[2];

        foreach (var Food in FoodsInDish)
        {
            if (Food != null)
                Destroy(Food.gameObject);
        }

        FoodsInDish.Clear();

        Dish = new FoodController.FoodType?[6];
        Index = 0;
        LastFood = null;
        IsVegetarianDish = true;

        Invoke(nameof(ResetDish), 0.5f);
    }

    private void ResetDish()
    {
        DishComplete = false;
    }
}