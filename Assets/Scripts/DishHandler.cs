using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class DishHandler : MonoBehaviour
{
    private FoodController.FoodType?[] Dish = new FoodController.FoodType?[7];
    private List<FoodController> FoodsInDish = new List<FoodController>();

    [SerializeField] private GameObject[] FinalDishes;
    private FoodController LastFood;
    private Dish LastDish;

    private int Index;
    public bool DishComplete { get; private set; }
    [System.NonSerialized] public bool IsVegetarianDish = true;

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

        var FinalDishIndex =
            IsVegetarianDish ? 0 :
            Dish[5] == FoodController.FoodType.Topping ? 1 :
            2;

        LastDish = Instantiate(
            FinalDishes[FinalDishIndex],
            transform.position,
            Quaternion.identity,
            transform
        ).GetComponent<Dish>();

        UIManager.Instance.CurrentDish = LastDish;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        foreach (var Food in FoodsInDish)
        {
            if (Food != null)
                Destroy(Food.gameObject);
        }

        FoodsInDish.Clear();

        Dish = new FoodController.FoodType?[6];
        Index = 0;
        LastFood = null;
    }

    public void ResetDish()
    {
        IsVegetarianDish = true;
        DishComplete = false;
        Destroy(LastDish.gameObject);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }
}